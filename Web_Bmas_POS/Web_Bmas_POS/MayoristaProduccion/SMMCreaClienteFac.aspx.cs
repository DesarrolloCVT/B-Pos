using DB_Bmas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_Bmas_POS.MayoristaProduccion
{
    public partial class SMMCreaClienteFac : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnContinuarCli_Click(object sender, EventArgs e)
        {
            citiesTabPage.ActiveTabIndex = 1;
            Session["CodCliNew"] = cboSiglaCliente.Value.ToString() + txtCodCliNew.Text.ToUpper();
        }

        protected void btnContinuarContacto_Click(object sender, EventArgs e)
        {
            citiesTabPage.ActiveTabIndex = 2;
        }

        protected void txtRutCli_TextChanged(object sender, EventArgs e)
        {
            PreventaSMMClasss mc = new PreventaSMMClasss();
            List<SMM_VW_INFO_CLIENTE_PREVENTA> ls = mc.ConsultaClientePreventaNuevo(txtRutCli.Text);


            bool vrut = ValidacionClass.ValidaRut(txtRutCli.Text);

            if (vrut == true)
            {
                if (ls.Count != 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Cliente ya existe');", true);
                    txtRutCli.Text = string.Empty;
                    txtRutCli.Focus();

                }
                else
                {
                    txtCodCliNew.Text = txtRutCli.Text;
                    txtRutCli.Focus();

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Ingrese Rut Valido');", true);
                txtRutCli.Text = string.Empty;
                txtRutCli.Focus();
            }
        }

        protected void btnGuardarContacto_Click(object sender, EventArgs e)
        {
            PreventaSMMClasss mc = new PreventaSMMClasss();

            SMM_Clientes_Contacto cc = new SMM_Clientes_Contacto();
            cc.CodCliente = Session["CodCliNew"].ToString();
            cc.Cargo = txtCargo.Text;
            cc.Nombre = txtNombreContacto.Text;
            cc.Apellido = txtApellidoContacto.Text;
            cc.Telefono1 = Convert.ToInt32(txtFono1.Text);
            cc.Telefono2 = Convert.ToInt32(txtFono2.Text);
            cc.EMail = txtEmailContacto.Text;
            int rn = mc.InsertaNuevoClienteContacto(cc);

            if (IsPostBack)
            {
                GvDatosContacto.DataBind();
            }

            if (rn != 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Contacto Registrado');", true);
                GvDatosContacto.DataBind();
            }

            LimpiaDatosContacto();
        }
        private void LimpiaDatosContacto()
        {
            txtCargo.Text = string.Empty;
            txtNombreContacto.Text = string.Empty;
            txtApellidoContacto.Text = string.Empty;
            txtFono1.Text = string.Empty;
            txtFono2.Text = string.Empty;
            txtEmailContacto.Text = string.Empty;
        }
        private void LimpiaDatosPrincipal()
        {
            txtRutCli.Text = string.Empty;
            txtCodCliNew.Text = string.Empty;
            txtRazonCli.Text = string.Empty;
            txtNombreFantasiaCli.Text = string.Empty;
            txtCelContacto.Text = string.Empty;
            cboGrupoCliente.SelectedIndex = -1;
            cboZona.SelectedIndex = -1;
            txtEmailFE.Text = string.Empty;
            txtGirocliNew.Text = string.Empty;
            cboVendAsignado.SelectedIndex = -1;
            cboTipoNegocio.SelectedIndex = -1;
            cboCondicionPago.SelectedIndex = -1;
            txtDescuento.Text = string.Empty;
            txtLineaCredito.Text = string.Empty;
            rbtNominacion.SelectedIndex = -1;

        }
        private void LimpiaDatosDir()
        {
            txtDireccionCliNew.Text = string.Empty;
            cboRegionNewCli.SelectedIndex = -1;
            txtCiudadCliNew.Text = string.Empty;
            txtComunaCliNew.Text = string.Empty;
            cboTipoDireccionNew.SelectedIndex = -1;
            chkDatFacturacion.Checked = false;

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            PopCancela.ShowOnPageLoad = true;
        }

        protected void btnSiCancela_Click(object sender, EventArgs e)
        {
            LimpiaDatosContacto();
            LimpiaDatosPrincipal();
            LimpiaDatosDir();
            citiesTabPage.ActiveTabIndex = 0;
            PopCancela.ShowOnPageLoad = false;


        }

        protected void ASPxButton2_Click(object sender, EventArgs e)
        {
            PopCancela.ShowOnPageLoad = true;
        }

        protected void citiesTabPage_ActiveTabChanged(object source, DevExpress.Web.TabControlEventArgs e)
        {

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

        protected void btnCraerNw_Click(object sender, EventArgs e)
        {
            PopConfirmaCreaCliente.ShowOnPageLoad = true;
        }

        protected void btnConfirmaCreaClienteNo_Click(object sender, EventArgs e)
        {


            PopConfirmaCreaCliente.ShowOnPageLoad = false;
        }

        protected void btnConfirmaCreaClienteSi_Click(object sender, EventArgs e)
        {



            int di = GvDatosDireccion.VisibleRowCount;
            int cont = GvDatosContacto.VisibleRowCount;

            if (di != 0 && cont != 0)
            {
                PreventaSMMClasss mc = new PreventaSMMClasss();
                
                SMM_Clientes c = new SMM_Clientes();
                c.Rut = txtRutCli.Text;
                c.Nombre = txtRazonCli.Text;
                c.Codigo = Session["CodCliNew"].ToString(); ;
                c.RazonSocial = txtRazonCli.Text;
                c.GrupoCliente = Convert.ToInt32(cboGrupoCliente.Value);
                c.Giro = txtGirocliNew.Text;
                c.Telefono = txtCelContacto.Text;
                c.Email = txtEmailFE.Text;
                c.NombreFantasia = txtNombreFantasiaCli.Text;
                c.IDVendAsignado = Convert.ToInt32(cboVendAsignado.Value);
                c.IDTipoNegocio = Convert.ToInt32(cboTipoNegocio.Value);
                c.IDCondicionPago = Convert.ToInt32(cboCondicionPago.Value);
                c.Descuento = Convert.ToDecimal(txtDescuento.Text);
                c.LineaCredito = Convert.ToInt32(txtLineaCredito.Text);
                c.Nominacion = Convert.ToString(rbtNominacion.Value);

                string rn = mc.InsertaNuevoCliente(c);

                PostVentasBMasClass ps = new PostVentasBMasClass();

                string rest = ps.CreaSocioNegocioMayorista(Session["CodCliNew"].ToString());


                if (rest == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Cliente Registrado');", true);
                    LimpiaDatosContacto();
                    LimpiaDatosDir();
                    LimpiaDatosPrincipal();
                    citiesTabPage.ActiveTabIndex = 0;
                    PopConfirmaCreaCliente.ShowOnPageLoad = false;
                    //PopNuevo.ShowOnPageLoad = false;

                }
                else
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Error al Registrar Cliente'('" + rest + "'));", true);
                    // PopConfirmaCreaCliente.ShowOnPageLoad = false;
                }
            }
            else { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Datos de contacto y direcciones son obligatorios');", true); }
            PopConfirmaCreaCliente.ShowOnPageLoad = false;
        }

        protected void CancelarDir_Click(object sender, EventArgs e)
        {
            PopCancela.ShowOnPageLoad = true;
        }

        protected void btnNoCancela_Click(object sender, EventArgs e)
        {
            PopCancela.ShowOnPageLoad = false;
        }
    }
}