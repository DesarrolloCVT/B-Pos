using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Web.Administration;


namespace DB_Bmas
{
    public class SAPSMM
    {
        DBML_SBO_MMetroDataContext DBSMMSap = new DBML_SBO_MMetroDataContext();
        DBML_DesaintDataContext DBDesaint = new DBML_DesaintDataContext();
        DBML_SMMWMSDataContext DBWMS = new DBML_SMMWMSDataContext();

        string SAPusername = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SAPusername"]);
        string SAPuserpass = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SAPuserpass"]);
        string SAPCompany = /*"ZBO_MMETRO";*/ Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SMMCompany"]);// "ZBO_MMETRO"; //////
        string DBServer = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["DBServer"]);
        string DBusername = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["DBusername"]);
        string DBuserpass = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["DBuserpass"]);
        string DBType = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["DBType"]);
        string ServerLicence = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ServerLicence"]);

        public SAPSMM() { }

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
                        RecycleAppPool();
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

        public string CreaEntradaAjusteDocumentoElectronico(int PreventaID, string usuario)
        {
            string ret = "-1";
            SAPbobsCOM.Company oCmp;
            string reterror = string.Empty;
            oCmp = CnxSAP(ref reterror);
            try
            {
                int iError = 0;


                SAPbobsCOM.Documents doc;
                doc = oCmp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInventoryGenEntry) as SAPbobsCOM.Documents;
                doc.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Items;
                doc.DocDate = DateTime.Now;
                doc.HandWritten = SAPbobsCOM.BoYesNoEnum.tNO;
                doc.UserFields.Fields.Item("U_IdWms").Value = PreventaID;
                doc.UserFields.Fields.Item("U_UsuarioWms").Value = usuario;
                //doc.UserFields.Fields.Item("U_TipoRecibo").Value = 0;
                //doc.PaymentGroupCode = -2;
                //doc.UserFields
                //doc.Lines.BaseType = 202;
                //doc.Lines.BaseEntry = OF;
                CVTWMSMetroClass vWMS = new CVTWMSMetroClass();
                List<SMM_Ajuste_Documento_Electronico> dt = new List<SMM_Ajuste_Documento_Electronico>();
                dt = vWMS.ObtieneDetosAjusteDocElectronico(PreventaID);
                foreach (var d in dt)
                {
                    doc.Comments = "Ajuste Preventa" + PreventaID;
                    doc.Lines.WarehouseCode = "01";
                    doc.Lines.ItemCode = d.ItemCode;
                    doc.Lines.ShipDate = DateTime.Now;
                    doc.Lines.Quantity = (double)d.Faltante;
                    doc.Lines.UnitPrice = ObtienePrecioPromedioProducto(d.ItemCode);

                    doc.Lines.Add();
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
            catch (Exception)
            {

                oCmp.Disconnect();

            }
            return ret;           
        }

        public double ObtienePrecioPromedioProducto(string CodProducto)
        {
            double ret = 0;
            try
            {
                var temp = (from pp in DBSMMSap.OITM
                            where pp.ItemCode.Equals(CodProducto)
                            select new { pp.AvgPrice }).FirstOrDefault();
                if (temp != null)
                {
                    ret = (double)temp.AvgPrice;
                }
            }
            catch (Exception)
            {


            }
            return ret;
        }


    }
}
