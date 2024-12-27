using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Bmas
{
    public class UsuarioClass
    {

        DBML_DesaintDataContext DBDatos = new DBML_DesaintDataContext();
        public UsuarioClass() { }

        public int VerificaUsuarioClaveEncriptada(string nomusu, string contrasena)
        {
            int ret = 0;
            try
            {
                var temp = (from u in DBDatos.CVT_Usuarios
                            where u.UsuarioSistema.Equals(nomusu) &&
                                  u.ClaveEncriptada.Equals(contrasena)
                            select new { u.IdUsuario }).First();
                if (temp != null)
                {
                    ret = temp.IdUsuario;
                }
            }
            catch { }
            return ret;
        }

        public List<CVT_Usuarios> ObtieneDatosUsuarioPorID(int CID)
        {
            List<CVT_Usuarios> ret = new List<CVT_Usuarios>();
            try
            {
                ret = (from u in DBDatos.CVT_Usuarios
                       where u.IdUsuario.Equals(CID)
                       select u).ToList<CVT_Usuarios>();
            }
            catch { }
            return ret;
        }

        public int TraeIdUsuarioVerificador(string nomusu, string contrasena)
        {
            int ret = 0;
            try
            {
                var temp = (from u in DBDatos.CVT_Usuarios
                            where u.UsuarioSistema.Equals(nomusu) &&
                                  u.ClaveEncriptada.Equals(contrasena) && u.Verificador.Equals(1)
                            select new { u.IdUsuario }).First();
                if (temp != null)
                {
                    ret = temp.IdUsuario;
                }
            }
            catch { }
            return ret;
        }

        public string idUsuarioVerificador(int idusu)
        {
            string ret = "0";
            try
            {
                var temp = (from u in DBDatos.CVT_Usuarios
                            where u.IdUsuario.Equals(idusu)
                            select new { u.Verificador }).First();
                if (temp != null)
                {
                    ret = Convert.ToString(temp.Verificador);
                }
            }
            catch { }
            return ret;
        }
    }
}
