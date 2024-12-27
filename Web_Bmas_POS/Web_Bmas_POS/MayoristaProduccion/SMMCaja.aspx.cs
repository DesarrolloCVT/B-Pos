using DB_Bmas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_Bmas_POS.MayoristaProduccion
{
    public partial class SMMCaja : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtNPreventa.Focus();
            Novisible();          
        }

        protected void txtNPreventa_TextChanged(object sender, EventArgs e)
        {

            try
            {

                PreventaCajaSMM pc = new PreventaCajaSMM();


                int estado = pc.TraeEstadoPreventa(Convert.ToInt32(txtNPreventa.Text));

                if (estado == 1)
                {
                    GvDatosclientes.Visible = true;
                    GvDatos.Visible = true;
                    GvDatoPagos.Visible = true;
                    btnPagar.Enabled = true;

                    string res = pc.TraeTipoVenta(Convert.ToInt32(txtNPreventa.Text));
                    if (res.Equals("B"))
                    {
                        rbtTipoDoc.SelectedIndex = 0;


                    }
                    else
                    {
                        rbtTipoDoc.SelectedIndex = 1;


                    }

                    int SumaMonIng = pc.sumaMontosIngresados(Convert.ToInt32(txtNPreventa.Text));

                    if (SumaMonIng == 0)
                    {
                        PreventaCajaSMM pv = new PreventaCajaSMM();
                        string mont = pv.TraeMontoVenta(Convert.ToInt32(txtNPreventa.Text));
                        txtMonto.Text = mont;
                        Session["MontoVent"] = mont;
                        stCuantoPaga.Visible = true;
                        tdCuantoPaga.Visible = true;
                        stVuelto.Visible = true;
                        tdVuelto.Visible = true;
                        cboTipoPago.SelectedIndex = 0;
                        txtCuantoPaga.Focus();
                    }
                    else
                    {
                        PreventaCajaSMM pv = new PreventaCajaSMM();
                        string mont = pv.TraeMontoVenta(Convert.ToInt32(txtNPreventa.Text));
                        stCuantoPaga.Visible = true;
                        tdCuantoPaga.Visible = true;
                        stVuelto.Visible = true;
                        tdVuelto.Visible = true;
                        txtMonto.Text = mont;
                        cboTipoPago.SelectedIndex = 0;
                        txtCuantoPaga.Focus();

                        // Session["MontoVent"] = mont;

                        txtMonto.Text = Convert.ToString(Convert.ToInt32(mont) - SumaMonIng);
                    }

                    string CodClient = pc.TraeCodigoCliente(Convert.ToInt32(txtNPreventa.Text));
                    if (CodClient == "C66666666-6")
                    {
                        rbtTipoDoc.Visible = false;
                    }
                    if (CodClient == "C66666666-6")
                    {
                        GvDatos.Columns["Descuento"].Visible = false;

                    }
                    else { GvDatos.Columns["Desc"].Visible = false; }

                    Session["cClient"] = CodClient;

                }
                else if (estado == 3)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Preventa ya se encuentra terminada !');", true);
                    txtNPreventa.Text = string.Empty;
                    txtNPreventa.Focus();
                    txtMonto.Text = string.Empty;
                    GvDatosclientes.Visible = false;
                    GvDatos.Visible = false;
                    GvDatoPagos.Visible = false;
                    btnPagar.Enabled = false;

                }
                else
                {

                    txtMonto.Text = string.Empty;
                    txtNPreventa.Focus();
                }
            }
            catch (Exception ex)
            {
                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "txtNPreventa_TextChanged - SMMCaja";
                sm.InsetLogError(err);
            }

        }

        protected void txtDun_TextChanged(object sender, EventArgs e)
        {
            try
            {
                PreventaSMMClasss mc = new PreventaSMMClasss();

                SMM_Preventa_Detalle pr = new SMM_Preventa_Detalle();

                List<SMM_VW_DATO_BUSCAPROD_PREVENTA> lt = mc.ConsultaProductoPreventa(txtDun.Text);
                int nOrden = mc.TraeNOrdenPrev(Convert.ToInt32(txtNPreventa.Text));

                if (lt.Count != 0)
                {
                    foreach (var t in lt)
                    {
                        int cod = mc.validaProdRegistradoPreVenta(t.ItemCode, Convert.ToInt32(txtNPreventa.Text), t.UomCode);


                        if (cod == 0)
                        {
                            int cantProd = mc.CuentaLineaProducto(Convert.ToInt32(Session["IdPreventa"]));

                            if (rbtTipoDoc.Value.Equals("F"))
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
                                    pr.Preventa_ID = Convert.ToInt32(txtNPreventa.Text);
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
                                pr.Preventa_ID = Convert.ToInt32(txtNPreventa.Text);
                                pr.ByOrder = nOrden + 1;

                                mc.InsertaPreventaDetalle(pr);
                                divPickear.Visible = true;
                                txtDun.Text = string.Empty;
                                txtDun.Focus();
                            }


                        }
                        else
                        {
                            mc.ActualizaCantidadPreventa(Convert.ToInt32(txtNPreventa.Text), t.ItemCode, 1, t.UomCode);
                            divPickear.Visible = true;
                            txtDun.Text = string.Empty;
                            txtDun.Focus();

                        }

                    }

                    PreventaCajaSMM pc = new PreventaCajaSMM();
                    string mon = pc.TraeMontoVenta(Convert.ToInt32(txtNPreventa.Text));
                    txtMonto.Text = mon;

                    stCuantoPaga.Visible = true;
                    tdCuantoPaga.Visible = true;
                    stVuelto.Visible = true;
                    tdVuelto.Visible = true;
                    GvDatos.DataBind();
                    GvDatoPagos.DataBind();
                    cboTipoPago.SelectedIndex = 0;

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Codigo de producto no existe!');", true);
                    divPickear.Visible = true;
                    txtDun.Text = string.Empty;
                    txtDun.Focus();
                }
            }
            catch (Exception ex )
            {

                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "txtDun_TextChanged - SMMCaja";
                sm.InsetLogError(err);
            }
           
           
        }

        protected void GvDatos_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
        {

            try
            {
                PreventaCajaSMM pc = new PreventaCajaSMM();
                string mon = pc.TraeMontoVenta(Convert.ToInt32(txtNPreventa.Text));

                stCuantoPaga.Visible = true;
                tdCuantoPaga.Visible = true;
                stVuelto.Visible = true;
                tdVuelto.Visible = true;
                cboTipoPago.SelectedIndex = 0;
                txtMonto.Text = mon;

                GvDatos.DataBind();
            }
            catch (Exception ex)
            {

                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "GvDatos_RowDeleted - SMMCaja";
                sm.InsetLogError(err);
            }
           
        }

        protected void GvDatos_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(e.NewValues["Descuento"]) > 5)
                {
                    e.Cancel = true;
                }
                GvDatos.DataBind();
            }
            catch (Exception ex)
            {

                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "GvDatos_RowUpdating - SMMCaja";
                sm.InsetLogError(err);
            }           
        }

        private void Novisible()
        {
            stCuantoPaga.Visible = false;
            tdCuantoPaga.Visible = false;
            stVuelto.Visible = false;
            tdVuelto.Visible = false;
            stBoucher.Visible = false;
            tdBoucher.Visible = false;
            tdCuotas.Visible = false;
            stNTarjeta.Visible = false;
            tdNTarjeta.Visible = false;
            stFechaDoc.Visible = false;
            tdFechaDoc.Visible = false;
            stNCheque.Visible = false;
            tdNCheque.Visible = false;
            dvGuardar.Visible = false;
        }

        protected void cboTipoPago_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                PreventaCajaSMM pv = new PreventaCajaSMM();
                string mont = pv.TraeMontoVentaSinRedondeo(Convert.ToInt32(txtNPreventa.Text));
                string monAct = pv.TraeMontoActualizado(Convert.ToInt32(txtNPreventa.Text));
                string monActSinR = pv.TraeMontoActualizadoSinRedondeo(Convert.ToInt32(txtNPreventa.Text));
                string montRed = pv.TraeMontoVenta(Convert.ToInt32(txtNPreventa.Text));

                switch (Convert.ToInt32(cboTipoPago.Value))
                {

                    case 1:
                        stCuantoPaga.Visible = true;
                        tdCuantoPaga.Visible = true;
                        stVuelto.Visible = true;
                        tdVuelto.Visible = true;
                        txtCuantoPaga.Focus();
                        if (monAct == "0")
                        {
                            txtMonto.Text = montRed;
                        }
                        else
                        {
                            txtMonto.Text = monAct;
                        }

                        break;
                    case 2:
                        stBoucher.Visible = true;
                        tdBoucher.Visible = true;
                        tdNTarjeta.Visible = true;
                        dvGuardar.Visible = true;
                        txtNTarjeta.Focus();
                        if (monAct == "0")
                        {
                            txtMonto.Text = mont;
                        }
                        else
                        {
                            //txtMonto.Text = mont;
                            txtMonto.Text = monActSinR;
                        }



                        break;
                    case 3:
                        tdCuotas.Visible = true;
                        stBoucher.Visible = true;
                        tdBoucher.Visible = true;
                        stNTarjeta.Visible = true;
                        tdNTarjeta.Visible = true;
                        dvGuardar.Visible = true;
                        txtNTarjeta.Focus();
                        if (monAct == "0")
                        {
                            txtMonto.Text = mont;
                        }
                        else
                        {
                            txtMonto.Text = monActSinR;
                        }



                        break;
                    case 4:

                        stBoucher.Visible = true;
                        tdBoucher.Visible = true;
                        dvGuardar.Visible = true;
                        txtNTarjeta.Focus();
                        if (monAct == "0")
                        {
                            txtMonto.Text = mont;
                        }
                        else
                        {
                            txtMonto.Text = monActSinR;
                        }


                        break;
                    case 5:

                        stFechaDoc.Visible = true;
                        tdFechaDoc.Visible = true;
                        tdNCheque.Visible = true;
                        dvGuardar.Visible = true;
                        txtNcheque.Focus();
                        if (monAct == "0")
                        {
                            txtMonto.Text = mont;
                        }
                        else
                        {
                            txtMonto.Text = monActSinR;
                        }


                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "cboTipoPago_SelectedIndexChanged - SMMCaja";
                sm.InsetLogError(err);
            }
           

           
        }

        protected void txtCuantoPaga_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(txtCuantoPaga.Text) > Convert.ToInt32(txtMonto.Text))
                {
                    Session["MN"] = Convert.ToInt32(txtMonto.Text);
                    txtVuelto.Text = Convert.ToString(Convert.ToInt32(txtCuantoPaga.Text) - Convert.ToInt32(Session["MN"]));
                    PreventaCajaSMM pv = new PreventaCajaSMM();

                    SMM_Preventa_DatoPago dp = new SMM_Preventa_DatoPago();

                    dp.Preventa_ID = Convert.ToInt32(txtNPreventa.Text);
                    dp.TipoPago = cboTipoPago.Value.ToString();
                    dp.Monto = Convert.ToInt32(txtMonto.Text);
                    dp.TipoDocumento = rbtTipoDoc.Value.ToString();


                    pv.InsertaDatoPago(dp);

                    string mon = pv.TraeMontoActualizado(Convert.ToInt32(txtNPreventa.Text));

                    txtMonto.Text = mon;

                    GvDatoPagos.DataBind();
                    stCuantoPaga.Visible = true;
                    tdCuantoPaga.Visible = true;
                    stVuelto.Visible = true;
                    tdVuelto.Visible = true;


                }
                else
                {


                    PreventaCajaSMM pv = new PreventaCajaSMM();

                    SMM_Preventa_DatoPago dp = new SMM_Preventa_DatoPago();

                    dp.Preventa_ID = Convert.ToInt32(txtNPreventa.Text);
                    dp.TipoPago = cboTipoPago.Value.ToString();
                    dp.Monto = Convert.ToInt32(txtCuantoPaga.Text);
                    dp.TipoDocumento = rbtTipoDoc.Value.ToString();


                    pv.InsertaDatoPago(dp);
                    GvDatoPagos.DataBind();
                    stCuantoPaga.Visible = true;
                    tdCuantoPaga.Visible = true;
                    stVuelto.Visible = true;
                    tdVuelto.Visible = true;

                    txtVuelto.Text = "0";
                    string montSR = pv.TraeMontoVentaSinRedondeo(Convert.ToInt32(txtNPreventa.Text));

                    //string mon = pv.TraeMontoActualizado(Convert.ToInt32(txtNPreventa.Text));

                    string montoRestanteSR = Convert.ToString(Convert.ToInt32(montSR) - Convert.ToInt32(txtCuantoPaga.Text)); // mon;
                    txtMonto.Text = montoRestanteSR;

                    //Session["MontoRestanteSinRedondeo"] =montoRestanteSR;
                    txtCuantoPaga.Text = string.Empty;
                    txtCuantoPaga.Focus();

                }


                SMM_LogCaja lg = new SMM_LogCaja();
                PreventaCajaSMM pvc = new PreventaCajaSMM();

                lg.Preventa_ID = Convert.ToInt32(txtNPreventa.Text);
                lg.FecharError = DateTime.Now;
                lg.Error = "Registra tipo pago : Efectivo";
                pvc.InsertaLog(lg);
            }
            catch (Exception ex)
            {

                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "txtCuantoPaga_TextChanged - SMMCaja";
                sm.InsetLogError(err);
            }

            

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //btnGuardar.Enabled = false;        
            //UpdatePanel1.Update();
            //LoadingPanel.Visible = true;

            //System.Threading.Thread.Sleep(2000);


            try
            {
                if (txtMonto.Text.Equals("0"))
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Monto esta en 0 favor verificar o pago ya registrado!');", true);


                }
                else
                        if (Convert.ToInt32(cboTipoPago.Value) == 2)
                {
                    PreventaCajaSMM pv = new PreventaCajaSMM();

                    SMM_Preventa_DatoPago dp = new SMM_Preventa_DatoPago();

                    dp.Preventa_ID = Convert.ToInt32(txtNPreventa.Text);
                    dp.TipoPago = cboTipoPago.Value.ToString();
                    dp.Monto = Convert.ToInt32(txtMonto.Text);
                    dp.TipoDocumento = rbtTipoDoc.Value.ToString();
                    dp.nBoucher = Convert.ToInt32(txtNBoucher.Text);
                    dp.NTarjeta = txtNTarjeta.Text;


                    pv.InsertaDatoPago(dp);

                    string mon = pv.TraeMontoActualizadoSinRedondeo(Convert.ToInt32(txtNPreventa.Text));

                    SMM_LogCaja lg = new SMM_LogCaja();
                    PreventaCajaSMM pvc = new PreventaCajaSMM();

                    lg.Preventa_ID = Convert.ToInt32(txtNPreventa.Text);
                    lg.FecharError = DateTime.Now;
                    lg.Error = "Registra tipo pago : Debito";
                    pvc.InsertaLog(lg);

                    txtMonto.Text = mon;

                    txtNBoucher.Text = string.Empty;
                    txtNTarjeta.Text = string.Empty;
                    GvDatoPagos.DataBind();

                    btnGuardar.Enabled = true;

                }
                else if (Convert.ToInt32(cboTipoPago.Value) == 3)
                {
                    PreventaCajaSMM pv = new PreventaCajaSMM();

                    SMM_Preventa_DatoPago dp = new SMM_Preventa_DatoPago();

                    dp.Preventa_ID = Convert.ToInt32(txtNPreventa.Text);
                    dp.TipoPago = cboTipoPago.Value.ToString();
                    dp.Monto = Convert.ToInt32(txtMonto.Text);
                    dp.TipoDocumento = rbtTipoDoc.Value.ToString();
                    dp.nBoucher = Convert.ToInt32(txtNBoucher.Text);
                    dp.NTarjeta = txtNTarjeta.Text;
                    dp.Cuotas = Convert.ToInt32(cboCuotas.Value);


                    pv.InsertaDatoPago(dp);

                    string mon = pv.TraeMontoActualizadoSinRedondeo(Convert.ToInt32(txtNPreventa.Text));

                    SMM_LogCaja lg = new SMM_LogCaja();
                    PreventaCajaSMM pvc = new PreventaCajaSMM();

                    lg.Preventa_ID = Convert.ToInt32(txtNPreventa.Text);
                    lg.FecharError = DateTime.Now;
                    lg.Error = "Registra tipo pago : Credito";
                    pvc.InsertaLog(lg);

                    txtMonto.Text = mon;

                    txtNBoucher.Text = string.Empty;
                    txtNTarjeta.Text = string.Empty;
                    cboCuotas.SelectedIndex = -1;
                    GvDatoPagos.DataBind();
                    btnGuardar.Enabled = true;


                }
                else if (Convert.ToInt32(cboTipoPago.Value) == 4)
                {
                    PreventaCajaSMM pv = new PreventaCajaSMM();

                    SMM_Preventa_DatoPago dp = new SMM_Preventa_DatoPago();

                    dp.Preventa_ID = Convert.ToInt32(txtNPreventa.Text);
                    dp.TipoPago = cboTipoPago.Value.ToString();
                    dp.Monto = Convert.ToInt32(txtMonto.Text);
                    dp.TipoDocumento = rbtTipoDoc.Value.ToString();
                    dp.nBoucher = Convert.ToInt32(txtNBoucher.Text);

                    pv.InsertaDatoPago(dp);

                    string mon = pv.TraeMontoActualizadoSinRedondeo(Convert.ToInt32(txtNPreventa.Text));


                    SMM_LogCaja lg = new SMM_LogCaja();
                    PreventaCajaSMM pvc = new PreventaCajaSMM();

                    lg.Preventa_ID = Convert.ToInt32(txtNPreventa.Text);
                    lg.FecharError = DateTime.Now;
                    lg.Error = "Registra tipo pago : Transferencia";
                    pvc.InsertaLog(lg);

                    txtMonto.Text = mon;

                    txtNBoucher.Text = string.Empty;
                    //txtNTarjeta.Text = string.Empty;
                    GvDatoPagos.DataBind();
                    btnGuardar.Enabled = true;

                }
                else if (Convert.ToInt32(cboTipoPago.Value) == 5)
                {
                    PreventaCajaSMM pv = new PreventaCajaSMM();

                    SMM_Preventa_DatoPago dp = new SMM_Preventa_DatoPago();

                    dp.Preventa_ID = Convert.ToInt32(txtNPreventa.Text);
                    dp.TipoPago = cboTipoPago.Value.ToString();
                    dp.Monto = Convert.ToInt32(txtMonto.Text);
                    dp.TipoDocumento = rbtTipoDoc.Value.ToString();
                    dp.NCheque = Convert.ToInt32(txtNcheque.Text);
                    dp.FechaDocumento = dteFechaDocumento.Date;

                    pv.InsertaDatoPago(dp);

                    string mon = pv.TraeMontoActualizadoSinRedondeo(Convert.ToInt32(txtNPreventa.Text));

                    SMM_LogCaja lg = new SMM_LogCaja();
                    PreventaCajaSMM pvc = new PreventaCajaSMM();

                    lg.Preventa_ID = Convert.ToInt32(txtNPreventa.Text);
                    lg.FecharError = DateTime.Now;
                    lg.Error = "Registra tipo pago : Cheque";
                    pvc.InsertaLog(lg);

                    txtMonto.Text = mon;

                    txtNcheque.Text = string.Empty;
                    GvDatoPagos.DataBind();
                    btnGuardar.Enabled = true;
                }
            }
            catch (Exception ex )
            {

                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "btnGuardar_Click - SMMCaja";
                sm.InsetLogError(err);
            }        
        }

        protected void GvDatoPagos_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {

            try
            {
                PreventaCajaSMM pv = new PreventaCajaSMM();
                int key = Convert.ToInt32(e.KeyValue);

                //int SumaMonIng = pv.sumaMontosIngresados(Convert.ToInt32(txtNPreventa.Text));
                int MontEliminar = pv.TraeMontoEliminar(key);

                txtMonto.Text = Convert.ToString(MontEliminar + Convert.ToInt32(txtMonto.Text));

                pv.EliminaMonto(key);

                SMM_LogCaja lg = new SMM_LogCaja();
                PreventaCajaSMM pvc = new PreventaCajaSMM();

                lg.Preventa_ID = Convert.ToInt32(txtNPreventa.Text);
                lg.FecharError = DateTime.Now;
                lg.Error = "Elimina tipo Pago";
                pvc.InsertaLog(lg);

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Pago Eliminado!');", true);
                stCuantoPaga.Visible = true;
                tdCuantoPaga.Visible = true;
                txtCuantoPaga.Text = string.Empty;
                txtCuantoPaga.Focus();
                txtVuelto.Text = string.Empty;
                stVuelto.Visible = true;
                tdVuelto.Visible = true;
                GvDatoPagos.DataBind();
                cboTipoPago.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "GvDatoPagos_RowCommand - SMMCaja";
                sm.InsetLogError(err);
            }
          
        }

        protected void btnAnular_Click(object sender, EventArgs e)
        {
            popAnulaVenta.ShowOnPageLoad = true;
        }

        protected void btnNoAnula_Click(object sender, EventArgs e)
        {
            popAnulaVenta.ShowOnPageLoad = false;
        }

        protected void btnSiAnula_Click(object sender, EventArgs e)
        {

            try
            {
                PreventaCajaSMM pc = new PreventaCajaSMM();

                int resp = pc.EliminaDatoPago(Convert.ToInt32(txtNPreventa.Text));

                if (resp != 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Venta Anulada !');", true);

                    SMM_LogCaja lg = new SMM_LogCaja();
                    PreventaCajaSMM pvc = new PreventaCajaSMM();

                    lg.Preventa_ID = Convert.ToInt32(txtNPreventa.Text);
                    lg.FecharError = DateTime.Now;
                    lg.Error = "Anula Venta";
                    pvc.InsertaLog(lg);

                    txtNPreventa.Text = string.Empty;
                    txtNPreventa.Focus();
                    txtMonto.Text = string.Empty;
                    txtCuantoPaga.Text = string.Empty;
                    txtVuelto.Text = string.Empty;
                    popAnulaVenta.ShowOnPageLoad = false;
                    Novisible();
                }
            }
            catch (Exception ex)
            {

                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "btnSiAnula_Click - SMMCaja";
                sm.InsetLogError(err);
            }
         
        }

        protected void rbtTipoDoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PreventaSMMClasss pv = new PreventaSMMClasss();
                pv.ActualizaTipoVenta(Convert.ToInt32(txtNPreventa.Text), Convert.ToString(rbtTipoDoc.Value));
                GvDatosclientes.DataBind();
            }
            catch (Exception ex)
            {
                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "rbtTipoDoc_SelectedIndexChanged - SMMCaja";
                sm.InsetLogError(err);
            }
           
        }

        protected void btnPagar_Click(object sender, EventArgs e)
        {


            try
            {
                //btnPagar.Enabled = false;
                int count = GvDatoPagos.VisibleRowCount;
                SMM_LogCaja lg = new SMM_LogCaja();
                PreventaCajaSMM pvc = new PreventaCajaSMM();


                lg.Preventa_ID = Convert.ToInt32(txtNPreventa.Text);
                lg.FecharError = DateTime.Now;
                lg.Error = "Preciona Pagar";
                lg.Usuario = Convert.ToString(Session["NombreUsuario"]);
                pvc.InsertaLog(lg);


                int Tt = ((txtMonto.Text.Equals(string.Empty) || txtMonto.Text.Equals("")) || txtMonto.Text == null) ? 0 : Convert.ToInt32(txtMonto.Text);


                if (Convert.ToInt32(Tt) < 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Monto negativo favor verificar !');", true);
                    lg.Preventa_ID = Convert.ToInt32(txtNPreventa.Text);
                    lg.FecharError = DateTime.Now;
                    lg.Error = " aviso, Monto negativo favor verificar";
                    lg.Usuario = Convert.ToString(Session["NombreUsuario"]);
                    pvc.InsertaLog(lg);
                }
                else
                if (txtNPreventa.Text == string.Empty || txtNPreventa.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Ingrese N° Preventa !');", true);

                    lg.Preventa_ID = Convert.ToInt32(txtNPreventa.Text);
                    lg.FecharError = DateTime.Now;
                    lg.Error = " aviso,Ingrese N° Preventa";
                    lg.Usuario = Convert.ToString(Session["NombreUsuario"]);
                    pvc.InsertaLog(lg);

                }
                else if (Convert.ToInt32(Tt) > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Tienes monto pendiente por pagar!');", true);
                    stCuantoPaga.Visible = true;
                    tdCuantoPaga.Visible = true;
                    stVuelto.Visible = true;
                    tdVuelto.Visible = true;
                    cboTipoPago.SelectedIndex = 0;
                    txtCuantoPaga.Focus();

                    lg.Preventa_ID = Convert.ToInt32(txtNPreventa.Text);
                    lg.FecharError = DateTime.Now;
                    lg.Error = " aviso,Tienes monto pendiente por pagar";
                    lg.Usuario = Convert.ToString(Session["NombreUsuario"]);
                    pvc.InsertaLog(lg);
                }
                else
               if (count != 0)
                {
                    LoadingPanel.Visible = true;
                    lg.Preventa_ID = Convert.ToInt32(txtNPreventa.Text);
                    lg.FecharError = DateTime.Now;
                    lg.Error = "Entra en procedimiento de pago ";
                    lg.Usuario = Convert.ToString(Session["NombreUsuario"]);
                    pvc.InsertaLog(lg);


                    PostVentasBMasClass pv = new PostVentasBMasClass();
                    PreventaSMMClasss prv = new PreventaSMMClasss();

                    string rest = pv.CreaDocumentoVenta(Convert.ToInt32(txtNPreventa.Text), Session["CIDUsuario"].ToString());
                    Session["PrevID"] = txtNPreventa.Text;
                    prv.ActualizaEstadoPreventa(Convert.ToInt32(Session["PrevID"]), 3);

                    if (rest.Equals("0"))
                    {
                        prv.ActualizaEstadoPreventa(Convert.ToInt32(txtNPreventa.Text), 3);

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Documento Creado !');", true);


                        lg.Preventa_ID = Convert.ToInt32(txtNPreventa.Text);
                        lg.FecharError = DateTime.Now;
                        lg.Error = "Documento Creado";
                        lg.Usuario = Convert.ToString(Session["NombreUsuario"]);
                        pvc.InsertaLog(lg);

                        prv.ActualizaEstadoPreventa(Convert.ToInt32(Session["PrevID"]), 3);

                        txtNPreventa.Text = string.Empty;
                        txtNPreventa.Focus();
                        txtCuantoPaga.Text = string.Empty;
                        txtVuelto.Text = string.Empty;
                        txtNTarjeta.Text = string.Empty;
                        txtNBoucher.Text = string.Empty;
                        txtNcheque.Text = string.Empty;
                        Novisible();
                    }
                    else
                    {

                        lg.Preventa_ID = Convert.ToInt32(txtNPreventa.Text);
                        lg.FecharError = DateTime.Now;
                        lg.Error = "documento con errores : " + rest;
                        lg.Usuario = Convert.ToString(Session["NombreUsuario"]);
                        pvc.InsertaLog(lg);

                        prv.ActualizaEstadoPreventa(Convert.ToInt32(txtNPreventa.Text), 2);

                        //LoadingPanel.Visible = false;

                        if (rest != "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('" + rest + "');", true);
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('eRROR ');", true);

                            prv.ActualizaEstadoPreventa(Convert.ToInt32(txtNPreventa.Text), 2);
                        }
                    }
                }
                else
                {

                    stCuantoPaga.Visible = true;
                    tdCuantoPaga.Visible = true;
                    stVuelto.Visible = true;
                    tdVuelto.Visible = true;
                    cboTipoPago.SelectedIndex = 0;
                    txtCuantoPaga.Focus();

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Registre metodo de pago ');", true);
                }

            }
            catch (Exception ex)
            {

                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "btnPagar_Click - SMMCaja";
                sm.InsetLogError(err);
            }      

            
        }

        protected void GvDatos_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
        {

            try
            {
                PreventaCajaSMM pc = new PreventaCajaSMM();
                string mon = pc.TraeMontoVenta(Convert.ToInt32(txtNPreventa.Text));

                //ActualizaMonto(Convert.ToInt32(txtNPreventa.Text));


                stCuantoPaga.Visible = true;
                tdCuantoPaga.Visible = true;
                stVuelto.Visible = true;
                tdVuelto.Visible = true;
                txtMonto.Text = mon;
                GvDatos.DataBind();
                cboTipoPago.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "btnPagar_Click - SMMCaja";
                sm.InsetLogError(err);
            }
           
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

        protected void btnCancela_Click(object sender, EventArgs e)
        {
            PopDesc.ShowOnPageLoad = false;
            divPickear.Visible = true;
            txtDun.Focus();
            if (Session["cClient"].ToString() == "C66666666-6")
            {
                GvDatos.Columns["Descuento"].Visible = false;
            }
            else { GvDatos.Columns["Desc"].Visible = false; }
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


                if (idUser == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Usuario no permitido para verificar');", true);
                }
                else
                {

                    PreventaSMMClasss pr = new PreventaSMMClasss();
                    //string verif = Convert.ToString(Session["NombreUsuario"]);

                    string[] co = new string[2];
                    string cP = Session["KeyDesc"].ToString();
                    co = cP.Split('|');

                    int PrevID = Convert.ToInt32(co[0].ToString());
                    string CodPro = co[1].ToString().Trim();
                    string UMed = co[2].ToString().Trim();

                    if (Convert.ToInt32(txtDescuentoEsp.Text) > 99)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Descuento no puede superar el 100%');", true);
                    }
                    else
                    {


                        bool res = pr.ActualizaDescuentoPrev(PrevID, CodPro, UMed, Convert.ToInt32(txtDescuentoEsp.Text));
                        PreventaCajaSMM pv = new PreventaCajaSMM();
                        string mont = pv.TraeMontoVenta(Convert.ToInt32(txtNPreventa.Text));
                        txtMonto.Text = mont;

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

                            if (Session["cClient"].ToString() == "C66666666-6")
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
                            if (Session["cClient"].ToString() == "C66666666-6")
                            {
                                GvDatos.Columns["Descuento"].Visible = false;
                            }
                            else { GvDatos.Columns["Desc"].Visible = false; }
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "btnVerificar_Click - SMMCaja";
                sm.InsetLogError(err);
            }
           
        }

        protected void btnVentaSinPago_Click(object sender, EventArgs e)
        {
            popSinPago.ShowOnPageLoad = true;
        }

        protected void btnCancelaNoPago_Click(object sender, EventArgs e)
        {
            popSinPago.ShowOnPageLoad = false;
            txtUser1.Text = string.Empty;
            txtPass1.Text = string.Empty;
            divPickear.Visible = true;
            //txtDun.Focus();
            //if (Session["cClient"].ToString() == "C66666666-6")
            //{
            //    GvDatos.Columns["Descuento"].Visible = false;
            //}
            //else { GvDatos.Columns["Desc"].Visible = false; }
        }

        protected void btnVerificaNoPago_Click(object sender, EventArgs e)
        {
            try
            {  //btnPagar.Enabled = false;
               //   int count = GvDatoPagos.VisibleRowCount;
                SMM_LogCaja lg = new SMM_LogCaja();
                PreventaCajaSMM pvc = new PreventaCajaSMM();


                lg.Preventa_ID = Convert.ToInt32(txtNPreventa.Text);
                lg.FecharError = DateTime.Now;
                lg.Error = "Preciona venta sin pago ";
                lg.Usuario = Convert.ToString(Session["NombreUsuario"]);
                pvc.InsertaLog(lg);

                LoadingPanel.Visible = true;

                if (txtNPreventa.Text == string.Empty || txtNPreventa.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Ingrese N° Preventa !');", true);
                }

                else
                {
                    PostVentasBMasClass pv = new PostVentasBMasClass();
                    PreventaSMMClasss prv = new PreventaSMMClasss();

                    string rest = pv.CreaDocumentoVentaSinPago(Convert.ToInt32(txtNPreventa.Text), Session["CIDUsuario"].ToString());
                    Session["PrevID"] = txtNPreventa.Text;
                    prv.ActualizaEstadoPreventa(Convert.ToInt32(Session["PrevID"]), 3);

                    if (rest.Equals("0"))
                    {
                        prv.ActualizaEstadoPreventa(Convert.ToInt32(txtNPreventa.Text), 3);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Documento Creado !');", true);


                        lg.Preventa_ID = Convert.ToInt32(txtNPreventa.Text);
                        lg.FecharError = DateTime.Now;
                        lg.Error = "Documento Creado";
                        lg.Usuario = Convert.ToString(Session["NombreUsuario"]);
                        pvc.InsertaLog(lg);

                        prv.ActualizaEstadoPreventa(Convert.ToInt32(Session["PrevID"]), 3);

                        txtNPreventa.Text = string.Empty;
                        txtNPreventa.Focus();
                        txtCuantoPaga.Text = string.Empty;
                        txtVuelto.Text = string.Empty;
                        txtNTarjeta.Text = string.Empty;
                        txtNBoucher.Text = string.Empty;
                        txtNcheque.Text = string.Empty;
                        Novisible();

                        popSinPago.ShowOnPageLoad = false;

                    }
                    else
                    {

                        lg.Preventa_ID = Convert.ToInt32(txtNPreventa.Text);
                        lg.FecharError = DateTime.Now;
                        lg.Error = "documento con errores : " + rest;
                        lg.Usuario = Convert.ToString(Session["NombreUsuario"]);
                        pvc.InsertaLog(lg);

                        prv.ActualizaEstadoPreventa(Convert.ToInt32(txtNPreventa.Text), 3);
                        popSinPago.ShowOnPageLoad = false;

                        //LoadingPanel.Visible = false;

                        if (rest != "-1")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('" + rest + "');", true);
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('eRROR ');", true);

                            prv.ActualizaEstadoPreventa(Convert.ToInt32(txtNPreventa.Text), 2);
                            popSinPago.ShowOnPageLoad = false;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Documento Creado !');", true);


                            lg.Preventa_ID = Convert.ToInt32(txtNPreventa.Text);
                            lg.FecharError = DateTime.Now;
                            lg.Error = "Documento Creado";
                            lg.Usuario = Convert.ToString(Session["NombreUsuario"]);
                            pvc.InsertaLog(lg);

                            // prv.ActualizaEstadoPreventa(Convert.ToInt32(Session["PrevID"]), 3);

                            txtNPreventa.Text = string.Empty;
                            txtNPreventa.Focus();
                            txtCuantoPaga.Text = string.Empty;
                            txtVuelto.Text = string.Empty;
                            txtNTarjeta.Text = string.Empty;
                            txtNBoucher.Text = string.Empty;
                            txtNcheque.Text = string.Empty;
                            Novisible();

                            popSinPago.ShowOnPageLoad = false;
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "btnVerificaNoPago_Click - SMMCaja";
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
                        mc.UpdateDescuentoPreventa(Convert.ToInt32(txtNPreventa.Text), Convert.ToInt32(txtApliDescuent.Text));
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Descuento Aplicado');", true);
                        txtApliDescuent.Text = string.Empty;
                        PreventaCajaSMM pv = new PreventaCajaSMM();

                        divPickear.Visible = true;
                        GvDatos.DataBind();
                        stCuantoPaga.Visible = true;
                        tdCuantoPaga.Visible = true;
                        stVuelto.Visible = true;
                        tdVuelto.Visible = true;
                        cboTipoPago.SelectedIndex = 0;
                        txtCuantoPaga.Focus();

                        string mont = pv.TraeMontoVenta(Convert.ToInt32(txtNPreventa.Text));
                        txtMonto.Text = mont;
                    }

                }
            }
            catch (Exception ex)
            {

                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "btnAplicarDesc_Click - SMMCaja";
                sm.InsetLogError(err);
            }
            
        }
    }
}