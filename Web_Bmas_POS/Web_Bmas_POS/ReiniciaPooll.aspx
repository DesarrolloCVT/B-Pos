<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ReiniciaPooll.aspx.cs" Inherits="Web_Bmas_POS.ReiniciaPooll" %>

<%@ Register Assembly="DevExpress.Web.v22.1, Version=22.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntMaster" runat="server">
     <br /><br />
    <div style="margin-left:50px"> 
          <dx:ASPxButton ID="btnReiniciar" runat="server" Text="Reiniciar Poolls" OnClick="btnReiniciar_Click"></dx:ASPxButton>
    </div>  
    <br />
    <div><dx:ASPxLabel ID="lblMessage" runat="server" Text="ASPxLabel"></dx:ASPxLabel></div>
    <br />
</asp:Content>
