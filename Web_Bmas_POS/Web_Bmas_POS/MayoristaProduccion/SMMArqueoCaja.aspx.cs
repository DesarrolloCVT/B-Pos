using DB_Bmas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_Bmas_POS.MayoristaProduccion
{
    public partial class SMMArqueoCaja : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                GvDatos.DataSource = Session["Datos"];
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            PreventaCajaSMM mm = new PreventaCajaSMM();

            DataTable td = mm.SP_ResumenArqueoCaja(dteFechArqueo.Date);
            Session["Datos"] = td;
            GvDatos.DataSource = Session["Datos"];
            GvDatos.DataBind();
            GvDatos.SettingsText.Title = "Fecha Arqueo: " + dteFechArqueo.Date.Day.ToString() + "-" + dteFechArqueo.Date.Month.ToString() + "-" + dteFechArqueo.Date.Year.ToString();
            txtObservacion.Text = string.Empty;

        }

        protected void GvDatos_ToolbarItemClick(object source, DevExpress.Web.Data.ASPxGridViewToolbarItemClickEventArgs e)
        {

            if (((DevExpress.Web.MenuItem)e.Item.Owner).Name.Equals("Export"))
            {
                GvDatos.SettingsExport.FileName = "Arqueo Caja Fecha" + dteFechArqueo.Date.Day.ToString() + "-" + dteFechArqueo.Date.Month.ToString() + "-" + dteFechArqueo.Date.Year.ToString();
            }
        }
        protected void btnGuardarArqueo_Click(object sender, EventArgs e)
        {
            PreventaCajaSMM mm = new PreventaCajaSMM();
            UsuarioClass us = new UsuarioClass();
            DataTable td = mm.SP_ResumenArqueoCaja(dteFechArqueo.Date);

            string FechaRes = mm.TraeFechaArqueo(dteFechArqueo.Date);

            string veri = us.idUsuarioVerificador(Convert.ToInt32(Session["IDCVTUsuario"]));

            if (veri.Equals("True"))
            {

                if (FechaRes == dteFechArqueo.Date.ToString())
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Error al Registrar Arqueo! O  ya se encuentra Registrado Favor verificar');", true);
                    dteFechArqueo.Text = string.Empty;
                    txtObservacion.Text = string.Empty;

                }
                else
                {
                    int res = 0;
                    int TEfec = 0;
                    int TTarj = 0;
                    int TCheq = 0;
                    int TTrans = 0;
                    int TGen = 0;
                    foreach (DataRow r in td.Rows)
                    {
                        SMM_Historial_Arqueo h = new SMM_Historial_Arqueo();

                        h.FechaArqueo = dteFechArqueo.Date;
                        h.Tipo = r[0].ToString();
                        h.Moneda = r[1].ToString();
                        h.Apertura = Convert.ToInt32(r[2]);
                        h.Efectivo = Convert.ToInt32(r[3]);
                        h.Tarjeta = Convert.ToInt32(r[4]);
                        h.Cheque = Convert.ToInt32(r[5]);
                        h.Transferencia = Convert.ToInt32(r[6]);
                        h.Total = Convert.ToInt32(r[7]);
                        h.Observacion = txtObservacion.Text;
                        h.FechaRegistroArqueo = DateTime.Now;
                        h.IdUsuario = Convert.ToInt32(Session["IDCVTUsuario"]);
                        //TAper = TAper+ Convert.ToInt32(r[2]);
                        TEfec = TEfec + Convert.ToInt32(r[3]);
                        TTarj = TTarj + Convert.ToInt32(r[4]);
                        TCheq = TCheq + Convert.ToInt32(r[5]);
                        TTrans = TTrans + Convert.ToInt32(r[6]);
                        TGen = TGen + Convert.ToInt32(r[7]);

                        res = mm.InsertaHistorialArqueo(h);
                    }

                    SMM_TotalArqueoReal ta = new SMM_TotalArqueoReal();
                    ta.FechArq_Caja = dteFechArqueo.Date;
                    ta.TotEfec_CAja = Convert.ToInt32(txtEfectivo.Text);
                    ta.TotEfec_Sist = TEfec;
                    ta.TotTarj_Caja = Convert.ToInt32(txtTarjeta.Text);
                    ta.TotTarj_Sist = TTarj;
                    ta.TotCheq_Caja = Convert.ToInt32(txtCheque.Text);
                    ta.TotCheq_Sist = TCheq;
                    ta.TotTransf_Caja = Convert.ToInt32(txtTransferencia.Text);
                    ta.TotTransf_Sist = TTrans;
                    ta.TotGen_Caja = Convert.ToInt32(txtEfectivo.Text) + Convert.ToInt32(txtTarjeta.Text) + Convert.ToInt32(txtCheque.Text) + Convert.ToInt32(txtTransferencia.Text);
                    ta.TotGen_Sist = TGen;
                    ta.IdUsuario = Convert.ToInt32(Session["IDCVTUsuario"]);
                    ta.Fecha_hist_Arqueo = dteFechArqueo.Date;
                    ta.Observacion = txtObservacion.Text;

                    mm.InsertaTotalArqueo(ta);

                    if (res != 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Arqueo Registrado !');", true);
                        dteFechArqueo.Text = string.Empty;
                        txtObservacion.Text = string.Empty;

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Error al Registrar Arqueo!);", true);
                        dteFechArqueo.Text = string.Empty;
                        txtObservacion.Text = string.Empty;
                    }
                }
            }
            else { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Error , Usuario no autorizado para realizar arqueo ');", true); }

        }
    }
}