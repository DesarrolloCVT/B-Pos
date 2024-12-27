using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Bmas
{

    public class CVTWMSMetroClass
    {
        public CVTWMSMetroClass(){}

        DBML_DesaintDataContext DBDesaint = new DBML_DesaintDataContext();
        DBML_SBO_MMetroDataContext DBSapSMM = new DBML_SBO_MMetroDataContext();      
        DBML_SMMWMSDataContext Mmtro = new DBML_SMMWMSDataContext();


        public List<SMM_Ajuste_Documento_Electronico> ObtieneDetosAjusteDocElectronico(int IdPrev)
        {
            List<SMM_Ajuste_Documento_Electronico> ret = new List<SMM_Ajuste_Documento_Electronico>();
            try
            {
                ret = (from r in DBSapSMM.SMM_Ajuste_Documento_Electronico
                       where r.Preventa_ID.Equals(IdPrev)
                       select r).ToList<SMM_Ajuste_Documento_Electronico>();
            }
            catch (Exception ex)
            {
                string a = ex.Message.ToString();
            }
            return ret;
        }

    }
}
