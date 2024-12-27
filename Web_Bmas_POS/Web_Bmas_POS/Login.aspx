<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Web_Bmas_POS.Login" %>

<%@ Register Assembly="DevExpress.Web.v22.1, Version=22.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    function capLock(e) {
        kc = e.keyCode ? e.keyCode : e.which;
        sk = e.shiftKey ? e.shiftKey : ((kc == 16) ? true : false);

        if (((kc >= 65 && kc <= 90) && !sk) || ((kc >= 97 && kc <= 122) && sk))
            document.getElementById('caplock').style.visibility = 'visible';
        else document.getElementById('caplock').style.visibility = 'hidden';
    }
</script>



<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <title>B+ POS </title>
    <link rel="shortcut icon" href="Images/favicon.ico" />
    <link rel="icon" href="Images/favicon.ico" />
</head>
<body id="login" style="background-color: White;">

    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="200px">
        <SettingsCollapsing ExpandOnPageLoad="True">
        </SettingsCollapsing>
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <div id="siteWrapper">
                    <img id="logo-grande" src="Images/LOGOTIPO B MAS_png.png" width="400px" height="200px" />
                    <div id="loginBox">
                        <form id="frmMain" runat="server">

                            <asp:Label ID="Label2" runat="server" Text="Usuario"></asp:Label>
                            <asp:TextBox ID="txtUser" runat="server" MaxLength="20" Width="244px"></asp:TextBox>
                            <asp:Label ID="Label3" runat="server" Text="Clave"></asp:Label>
                            <asp:TextBox ID="txtPass" runat="server" TextMode="Password" MaxLength="25" Width="244px"></asp:TextBox>
                            <asp:Button ID="btnAceptar" runat="server"
                                OnClick="btnAceptar_Click" Text="Ingresar" />

                        </form>
                    </div>
                </div>
                <div id="footer">
                    <span><strong>CV Trading  </strong>/  Camino Valdivia de Paine S/N Km. 3 Buin  /  Santiago, Chile  /  Teléfono 562 783 4000</span>
                    <div id="datosContacto">
                        <a href="mailto:info@cvtrading.cl">info@cvtrading.cl</a> / <a href="http://www.cvtrading.cl" target="_blank"><strong>www.CvTraiding.cl</strong></a>
                    </div>
                </div>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>
</body>
</html>
