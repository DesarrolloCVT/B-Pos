using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Bmas
{
    public class PreventaCajaSMM
    {

        DBML_SMMWMSDataContext  WMSMetro = new DBML_SMMWMSDataContext();
        DBML_SBO_MMetroDataContext SapSMM = new DBML_SBO_MMetroDataContext();
        public PreventaCajaSMM() { }

        public int TraeEstadoPreventa(int IdPreventa)
        {
            int ret = 0;
            try
            {
                var temp = (from s in WMSMetro.SMM_Preventa
                            where s.Preventa_ID.Equals(IdPreventa)
                            select new { s.Estado }).FirstOrDefault();
                if (temp != null)
                {
                    ret = Convert.ToInt32(temp.Estado);
                }
            }
            catch (Exception)
            {

            }
            return ret;
        }

        public string TraeTipoVenta(int IdPreventa)
        {
            string ret = "0";
            try
            {
                var temp = (from s in WMSMetro.SMM_Preventa
                            where s.Preventa_ID.Equals(IdPreventa)
                            select new { s.TipoVenta }).FirstOrDefault();
                if (temp != null)
                {
                    ret = temp.TipoVenta;
                }
            }
            catch (Exception)
            {

            }
            return ret;
        }

        public int sumaMontosIngresados(int idPrev)
        {
            int ret = 0;
            try
            {
                var temp = (from s in WMSMetro.SMM_Preventa_DatoPago
                            where s.Preventa_ID.Equals(idPrev)
                            select s.Monto).Sum();

                ret = Convert.ToInt32(temp);

            }
            catch (Exception)
            {

            }
            return ret;
        }
        public string TraeMontoVenta(int IdPreventa)
        {
            string ret = "0";
            try
            {
                var temp = (from s in WMSMetro.VW_MONTO_VENTA_SMM
                            where s.Preventa_ID.Equals(IdPreventa)
                            select new { s.MontoVenta }).FirstOrDefault();
                if (temp != null)
                {
                    ret = temp.MontoVenta.ToString();
                }
            }
            catch (Exception)
            {

            }
            return ret;
        }

        public string TraeCodigoCliente(int IdPreventa)
        {
            string ret = "0";
            try
            {
                var temp = (from s in WMSMetro.SMM_Preventa
                            where s.Preventa_ID.Equals(IdPreventa)
                            select new { s.CodigoCliente }).FirstOrDefault();
                if (temp != null)
                {
                    ret = temp.CodigoCliente.ToString();
                }
            }
            catch (Exception)
            {

            }
            return ret;
        }

        public string TraeMontoVentaSinRedondeo(int IdPreventa)
        {
            string ret = "0";
            try
            {
                var temp = (from s in WMSMetro.VW_MONTO_VENTA_SIN_REDONDEO_SMM
                            where s.Preventa_ID.Equals(IdPreventa)
                            select new { s.MontoVenta }).FirstOrDefault();
                if (temp != null)
                {
                    ret = temp.MontoVenta.ToString();
                }
            }
            catch (Exception)
            {

            }
            return ret;
        }

        public string TraeMontoActualizado(int IdPreventa)
        {
            string ret = "0";
            try
            {
                var temp = (from s in WMSMetro.VW_Calculo_monto_venta
                            where s.Preventa_ID.Equals(IdPreventa)
                            select new { s.Monto }).FirstOrDefault();
                if (temp != null)
                {
                    ret = temp.Monto.ToString();
                }
            }
            catch (Exception)
            {

            }
            return ret;
        }

        public string TraeMontoActualizadoSinRedondeo(int IdPreventa)
        {
            string ret = "0";
            try
            {
                var temp = (from s in WMSMetro.VW_Calculo_monto_venta_sin_Redondeo
                            where s.Preventa_ID.Equals(IdPreventa)
                            select new { s.Monto }).FirstOrDefault();
                if (temp != null)
                {
                    ret = temp.Monto.ToString();
                }
            }
            catch (Exception)
            {

            }
            return ret;
        }

        public bool InsertaDatoPago(SMM_Preventa_DatoPago nuevo)
        {
            bool ret = false;
            try
            {
                WMSMetro.SMM_Preventa_DatoPago.InsertOnSubmit(nuevo);
                WMSMetro.SubmitChanges();
                ret = true;

            }
            catch
            {


            }
            return ret;

        }
        public bool InsertaLog(SMM_LogCaja nuevo)
        {
            bool ret = false;
            try
            {
                WMSMetro.SMM_LogCaja.InsertOnSubmit(nuevo);
                WMSMetro.SubmitChanges();
                ret = true;

            }
            catch
            {


            }
            return ret;

        }

        public bool InsetLogError(SMM_LogErrorPOS nuevo)
        {
            bool ret = false;
            try
            {
                WMSMetro.SMM_LogErrorPOS.InsertOnSubmit(nuevo);
                WMSMetro.SubmitChanges();
                ret = true;

            }
            catch
            {


            }
            return ret;

        }
        public int TraeMontoEliminar(int idMon)
        {
            int ret = 0;
            try
            {
                var temp = (from s in WMSMetro.SMM_Preventa_DatoPago
                            where s.ID_TipoPago.Equals(idMon)
                            select new { s.Monto }).FirstOrDefault();
                if (temp != null)
                {
                    ret = Convert.ToInt32(temp.Monto);
                }
            }
            catch (Exception)
            {

            }
            return ret;
        }

        public bool EliminaMonto(int idMonto)
        {
            bool ret = false;
            try
            {
                var t = from td in WMSMetro.SMM_Preventa_DatoPago
                        where td.ID_TipoPago.Equals(idMonto)
                        select td;
                foreach (var detail in t)
                {
                    WMSMetro.SMM_Preventa_DatoPago.DeleteOnSubmit(detail);
                }
                WMSMetro.SubmitChanges();


                ret = true;
            }
            catch (Exception)
            {


            }
            return ret;
        }

        public int EliminaDatoPago(int PreventaId)
        {
            int ret = 0;
            try
            {
                var temp = from p in WMSMetro.SMM_Preventa_DatoPago
                           where p.Preventa_ID == PreventaId
                           select p;
                foreach (var detail in temp)
                {
                    WMSMetro.SMM_Preventa_DatoPago.DeleteOnSubmit(detail);
                }
                WMSMetro.SubmitChanges();
                ret = 1;
            }
            catch
            {
                return ret;
            }
            return ret;
        }

        public int InsertaAperturaCaja(SMM_AperturaCaja nuevo)
        {
            int ret = 0;
            try
            {
                WMSMetro.SMM_AperturaCaja.InsertOnSubmit(nuevo);
                WMSMetro.SubmitChanges();
                ret = 1;

            }
            catch
            {


            }
            return ret;

        }

        public DataTable SP_ResumenArqueoCaja(DateTime FArqueo)

        {

            DataTable ret = new DataTable();
            try
            {

                SapSMM.CommandTimeout = 60000;
                //Fini = Convert.ToDateTime(Fini.ToShortDateString());
                //Fterm = Convert.ToDateTime(Fterm.ToShortDateString());
                ret = Utilidades.LINQToDataTable(SapSMM.ResumenArqueoCaja(FArqueo));
            }
            catch (Exception)
            {

            }
            return ret;
        }

        public string TraeFechaArqueo(DateTime FechArqueo)
        {
            string ret = "0";
            try
            {
                var temp = (from s in WMSMetro.SMM_Historial_Arqueo
                            where s.FechaArqueo.Equals(FechArqueo)
                            select new { s.FechaArqueo }).FirstOrDefault();
                if (temp != null)
                {
                    ret = temp.FechaArqueo.ToString();
                }
            }
            catch (Exception)
            {

            }
            return ret;
        }
        public int InsertaHistorialArqueo(SMM_Historial_Arqueo nuevo)
        {
            int ret = 0;
            try
            {
                WMSMetro.SMM_Historial_Arqueo.InsertOnSubmit(nuevo);
                WMSMetro.SubmitChanges();
                ret = 1;

            }
            catch
            {


            }
            return ret;
        }

        public int InsertaTotalArqueo(SMM_TotalArqueoReal nuevo)
        {
            int ret = 0;
            try
            {
                WMSMetro.SMM_TotalArqueoReal.InsertOnSubmit(nuevo);
                WMSMetro.SubmitChanges();
                ret = 1;

            }
            catch
            {


            }
            return ret;

        }



    }
}
