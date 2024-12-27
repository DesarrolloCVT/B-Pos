using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_Bmas_POS.MayoristaProduccion
{
    public partial class HistorialArqueoCaja : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void GvDatos_ToolbarItemClick(object source, DevExpress.Web.Data.ASPxGridViewToolbarItemClickEventArgs e)
        {
            if (((DevExpress.Web.MenuItem)e.Item.Owner).Name.Equals("Export"))
            {
                GvDatos.SettingsExport.FileName = "Historial arqueo Caja Fecha " + dteInicio.Date.Day.ToString() + "-" + dteInicio.Date.Month.ToString() + "-" + dteInicio.Date.Year.ToString() + " al " + dteTermino.Date.Day.ToString() + "-" + dteTermino.Date.Month.ToString() + "-" + dteTermino.Date.Year.ToString();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

        }
    }
}