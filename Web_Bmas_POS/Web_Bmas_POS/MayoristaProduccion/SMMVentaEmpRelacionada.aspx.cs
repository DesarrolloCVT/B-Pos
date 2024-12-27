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
    public partial class SMMVentaEmpRelacionada : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            divPickear.Visible = false;

            if (!IsPostBack)
            {
                cboDireccionEnvio.SelectedIndex = 0;
                cboDireccionFacturacion.SelectedIndex = 0;

                LqsGrupoCliente.WhereParameters.Clear();
                LqsGrupoCliente.WhereParameters.Add("IdGrup", DbType.Int32, "112");
                LqsGrupoCliente.Where = "GroupCode=@IdGrup";
                cboGrupoCliente.SelectedIndex = 0;

                //cboProductosinDun.SelectedIndex = -1;
            }
        }

        protected void cboBusCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            PreventaSMMClasss mc = new PreventaSMMClasss();

            string varRunClien = cboBusCliente.Value.ToString();

            if (varRunClien.Equals("") || varRunClien == null)
            {
                txtCodigoCliente.Text = "C66666666-6";
                txtIdentificador.Text = "66666666-6";
                txtTelefono.Text = "984321168";
                txtNombreCliente.Text = string.Empty;
                txtEmail.Text = string.Empty;
                varRunClien = string.Empty;
                //rbTipoVenta.SelectedIndex = -1;
                //rbTipoVenta.Focus();
                if (IsPostBack)
                {
                    LqsGrupoCliente.WhereParameters.Clear();
                    LqsGrupoCliente.WhereParameters.Add("IdGrup", DbType.Int32, "112");
                    LqsGrupoCliente.Where = "GroupCode=@IdGrup";

                    cboDireccionEnvio.SelectedIndex = 0;
                    cboDireccionFacturacion.SelectedIndex = 0;
                    cboGrupoCliente.SelectedIndex = 0;



                }
            }
            else
            {
                List<SMM_VW_INFO_CLIENTE_PREVENTA> ls = mc.ConsultaClientePreventaRelacionada(varRunClien);

                if (ls.Count != 0)
                {
                    foreach (var t in ls)
                    {
                        LqsGrupoCliente.WhereParameters.Clear();
                        LqsGrupoCliente.WhereParameters.Add("IdGrup", DbType.Int32, t.CodGrupCliente.ToString());
                        LqsGrupoCliente.Where = "GroupCode=@IdGrup";

                        txtCodigoCliente.Text = t.CodCliente;
                        txtNombreCliente.Text = t.NombreCliente;
                        txtIdentificador.Text = t.RutCliente;
                        txtTelefono.Text = t.Telefono;
                        txtEmail.Text = t.E_Mail;
                        cboDireccionEnvio.SelectedIndex = 0;
                        cboDireccionFacturacion.SelectedIndex = 0;
                        cboGrupoCliente.SelectedIndex = 0;
                        Session["GrupoCli"] = t.CodGrupCliente;
                    }
                }
                else
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Cliente no existe o no se puede encontrar , favor verificar o registrar');", true);
                }

            }
        }

        protected void btnReimprimir_Click(object sender, EventArgs e)
        {
            string javaScript = "printReport()";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);
            Session["IdPreventa"] = txtNPreventaImp.Text;
        }

        protected void btnPickear_Click(object sender, EventArgs e)
        {
            PreventaSMMClasss mcc = new PreventaSMMClasss();
            SMM_Preventa p = new SMM_Preventa();
            SAPSMM sp = new SAPSMM();


            string codEnv = mcc.VerificaDireccionCliente(txtCodigoCliente.Text, "S");
            string codFac = mcc.VerificaDireccionCliente(txtCodigoCliente.Text, "B");

            string d1 = cboDireccionFacturacion.Value == null ? "0" : cboDireccionFacturacion.Value.ToString(); /*sp.ObtieneNombreDireccionCliente(txtCodigoCliente.Text, "S");*/
            string d2 = cboDireccionEnvio.Value == null ? "0" : cboDireccionEnvio.Value.ToString();

            if (codEnv.Equals(d2) && codFac.Equals(d1))
            {
                try
                {

                    p.CodigoCliente = txtCodigoCliente.Text;
                    p.DireccionFact = cboDireccionFacturacion.Value == null ? null : cboDireccionFacturacion.Value.ToString(); /*sp.ObtieneNombreDireccionCliente(txtCodigoCliente.Text, "S");*/
                    p.DireccionEnvio = cboDireccionEnvio.Value == null ? null : cboDireccionEnvio.Value.ToString(); /*sp.ObtieneNombreDireccionCliente(txtCodigoCliente.Text, "B");*/
                    p.RunCliente = txtIdentificador.Text;
                    p.Telefono = txtTelefono.Text;
                    p.Email = txtEmail.Text;
                    p.TipoVenta = "F";
                    p.GrupoCliente = mcc.TraeGrupoCliente(txtCodigoCliente.Text);
                    p.IdUsuario = Convert.ToInt32(Session["IDCVTUsuario"]);
                    Session["IdPreventa"] = mcc.InsertaPreventaCabecera(p);
                    // int s =Convert.ToInt32(Session["IdPreventa"]);
                    divPickear.Visible = true;
                    txtDun.Focus();
                    roundCliente.Collapsed = true;
                    roundCliente.HeaderText = "Cliente : " + txtNombreCliente.Text;
                    btnPickear.Enabled = false;

                }
                catch { }

                if (txtCodigoCliente.Text == "C66666666-6")
                {
                    GvDatos.Columns["Descuento"].Visible = false;
                }
                else { GvDatos.Columns["Desc"].Visible = false; }

                mcc.ActualizaEstadoPreventa(Convert.ToInt32(Session["IdPreventa"]), 1);
                divDatos_Client.Visible = true;
                lblCantidadProd.Text = string.Empty;
            }
            else
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Direccion Cliente No Encontrada, favor verificar o registrar');", true);
            }


            divTotalVis.Visible = true;
            txtTotal.Text = "0";
        }

        protected void txtDun_TextChanged(object sender, EventArgs e)
        {
            PreventaSMMClasss mc = new PreventaSMMClasss();
            SMM_Preventa_Detalle pr = new SMM_Preventa_Detalle();

            List<SMM_VW_DATO_BUSCAPROD_PREVENTA_EERR> lt = mc.ConsultaProductoPreventaEERR(txtDun.Text);
            int nOrden = mc.TraeNOrdenPrev(Convert.ToInt32(Session["IdPreventa"]));

            string tipVent = "F";



            if (lt.Count != 0)
            {
                foreach (var t in lt)
                {
                    int cod = mc.validaProdRegistradoPreVenta(t.ItemCode, Convert.ToInt32(Session["IdPreventa"]), t.UomCode);


                    if (cod == 0)
                    {
                        int cantProd = mc.CuentaLineaProducto(Convert.ToInt32(Session["IdPreventa"]));

                        if (tipVent.Equals("F"))
                        {

                            if (cantProd <= 59)
                            {
                                pr.CodProducto = t.ItemCode;
                                pr.Producto = t.ItemName;
                                pr.Cantidad = 1;
                                pr.Precio = t.Price;
                                pr.CodBarra = t.BcdCode;
                                pr.UniMedida = t.UomCode;
                                pr.CodUniMedida = t.UomEntry;
                                pr.Preventa_ID = Convert.ToInt32(Session["IdPreventa"]);
                                pr.ByOrder = nOrden + 1;

                                mc.InsertaPreventaDetalle(pr);
                                divPickear.Visible = true;
                                txtDun.Text = string.Empty;
                                txtDun.Focus();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Supera cantidad permitida en Factura!');", true);
                                divPickear.Visible = true;
                            }
                        }
                        else
                        {
                            pr.CodProducto = t.ItemCode;
                            pr.Producto = t.ItemName;
                            pr.Cantidad = 1;
                            pr.Precio = t.Price;
                            pr.CodBarra = t.BcdCode;
                            pr.UniMedida = t.UomCode;
                            pr.CodUniMedida = t.UomEntry;
                            pr.Preventa_ID = Convert.ToInt32(Session["IdPreventa"]);
                            pr.ByOrder = nOrden + 1;

                            mc.InsertaPreventaDetalle(pr);
                            divPickear.Visible = true;
                            txtDun.Text = string.Empty;
                            txtDun.Focus();
                        }

                        //int totProd = cantProd + 1;
                        //lblCantidadProd.Text = "Cantidad de Productos" + ": " + totProd;
                    }
                    else
                    {
                        mc.ActualizaCantidadPreventa(Convert.ToInt32(Session["IdPreventa"]), t.ItemCode, 1, t.UomCode);
                        divPickear.Visible = true;
                        txtDun.Text = string.Empty;
                        txtDun.Focus();


                    }
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Codigo de producto no existe!');", true);
                divPickear.Visible = true;
                txtDun.Text = string.Empty;
                txtDun.Focus();
            }
            txtTotal.Text = mc.traetotalPreventa(Convert.ToInt32(Session["IdPreventa"])).ToString();
        }
        protected void GvDatos_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (Convert.ToInt32(e.NewValues["Descuento"]) > 5)
            {
                e.Cancel = true;
            }

            divPickear.Visible = true;
            GvDatos.DataBind();
        }

        protected void btnAnular_Click(object sender, EventArgs e)
        {

            popAnulaPreventa.ShowOnPageLoad = true;
            divPickear.Visible = true;


        }
        protected void GvDatos_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
        {


            PreventaSMMClasss mc = new PreventaSMMClasss();


            txtTotal.Text = mc.traetotalPreventa(Convert.ToInt32(Session["IdPreventa"])).ToString();

            divPickear.Visible = true;

            GvDatos.DataBind();


        }
        protected void btnSiAnula_Click(object sender, EventArgs e)
        {
            int idPrev = Convert.ToInt32(Session["IdPreventa"]);

            PreventaSMMClasss pv = new PreventaSMMClasss();

            int resB = pv.EliminaProductosPreventa(idPrev);
            if (resB != 0)
            {
                int resC = pv.EliminaPreventa(idPrev);

                if (resC != 0)
                {
                    txtCodigoCliente.Text = "C66666666-6";
                    txtIdentificador.Text = "66666666-6";
                    txtTelefono.Text = "984321168";
                    txtNombreCliente.Text = string.Empty;
                    txtEmail.Text = string.Empty;
                    cboBusCliente.SelectedIndex = -1;
                    //rbTipoVenta.SelectedIndex = -1;
                    //rbTipoVenta.Focus();
                    if (IsPostBack)
                    {
                        LqsGrupoCliente.WhereParameters.Clear();
                        LqsGrupoCliente.WhereParameters.Add("IdGrup", DbType.Int32, "112");
                        LqsGrupoCliente.Where = "GroupCode=@IdGrup";

                        cboDireccionEnvio.SelectedIndex = 0;
                        cboDireccionFacturacion.SelectedIndex = 0;
                        cboGrupoCliente.SelectedIndex = 0;



                    }

                    roundCliente.Collapsed = false;
                    roundCliente.HeaderText = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Anulado !');", true);
                    popAnulaPreventa.ShowOnPageLoad = false;
                    btnPickear.Enabled = true;


                }
            }
            divTotalVis.Visible = false;
            txtTotal.Text = string.Empty;
            txtApliDescuent.Text = "5";
        }

        protected void btnNoAnula_Click(object sender, EventArgs e)
        {
            divPickear.Visible = true;
            divTotalVis.Visible = true;
            popAnulaPreventa.ShowOnPageLoad = false;
            txtDun.Focus();
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {

            PreventaSMMClasss pv = new PreventaSMMClasss();
            pv.ActualizaEstadoPreventa(Convert.ToInt32(Session["IdPreventa"]), 1);

            string javaScript = "printReport()";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);

            txtCodigoCliente.Text = "C66666666-6";
            txtIdentificador.Text = "66666666-6";
            txtTelefono.Text = "984321168";
            txtNombreCliente.Text = string.Empty;
            txtEmail.Text = string.Empty;
            cboBusCliente.SelectedIndex =-1;
            //rbTipoVenta.SelectedIndex = -1;
            if (IsPostBack)
            {
                cboDireccionEnvio.SelectedIndex = 0;
                cboDireccionFacturacion.SelectedIndex = 0;
                LqsGrupoCliente.WhereParameters.Clear();
                LqsGrupoCliente.WhereParameters.Add("IdGrup", DbType.Int32, "112");
                LqsGrupoCliente.Where = "GroupCode=@IdGrup";
                cboGrupoCliente.SelectedIndex = 0;


            }

            roundCliente.Collapsed = false;
            roundCliente.HeaderText = string.Empty;
            //rbTipoVenta.Focus();
            btnPickear.Enabled = true;

            SMM_LogCaja lg = new SMM_LogCaja();
            PreventaCajaSMM pvc = new PreventaCajaSMM();

            lg.Preventa_ID = Convert.ToInt32(Convert.ToInt32(Session["IdPreventa"]));
            lg.FecharError = DateTime.Now;
            lg.Error = "Crea Preventa empresa relacionada n° " + Convert.ToInt32(Session["IdPreventa"]);
            lg.Usuario =Convert.ToString(Session["NombreUsuario"]);
            pvc.InsertaLog(lg);

            divTotalVis.Visible = false;
            txtTotal.Text = string.Empty;
        }

        protected void GvDatos_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            if (e.CommandArgs.CommandName == "cmdDescEspecial")
            {
                Session["KeyDesc"] = e.KeyValue.ToString();
                PopDesc.ShowOnPageLoad = true;
                // Response.Redirect("~/Calidad/Inspecciones/InspeccionArrozDetalle.aspx");
            }
        }

        protected void btnVerificar_Click(object sender, EventArgs e)
        {
            LogClass lg = new LogClass();
            //encriptar la clave que ingresa el usuario para ser comparada en la bd
            string pass = lg.Encrypt(txtContrasena.Text, true);

            UsuarioClass us = new UsuarioClass();
            int idUser = us.TraeIdUsuarioVerificador(txtUsuario.Text, pass);
            PreventaSMMClasss pr = new PreventaSMMClasss();

            if (idUser == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Usuario no permitido para verificar');", true);
            }
            else
            {


                //string verif = Convert.ToString(Session["NombreUsuario"]);

                string[] co = new string[2];
                string cP = Session["KeyDesc"].ToString();
                co = cP.Split('|');

                int PrevID = Convert.ToInt32(co[0].ToString());
                string CodPro = co[1].ToString().Trim();
                string UMed = co[2].ToString().Trim();

                if (Convert.ToInt32(txtDescuentoEsp.Text) > 100)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Descuento no puede superar el 100%');", true);
                }
                else
                {


                    bool res = pr.ActualizaDescuentoPrev(PrevID, CodPro, UMed, Convert.ToInt32(txtDescuentoEsp.Text));

                    if (res == true)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Registrado');", true);
                        GvDatos.DataBind();
                        txtContrasena.Text = string.Empty;
                        txtUsuario.Text = string.Empty;
                        txtDescuentoEsp.Text = string.Empty;
                        PopDesc.ShowOnPageLoad = false;
                        divPickear.Visible = true;
                        txtDun.Focus();
                        roundCliente.Collapsed = true;
                        if (txtCodigoCliente.Text == "C66666666-6")
                        {
                            GvDatos.Columns["Descuento"].Visible = false;
                        }
                        else { GvDatos.Columns["Desc"].Visible = false; }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Error al Verificar Contactar con Administrador');", true);
                        txtContrasena.Text = string.Empty;
                        txtUsuario.Text = string.Empty;
                        PopDesc.ShowOnPageLoad = true;
                        if (txtCodigoCliente.Text == "C66666666-6")
                        {
                            GvDatos.Columns["Descuento"].Visible = false;
                        }
                        else { GvDatos.Columns["Desc"].Visible = false; }
                    }
                }

            }

            txtTotal.Text = pr.traetotalPreventa(Convert.ToInt32(Session["IdPreventa"])).ToString();
            divPickear.Visible = true;
        }
        protected void btnCancela_Click(object sender, EventArgs e)
        {
            PopDesc.ShowOnPageLoad = false;
            divPickear.Visible = true;
            txtDun.Focus();
            roundCliente.Collapsed = true;
            if (txtCodigoCliente.Text == "C66666666-6")
            {
                GvDatos.Columns["Descuento"].Visible = false;
            }
            else { GvDatos.Columns["Desc"].Visible = false; }
        }
        protected void GvDatos_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
        {
            PreventaSMMClasss mc = new PreventaSMMClasss();
            txtTotal.Text = mc.traetotalPreventa(Convert.ToInt32(Session["IdPreventa"])).ToString();
            divPickear.Visible = true;

            GvDatos.DataBind();
        }

        protected void btnAplicarDesc_Click(object sender, EventArgs e)
        {
            PreventaSMMClasss mc = new PreventaSMMClasss();
            if (Convert.ToInt32(txtApliDescuent.Text) > 5)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Descuento no puede superar el 5% , de otra manera utilice descuento especial');", true);
                txtApliDescuent.Text = string.Empty;
            }
            else
            {
                mc.UpdateDescuentoPreventa(Convert.ToInt32(Session["IdPreventa"]), Convert.ToInt32(txtApliDescuent.Text));
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Descuento Aplicado');", true);
                txtApliDescuent.Text = string.Empty;
            }

            txtTotal.Text = mc.traetotalPreventa(Convert.ToInt32(Session["IdPreventa"])).ToString();
            divPickear.Visible = true;
        }
    }
}