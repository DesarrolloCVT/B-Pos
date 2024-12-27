using DB_Bmas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_Bmas_POS
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            //Elimina Sesiones anteriores
            Session.RemoveAll();
            LogClass lg = new LogClass();
            //encriptar la clave que ingresa el usuario para ser comparada en la bd
            string pass = lg.Encrypt(txtPass.Text, true);

            UsuarioClass vUsuario = new UsuarioClass();
            //Verifica Datos del usuario de ser 0, datos usuario erroneos
            int CID = vUsuario.VerificaUsuarioClaveEncriptada(txtUser.Text, pass);
            if (CID != 0)
            {
                //si datos son correctos obtiene datos del usuario y los guarda en sesiones
                List<CVT_Usuarios> DatosUsu = new List<CVT_Usuarios>();
                DatosUsu = vUsuario.ObtieneDatosUsuarioPorID(CID);
                foreach (var d in DatosUsu)
                {
                    Session["IDCVTUsuario"] = d.IdUsuario;
                    Session["CIDUsuario"] = d.UsuarioSistema;
                    Session["NombreUsuario"] = d.NombreUsuario.ToString();
                    Session["PerfilId"] = d.IdPerfil;
                    //LogClass vLOG = new LogClass();
                    //LOG_SistemaCVT vNuevo = new LOG_SistemaCVT();
                    //vNuevo.Entidad = "LOGIN";
                    //vNuevo.Entidad_Id = 0;
                    //vNuevo.Fecha = DateTime.Now;
                    //vNuevo.Usuario_Id = d.IdUsuario.ToString();
                    //vNuevo.Valor_Antiguo = Request.ServerVariables["REMOTE_HOST"].ToString();
                    //vNuevo.Valor_Nuevo = "INGRESO A SISTEMA";
                    //vLOG.InsertaRegistroLogDesaint(vNuevo);
                }

                //redirecciona a pagina principal
                Response.Redirect("Blank.aspx");
            }
            else
            {
                //mensaje usuario o contraseña incorrectos
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Usuario o Contraseña no Valido');", true);
            }
        }
    }
}