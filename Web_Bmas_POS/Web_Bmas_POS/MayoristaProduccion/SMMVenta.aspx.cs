using DB_Bmas;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_Bmas_POS.MayoristaProduccion
{
    public partial class SMMVenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // txtDun.Focus();
            //divPickear.Visible = true;

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

        protected void btnReimprimir_Click(object sender, EventArgs e)
        {
            string javaScript = "printReport()";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);
            Session["IdPreventa"] = txtNPreventaImp.Text;
        }


        //protected void txtDun_TextChanged(object sender, EventArgs e)
        //{
        //    PreventaSMMClasss mc = new PreventaSMMClasss();
        //    SMM_Preventa_Detalle pr = new SMM_Preventa_Detalle();

        //    List<SMM_VW_DATO_BUSCAPROD_PREVENTA> lt = mc.ConsultaProductoPreventa(txtDun.Text);
        //    int nOrden = mc.TraeNOrdenPrev(Convert.ToInt32(Session["IdPreventa"]));          

        //    if (lt.Count != 0)
        //    {
        //        foreach (var t in lt)
        //        {
        //            int cod = mc.validaProdRegistradoPreVenta(t.ItemCode, Convert.ToInt32(Session["IdPreventa"]), t.UomCode);


        //            if (cod == 0)
        //            {
        //                int cantProd = mc.CuentaLineaProducto(Convert.ToInt32(Session["IdPreventa"]));

        //                if (rbTipoVenta.Value.Equals("F"))
        //                {

        //                    if (cantProd <= 59)
        //                    {
        //                        pr.CodProducto = t.ItemCode;
        //                        pr.Producto = t.ItemName;
        //                        pr.Cantidad = 1;
        //                        pr.Precio = t.Price;
        //                        pr.CodBarra = t.BcdCode;
        //                        pr.UniMedida = t.UomCode;
        //                        pr.CodUniMedida = t.UomEntry;
        //                        pr.Preventa_ID = Convert.ToInt32(Session["IdPreventa"]);
        //                        pr.ByOrder = nOrden + 1;

        //                        mc.InsertaPreventaDetalle(pr);
        //                        divPickear.Visible = true;
        //                        txtDun.Text = string.Empty;
        //                        txtDun.Focus();
        //                    }
        //                    else
        //                    {
        //                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Supera cantidad permitida en Factura!');", true);
        //                        divPickear.Visible = true;
        //                    }
        //                }
        //                else
        //                {
        //                    pr.CodProducto = t.ItemCode;
        //                    pr.Producto = t.ItemName;
        //                    pr.Cantidad = 1;
        //                    pr.Precio = t.Price;
        //                    pr.CodBarra = t.BcdCode;
        //                    pr.UniMedida = t.UomCode;
        //                    pr.CodUniMedida = t.UomEntry;
        //                    pr.Preventa_ID = Convert.ToInt32(Session["IdPreventa"]);
        //                    pr.ByOrder = nOrden + 1;

        //                    mc.InsertaPreventaDetalle(pr);
        //                    divPickear.Visible = true;
        //                    txtDun.Text = string.Empty;
        //                    txtDun.Focus();
        //                }

        //                //int totProd = cantProd + 1;
        //                //lblCantidadProd.Text = "Cantidad de Productos" + ": " + totProd;
        //            }
        //            else
        //            {
        //                mc.ActualizaCantidadPreventa(Convert.ToInt32(Session["IdPreventa"]), t.ItemCode, 1, t.UomCode);
        //                divPickear.Visible = true;
        //                txtDun.Text = string.Empty;
        //                txtDun.Focus();


        //            }
        //        }

        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Codigo de producto no existe!');", true);
        //        divPickear.Visible = true;
        //        txtDun.Text = string.Empty;
        //        txtDun.Focus();
        //    }
        //    txtTotal.Text = mc.traetotalPreventa(Convert.ToInt32(Session["IdPreventa"])).ToString();
        //}

        protected void GvDatos_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(e.NewValues["Descuento"]) > 5)
                {
                    e.Cancel = true;
                }

                //divPickear.Visible = true;
                GvDatos.DataBind();
            }
            catch (Exception ex)
            {
                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "GvDatos_RowUpdating - SMMVentas";
                sm.InsetLogError(err);


            }
           
        }

        protected void ASPxButtonEdit1_ButtonClick(object source, DevExpress.Web.ButtonEditClickEventArgs e)
        {

            try
            {
                switch (e.ButtonIndex)
                {
                    case 0:
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Edito Cliente en Construccion ');", true);
                        break;
                    default:
                        //PopNuevo.ShowOnPageLoad = true;
                        //citiesTabPage.ActiveTabIndex = 0;
                        //txtRutCliNew.Focus();
                        //btnCrearNewCli.Enabled = true;
                        Response.Redirect("SMMCreaCliente.aspx");
                        break;
                }
            }
            catch (Exception ex)
            {

                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "ASPxButtonEdit1_ButtonClick - SMMVentas";
                sm.InsetLogError(err);
            }
           
        }

        protected void rbTipoVenta_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                if (rbTipoVenta.Value.Equals("B"))
                {
                    txtBusCliente.Focus();
                }
                else
                {

                    txtBusCliente.Focus();
                }
            }
            catch (Exception ex)
            {


                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "rbTipoVenta_SelectedIndexChanged - SMMVentas";
                sm.InsetLogError(err);
            }
            
        }

        protected void txtBusCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                PreventaSMMClasss mc = new PreventaSMMClasss();

                if (txtBusCliente.Text.Equals("") || txtBusCliente.Text == null)
                {

                    txtCodigoCliente.Text = "C66666666-6";
                    txtIdentificador.Text = "66666666-6";
                    txtTelefono.Text = "984321168";
                    txtNombreCliente.Text = string.Empty;
                    txtEmail.Text = string.Empty;
                    txtBusCliente.Text = string.Empty;
                    rbTipoVenta.SelectedIndex = -1;
                    rbTipoVenta.Focus();
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
                    List<SMM_VW_INFO_CLIENTE_PREVENTA> ls = mc.ConsultaClientePreventa(txtBusCliente.Text);

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
            catch (Exception ex)
            {

                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "txtBusCliente_TextChanged - SMMVentas";
                sm.InsetLogError(err);
            }           
        }

        protected void btnPickear_Click(object sender, EventArgs e)
        {

            try
            {
                PreventaSMMClasss mcc = new PreventaSMMClasss();
                SMM_Preventa p = new SMM_Preventa();
                SAPSMM sp = new SAPSMM();


                string codEnv = mcc.VerificaDireccionCliente(txtCodigoCliente.Text, "S");
                string codFac = mcc.VerificaDireccionCliente(txtCodigoCliente.Text, "B");

                string d1 = cboDireccionFacturacion.Value == null ? "0" : cboDireccionFacturacion.Value.ToString(); /*sp.ObtieneNombreDireccionCliente(txtCodigoCliente.Text, "S");*/
                string d2 = cboDireccionEnvio.Value == null ? "0" : cboDireccionEnvio.Value.ToString();

                if (txtCodigoCliente.Text == "C66666666-6" && rbTipoVenta.Value.ToString().Equals("F"))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Cliente solo puede generar Boletas');", true);
                    divTotalVis.Visible = false;
                }
                else if (codEnv.Equals(d2) && codFac.Equals(d1))
                {
                    try
                    {

                        p.CodigoCliente = txtCodigoCliente.Text;
                        p.DireccionFact = cboDireccionFacturacion.Value == null ? null : cboDireccionFacturacion.Value.ToString(); /*sp.ObtieneNombreDireccionCliente(txtCodigoCliente.Text, "S");*/
                        p.DireccionEnvio = cboDireccionEnvio.Value == null ? null : cboDireccionEnvio.Value.ToString(); /*sp.ObtieneNombreDireccionCliente(txtCodigoCliente.Text, "B");*/
                        p.RunCliente = txtIdentificador.Text;
                        p.Telefono = txtTelefono.Text;
                        p.Email = txtEmail.Text;
                        p.TipoVenta = rbTipoVenta.Value.ToString();
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
                    divTotalVis.Visible = true;
                    txtTotal.Text = "0";
                }
                else
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Direccion Cliente No Encontrada, favor verificar o registrar');", true);
                }

            }
            catch (Exception ex)
            {

                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "btnPickear_Click - SMMVentas";
                sm.InsetLogError(err);
            }
          

            
        }

        protected void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                divPickear.Visible = true;
            }
            catch (Exception ex)
            {

                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "btnBuscarProducto_Click - SMMVentas";
                sm.InsetLogError(err);
            }
           
        }

        //protected void btnAgregaProdSinDun_Click(object sender, EventArgs e)
        //{
        //    PreventaSMMClasss mc = new PreventaSMMClasss();

        //    if (cboProductosinDun.Value != null)
        //    {
        //        divPickear.Visible = true;

               
        //        SMM_Preventa_Detalle pr = new SMM_Preventa_Detalle();

        //        string[] co = new string[2];
        //        string cP = cboProductosinDun.Value.ToString();
        //        co = cP.Split(';');

        //        string coPro = co[0].ToString();
        //        string uMed = co[1].ToString().Trim();


        //        List<SMM_VW_DATO_BUSCAPROD_PREVENTA> lt = mc.BusquedaProductoSinDun(coPro, uMed);


        //        int cod = mc.validaProdRegistradoPreVenta(coPro, Convert.ToInt32(Session["IdPreventa"]), uMed);
        //        int nOrden = mc.TraeNOrdenPrev(Convert.ToInt32(Session["IdPreventa"]));


        //        if (cod == 0)
        //        {
        //            int cantProd = mc.CuentaLineaProducto(Convert.ToInt32(Session["IdPreventa"]));

        //            if (rbTipoVenta.Value.Equals("F"))
        //            {

        //                if (cantProd <= 59)
        //                {
        //                    foreach (var t in lt)
        //                    {
        //                        pr.CodProducto = t.ItemCode;
        //                        pr.Producto = t.ItemName;
        //                        pr.Cantidad = 1;
        //                        pr.Precio = t.Price;
        //                        pr.CodBarra = t.BcdCode;
        //                        pr.UniMedida = t.UomCode;
        //                        pr.CodUniMedida = t.UomEntry;
        //                        pr.Preventa_ID = Convert.ToInt32(Session["IdPreventa"]);
        //                        pr.ByOrder = nOrden + 1;

        //                        mc.InsertaPreventaDetalle(pr);
        //                        divPickear.Visible = true;
        //                        txtDun.Text = string.Empty;
        //                        cboProductosinDun.SelectedIndex = -1;
        //                        //cboProductosinDun.Text = string.Empty;
        //                        txtDun.Focus();

        //                    }
        //                }
        //                else
        //                {
        //                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Supera cantidad permitida en Factura!');", true);
        //                    divPickear.Visible = true;
        //                }
        //            }
        //            else
        //            {
        //                foreach (var t in lt)
        //                {
        //                    pr.CodProducto = t.ItemCode;
        //                    pr.Producto = t.ItemName;
        //                    pr.Cantidad = 1;
        //                    pr.Precio = t.Price;
        //                    pr.CodBarra = t.BcdCode;
        //                    pr.UniMedida = t.UomCode;
        //                    pr.CodUniMedida = t.UomEntry;
        //                    pr.Preventa_ID = Convert.ToInt32(Session["IdPreventa"]);
        //                    pr.ByOrder = nOrden + 1;

        //                    mc.InsertaPreventaDetalle(pr);
        //                    divPickear.Visible = true;
        //                    txtDun.Text = string.Empty;
        //                    cboProductosinDun.SelectedIndex = -1;
        //                    //cboProductosinDun.Text = string.Empty;
        //                    txtDun.Focus();

        //                }
        //            }

        //            //int totProd = cantProd + 1;
        //            //lblCantidadProd.Text = "Cantidad de Productos" + ": " + totProd;
        //        }
        //        else
        //        {
        //            foreach (var t in lt)
        //            {

        //                mc.ActualizaCantidadPreventa(Convert.ToInt32(Session["IdPreventa"]), t.ItemCode, 1, uMed);
        //                divPickear.Visible = true;
        //                txtDun.Text = string.Empty;
        //                cboProductosinDun.SelectedIndex = -1;
        //                //cboProductosinDun.Text = string.Empty;
        //                txtDun.Focus();
        //            }
        //        }



        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Seleccione Producto!');", true);
        //        divPickear.Visible = true;

        //    }

        //    txtTotal.Text= mc.traetotalPreventa(Convert.ToInt32(Session["IdPreventa"])).ToString();
        //}
        protected void btnAnular_Click(object sender, EventArgs e)
        {
            try
            {
                popAnulaPreventa.ShowOnPageLoad = true;
                divPickear.Visible = true;
            }
            catch (Exception  ex)
            {

                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "btnAnular_Click - SMMVentas";
                sm.InsetLogError(err);
            }
          

           
        }
        protected void GvDatos_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
        {
            try
            {
                PreventaSMMClasss mc = new PreventaSMMClasss();
                txtTotal.Text = mc.traetotalPreventa(Convert.ToInt32(Session["IdPreventa"])).ToString();

                divPickear.Visible = true;
                GvDatos.DataBind();
            }
            catch (Exception ex)
            {
                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "GvDatos_RowDeleted - SMMVentas";
                sm.InsetLogError(err);
            }
                 
        }

        protected void btnSiAnula_Click(object sender, EventArgs e)
        {
            try
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
                        txtBusCliente.Text = string.Empty;
                        rbTipoVenta.SelectedIndex = -1;
                        rbTipoVenta.Focus();
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
                //divTotalVis.Visible = false;
                txtTotal.Text = string.Empty;
                txtApliDescuent.Text = "5";
            }
            catch (Exception ex)
            {
                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "btnSiAnula_Click - SMMVentas";
                sm.InsetLogError(err);
            }
           
        }

        protected void btnNoAnula_Click(object sender, EventArgs e)
        {
            try
            {
                divPickear.Visible = true;
                divTotalVis.Visible = true;
                popAnulaPreventa.ShowOnPageLoad = false;
                txtDun.Focus();
            }
            catch (Exception ex)
            {

                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "btnNoAnula_Click - SMMVentas";
                sm.InsetLogError(err);
            }
          
        }
        protected void btnCrear_Click(object sender, EventArgs e)
        {

            try
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
                txtBusCliente.Text = string.Empty;
                rbTipoVenta.SelectedIndex = -1;
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
                rbTipoVenta.Focus();
                btnPickear.Enabled = true;

                SMM_LogCaja lg = new SMM_LogCaja();
                PreventaCajaSMM pvc = new PreventaCajaSMM();

                lg.Preventa_ID = Convert.ToInt32(Convert.ToInt32(Session["IdPreventa"]));
                lg.FecharError = DateTime.Now;
                lg.Error = "Crea Preventa n° " + Convert.ToInt32(Session["IdPreventa"]);
                lg.Usuario = Convert.ToString(Session["NombreUsuario"]);
                pvc.InsertaLog(lg);

                //divTotalVis.Visible = false;
                txtTotal.Text = string.Empty;
            }
            catch (Exception ex)
            {

                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "btnCrear_Click - SMMVentas";
                sm.InsetLogError(err);
            }

            LinqDataSource1.Where = "Preventa_ID=0";
            GvDatos.DataBind();
        }

        protected void GvDatos_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            if (e.CommandArgs.CommandName == "cmdDescEspecial")
            {
                divPickear.Visible = true;
                Session["KeyDesc"] = e.KeyValue.ToString();
                PopDesc.ShowOnPageLoad = true;
                // Response.Redirect("~/Calidad/Inspecciones/InspeccionArrozDetalle.aspx");
            }
        }

        protected void btnVerificar_Click(object sender, EventArgs e)
        {

            try
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
            catch (Exception ex)
            {

                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "btnVerificar_Click - SMMVentas";
                sm.InsetLogError(err);
            }
         
        }

        protected void btnCancela_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {

                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "btnCancela_Click - SMMVentas";
                sm.InsetLogError(err);
            }
          
        }

        protected void GvDatos_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
        {
            try
            {
                PreventaSMMClasss mc = new PreventaSMMClasss();
                txtTotal.Text = mc.traetotalPreventa(Convert.ToInt32(Session["IdPreventa"])).ToString();
                divPickear.Visible = true;

                GvDatos.DataBind();
            }
            catch (Exception ex)
            {

                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "GvDatos_RowUpdated - SMMVentas";
                sm.InsetLogError(err);
            }
         
        }

        protected void btnAplicarDesc_Click(object sender, EventArgs e)
        {
            try
            {
                PreventaSMMClasss mc = new PreventaSMMClasss();
                if (txtApliDescuent.Text.Equals(string.Empty) || txtApliDescuent.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('ingrese descuento');", true);
                }
                else
                {

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
            catch (Exception ex)
            {

                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "btnAplicarDesc_Click - SMMVentas";
                sm.InsetLogError(err);
            }
        
        }

        protected void ASPxCallback1_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            try
            {
                PreventaSMMClasss mc = new PreventaSMMClasss();
                SMM_Preventa_Detalle pr = new SMM_Preventa_Detalle();

                List<SMM_VW_DATO_BUSCAPROD_PREVENTA> lt = mc.ConsultaProductoPreventa(txtDun.Text);
                int nOrden = mc.TraeNOrdenPrev(Convert.ToInt32(Session["IdPreventa"]));

                Session["Dun"] = txtDun.Text;

                if (btnPickear.IsEnabled())
                {

                    e.Result = "Presiona Pickear";
                }
                else
                {
                    if (lt.Count != 0)
                    {
                        foreach (var t in lt)
                        {
                            int cod = mc.validaProdRegistradoPreVenta(t.ItemCode, Convert.ToInt32(Session["IdPreventa"]), t.UomCode);


                            if (cod == 0)
                            {
                                int cantProd = mc.CuentaLineaProducto(Convert.ToInt32(Session["IdPreventa"]));

                                if (rbTipoVenta.Value.Equals("F"))
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
                                        //txtDun.Focus();

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
                                    //txtDun.Focus();
                                }

                                //int totProd = cantProd + 1;
                                //lblCantidadProd.Text = "Cantidad de Productos" + ": " + totProd;
                            }
                            else
                            {
                                mc.ActualizaCantidadPreventa(Convert.ToInt32(Session["IdPreventa"]), t.ItemCode, 1, t.UomCode);
                                divPickear.Visible = true;
                                txtDun.Text = string.Empty;
                                //txtDun.Focus();

                            }


                        }

                        e.Result = mc.traetotalPreventa(Convert.ToInt32(Session["IdPreventa"])).ToString();
                    }
                    else
                    {
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Codigo de producto no existe!');", true);

                        e.Result = "Codigo no existe!";
                        divPickear.Visible = true;

                        //txtDun.Focus();
                    }
                    /*  txtTotal.Text */
                    //string script = mc.traetotalPreventa(Convert.ToInt32(Session["IdPreventa"])).ToString();
                    //e.Result = script;
                }
            }
            catch (Exception ex)
            {

                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "ASPxCallback1_Callback - SMMVentas";
                sm.InsetLogError(err);
            }
       
        }     

        protected void GvDatos_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {

            try
            {
                int prevId = Convert.ToInt32(e.Keys["Preventa_ID"]);
                string codPro = Convert.ToString(e.Keys["CodProducto"]);
                string uniMed = Convert.ToString(e.Keys["UniMedida"]);
                PreventaSMMClasss mc = new PreventaSMMClasss();

                mc.EliminaProductosPreventaDetalle(prevId, codPro, uniMed);
                //int 
                //throw new Exception("Error");
                //e.Cancel = true;
                GvDatos.DataBind();
                e.Cancel = true;

                //string r1 = mc.traetotalPreventa(Convert.ToInt32(Session["IdPreventa"])).ToString();

                //string script = "function(){ updText('" + r1 + "'); };";
                //ClientScript.RegisterStartupScript(this.GetType(), "updText", "updText('" + r1 + "');", true);  
            }
            catch (Exception ex)
            {

                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "GvDatos_RowDeleting - SMMVentas";
                sm.InsetLogError(err);
            }
               
        }

        protected void ASPxCallback1_Callback1(object source, CallbackEventArgs e)
        {

            try
            {
                PreventaSMMClasss mc = new PreventaSMMClasss();
                string script = mc.traetotalPreventa(Convert.ToInt32(Session["IdPreventa"])).ToString();

                string dun = Session["Dun"].ToString();

                List<SMM_VW_DATO_BUSCAPROD_PREVENTA> lt = mc.ConsultaProductoPreventa(dun);

                if (lt.Count == 0)
                {
                    e.Result = "Codigo no existe";
                }
                else
                if (btnPickear.IsEnabled())
                {
                    txtTotal.Font.Size = FontUnit.Small;
                    e.Result = "Presiona Pickear";
                }
                else { e.Result = script; }
            }
            catch (Exception ex)
            {

                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "ASPxCallback1_Callback1 - SMMVentas";
                sm.InsetLogError(err);
            }
           
            
        }

        protected void txtDun_TextChanged(object sender, EventArgs e)
        {

        }
    }
}