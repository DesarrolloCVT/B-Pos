using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_Bmas_POS.ControlesSMM
{
    public partial class MenuSMM : System.Web.UI.UserControl
    {
        public enum TipoMenu : int { acceso, recepcion, almacenamiento, despacho, calidad, etiquetado, menu, mantenedores, Ninguno };
        public MenuItemCollection Items { get; set; }

        public bool IsStart
        {
            get
            {
                return Convert.ToBoolean(ViewState["StartMenu"] != null ? ViewState["StartMenu"] : 0);
            }
            set
            {
                if (ViewState["StartMenu"] != null)
                    ViewState["StartMenu"] = value;
                else
                    ViewState.Add("StartMenu", value);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Text.StringBuilder Sb = new System.Text.StringBuilder();
            Sb.Append("<ul>" + Environment.NewLine);
            int ContaMen = 0;
            string MenuDesPress = "";
            HttpCookie CkMenPress;
            CkMenPress = Request.Cookies["MenuPress"];
            MenuDesPress = CkMenPress != null ? CkMenPress.Value.ToString() : string.Empty;
            if (this.Items == null) return;
            foreach (MenuItem Men in this.Items)
            {
                if (Men.Value != TipoMenu.Ninguno.ToString())
                    Sb.Append(@"<li><a rel='desplegable' href='#' class='' onclick='MenuPresionado(this)'>" + Men.Text + "</a>" + Environment.NewLine);
                else
                    Sb.Append(@"<li><a rel='desplegable' href='#' class='' onclick='MenuPresionado(this)'>" + Men.Text + "</a>" + Environment.NewLine);
                if (Men.ChildItems.Count > 0)
                {
                    if (MenuDesPress != string.Empty && MenuDesPress.Replace("%20", " ").Replace("%E1", "á").Replace("%E9", "é").Replace("%ED", "í").Replace("%F3", "ó").Replace("%FA", "ú").Replace("%F1", "ñ").Equals(Men.Text.Trim()))
                        Sb.Append(@"<ul class='inactive' >" + Environment.NewLine);
                    else
                        Sb.Append(@"<ul class='inactive' style='display:none'>" + Environment.NewLine);
                }
                foreach (MenuItem SMen in this.Items[ContaMen].ChildItems)
                {
                    Sb.Append(@"<li><a href='" + SMen.Value + "'>" + SMen.Text + "</a></li>" + Environment.NewLine);
                }
                ContaMen += 1;
                if (Men.ChildItems.Count > 0)
                    Sb.Append(@"</ul>");
                Sb.Append(@"</li>" + Environment.NewLine);
            }
            Sb.Append("</ul>" + Environment.NewLine);
            this.lblHTML.Text = Sb.ToString();
        }
    }
}