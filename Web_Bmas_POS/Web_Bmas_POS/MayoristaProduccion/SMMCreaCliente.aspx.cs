using DB_Bmas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_Bmas_POS.MayoristaProduccion
{
    public partial class SMMCreaCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnContinuarCli_Click(object sender, EventArgs e)
        {
            citiesTabPage.ActiveTabIndex = 1;
            Session["CodCliNew"] = cboSiglaCliente.Value.ToString() + txtCodCliNew.Text;

            //string cod_ = Session["CodCliNew"].ToString();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            txtRutCliNew.Text = string.Empty;
            txtNombreCliNew.Text = string.Empty;
            txtCodCliNew.Text = string.Empty;
            txtRazonCliNew.Text = string.Empty;
            cboGrupoClienteNew.SelectedIndex = -1;
            txtGirocliNew.Text = string.Empty;
            txtTelefonoCliNew.Text = string.Empty;
            txtEmailCliNew.Text = string.Empty;
        }

        protected void btnAgregarDir_Click(object sender, EventArgs e)
        {

            PreventaSMMClasss mc = new PreventaSMMClasss();

            SMM_Clientes_Direccion c = new SMM_Clientes_Direccion();

            c.CodCliente = Session["CodCliNew"].ToString();
            c.Direccion = txtDireccionCliNew.Text;
            c.Region = Convert.ToInt32(cboRegionNewCli.Value);
            c.Ciudad = txtCiudadCliNew.Text;
            c.Comuna = txtComunaCliNew.Text;
            c.TipoDir = cboTipoDireccionNew.Value.ToString();
            string rn = mc.InsertaDireccionCliente(c);

            if (chkDatFacturacion.Checked == true)
            {
                SMM_Clientes_Direccion cc = new SMM_Clientes_Direccion();

                cc.CodCliente = Session["CodCliNew"].ToString();
                cc.Direccion = txtDireccionCliNew.Text;
                cc.Region = Convert.ToInt32(cboRegionNewCli.Value);
                cc.Ciudad = txtCiudadCliNew.Text;
                cc.Comuna = txtComunaCliNew.Text;
                cc.TipoDir = "B";

                string rr = mc.InsertaDireccionCliente(cc);
                //if (rr != "")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Direccion Facturacion Registrada');", true);
                //}
            }

            if (rn != "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Direccion Registrada');", true);
            }
            else { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Error al registrar direccion');", true); }

            GvDatosDireccion.DataBind();
            cboRegionNewCli.SelectedIndex = -1;
            txtCiudadCliNew.Text = string.Empty;
            txtComunaCliNew.Text = string.Empty;
            cboTipoDireccionNew.SelectedIndex = -1;
            txtDireccionCliNew.Text = string.Empty;
            chkDatFacturacion.Checked = false;


            //citiesTabPage.ActiveTabIndex = 1;
            //Session["CodCliNew"] = txtCodCliNew.Text;
            //btnCrearNewCli.Enabled = false;
        }

        protected void btnCrearNewCli_Click(object sender, EventArgs e)
        {
            PopConfirmaCreaCliente.ShowOnPageLoad = true;
        }

        protected void CancelarDir_Click(object sender, EventArgs e)
        {
            txtRutCliNew.Text = string.Empty;
            txtNombreCliNew.Text = string.Empty;
            txtCodCliNew.Text = string.Empty;
            txtRazonCliNew.Text = string.Empty;
            cboGrupoClienteNew.SelectedIndex = -1;
            txtGirocliNew.Text = string.Empty;
            txtTelefonoCliNew.Text = string.Empty;
            txtEmailCliNew.Text = string.Empty;
            cboRegionNewCli.SelectedIndex = -1;
            txtCiudadCliNew.Text = string.Empty;
            txtComunaCliNew.Text = string.Empty;
            txtEmailCliNew.Text = string.Empty;
            cboTipoDireccionNew.SelectedIndex = -1;
            txtDireccionCliNew.Text = string.Empty;
            chkDatFacturacion.Checked = false;
        }

        protected void btnConfirmaCreaClienteSi_Click(object sender, EventArgs e)
        {
            PopConfirmaCreaCliente.ShowOnPageLoad = false;
        }

        protected void btnConfirmaCreaClienteNo_Click(object sender, EventArgs e)
        {
            PreventaSMMClasss mc = new PreventaSMMClasss();

            SMM_Clientes c = new SMM_Clientes();
            c.Rut = txtRutCliNew.Text;
            c.Nombre = txtNombreCliNew.Text;
            c.Codigo = Session["CodCliNew"].ToString(); ;
            c.RazonSocial = txtRazonCliNew.Text;
            c.GrupoCliente = Convert.ToInt32(cboGrupoClienteNew.Value);
            c.Giro = txtGirocliNew.Text;
            c.Telefono = txtTelefonoCliNew.Text;
            c.Email = txtEmailCliNew.Text;
            string rn = mc.InsertaNuevoCliente(c);

            PostVentasBMasClass ps = new PostVentasBMasClass();

            string rest = ps.CreaSocioNegocio(Session["CodCliNew"].ToString());

            if (rest == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Cliente Registrado');", true);

                txtRutCliNew.Text = string.Empty;
                txtNombreCliNew.Text = string.Empty;
                txtCodCliNew.Text = string.Empty;
                txtRazonCliNew.Text = string.Empty;
                cboGrupoClienteNew.SelectedIndex = -1;
                cboSiglaCliente.SelectedIndex = 0;
                txtGirocliNew.Text = string.Empty;
                txtTelefonoCliNew.Text = string.Empty;
                txtEmailCliNew.Text = string.Empty;
                cboRegionNewCli.SelectedIndex = -1;
                txtCiudadCliNew.Text = string.Empty;
                txtComunaCliNew.Text = string.Empty;
                txtEmailCliNew.Text = string.Empty;
                cboTipoDireccionNew.SelectedIndex = -1;
                txtDireccionCliNew.Text = string.Empty;
                chkDatFacturacion.Checked = false;
                citiesTabPage.ActiveTabIndex = 0;
                PopConfirmaCreaCliente.ShowOnPageLoad = false;
                //PopNuevo.ShowOnPageLoad = false;

            }
            else
            {
                PopConfirmaCreaCliente.ShowOnPageLoad = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Error al Registrar Cliente'('" + rest + "'));", true);
            }
        }

        protected void txtRutCliNew_TextChanged(object sender, EventArgs e)
        {
            PreventaSMMClasss mc = new PreventaSMMClasss();
            List<SMM_VW_INFO_CLIENTE_PREVENTA> ls = mc.ConsultaClientePreventaNuevo(txtRutCliNew.Text);


            bool vrut = ValidacionClass.ValidaRut(txtRutCliNew.Text);

            if (vrut == true)
            {
                if (ls.Count != 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Cliente ya existe');", true);
                    txtRutCliNew.Text = string.Empty;
                    txtRutCliNew.Focus();

                }
                else
                {
                    txtCodCliNew.Text = txtRutCliNew.Text;
                    txtNombreCliNew.Focus();

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Ingrese Rut Valido');", true);
                txtRutCliNew.Text = string.Empty;
                txtRutCliNew.Focus();
            }

        }

        protected void txtDireccionCliNew_TextChanged(object sender, EventArgs e)
        {

        }
    }
}