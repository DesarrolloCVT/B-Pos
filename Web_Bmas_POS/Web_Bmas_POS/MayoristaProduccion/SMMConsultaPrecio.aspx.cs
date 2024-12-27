using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DB_Bmas;

namespace Web_Bmas_POS.MayoristaProduccion
{
    public partial class SMMConsultaPrecio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void txt_DUN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                PreventaSMMClasss mc = new PreventaSMMClasss();

                string dun = txt_DUN.Text;

                List<SMM_VW_DATO_BUSCAPROD_PREVENTA> lt = mc.ConsultaProductoPreventa(dun);
                GV_GConsulaPrecios.DataSource = lt;
                GV_GConsulaPrecios.DataBind();
            }
            catch
            {

            }
        }
    }
}