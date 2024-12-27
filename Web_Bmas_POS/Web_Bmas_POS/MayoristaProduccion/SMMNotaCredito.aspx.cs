using DB_Bmas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_Bmas_POS.MayoristaProduccion
{
    public partial class SMMNotaCredito : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string TipoDoc;
            if (rbTipoDoc.Value.Equals("B"))
            {
                TipoDoc = "IB";
            }
            else
            {
                TipoDoc = "--";
            }

            SMMNotaCreditoClass nc = new SMMNotaCreditoClass();

            List<VW_SMM_DATO_CLIENTES_NOTACREDITO> dt1 = nc.ListaDatoCliente(Convert.ToInt32(txtfolio.Text), TipoDoc);




            int DocEntr = 0;
            int res = 0;
            if (dt1.Count != 0)
            {
                foreach (var t in dt1)
                {
                    SMM_NotaCredito_DatoCliente dnc = new SMM_NotaCredito_DatoCliente();

                    dnc.FolioDocumento = Convert.ToInt32(txtfolio.Text);
                    dnc.TipoDocumento = rbTipoDoc.Value.ToString();
                    dnc.CodCliente = t.CardCode;
                    dnc.NombreCliente = t.CardName;
                    dnc.Direccion_Despacho = t.Direccion_Despacho;
                    dnc.Direccion_Facturacion = t.Direccion_Facturacion;
                    dnc.FechaDocumento = t.DocDate;
                    dnc.FechaCreacionRegistoNota = DateTime.Now;
                    dnc.TipoNC = 1;

                    DocEntr = t.DocEntry;

                    Session["IdNota"] = nc.InsertaDatosClienteNota(dnc);

                    List<VW_SMM_DATO_PRODUCTOS_NOTACREDITO> dt2 = nc.ListaDatoProductos(DocEntr);

                    foreach (var l in dt2)
                    {
                        int idcab = Convert.ToInt32(Session["IdNota"]);
                        SMM_NotaCredito_DatoCompra dc = new SMM_NotaCredito_DatoCompra();

                        dc.ID_NotaCredito = idcab;
                        dc.CodProducto = l.ItemCode;
                        dc.Producto = l.Dscription;
                        dc.Cantidad = l.Quantity;
                        dc.Precio = l.Price;
                        dc.Descuento = l.Descuento;
                        //dc.Total = l.Total;

                        res = nc.InsertaDatoCompra(dc);

                    }
                }

                //string mont = nc.TraeMontoCompra(Convert.ToInt32(Session["IdNota"]));
                //txtMonto.Text = mont;
            }

            GvDatosCliente.DataBind();
            GvDatosCompra.DataBind();
        }

        protected void btnConfirma_Click(object sender, EventArgs e)
        {
            SMMNotaCreditoClass nc = new SMMNotaCreditoClass();
            PostVentasBMasClass vPV = new PostVentasBMasClass();

            int idcab = Convert.ToInt32(Session["IdNota"]);

            string mont = nc.TraeMontoCompra(idcab);
            SMM_NotaCredito_DetallePago dc = new SMM_NotaCredito_DetallePago();

            dc.ID_NotaCredito = idcab;
            // dc.Monto =Convert.ToInt32(mont);
            dc.Fecha = DateTime.Now;
            int res = nc.InsertaDatoPago(dc);
            string ret = vPV.CreaNotaCreditoVenta(idcab, Session["CIDUsuario"].ToString());

            if (ret.Equals("0"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Documento Creado !');", true);
                txtfolio.Text = string.Empty;
                txtfolio.Focus();
                rbTipoDoc.SelectedIndex = -1;
                GvDatosCliente.DataBind();
                GvDatosCompra.DataBind();
            }
            else { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('" + ret + "');", true); }
        }

        protected void GvDatosCompra_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            GvDatosCompra.DataBind();
            SMMNotaCreditoClass vNC = new SMMNotaCreditoClass();
            vNC.ActualizaTipoNC(Convert.ToInt32(Session["IdNota"]));
        }

        protected void GvDatosCompra_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (Convert.ToDecimal(e.OldValues["Cantidad"]) < Convert.ToDecimal(e.NewValues["Cantidad"]))
            {
                e.Cancel = true;
            }
            else
            {
                SMMNotaCreditoClass vNC = new SMMNotaCreditoClass();
                vNC.ActualizaTipoNC(Convert.ToInt32(Session["IdNota"]));
            }
        }

        protected void btnAnula_Click(object sender, EventArgs e)
        {
            popAnulaNota.ShowOnPageLoad = true;
        }

        protected void btnNoAnula_Click(object sender, EventArgs e)
        {
            popAnulaNota.ShowOnPageLoad = false;
        }

        protected void btnSiAnula_Click(object sender, EventArgs e)
        {
            int idnot = Convert.ToInt32(Session["IdNota"]);

            SMMNotaCreditoClass nc = new SMMNotaCreditoClass();

            int resB = nc.EliminaDatoCliente(idnot);
            if (resB != 0)
            {
                int resC = nc.EliminaDatoCompra(idnot);

                txtfolio.Text = string.Empty;
                rbTipoDoc.SelectedIndex = -1;
                rbTipoDoc.Focus();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Aviso", "alert('Nota de Credito Anulada!');", true);
                popAnulaNota.ShowOnPageLoad = false;
                GvDatosCliente.DataBind();
                GvDatosCompra.DataBind();
            }
        }
    }
}