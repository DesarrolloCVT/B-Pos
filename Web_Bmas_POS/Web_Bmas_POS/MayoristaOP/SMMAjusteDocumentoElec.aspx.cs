using DB_Bmas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_Bmas_POS.MayoristaOP
{
    public partial class SMMAjusteDocumentoElec : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void BtnBuscar_Click(object sender, EventArgs e)
        {

            GvDatos.DataBind();

        }

        protected void btnAjustar_Click(object sender, EventArgs e)
        {
            PreventaSMMClasss pv = new PreventaSMMClasss();

            //string[] param = new string[2];
            //param[0] = "Preventa_ID";
            //param[1] = "ItemCode";
            ////param[2] = "Layout_Id";
            //var list = GvDatos.GetSelectedFieldValues(param);

            int rs = 0;

            //int pr = 0;
            //string cp = "";
            //foreach (object[] a in list)
            //{

            //    pr = Convert.ToInt32(a[0]);
            //    cp = Convert.ToString(a[1]);

            //}
            rs = pv.VerificaDocAjustado(Convert.ToInt32(txtPrevID.Text));
            //rs = 0;
            if (rs == 0)
            {
                SAPSMM sp = new SAPSMM();
                pv.InsertaDatAjustDocument(Convert.ToInt32(txtPrevID.Text), Convert.ToInt32(Session["IDCVTUsuario"]));
                string ret = sp.CreaEntradaAjusteDocumentoElectronico(Convert.ToInt32(txtPrevID.Text), Session["CIDUsuario"].ToString());

                if (ret == "0")
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Ajuste Creado');", true);
                }
                else { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('" + ret + "');", true); }

            }
            else
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('ajuste no se puede crear favor verificar');", true);
            }
        }

    }
}