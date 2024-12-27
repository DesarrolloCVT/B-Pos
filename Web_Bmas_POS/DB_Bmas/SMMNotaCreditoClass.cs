using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Bmas
{
    public class SMMNotaCreditoClass
    {
        DBML_SMMWMSDataContext Mmtro = new DBML_SMMWMSDataContext();
        public SMMNotaCreditoClass() { }

        public List<VW_SMM_DATO_CLIENTES_NOTACREDITO> ListaDatoCliente(int FolioNota, string tipoDocumento)
        {
            List<VW_SMM_DATO_CLIENTES_NOTACREDITO> ret = new List<VW_SMM_DATO_CLIENTES_NOTACREDITO>();
            try
            {
                ret = (from td in Mmtro.VW_SMM_DATO_CLIENTES_NOTACREDITO
                       where td.FolioNum.Equals(FolioNota) && td.DocSubType.Equals(tipoDocumento)
                       select td).ToList<VW_SMM_DATO_CLIENTES_NOTACREDITO>();
            }
            catch (Exception)
            {


            }
            return ret;
        }

        public int InsertaDatosClienteNota(SMM_NotaCredito_DatoCliente nuevo)
        {
            int ret = 0;
            try
            {
                Mmtro.SMM_NotaCredito_DatoCliente.InsertOnSubmit(nuevo);
                Mmtro.SubmitChanges();
                ret = nuevo.ID_NotaCredito;
            }
            catch
            {
            }
            return ret;
        }
        public List<VW_SMM_DATO_PRODUCTOS_NOTACREDITO> ListaDatoProductos(int idVenta)
        {
            List<VW_SMM_DATO_PRODUCTOS_NOTACREDITO> ret = new List<VW_SMM_DATO_PRODUCTOS_NOTACREDITO>();
            try
            {
                ret = (from td in Mmtro.VW_SMM_DATO_PRODUCTOS_NOTACREDITO
                       where td.DocEntry.Equals(idVenta)
                       select td).ToList<VW_SMM_DATO_PRODUCTOS_NOTACREDITO>();
            }
            catch (Exception)
            {


            }
            return ret;
        }

        public int InsertaDatoCompra(SMM_NotaCredito_DatoCompra nuevo)
        {
            int ret = 0;
            try
            {
                Mmtro.SMM_NotaCredito_DatoCompra.InsertOnSubmit(nuevo);
                Mmtro.SubmitChanges();
                ret = nuevo.ID_NotaCredito;
            }
            catch
            {
            }
            return ret;
        }

        public string TraeMontoCompra(int IdNotaCre)
        {
            string ret = "0";
            try
            {
                var temp = (from s in Mmtro.VW_MONTO_NOTACREDITO
                            where s.ID_NotaCredito.Equals(IdNotaCre)
                            select new { s.MontoCompra }).FirstOrDefault();
                if (temp != null)
                {
                    ret = temp.MontoCompra.ToString();
                }
            }
            catch (Exception)
            {

            }
            return ret;
        }
        public int InsertaDatoPago(SMM_NotaCredito_DetallePago nuevo)
        {
            int ret = 0;
            try
            {
                Mmtro.SMM_NotaCredito_DetallePago.InsertOnSubmit(nuevo);
                Mmtro.SubmitChanges();
                ret = Convert.ToInt32(nuevo.ID_Detalle_PagoNota);
            }
            catch
            {
            }
            return ret;
        }
        public bool ActualizaTipoNC(int NCID)
        {
            bool flag = false;
            try
            {
                IQueryable<SMM_NotaCredito_DatoCliente> queryable = from t in this.Mmtro.SMM_NotaCredito_DatoCliente
                                                                    where t.ID_NotaCredito.Equals(NCID)
                                                                    select t;
                foreach (SMM_NotaCredito_DatoCliente td in queryable)
                {

                    td.TipoNC = 3;


                }
                this.Mmtro.SubmitChanges();
            }
            catch
            {
                flag = false;
            }
            return flag;
        }
        public int EliminaDatoCliente(int NotaID)
        {
            int ret = 0;
            try
            {
                var temp = from p in Mmtro.SMM_NotaCredito_DatoCliente
                           where p.ID_NotaCredito.Equals(NotaID)
                           select p;
                foreach (var detail in temp)
                {
                    Mmtro.SMM_NotaCredito_DatoCliente.DeleteOnSubmit(detail);
                }
                Mmtro.SubmitChanges();
                ret = 1;
            }
            catch
            {
                return ret;
            }
            return ret;
        }
        public int EliminaDatoCompra(int NotaID)
        {
            int ret = 0;
            try
            {
                var temp = from p in Mmtro.SMM_NotaCredito_DatoCompra
                           where p.ID_NotaCredito.Equals(NotaID)
                           select p;
                foreach (var detail in temp)
                {
                    Mmtro.SMM_NotaCredito_DatoCompra.DeleteOnSubmit(detail);
                }
                Mmtro.SubmitChanges();
                ret = 1;
            }
            catch
            {
                return ret;
            }
            return ret;
        }
    }
}
