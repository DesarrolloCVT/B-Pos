using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Bmas
{
   public class PostVentasBMasClass
    {
        public PostVentasBMasClass() { }

        DBML_SBO_MMetroDataContext DBSMMSap = new DBML_SBO_MMetroDataContext();
        DBML_DesaintDataContext DBDesaint = new DBML_DesaintDataContext();
        //DBMLCVTWMSDataContext DBWMS = new DBMLCVTWMSDataContext();
        DBML_SMMWMSDataContext DBWMSBMAS = new DBML_SMMWMSDataContext();
        string SAPusername = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SAPusername"]);
        string SAPuserpass = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SAPuserpass"]);
        string SAPCompany = /*"ZBO_MMETRO";*/Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SMMCompany"]);
        string DBServer = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["DBServer"]);
        string DBusername = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["DBusername"]);
        string DBuserpass = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["DBuserpass"]);
        string DBType = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["DBType"]);
        string ServerLicence = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ServerLicence"]);
        public SAPbobsCOM.Company CnxSAP(ref string reterror)
        {
            SAPbobsCOM.Company oCompany = new SAPbobsCOM.Company();
            if (oCompany.Connected == true)
            {
                return oCompany;
            }
            else
            {               
                int _connected = 0;
                int _errcode = 0;
                string _messgcode = "";


                try
                {

                    if (DBType == "2000")
                        oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL;

                    if (DBType == "2005")
                        oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2005;

                    if (DBType == "2008")
                        oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008;

                    if (DBType == "2012")
                        oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012;

                    if (DBType == "2019")
                        oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2019;



                    oCompany.Server = DBServer;
                    oCompany.LicenseServer = ServerLicence + ":30000";
                    oCompany.DbUserName = DBusername;
                    oCompany.DbPassword = DBuserpass;
                    oCompany.CompanyDB = SAPCompany;
                    oCompany.UserName = SAPusername;
                    oCompany.Password = SAPuserpass;
                    oCompany.language = SAPbobsCOM.BoSuppLangs.ln_Spanish_La;
                    _connected = oCompany.Connect();


                    if (_connected != 0)
                    {
                        //RecycleAppPool();
                        ReiniciarPool();
                        _connected = oCompany.Connect();
                        if (_connected != 0)
                        {
                            // este si es importante, para saber por que no se conecta
                            oCompany.GetLastError(out _errcode, out _messgcode);
                            //System.Diagnostics.EventLog.WriteEntry("Sync Solicitudes de Compra", "Error en Conexion: " + _errcode.ToString() + " " + _messgcode, System.Diagnostics.EventLogEntryType.Error);
                            reterror = _errcode + " " + _messgcode;
                            // el mensaje de error hacerlo persistir en un log para auditorias y revision o emitir mensajes
                        }
                    }

                }
                catch (Exception err)
                {
                    ReiniciarPool();
                    System.Diagnostics.EventLog.WriteEntry("SISTEMA CVT", "Error en Conexion: " + _errcode.ToString() + " " + _messgcode, System.Diagnostics.EventLogEntryType.Error);
                }
                return oCompany;

            }          

        }
        public static void RecycleAppPool()
        {
            ServerManager serverManager = new ServerManager();
            ApplicationPool appPool = serverManager.ApplicationPools["bmas"];
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
            }
        }
        public string CreaDocumentoVenta(int preventaId, string NomUsuario)
        {
            SAPbobsCOM.Company oCmp;

            string ret = "-1";
          
            string reterror = string.Empty;
            oCmp = CnxSAP(ref reterror);
            try
            {
                int iError = 0;
                int grupoCliente = 0;
                List<SMM_VW_DETALLE_CLIENTE> DtEnc = new List<SMM_VW_DETALLE_CLIENTE>();
                DtEnc = ObtieneEncabezadoPreventa(preventaId);
                foreach (var enc in DtEnc)
                {
                    grupoCliente = enc.GroupCode;
                    SAPbobsCOM.Documents doc;
                    doc = (SAPbobsCOM.Documents)oCmp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInvoices);
                    doc.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Items;
                    if (enc.TipoVenta == "B")
                    {
                        doc.DocumentSubType = SAPbobsCOM.BoDocumentSubType.bod_Bill;
                        doc.Indicator = "39";
                    }
                    else
                        doc.Indicator = "33";

                    doc.DocDate = DateTime.Now;

                    doc.CardCode = enc.CodigoCliente;
                    if (enc.DireccionEnvio != null)
                        doc.ShipToCode = enc.DireccionEnvio;
                    if (enc.DireccionFact != null)
                        doc.PayToCode = enc.DireccionFact;
                    doc.Comments = "Creado en base preventa:" + preventaId.ToString();
                    doc.UserFields.Fields.Item("U_IDCaja").Value = NomUsuario;

                    //doc.NumAtCard="PV:"+ preventaId.ToString();

                    decimal? acum = 0;
                    List<SMM_VW_DETALLE_PREVENTA> dtdet = new List<SMM_VW_DETALLE_PREVENTA>();
                    dtdet = ObtieneDetallePreventa(preventaId);
                    foreach (var det in dtdet)
                    {
                        doc.Lines.WarehouseCode = "01";
                        doc.Lines.ItemCode = det.CodProducto;
                        doc.Lines.ShipDate = DateTime.Now;
                        doc.Lines.Quantity = (double)det.Cantidad;
                        doc.Lines.DiscountPercent = (double)det.Descuento;
                        doc.Lines.UoMEntry = (int)det.CodUniMedida;
                        if (enc.TipoVenta == "B")
                        {
                            //doc.Lines.UnitPrice =Math.Round((double)(det.Precio / 1.19));
                            doc.Lines.LineTotal = (double)(det.Precio * det.Cantidad);
                            doc.Lines.TaxCode = "IVA";
                            acum = acum + ((det.Precio * det.Cantidad) - ((det.Precio * det.Cantidad) * det.Descuento) / 100);

                        }
                        else
                        {
                            if (grupoCliente != 103)
                            {
                                List<SMM_ImpuestoProducto> dtImp = new List<SMM_ImpuestoProducto>();
                                dtImp = ObtieneImpuestoProducto(det.CodProducto);
                                foreach (var im in dtImp)
                                {
                                    ////doc.Lines.Price= Math.Round((double)(det.Precio / im.Rate));
                                    //doc.Lines.Price = (double)det.Precio;
                                    doc.Lines.TaxCode = im.taxcodear;
                                    doc.Lines.LineTotal = (double)(det.Precio * det.Cantidad);
                                    acum = acum + ((det.Precio * det.Cantidad) - ((det.Precio * det.Cantidad) * det.Descuento) / 100);
                                }
                            }
                            else
                            {
                                List<SMM_ImpuestoProducto> dtImp = new List<SMM_ImpuestoProducto>();
                                dtImp = ObtieneImpuestoProducto(det.CodProducto);
                                foreach (var im in dtImp)
                                {
                                    doc.Lines.UnitPrice = (double)det.Precio;/*Math.Round((double)(det.Precio / im.Rate));*/
                                    doc.Lines.TaxCode = im.taxcodear;
                                    doc.Lines.LineTotal = (double)(det.Precio * det.Cantidad);
                                    acum = acum + ((det.Precio * det.Cantidad) - ((det.Precio * det.Cantidad) * det.Descuento) / 100);
                                }
                            }
                           
                        }

                        doc.Lines.Add();
                    }
                    int TipoPago = ObtieneTipoPago(preventaId);
                    if (TipoPago == 1)
                    {
                        doc.Rounding = SAPbobsCOM.BoYesNoEnum.tYES;
                        doc.RoundingDiffAmount = ObtieneMontoEfectivo(preventaId) - (int)acum;
                    }

                    iError = doc.Add();
                    if (iError == 0)
                    {
                        //Salta Pago empresa relacionada

                        if (grupoCliente != 103)
                        {
                            int DocEntry = Convert.ToInt32(oCmp.GetNewObjectKey());
                            ret = CreaPagoRecibido(preventaId, DocEntry, NomUsuario);

                            if (ret != "0")
                            {
                                SMM_LogCaja lg = new SMM_LogCaja();
                                PreventaCajaSMM pvc = new PreventaCajaSMM();

                                lg.Preventa_ID = preventaId;
                                lg.FecharError = DateTime.Now;
                                lg.Error = ret;                               
                                pvc.InsertaLog(lg);

                                PreventaSMMClasss pr = new PreventaSMMClasss();
                                pr.ActualizaEstadoPreventa(preventaId, 2);
                            }
                        }
                        else
                        {
                            SMM_LogCaja lg = new SMM_LogCaja();
                            PreventaCajaSMM pvc = new PreventaCajaSMM();

                            lg.Preventa_ID = preventaId;
                            lg.FecharError = DateTime.Now;
                            lg.Error = "Pago Empresa Relacionada";
                            pvc.InsertaLog(lg);

                            PreventaSMMClasss pr = new PreventaSMMClasss();
                            pr.ActualizaEstadoPreventa(preventaId, 3);
                        }
                    }
                    else
                    {
                        ret = oCmp.GetLastErrorDescription();

                        SMM_LogCaja lg = new SMM_LogCaja();
                        PreventaCajaSMM pvc = new PreventaCajaSMM();

                        lg.Preventa_ID = preventaId;
                        lg.FecharError = DateTime.Now;
                        lg.Error = ret;
                        pvc.InsertaLog(lg);

                        PreventaCajaSMM sm = new PreventaCajaSMM();
                        SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                        err.Descripcion = ret;
                        err.fecha = DateTime.Now;
                        err.Modulo = "CreaDocumentoVenta - SMMCaja";
                        sm.InsetLogError(err);
                        ReiniciarPool();

                    }
                    oCmp.Disconnect();
                }
            }
            catch (Exception ex)
            {
                ret = ex.Message.ToString();               

                SMM_LogCaja lg = new SMM_LogCaja();
                PreventaCajaSMM pvc = new PreventaCajaSMM();

                lg.Preventa_ID = preventaId;
                lg.FecharError = DateTime.Now;
                lg.Error = ret;

                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "CreaDocumentoVenta - SMMCaja";
                sm.InsetLogError(err);

                pvc.InsertaLog(lg);
                oCmp.Disconnect();
                ReiniciarPool();

            }
            return ret;
        }
        public string CreaDocumentoVentaSinPago(int preventaId, string NomUsuario)
        {

            string ret = "-1";
            SAPbobsCOM.Company oCmp;
            string reterror = string.Empty;
            oCmp = CnxSAP(ref reterror);
            try
            {
                int iError = 0;
                int grupoCliente = 0;
                List<SMM_VW_DETALLE_CLIENTE> DtEnc = new List<SMM_VW_DETALLE_CLIENTE>();
                DtEnc = ObtieneEncabezadoPreventa(preventaId);
                foreach (var enc in DtEnc)
                {
                    grupoCliente = enc.GroupCode;
                    SAPbobsCOM.Documents doc;
                    doc = (SAPbobsCOM.Documents)oCmp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInvoices);
                    doc.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Items;
                    if (enc.TipoVenta == "B")
                    {
                        doc.DocumentSubType = SAPbobsCOM.BoDocumentSubType.bod_Bill;
                        doc.Indicator = "39";
                    }
                    else
                        doc.Indicator = "33";

                    doc.DocDate = DateTime.Now;

                    doc.CardCode = enc.CodigoCliente;
                    if (enc.DireccionEnvio != null)
                        doc.ShipToCode = enc.DireccionEnvio;
                    if (enc.DireccionFact != null)
                        doc.PayToCode = enc.DireccionFact;
                    doc.Comments = "Creado en base preventa:" + preventaId.ToString();
                    doc.UserFields.Fields.Item("U_IDCaja").Value = NomUsuario;

                    //doc.NumAtCard="PV:"+ preventaId.ToString();

                    decimal? acum = 0;
                    List<SMM_VW_DETALLE_PREVENTA> dtdet = new List<SMM_VW_DETALLE_PREVENTA>();
                    dtdet = ObtieneDetallePreventa(preventaId);
                    foreach (var det in dtdet)
                    {
                        doc.Lines.WarehouseCode = "01";
                        doc.Lines.ItemCode = det.CodProducto;
                        doc.Lines.ShipDate = DateTime.Now;
                        doc.Lines.Quantity = (double)det.Cantidad;
                        doc.Lines.DiscountPercent = (double)det.Descuento;
                        doc.Lines.UoMEntry = (int)det.CodUniMedida;
                        if (enc.TipoVenta == "B")
                        {
                            //doc.Lines.UnitPrice =Math.Round((double)(det.Precio / 1.19));
                            doc.Lines.LineTotal = (double)(det.Precio * det.Cantidad);
                            doc.Lines.TaxCode = "IVA";
                            acum = acum + ((det.Precio * det.Cantidad) - ((det.Precio * det.Cantidad) * det.Descuento) / 100);

                        }
                        else
                        {
                            if (grupoCliente == 103)
                            {
                                List<SMM_ImpuestoProducto> dtImp = new List<SMM_ImpuestoProducto>();
                                dtImp = ObtieneImpuestoProducto(det.CodProducto);
                                foreach (var im in dtImp)
                                {
                                    doc.Lines.UnitPrice =(double)det.Precio;/*Math.Round((double)(det.Precio / im.Rate));*/
                                    doc.Lines.TaxCode = im.taxcodear;
                                    doc.Lines.LineTotal = (double)(det.Precio * det.Cantidad);
                                    acum = acum + ((det.Precio * det.Cantidad) - ((det.Precio * det.Cantidad) * det.Descuento) / 100);
                                }
                            }
                            else
                            {
                                List<SMM_ImpuestoProducto> dtImp = new List<SMM_ImpuestoProducto>();
                                dtImp = ObtieneImpuestoProducto(det.CodProducto);
                                foreach (var im in dtImp)
                                {
                                    //doc.Lines.Price= Math.Round((double)(det.Precio / im.Rate));
                                    doc.Lines.TaxCode = im.taxcodear;
                                    doc.Lines.LineTotal = (double)(det.Precio * det.Cantidad);
                                    acum = acum + ((det.Precio * det.Cantidad) - ((det.Precio * det.Cantidad) * det.Descuento) / 100);
                                }
                            }                           
                        }

                        doc.Lines.Add();

                    }
                    int TipoPago = ObtieneTipoPago(preventaId);
                    if (TipoPago == 1)
                    {
                        doc.Rounding = SAPbobsCOM.BoYesNoEnum.tYES;
                        doc.RoundingDiffAmount = ObtieneMontoEfectivo(preventaId) - (int)acum;
                    }

                    iError = doc.Add();
                    if (iError == 0)
                    {
                        //Salta Pago empresa relacionada

                        //if (grupoCliente != 103)
                        //{
                        //    int DocEntry = Convert.ToInt32(oCmp.GetNewObjectKey());
                        //    ret = CreaPagoRecibido(preventaId, DocEntry, NomUsuario);

                        //    if (ret != "0")
                        //    {
                        //        SMM_LogCaja lg = new SMM_LogCaja();
                        //        PreventaCajaSMM pvc = new PreventaCajaSMM();

                        //        lg.Preventa_ID = preventaId;
                        //        lg.FecharError = DateTime.Now;
                        //        lg.Error = ret;
                        //        pvc.InsertaLog(lg);

                        //        PreventaSMMClasss pr = new PreventaSMMClasss();
                        //        pr.ActualizaEstadoPreventa(preventaId, 2);
                        //    }
                        //}
                        //else
                        //{
                            SMM_LogCaja lg = new SMM_LogCaja();
                            PreventaCajaSMM pvc = new PreventaCajaSMM();

                            lg.Preventa_ID = preventaId;
                            lg.FecharError = DateTime.Now;
                            lg.Error = "Crea Venta sin Pago";                       
                            pvc.InsertaLog(lg);

                            PreventaSMMClasss pr = new PreventaSMMClasss();
                            pr.ActualizaEstadoPreventa(preventaId, 3);
                        //}
                    }
                    else
                    {
                        ret = oCmp.GetLastErrorDescription();

                        SMM_LogCaja lg = new SMM_LogCaja();
                        PreventaCajaSMM pvc = new PreventaCajaSMM();

                        lg.Preventa_ID = preventaId;
                        lg.FecharError = DateTime.Now;
                        lg.Error = ret;
                        pvc.InsertaLog(lg);

                        PreventaCajaSMM sm = new PreventaCajaSMM();
                        SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                        err.Descripcion = ret;
                        err.fecha = DateTime.Now;
                        err.Modulo = "CreaDocumentoVenta - SMMCaja";
                        sm.InsetLogError(err);

                        ReiniciarPool();

                    }
                    oCmp.Disconnect();
                }
            }
            catch (Exception ex)
            {
                ret = ex.Message.ToString();
                oCmp.Disconnect();

                SMM_LogCaja lg = new SMM_LogCaja();
                PreventaCajaSMM pvc = new PreventaCajaSMM();

                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err = new SMM_LogErrorPOS();

                err.Descripcion = ex.Message;
                err.fecha = DateTime.Now;
                err.Modulo = "CreaDocumentoVentaSinPago - SMMCaja";
                sm.InsetLogError(err);

                lg.Preventa_ID = preventaId;
                lg.FecharError = DateTime.Now;
                lg.Error = ret;
                pvc.InsertaLog(lg);
                ReiniciarPool();

            }
            return ret;
        }

        public string CreaPagoRecibido(int preventaId, int docentry, string NomUsuario)
        {
            string ret = "-1";
            SAPbobsCOM.Company oCmp;
            string reterror = string.Empty;
            oCmp = CnxSAP(ref reterror);
            try
            {

                int iError = 0;
                List<SMM_VW_DETALLE_PAGO> DtPago = new List<SMM_VW_DETALLE_PAGO>();
                DtPago = ObtienePagoPreventa(preventaId);
                SAPbobsCOM.Payments doc;
                doc = (SAPbobsCOM.Payments)oCmp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oIncomingPayments);

                foreach (var pago in DtPago)
                {

                    doc.CardCode = pago.CodigoCliente;
                    doc.DocDate = DateTime.Now;
                    doc.Invoices.InvoiceType = SAPbobsCOM.BoRcptInvTypes.it_Invoice;
                    doc.Invoices.DocEntry = docentry;
                    doc.UserFields.Fields.Item("U_IDCaja").Value = NomUsuario;

                    //Efectivo
                    if (pago.TipoPago == "1")
                    {
                        doc.CashSum = (double)pago.Monto;
                        doc.CashAccount = "11.11.101";
                    }
                    //Debito
                    if (pago.TipoPago == "2")
                    {
                        doc.CreditCards.CreditCard = 1;
                        doc.CreditCards.CreditCardNumber = pago.NTarjeta;
                        doc.CreditCards.CardValidUntil = DateTime.Now;
                        doc.CreditCards.CreditSum = (double)pago.Monto;
                        doc.CreditCards.VoucherNum = pago.nBoucher.ToString();
                        doc.CreditCards.NumOfPayments = 1;
                        doc.CreditCards.Add();

                    }
                    //Credito
                    if (pago.TipoPago == "3")
                    {
                        doc.CreditCards.CreditCard = 2;
                        doc.CreditCards.CreditCardNumber = pago.NTarjeta;
                        doc.CreditCards.CardValidUntil = DateTime.Now;
                        doc.CreditCards.CreditSum = (double)pago.Monto;
                        doc.CreditCards.VoucherNum = pago.nBoucher.ToString();
                        if (pago.Cuotas == 3)
                        {
                            doc.CreditCards.NumOfPayments = 3;
                            doc.CreditCards.SplitPayments = SAPbobsCOM.BoYesNoEnum.tYES;
                        }
                        else
                        {
                            doc.CreditCards.NumOfPayments = 1;
                        }
                        doc.CreditCards.Add();
                    }
                    //Transferencia
                    if (pago.TipoPago == "4")
                    {
                        doc.TransferAccount = "11.12.102";
                        doc.TransferDate = DateTime.Now;
                        doc.TransferReference = pago.nBoucher.ToString();
                        doc.TransferSum = (double)pago.Monto;
                    }
                    //Cheque
                    if (pago.TipoPago == "5")
                    {
                        doc.CheckAccount = "11.16.108";
                        doc.Checks.CheckNumber = (int)pago.NCheque;
                        doc.Checks.CheckSum = (double)pago.Monto;
                        doc.Checks.DueDate = (DateTime)pago.FechaDocumento;
                        doc.Checks.Add();
                    }


                }
                iError = doc.Add();
                if (iError == 0)
                {
                    ret = "0";
                }
                else
                {
                    ret = oCmp.GetLastErrorDescription();

                    SMM_LogCaja lg = new SMM_LogCaja();
                    PreventaCajaSMM pvc = new PreventaCajaSMM();

                    lg.Preventa_ID = preventaId;
                    lg.FecharError = DateTime.Now;
                    lg.Error = ret + "Error al crear pago recibido Sap";
                    pvc.InsertaLog(lg);

                    PreventaCajaSMM sm = new PreventaCajaSMM();
                    SMM_LogErrorPOS err1 = new SMM_LogErrorPOS();

                    err1.Descripcion = ret;
                    err1.fecha = DateTime.Now;
                    err1.Modulo = "Error Ejecucion Pago recibido sap - SMMCaja";
                    sm.InsetLogError(err1);

                    ReiniciarPool();
                }
                oCmp.Disconnect();

            }
            catch (Exception ex)
            {
                string err = ex.Message;

                SMM_LogCaja lg = new SMM_LogCaja();
                PreventaCajaSMM pvc = new PreventaCajaSMM();

                lg.Preventa_ID = preventaId;
                lg.FecharError = DateTime.Now;
                lg.Error = ret + "Error Ejecucion Pago recibido sap : " + ex;
                pvc.InsertaLog(lg);

                PreventaCajaSMM sm = new PreventaCajaSMM();
                SMM_LogErrorPOS err1 = new SMM_LogErrorPOS();

                err1.Descripcion = ex.Message;
                err1.fecha = DateTime.Now;
                err1.Modulo = "Error Ejecucion Pago recibido sap - SMMCaja";
                sm.InsetLogError(err1);

                oCmp.Disconnect();
                ReiniciarPool();
            }
            return ret;
        }

        public string CreaNotaCreditoVenta(int NotaCreditoId, string NomUsuario)
        {

            string ret = "-1";
            SAPbobsCOM.Company oCmp;
            string reterror = string.Empty;
            oCmp = CnxSAP(ref reterror);
            try
            {
                int iError = 0;
                int grupoCliente = 0;
                List<SMM_VW_NOTACREDITO_DETALLE_CLIENTE> DtEnc = new List<SMM_VW_NOTACREDITO_DETALLE_CLIENTE>();
                DtEnc = ObtieneEncabezadoNotaCredito(NotaCreditoId);
                foreach (var enc in DtEnc)
                {
                    //grupoCliente = enc.GroupCode;
                    SAPbobsCOM.Documents doc;
                    doc = (SAPbobsCOM.Documents)oCmp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oCreditNotes);
                    doc.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Items;
                    string TipoDoc = string.Empty;
                    if (enc.TipoDocumento == "B")
                    {
                        //doc.DocumentSubType = SAPbobsCOM.BoDocumentSubType.bod_Bill;
                        doc.Indicator = "39";
                        doc.UserFields.Fields.Item("U_FE_TDBSII").Value = "39";
                        TipoDoc = "IB";
                        doc.Comments = "Creado en base BOL:" + enc.FolioDocumento.ToString();

                    }
                    else
                    {
                        doc.Indicator = "33";
                        doc.UserFields.Fields.Item("U_FE_TDBSII").Value = "33";
                        TipoDoc = "--";
                        doc.Comments = "Creado en base FAC:" + enc.FolioDocumento.ToString();

                    }
                    doc.UserFields.Fields.Item("U_FE_FECHADB").Value = enc.FechaDocumento;
                    doc.UserFields.Fields.Item("U_FE_FOLIODB").Value = enc.FolioDocumento.ToString();
                    doc.UserFields.Fields.Item("U_IDCaja").Value = NomUsuario;



                    doc.DocDate = DateTime.Now;

                    doc.CardCode = enc.CodCliente;
                    if (enc.Direccion_Despacho != null)
                        doc.ShipToCode = enc.Direccion_Despacho;
                    if (enc.Direccion_Facturacion != null)
                        doc.PayToCode = enc.Direccion_Facturacion;


                    doc.NumAtCard = "NC:" + NotaCreditoId.ToString();

                    decimal? acum = 0;
                    decimal? acumImp = 0;
                    List<SMM_VW_NOTACREDITO_DETALLE_COMPRA> dtdet = new List<SMM_VW_NOTACREDITO_DETALLE_COMPRA>();
                    dtdet = ObtieneDetalleNotaCredito(NotaCreditoId);
                    foreach (var det in dtdet)
                    {
                        doc.Lines.WarehouseCode = "01";
                        doc.Lines.ItemCode = det.CodProducto;
                        doc.Lines.ShipDate = DateTime.Now;
                        doc.Lines.Quantity = (double)det.Cantidad;
                        doc.Lines.DiscountPercent = (double)det.Descuento;

                        if (enc.TipoDocumento == "B")
                        {
                            //doc.Lines.UnitPrice =Math.Round((double)(det.Precio / 1.19));
                            doc.Lines.LineTotal = (double)(det.Total);
                            doc.Lines.TaxCode = "IVA";
                            acum = acum + ((det.Precio * det.Cantidad) - (((det.Precio * det.Cantidad) * det.Descuento) / 100));
                            acumImp = acumImp + ((det.Precio * det.Cantidad) - (((det.Precio * det.Cantidad) * det.Descuento) / 100) * (decimal)1.19);
                        }
                        else
                        {
                            List<SMM_ImpuestoProducto> dtImp = new List<SMM_ImpuestoProducto>();
                            dtImp = ObtieneImpuestoProducto(det.CodProducto);
                            foreach (var im in dtImp)
                            {
                                //doc.Lines.Price= Math.Round((double)(det.Precio / im.Rate));
                                doc.Lines.TaxCode = im.taxcodear;
                                doc.Lines.LineTotal = (double)(det.Total);
                                acum = acum + ((det.Precio * det.Cantidad) - (((det.Precio * det.Cantidad) * det.Descuento) / 100));
                                acumImp = acumImp + ((det.Precio * det.Cantidad) - (((det.Precio * det.Cantidad) * det.Descuento) / 100) * im.Rate);
                            }
                        }

                        doc.Lines.Add();

                    }
                    //decimal porc = (ObtieneMontoDocumentoOrigen((int)enc.FolioDocumento, TipoDoc) * 100) / Convert.ToInt32(acumImp);
                    //decimal diff = 100 - porc;
                    //if (diff < 0)
                    //    diff = diff * -1;
                    //if (diff < 1)
                    //{
                    //    doc.UserFields.Fields.Item("U_FE_CODREFDB").Value = "1";

                    //}
                    //else
                    //{
                    //    doc.UserFields.Fields.Item("U_FE_CODREFDB").Value = "3";
                    //}
                    if (enc.TipoNC == 1)
                    {
                        doc.UserFields.Fields.Item("U_FE_CODREFDB").Value = "1";
                        doc.Rounding = SAPbobsCOM.BoYesNoEnum.tYES;
                        doc.RoundingDiffAmount = LeyRedondeo(ObtieneMontoDocumentoOrigen((int)enc.FolioDocumento, TipoDoc)) - ObtieneMontoDocumentoOrigen((int)enc.FolioDocumento, TipoDoc);

                    }
                    else
                    {
                        doc.UserFields.Fields.Item("U_FE_CODREFDB").Value = "3";
                        doc.Rounding = SAPbobsCOM.BoYesNoEnum.tYES;
                        doc.RoundingDiffAmount = Convert.ToInt32(acumImp) - LeyRedondeo(Convert.ToInt32(acumImp));

                    }
                    //if (Convert.ToInt32(acumImp) != LeyRedondeo(Convert.ToInt32(acumImp)))
                    //{
                    //    doc.Rounding = SAPbobsCOM.BoYesNoEnum.tYES;
                    //    doc.RoundingDiffAmount = LeyRedondeo(ObtieneMontoDocumentoOrigen((int)enc.FolioDocumento, TipoDoc)) - ObtieneMontoDocumentoOrigen((int)enc.FolioDocumento, TipoDoc);
                    //}




                    iError = doc.Add();
                    if (iError == 0)
                    {


                        //Salta Pago empresa relacionada
                        if (grupoCliente != 103)
                        {
                            int DocEntry = Convert.ToInt32(oCmp.GetNewObjectKey());
                            ret = CreaPagoEfectuado(NotaCreditoId, DocEntry, NomUsuario);
                        }

                    }
                    else
                    {
                        ret = oCmp.GetLastErrorDescription();
                    }
                    oCmp.Disconnect();
                }
            }
            catch (Exception ex)
            {
                ret = ex.Message.ToString();
                oCmp.Disconnect();
                ReiniciarPool();

            }
            return ret;
        }
        public string CreaPagoEfectuado(int NotaCreditoID, int docentry, string NomUsuario)
        {
            string ret = "-1";
            SAPbobsCOM.Company oCmp;
            string reterror = string.Empty;
            oCmp = CnxSAP(ref reterror);
            try
            {

                int iError = 0;
                List<SMM_VW_NOTACREDITO_DETALLE_PAGO> DtPago = new List<SMM_VW_NOTACREDITO_DETALLE_PAGO>();
                DtPago = ObtienePagoNotaCredito(NotaCreditoID);
                SAPbobsCOM.Payments doc;
                doc = (SAPbobsCOM.Payments)oCmp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oVendorPayments);
                doc.DocObjectCode = SAPbobsCOM.BoPaymentsObjectType.bopot_OutgoingPayments;
                doc.DocType = SAPbobsCOM.BoRcptTypes.rCustomer;
                doc.UserFields.Fields.Item("U_IDCaja").Value = NomUsuario;
                foreach (var pago in DtPago)
                {

                    doc.CardCode = pago.CodCliente;
                    doc.DocDate = DateTime.Now;
                    doc.Invoices.InvoiceType = SAPbobsCOM.BoRcptInvTypes.it_CredItnote;
                    doc.Invoices.DocEntry = docentry;

                    doc.CashSum = (double)ObtieneMontoNCPrueba(docentry);
                    doc.CashAccount = "11.11.101";

                }
                iError = doc.Add();
                if (iError == 0)
                {
                    ret = "0";
                }
                else
                {
                    ret = oCmp.GetLastErrorDescription();
                }
                oCmp.Disconnect();



            }
            catch
            {
                oCmp.Disconnect();
                ReiniciarPool();
            }

            return ret;
        }
        public string CreaSocioNegocio(string codcliente)
        {
            string ret = "-1";
            SAPbobsCOM.Company oCmp;
            string reterror = string.Empty;
            oCmp = CnxSAP(ref reterror);
            try
            {
                int iError = 0;
                SAPbobsCOM.BusinessPartners oBP;
                oBP = (SAPbobsCOM.BusinessPartners)oCmp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
                List<SMM_Clientes> dt = ObtieneDatosCliente(codcliente);
                foreach (var a in dt)
                {
                    oBP.CardCode = a.Codigo;
                    oBP.CardName = a.Nombre;
                    oBP.GroupCode = (int)a.GrupoCliente;
                    oBP.CardType = SAPbobsCOM.BoCardTypes.cCustomer;
                    oBP.FederalTaxID = a.Rut;
                    oBP.Phone1 = a.Telefono;
                    oBP.EmailAddress = a.Email;

                    //oBP.UserFields.Fields.Item("U_GIRO").Value = a.Giro.ToString();
                    List<SMM_ObtieneDatosDirecciones> dtd = ObtieneDatosDirecciones(codcliente);
                    foreach (var b in dtd)
                    {
                        oBP.Addresses.AddressName = b.CodDireccion.ToString();
                        oBP.Addresses.Street = b.Direccion;
                        oBP.Addresses.Block = b.Comuna;
                        oBP.Addresses.County = b.Comuna;
                        oBP.Addresses.City = b.Ciudad;
                        oBP.Addresses.State = b.Region.ToString();
                        oBP.Addresses.Country = "CL";
                        oBP.Addresses.AddressType = b.TipoDir == "B" ? SAPbobsCOM.BoAddressType.bo_BillTo : SAPbobsCOM.BoAddressType.bo_ShipTo;
                        oBP.Addresses.Add();
                        //oBP.ContactEmployees.
                    }

                    iError = oBP.Add();
                    if (iError == 0)
                    {
                        ret = "0";
                        oBP.GetByKey(codcliente);
                        oBP.UserFields.Fields.Item("U_GIRO").Value = a.Giro.ToString();
                        oBP.Update();
                    }
                    else
                    {
                        ret = oCmp.GetLastErrorDescription();
                    }
                    oCmp.Disconnect();
                }
            }
            catch (Exception ex)
            {
                ret = ex.Message.ToString();
                oCmp.Disconnect();
                ReiniciarPool();
            }
            return ret;
        }

        public string CreaSocioNegocioMayorista(string codcliente)
        {
            string ret = "-1";
            SAPbobsCOM.Company oCmp;
            string reterror = string.Empty;
            oCmp = CnxSAP(ref reterror);
            try
            {
                int iError = 0;
                SAPbobsCOM.BusinessPartners oBP;
                oBP = (SAPbobsCOM.BusinessPartners)oCmp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
                List<SMM_Clientes> dt = ObtieneDatosCliente(codcliente);
                foreach (var a in dt)
                {
                    oBP.CardCode = a.Codigo;
                    oBP.CardName = a.Nombre;
                    oBP.GroupCode = (int)a.GrupoCliente;
                    oBP.CardType = SAPbobsCOM.BoCardTypes.cCustomer;
                    oBP.Cellular = a.Telefono;
                    oBP.FederalTaxID = a.Rut;
                    oBP.EmailAddress = a.Email;
                    oBP.PayTermsGrpCode = Convert.ToInt32(a.IDCondicionPago);
                    oBP.DiscountPercent = Convert.ToDouble(a.Descuento);
                    oBP.CardForeignName = a.NombreFantasia;
                    oBP.Password = a.Giro;
                    oBP.SalesPersonCode = Convert.ToInt32(a.IDVendAsignado);
                    oBP.CreditLimit = Convert.ToInt32(a.LineaCredito);
                    oBP.Industry = Convert.ToInt32(a.IDTipoNegocio);
                    oBP.IBAN = a.Nominacion;
                    //oBP.UserFields.Fields.Item("U_GIRO").Value = a.Giro.ToString();
                    List<SMM_ObtieneDatosDirecciones> dtd = ObtieneDatosDirecciones(codcliente);
                    foreach (var b in dtd)
                    {
                        oBP.Addresses.AddressName = b.CodDireccion.ToString();
                        oBP.Addresses.Street = b.Direccion;
                        oBP.Addresses.Block = b.Comuna;
                        oBP.Addresses.County = b.Comuna;
                        oBP.Addresses.City = b.Ciudad;
                        oBP.Addresses.State = b.Region.ToString();
                        oBP.Addresses.Country = "CL";
                        oBP.Addresses.AddressType = b.TipoDir == "B" ? SAPbobsCOM.BoAddressType.bo_BillTo : SAPbobsCOM.BoAddressType.bo_ShipTo;
                        oBP.Addresses.Add();
                        //oBP.ContactEmployees.
                    }

                    List<SMM_Clientes_Contacto> cc = ObtieneDatosContacto(codcliente);
                    foreach (var c in cc)
                    {
                        //oBP.Addresses.AddressName = b.CodDireccion.ToString();
                        //oBP.Addresses.Street = b.Direccion;
                        //oBP.Addresses.Block = b.Comuna;
                        //oBP.Addresses.County = b.Comuna;
                        //oBP.Addresses.City = b.Ciudad;
                        //oBP.Addresses.State = b.Region.ToString();
                        //oBP.Addresses.Country = "CL";
                        //oBP.Addresses.AddressType = b.TipoDir == "B" ? SAPbobsCOM.BoAddressType.bo_BillTo : SAPbobsCOM.BoAddressType.bo_ShipTo;

                        oBP.ContactEmployees.Name = c.Cargo;
                        oBP.ContactEmployees.FirstName = c.Nombre;
                        oBP.ContactEmployees.LastName = c.Apellido;
                        oBP.ContactEmployees.Title = c.Cargo;
                        oBP.ContactEmployees.Name = c.Cargo;
                        oBP.ContactEmployees.Phone1 = c.Telefono1.ToString();
                        oBP.ContactEmployees.Phone2 = c.Telefono2.ToString();
                        oBP.ContactEmployees.E_Mail = c.EMail;

                        oBP.ContactEmployees.Add();
                    }

                    iError = oBP.Add();
                    if (iError == 0)
                    {
                        ret = "0";
                        oBP.GetByKey(codcliente);
                        oBP.UserFields.Fields.Item("U_GIRO").Value = a.Giro.ToString();
                        oBP.Update();
                    }
                    else
                    {
                        ret = oCmp.GetLastErrorDescription();
                    }
                    oCmp.Disconnect();
                }
            }
            catch (Exception ex)
            {
                ret = ex.Message.ToString();
                oCmp.Disconnect();
                ReiniciarPool();
            }
            return ret;
            
        }

        public List<SMM_Clientes_Contacto> ObtieneDatosContacto(string CodClienteId)
        {
            List<SMM_Clientes_Contacto> ret = new List<SMM_Clientes_Contacto>();
            try
            {
                ret = (from t in DBWMSBMAS.SMM_Clientes_Contacto
                       where t.CodCliente.Equals(CodClienteId)
                       select t).ToList<SMM_Clientes_Contacto>();
            }
            catch
            {

            }
            return ret;
        }
        public List<SMM_ObtieneDatosDirecciones> ObtieneDatosDirecciones(string CodClienteId)
        {
            List<SMM_ObtieneDatosDirecciones> ret = new List<SMM_ObtieneDatosDirecciones>();
            try
            {
                ret = (from t in DBWMSBMAS.SMM_ObtieneDatosDirecciones
                       where t.CodCliente.Equals(CodClienteId)
                       select t).ToList<SMM_ObtieneDatosDirecciones>();
            }
            catch
            {

            }
            return ret;
        }
        public List<SMM_Clientes> ObtieneDatosCliente(string CodClienteId)
        {
            List<SMM_Clientes> ret = new List<SMM_Clientes>();
            try
            {
                ret = (from t in DBWMSBMAS.SMM_Clientes
                       where t.Codigo.Equals(CodClienteId)
                       select t).ToList<SMM_Clientes>();
            }
            catch
            {

            }
            return ret;
        }

        public int ObtieneMontoNCPrueba(int docentry)
        {
            int ret = 0;
            var temp = (from vm in DBWMSBMAS.SMM_VW_MontoNotaCreditoPrueba
                        where vm.DocEntry.Equals(docentry)
                        select new { vm.DocTotal }).FirstOrDefault();
            if (temp != null)
            {
                ret = Convert.ToInt32(temp.DocTotal);
            }
            return ret;
        }

        public List<SMM_VW_NOTACREDITO_DETALLE_PAGO> ObtienePagoNotaCredito(int NotaCreditoID)
        {
            List<SMM_VW_NOTACREDITO_DETALLE_PAGO> ret = new List<SMM_VW_NOTACREDITO_DETALLE_PAGO>();
            ret = (from t in DBWMSBMAS.SMM_VW_NOTACREDITO_DETALLE_PAGO
                   where t.ID_NotaCredito.Equals(NotaCreditoID)
                   select t).ToList<SMM_VW_NOTACREDITO_DETALLE_PAGO>();
            return ret;
        }

        public int ObtieneMontoDocumentoOrigen(int folio, string tipo)
        {
            int ret = 0;
            var temp = (from vm in DBWMSBMAS.SMM_VW_TOTALDOCUMENTOORIGEN
                        where vm.DocSubType.Equals(tipo) && vm.FolioNum.Equals(folio)
                        select new { vm.DocTotal }).FirstOrDefault();
            if (temp != null)
            {
                ret = Convert.ToInt32(temp.DocTotal);
            }
            return ret;
        }
        public int LeyRedondeo(int Monto)
        {
            DataTable dt = new DataTable();
            int ret = 0;
            try
            {
                dt = Utilidades.LINQToDataTable(DBWMSBMAS.SP_LeyRedondeo(Monto));
                foreach (DataRow dr in dt.Rows)
                {
                    ret = Convert.ToInt32(dr["Redondeo"]);
                }
            }
            catch
            {

            }
            return ret;
        }
        public List<SMM_VW_NOTACREDITO_DETALLE_COMPRA> ObtieneDetalleNotaCredito(int NotaCreditoID)
        {
            List<SMM_VW_NOTACREDITO_DETALLE_COMPRA> ret = new List<SMM_VW_NOTACREDITO_DETALLE_COMPRA>();
            ret = (from t in DBWMSBMAS.SMM_VW_NOTACREDITO_DETALLE_COMPRA
                   where t.ID_NotaCredito.Equals(NotaCreditoID)
                   select t).ToList<SMM_VW_NOTACREDITO_DETALLE_COMPRA>();
            return ret;
        }

        public List<SMM_VW_NOTACREDITO_DETALLE_CLIENTE> ObtieneEncabezadoNotaCredito(int NotaCreditoID)
        {
            List<SMM_VW_NOTACREDITO_DETALLE_CLIENTE> ret = new List<SMM_VW_NOTACREDITO_DETALLE_CLIENTE>();
            ret = (from t in DBWMSBMAS.SMM_VW_NOTACREDITO_DETALLE_CLIENTE
                   where t.ID_NotaCredito.Equals(NotaCreditoID)
                   select t).ToList<SMM_VW_NOTACREDITO_DETALLE_CLIENTE>();
            return ret;
        }
        public int ObtieneMontoEfectivo(int preventaId)
        {
            int ret = 0;
            try
            {
                var temp = (from tp in DBWMSBMAS.SMM_VW_MontoEfectivo
                            where tp.preventa_id.Equals(preventaId)
                            select new { tp.Monto }).FirstOrDefault();
                if (temp != null)
                {
                    ret = (int)temp.Monto;
                }
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }
        public List<SMM_VW_DETALLE_CLIENTE> ObtieneEncabezadoPreventa(int PreventaId)
        {
            List<SMM_VW_DETALLE_CLIENTE> ret = new List<SMM_VW_DETALLE_CLIENTE>();
            try
            {
             
                ret = (from t in DBWMSBMAS.SMM_VW_DETALLE_CLIENTE
                       where t.IdPreventa.Equals(PreventaId)
                       select t).ToList<SMM_VW_DETALLE_CLIENTE>();
                return ret;
            }
            catch (Exception ex)
            {

            }
            return ret;
        }

        public List<SMM_VW_DETALLE_PREVENTA> ObtieneDetallePreventa(int PreventaId)
        {
            List<SMM_VW_DETALLE_PREVENTA> ret = new List<SMM_VW_DETALLE_PREVENTA>();
            try
            {
              
                ret = (from t in DBWMSBMAS.SMM_VW_DETALLE_PREVENTA
                       where t.Preventa_ID.Equals(PreventaId)
                       select t).ToList<SMM_VW_DETALLE_PREVENTA>();
                return ret;
            }
            catch (Exception ex)
            {

               
            }
            return ret;
        }

        public List<SMM_VW_DETALLE_PAGO> ObtienePagoPreventa(int PreventaId)
        {
            List<SMM_VW_DETALLE_PAGO> ret = new List<SMM_VW_DETALLE_PAGO>();
            ret = (from t in DBWMSBMAS.SMM_VW_DETALLE_PAGO
                   where t.Preventa_ID.Equals(PreventaId)
                   select t).ToList<SMM_VW_DETALLE_PAGO>();
            return ret;
        }
        public List<SMM_ImpuestoProducto> ObtieneImpuestoProducto(string CodProducto)
        {
            List<SMM_ImpuestoProducto> ret = new List<SMM_ImpuestoProducto>();
            ret = (from p in DBSMMSap.SMM_ImpuestoProducto
                   where p.itemcode.Equals(CodProducto)
                   select p).ToList<SMM_ImpuestoProducto>();
            return ret;
        }

        public int ObtieneTipoPago(int preventaId)
        {
            int ret = 0;
            try
            {
                var temp = (from tp in DBWMSBMAS.SMM_VW_TipoPago
                            where tp.preventa_id.Equals(preventaId)
                            select new { tp.TipoPago }).FirstOrDefault();
                if (temp != null)
                {
                    ret = (int)temp.TipoPago;
                }
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }


        protected void ReiniciarPool()
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
                       // lblMessage.Text = "Application Pool " + appPoolName + " ha sido reciclado.";
                    }
                    else
                    {
                        //lblMessage.Text = "No se pudo encontrar el Application Pool " + appPoolName + ".";
                    }
                }
            }
            catch (Exception ex)
            {
             string err = "Error al reciclar Application Pool: " + ex.Message;
            }
        }
    }
}
