using CVT_MermasRecepcion.MayoristaProduccion;
using DB_Bmas;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_Bmas_POS.MayoristaProduccion
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                TickPreventa r = new TickPreventa();
                r.Parameters["v_IdPrev"].Value = Convert.ToInt32(Session["IdPreventa"]);


                PreventaSMMClasss mc = new PreventaSMMClasss();
                string CodCli = mc.TraeCodCliente(Convert.ToInt32(Session["IdPreventa"]));
                int TotalPrevPromo = mc.TraeDatoTickPromo(Convert.ToInt32(Session["IdPreventa"]));

                int cantCom = 1;
                mc.TraeCodClientPromo(CodCli);

                if (CodCli.Equals("C782549901") || CodCli.Equals("C765475767")||CodCli.Equals("C995108607") )
                {
                    cantCom = 2;
                }
                else { 
                 cantCom = mc.TraeCodClientPromo(CodCli);
                }

                /////////////////////// logica imp//////////////////////////////////////////////
                
                if (cantCom != 0 && TotalPrevPromo > 3000)
                {
                    TicketPromo a = new TicketPromo();
                    a.Parameters["v_IdCli"].Value = CodCli;
                    a.Parameters["v_IdPrev"].Value = Convert.ToInt32(Session["IdPreventa"]);
                    r.CreateDocument();
                    a.CreateDocument();
                    r.ModifyDocument(x =>
                    {
                        x.AddPages(a.Pages);
                        ;
                    });
                  
                }else if(cantCom == 0 && TotalPrevPromo > 3000) 
                {
                    TickDescuento d = new TickDescuento();
                    TicketPromo a = new TicketPromo();
                    d.Parameters["v_IdCli"].Value = CodCli;
                    d.Parameters["v_IdPrev"].Value = Convert.ToInt32(Session["IdPreventa"]);
                    d.Parameters["v_Fecha"].Value = Convert.ToString(DateTime.Now.AddDays(30));

                    a.Parameters["v_IdCli"].Value = CodCli;
                    a.Parameters["v_IdPrev"].Value = Convert.ToInt32(Session["IdPreventa"]);
                    //a.Parameters["v_Fecha"].Value = Convert.ToString(DateTime.Now.AddDays(30));
                    r.CreateDocument();
                    d.CreateDocument();
                    a.CreateDocument();

                    r.ModifyDocument(x => {
                        x.AddPages(d.Pages);
                        ;
                    });

                    r.ModifyDocument(x =>
                    {
                        x.AddPages(a.Pages);
                        ;
                    });
                }else if(cantCom == 0)
                {
                    TickDescuento d = new TickDescuento();
                    d.Parameters["v_IdCli"].Value = CodCli;
                    d.Parameters["v_IdPrev"].Value = Convert.ToInt32(Session["IdPreventa"]);
                    d.Parameters["v_Fecha"].Value = Convert.ToString(DateTime.Now.AddDays(30));
                    r.CreateDocument();
                    d.CreateDocument();
                    r.ModifyDocument(x => {
                        x.AddPages(d.Pages);
                        ;
                    });
                }
                else
                {
                    r.CreateDocument();
                }

               
                //r.Print("BIXOLON SRP-Q300");

                PdfExportOptions opts = new PdfExportOptions();
                opts.ShowPrintDialogOnOpen = true;
                r.ExportToPdf(ms, opts);
                ms.Seek(0, SeekOrigin.Begin);
                byte[] report = ms.ToArray();
                Page.Response.ContentType = "application/pdf";
                Page.Response.Clear();
                Page.Response.OutputStream.Write(report, 0, report.Length);
                Page.Dispose();                
                Page.Response.End();
                r.Dispose();
            }
        }
    }
}