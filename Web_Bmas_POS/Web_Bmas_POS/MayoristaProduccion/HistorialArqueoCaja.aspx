<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="HistorialArqueoCaja.aspx.cs" Inherits="Web_Bmas_POS.MayoristaProduccion.HistorialArqueoCaja" %>

<%@ Register Assembly="DevExpress.Web.v22.1, Version=22.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style5 {
            width: 20px;
        }

        .auto-style6 {
            width: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntMaster" runat="server">
    <div id="breadcrumbs">
        <ul>
            <li><a href="../Blank.aspx">Inicio</a></li>
            <li><a>Produccion</a></li>
            <li><span id="current">HISTORIAL ARQUEO CAJA</span></li>
        </ul>
    </div>
    <div id="tableHeader">
        <span class="tableTitle">HISTORIAL ARQUEO CAJA</span>
        <span class="tab-end"></span>
    </div>
    <br />
    <br />
     <div style="margin-left: 100px">
        <table>
            <tr>
                <td>
                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Fecha inicio" Theme="iOS"></dx:ASPxLabel>
                    <dx:ASPxDateEdit ID="dteInicio" runat="server" Theme="iOS"></dx:ASPxDateEdit>
                </td>
                <td class="auto-style5"></td>
                <td>
                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Fecha Termino" Theme="iOS"></dx:ASPxLabel>
                    <dx:ASPxDateEdit ID="dteTermino" runat="server" Theme="iOS"></dx:ASPxDateEdit>
                </td>
                <td class="auto-style6"></td>
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
    <div>
        <dx:ASPxGridView ID="GvDatos" runat="server" AutoGenerateColumns="False" DataSourceID="LqsHistorial" EnableTheming="True" KeyFieldName="IdArqueo" Theme="iOS" OnToolbarItemClick="GvDatos_ToolbarItemClick">
            <SettingsPopup>
                <FilterControl AutoUpdatePosition="False"></FilterControl>
            </SettingsPopup>
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
            <SettingsPager PageSize="100">
            </SettingsPager>
            <SettingsBehavior AllowFocusedRow="True" />            
            <SettingsSearchPanel CustomEditorID="tbToolbarSearch" />
            <Columns>
                <dx:GridViewDataTextColumn FieldName="IdArqueo" ReadOnly="True" Visible="False" VisibleIndex="0">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="FechaArqueo" ReadOnly="True" VisibleIndex="1">
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn FieldName="Apertura" VisibleIndex="4">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Tipo" VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Caja" FieldName="Moneda" VisibleIndex="3">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Efectivo" VisibleIndex="5">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Tarjeta" VisibleIndex="6">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Cheque" VisibleIndex="7">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Transferencia" VisibleIndex="8">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Total" VisibleIndex="9">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Observacion" VisibleIndex="10">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="FechaRegistroArqueo" Visible="False" VisibleIndex="12">
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataComboBoxColumn Caption="Usuario" FieldName="IdUsuario" VisibleIndex="11">
                    <PropertiesComboBox DataSourceID="LinqDataSource1" TextField="NombreUsuario" ValueField="IdUsuario">
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
            </Columns>
            <Styles>
                <Header BackColor="#FF9900" Font-Bold="True" ForeColor="Black">
                </Header>                
            </Styles>
        </dx:ASPxGridView>
        <asp:LinqDataSource ID="LqsHistorial" runat="server" ContextTypeName="DB_Bmas.DBML_SMMWMSDataContext" EntityTypeName="" TableName="SMM_Historial_Arqueo" Where="FechaArqueo &gt;= @FechaArqueo &amp;&amp; FechaArqueo &lt;= @FechaArqueo1">
            <WhereParameters>
                <asp:ControlParameter ControlID="dteInicio" DefaultValue="01-01-1990" Name="FechaArqueo" PropertyName="Value" Type="DateTime" />
                <asp:ControlParameter ControlID="dteTermino" DefaultValue="01-01-1990" Name="FechaArqueo1" PropertyName="Value" Type="DateTime" />
            </WhereParameters>
        </asp:LinqDataSource>
        <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="DB_Bmas.DBML_DesaintDataContext" EntityTypeName="" TableName="CVT_Usuarios">
        </asp:LinqDataSource>
    </div>
</asp:Content>
