using DB_Bmas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_Bmas_POS.MayoristaProduccion
{
    public partial class SMMAperturaCaja : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnAbrir_Click(object sender, EventArgs e)
        {
            PreventaCajaSMM p = new PreventaCajaSMM();
            SMM_AperturaCaja ap = new SMM_AperturaCaja();

            ap.Fecha = Convert.ToDateTime(DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year);
            ap.Usuario = Convert.ToInt32(Session["IDCVTUsuario"]);
            ap.Monto = Convert.ToInt32(txtMonto.Text);
            ap.IdCajero = cboEncargadoCaja.Value.ToString();

            int res = p.InsertaAperturaCaja(ap);

            if (res != 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Apertura Registrada!');", true);
                txtMonto.Text = string.Empty;
                cboEncargadoCaja.SelectedIndex = -1;
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Apertura de Caja ya se encuentra Registrada!');", true);
            }
        }
    }
}