using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_Bmas_POS
{
    public partial class ReiniciaPooll : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnReiniciar_Click(object sender, EventArgs e)
        {
            try
            {
                string appPoolName = "bmas"; // Cambia esto con el nombre de tu Application Pool
                using (ServerManager serverManager = new ServerManager())
                {
                    ApplicationPool appPool = serverManager.ApplicationPools[appPoolName];
                    if (appPool != null)
                    {
                        if (appPool.State == ObjectState.Stopped)
                        {
                            appPool.Start();
                        }
                        else
                        {
                            appPool.Recycle();
                        }
                        lblMessage.Text = "Application Pool " + appPoolName + " ha sido reciclado.";
                    }
                    else
                    {
                        lblMessage.Text = "No se pudo encontrar el Application Pool " + appPoolName + ".";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error al reciclar Application Pool: " + ex.Message;
            }
        }
    }
}