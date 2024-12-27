<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SMMArqueoCaja.aspx.cs" Inherits="Web_Bmas_POS.MayoristaProduccion.SMMArqueoCaja" %>

<%@ Register Assembly="DevExpress.Web.v22.1, Version=22.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style5 {
            width: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntMaster" runat="server">
    <div id="breadcrumbs">
        <ul>
            <li><a href="../Blank.aspx">Inicio</a></li>
            <li><a>Produccion</a></li>
            <li><span id="current">B+ ARQUEO CAJA</span></li>
        </ul>
    </div>
    <div id="tableHeader">
        <span class="tableTitle">B+ ARQUEO CAJA</span>
        <span class="tab-end"></span>
    </div>
    <br />
    <br />
    <div style="margin-left: 150px">
        <table>
            <tr>
                <td>
                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Fecha Arqueo" Theme="iOS"></dx:ASPxLabel>
                    <dx:ASPxDateEdit ID="dteFechArqueo" runat="server" Theme="iOS">
                        <ValidationSettings Display="Dynamic" ValidationGroup="a1">
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </dx:ASPxDateEdit>
                </td>
                <td></td>
                <td style="padding-top: 20px">
                    <dx:ASPxButton ID="btnBuscar" runat="server" Text="Buscar" Height="48px" Theme="iOS" Width="152px" OnClick="btnBuscar_Click" ValidationGroup="a1">
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <div style="margin-left: 25px">
        <dx:ASPxGridView ID="GvDatos" runat="server" AutoGenerateColumns="False" Theme="iOS" OnToolbarItemClick="GvDatos_ToolbarItemClick">
            <SettingsPopup>
                <FilterControl AutoUpdatePosition="False"></FilterControl>
            </SettingsPopup>
            <Settings ShowTitlePanel="True" ShowFooter="True" />
            <SettingsText Title="" />
            <Toolbars>
                <dx:GridViewToolbar Position="Top">
                    <Items>
                        <dx:GridViewToolbarItem Text="Exportar" Name="Export" Image-IconID="actions_download_16x16office2013" Alignment="Left" DropDownMode="False" BeginGroup="True" GroupName="a1" Target="_parent">
                            <Items>
                                <dx:GridViewToolbarItem Command="ExportToXls" />
                                <dx:GridViewToolbarItem Command="ExportToXlsx" />
                                <dx:GridViewToolbarItem Command="ExportToCsv" />
                                <dx:GridViewToolbarItem Command="ExportToPdf" />
                            </Items>
                            <Image IconID="actions_download_16x16office2013"></Image>
                        </dx:GridViewToolbarItem>
                        <dx:GridViewToolbarItem Alignment="Left">
                            <Template>
                                <dx:ASPxButtonEdit ID="tbToolbarSearch" runat="server" NullText="Search..." Height="100%">
                                    <Buttons>
                                        <dx:SpinButtonExtended Image-IconID="find_find_16x16gray" />
                                    </Buttons>
                                </dx:ASPxButtonEdit>
                            </Template>
                        </dx:GridViewToolbarItem>
                    </Items>
                </dx:GridViewToolbar>
            </Toolbars>
            <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="Default" />
            <SettingsBehavior AllowFocusedRow="True" />
            <SettingsSearchPanel CustomEditorID="tbToolbarSearch" />
            <Columns>
                <dx:GridViewDataTextColumn Caption="TIPO" FieldName="Tipo" VisibleIndex="0">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="EFECTIVO" VisibleIndex="3">
                    <PropertiesTextEdit DisplayFormatString="N0">
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="TARJETA" VisibleIndex="4">
                    <PropertiesTextEdit DisplayFormatString="N0">
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CHEQUE" VisibleIndex="5">
                    <PropertiesTextEdit DisplayFormatString="N0">
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="TRANSFERENCIA" VisibleIndex="6">
                    <PropertiesTextEdit DisplayFormatString="N0">
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="TOTAL" VisibleIndex="7">
                    <PropertiesTextEdit DisplayFormatString="N0">
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="APERTURA" FieldName="Apertura" VisibleIndex="2" ShowInCustomizationForm="True">
                    <PropertiesTextEdit DisplayFormatString="N0">
                    </PropertiesTextEdit>
                    <PropertiesTextEdit DisplayFormatString="N0">
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="CAJA" FieldName="Caja" ShowInCustomizationForm="True" VisibleIndex="1">
                </dx:GridViewDataTextColumn>
            </Columns>
            <TotalSummary>
                <dx:ASPxSummaryItem DisplayFormat="Total Apertura : {0:N0}" FieldName="Apertura" ShowInColumn="APERTURA" SummaryType="Sum" ValueDisplayFormat="N0" />
                <dx:ASPxSummaryItem DisplayFormat="Total Efectivo:{0:N0}" FieldName="EFECTIVO" ShowInColumn="EFECTIVO" SummaryType="Sum" ValueDisplayFormat="N0" />
                <dx:ASPxSummaryItem DisplayFormat="Total Tarjeta : {0:N0}" FieldName="TARJETA" ShowInColumn="TARJETA" SummaryType="Sum" />
                <dx:ASPxSummaryItem DisplayFormat="Total Cheque : {0:N0}" FieldName="CHEQUE" ShowInColumn="CHEQUE" SummaryType="Sum" />
                <dx:ASPxSummaryItem DisplayFormat="Total Transferencia :{0:N0}" FieldName="TRANSFERENCIA" ShowInColumn="TRANSFERENCIA" SummaryType="Sum" />
                <dx:ASPxSummaryItem DisplayFormat="Total General: {0:N0}" FieldName="TOTAL" ShowInColumn="TOTAL" SummaryType="Sum" />
            </TotalSummary>
            <Styles>
                <Header BackColor="#FF9900">
                </Header>
            </Styles>
        </dx:ASPxGridView>
    </div>
    <br />
    <br />
    <br />
    <div>
        <div style="margin-left: 100PX">
            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="SALDO REAL EN CAJA" Font-Bold="true" Font-Size="Medium"></dx:ASPxLabel>
        </div>
        <br />
        <div style="margin-left: 25px">
            <table>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="EFECTIVO" Theme="iOS"></dx:ASPxLabel>
                        <dx:ASPxTextBox ID="txtEfectivo" runat="server" Width="170px" Height="38px" Theme="iOS"></dx:ASPxTextBox>

                    </td>
                    <td class="auto-style5"></td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="TARJETA" Theme="iOS"></dx:ASPxLabel>
                        <dx:ASPxTextBox ID="txtTarjeta" runat="server" Width="170px" Height="38px" Theme="iOS"></dx:ASPxTextBox>
                    </td>
                    <td class="auto-style5"></td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="CHEQUE" Theme="iOS"></dx:ASPxLabel>
                        <dx:ASPxTextBox ID="txtCheque" runat="server" Width="170px" Height="40px" Theme="iOS"></dx:ASPxTextBox>
                    </td>
                    <td class="auto-style5"></td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="TRANSFERENCIA" Theme="iOS"></dx:ASPxLabel>
                        <dx:ASPxTextBox ID="txtTransferencia" runat="server" Width="170px" Height="38px" Theme="iOS"></dx:ASPxTextBox>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <br />
    <br />
    <div style="margin-left: 25px">
        <dx:ASPxMemo ID="txtObservacion" runat="server" Height="82px" Width="743px" Theme="iOS" Caption="Observaciones"></dx:ASPxMemo>
    </div>
    <br />
    <br />
    <div style="margin-left: 125px">
        <dx:ASPxButton ID="btnGuardarArqueo" runat="server" Text="Guardar Arqueo" Theme="iOS" Height="53px" Width="209px" BackColor="#00cc00" OnClick="btnGuardarArqueo_Click" ValidationGroup="a1">
            <Image IconID="save_save_svg_white_32x32">
            </Image>
        </dx:ASPxButton>
    </div>

</asp:Content>
