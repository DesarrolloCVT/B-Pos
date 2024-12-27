using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_Bmas_POS.MayoristaReportes
{
    public partial class AjustesEnCaja : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GvDatos_ToolbarItemClick(object source, DevExpress.Web.Data.ASPxGridViewToolbarItemClickEventArgs e)
        {
            if (((DevExpress.Web.MenuItem)e.Item.Owner).Name.Equals("Export"))
            {
                GvDatos.SettingsExport.FileName = "Ajustes en Caje Fecha :" + dteInicio.Value.ToString() + "-" + dteTermino.Value.ToString();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            GvDatos.DataBind();
        }
    }
}