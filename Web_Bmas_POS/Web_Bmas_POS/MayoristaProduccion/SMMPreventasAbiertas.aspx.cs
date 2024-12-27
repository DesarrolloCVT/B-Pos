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
    public partial class SMMPreventasAbiertas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                //GvDatosV.DataSource = Session["Datos"];

                GvDatos.DataSource = Session["Datos"];
            }

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

            PreventaSMMClasss pv = new PreventaSMMClasss();
            DataTable dt = new DataTable();
            dt = pv.SP_HistorialPreventas(dteInicio.Date, dteTermino.Date);
            GvDatos.DataSource = dt;
            Session["Datos"] = dt;



            GvDatos.DataBind();
        }

        protected void GvDatos_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            if (e.CommandArgs.CommandName == "cmdReimprimir")
            {
                Session["IdPreventa"] = e.KeyValue;

                string javaScript = "printReport()";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);

            }
        }
    }
}