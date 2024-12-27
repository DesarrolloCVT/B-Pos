<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SMMAperturaCaja.aspx.cs" Inherits="Web_Bmas_POS.MayoristaProduccion.SMMAperturaCaja" %>

<%@ Register Assembly="DevExpress.Web.v22.1, Version=22.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style5 {
            width: 20px;
        }

        .auto-style6 {
            width: 97px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntMaster" runat="server">
    <div id="breadcrumbs">
        <ul>
            <li><a href="../Blank.aspx">Inicio</a></li>
            <li><a>Produccion</a></li>
            <li><span id="current">B+ APERTURA CAJA</span></li>
        </ul>
    </div>

    <div id="tableHeader">
        <span class="tableTitle">B+ APERTURA CAJA</span>
        <span class="tab-end"></span>
    </div>
    <br />
    <br />
    <div style="margin-left: 45PX">
        <table>
            <tr>
                <td>
                    <dx:aspxlabel id="ASPxLabel1" runat="server" text="Monto" theme="iOS"></dx:aspxlabel>
                    <dx:aspxtextbox id="txtMonto" runat="server" width="170px" theme="iOS"></dx:aspxtextbox>
                </td>
                <td class="auto-style5"></td>
                <td class="auto-style6">
                    <dx:aspxlabel id="ASPxLabel2" runat="server" text="Encargado Caja" theme="iOS"></dx:aspxlabel>
                    <dx:aspxcombobox id="cboEncargadoCaja" runat="server" enabletheming="True" theme="iOS" width="258px" datasourceid="LqsUsuaio" textfield="NombreUsuario" valuefield="UsuarioSistema">
                    </dx:aspxcombobox>

                </td>
                <td class="auto-style5"></td>
                <td style="padding-top: 15px">
                    <dx:aspxbutton id="btnAbrir" runat="server" text="ABRIR" height="53px" theme="iOS" width="149px" onclick="btnAbrir_Click">
                        <image iconid="scheduling_forward_svg_white_32x32">
                        </image>
                    </dx:aspxbutton>
                </td>

            </tr>
        </table>
    </div>
    <asp:LinqDataSource ID="LqsUsuaio" runat="server" ContextTypeName="DB_Bmas.DBML_DesaintDataContext" EntityTypeName="" TableName="CVT_Usuarios" Where="IdPerfil == @IdPerfil">
        <WhereParameters>
            <asp:Parameter DefaultValue="39" Name="IdPerfil" Type="Int32" />
        </WhereParameters>
    </asp:LinqDataSource>
</asp:Content>
