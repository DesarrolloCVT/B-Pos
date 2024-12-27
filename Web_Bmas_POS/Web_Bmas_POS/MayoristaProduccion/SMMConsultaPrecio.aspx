<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SMMConsultaPrecio.aspx.cs" Inherits="Web_Bmas_POS.MayoristaProduccion.SMMConsultaPrecio" %>

<%@ Register Assembly="DevExpress.Web.v22.1, Version=22.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntMaster" runat="server">

    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Codigo Producto"></dx:ASPxLabel>
    <dx:ASPxTextBox ID="txt_DUN" runat="server" Width="170px" AutoPostBack="True" OnTextChanged="txt_DUN_TextChanged"></dx:ASPxTextBox>

    <dx:ASPxGridView ID="GV_GConsulaPrecios" runat="server" AutoGenerateColumns="False">
<SettingsPopup>
<FilterControl AutoUpdatePosition="False"></FilterControl>
</SettingsPopup>
        <Columns>
            <dx:GridViewDataTextColumn Caption="CodProducto" FieldName="ItemCode" VisibleIndex="0">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Producto" FieldName="ItemName" VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Cod Barra" FieldName="BcdCode" VisibleIndex="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="U Medida" FieldName="UomCode" VisibleIndex="3">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="UomEntry" Visible="False" VisibleIndex="4">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Precio" FieldName="Price" VisibleIndex="5">
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
</asp:Content>
