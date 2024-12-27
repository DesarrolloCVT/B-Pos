<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SMMFlujoCaja.aspx.cs" Inherits="Web_Bmas_POS.MayoristaProduccion.SMMFlujoCaja" %>

<%@ Register Assembly="DevExpress.Web.v22.1, Version=22.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntMaster" runat="server">
        <div id="breadcrumbs">
        <ul>
            <li><a href="../Blank.aspx">Inicio</a></li>
            <li><a>Produccion</a></li>
            <li><span id="current">FLUJO CAJA</span></li>
        </ul>
    </div>

    <div id="tableHeader">
        <span class="tableTitle">FLUJO CAJA</span>
        <span class="tab-end"></span>
    </div>
    <br />
    <br />
    <div style="margin-left: 100px">
        <table>
            <tr>
                <td>
                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Fecha" Theme="iOS"></dx:ASPxLabel>
                    <dx:ASPxDateEdit ID="dteInicio" runat="server" Theme="iOS"></dx:ASPxDateEdit>
                </td>
                <td class="auto-style5"></td>       
                <td style="padding-top: 10px">
                    <dx:ASPxButton ID="btnBuscar" runat="server" Text="Buscar" Height="49px" Theme="iOS" Width="166px" OnClick="btnBuscar_Click">
                        <Image IconID="iconbuilder_actions_zoom_svg_white_32x32">
                        </Image>
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <br />
</asp:Content>
