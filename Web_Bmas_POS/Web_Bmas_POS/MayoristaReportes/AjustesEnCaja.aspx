<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AjustesEnCaja.aspx.cs" Inherits="Web_Bmas_POS.MayoristaReportes.AjustesEnCaja" %>

<%@ Register Assembly="DevExpress.Web.v22.1, Version=22.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntMaster" runat="server">
    <div id="breadcrumbs">
        <ul>
            <li><a href="../Blank.aspx">Inicio</a></li>
            <li><a>Mayorista</a></li>
            <li><span id="current">Informe Ajustes en Caja</span></li>
        </ul>
    </div>

    <div id="tableHeader">
        <span class="tableTitle">Informe Ajustes en Caja</span>
        <span class="tab-end"></span>
    </div>
    <br />
    <br />
    <div style="margin-left: 25px">
        <table>
            <tr>
                <td>
                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Fecha inicio" Theme="iOS"></dx:ASPxLabel>
                    <dx:ASPxDateEdit ID="dteInicio" runat="server" Theme="iOS"></dx:ASPxDateEdit>
                </td>
                <td></td>
                <td>
                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Fecha Termino" Theme="iOS"></dx:ASPxLabel>
                    <dx:ASPxDateEdit ID="dteTermino" runat="server" Theme="iOS">
                    </dx:ASPxDateEdit>
                </td>
                <td></td>
                <td>
                    <dx:ASPxButton ID="btnBuscar" runat="server" Text="Buscar" Height="57px" OnClick="btnBuscar_Click" Theme="iOS"></dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <div>
        <dx:ASPxGridView ID="GvDatos" runat="server" AutoGenerateColumns="False" DataSourceID="LqsListadoCaja" OnToolbarItemClick="GvDatos_ToolbarItemClick">
            <SettingsPager PageSize="100">
            </SettingsPager>
            <Settings AutoFilterCondition="Contains" ShowFilterRow="True" />
            <SettingsPopup>
                <FilterControl AutoUpdatePosition="False"></FilterControl>
            </SettingsPopup>
            <Toolbars>
                <dx:GridViewToolbar>
                    <Items>
                        <dx:GridViewToolbarItem Text="Exportar" Name="Export" Image-IconID="actions_download_16x16office2013" Alignment="Left" DropDownMode="False" BeginGroup="True" GroupName="a1" Target="_parent">
                            <Items>
                                <dx:GridViewToolbarItem Command="ExportToXls" />
                                <dx:GridViewToolbarItem Command="ExportToXlsx" />
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
            <SettingsSearchPanel CustomEditorID="tbToolbarSearch" />
            <Columns>
                <dx:GridViewDataTextColumn FieldName="PreventaID" VisibleIndex="0">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Codproducto" VisibleIndex="1">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="NomProd" VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="StockSala" VisibleIndex="3">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CantUni" VisibleIndex="7">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Faltante" VisibleIndex="8">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="FechaAjuste" VisibleIndex="9">
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn FieldName="IdUserAjuste" VisibleIndex="10">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="UsuarioSistema" VisibleIndex="11">
                </dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>
        <asp:LinqDataSource ID="LqsListadoCaja" runat="server" ContextTypeName="DB_Bmas.DBML_SMMWMSDataContext" EntityTypeName="" TableName="SMM_VW_DocAjustadas" Where="FechaAjuste &gt;= @FechaAjuste &amp;&amp; FechaAjuste &lt;= @FechaAjuste1">
            <WhereParameters>
                <asp:ControlParameter ControlID="dteInicio" DefaultValue="01-01-1990" Name="FechaAjuste" PropertyName="Value" Type="DateTime" />
                <asp:ControlParameter ControlID="dteTermino" DefaultValue="01-01-1990" Name="FechaAjuste1" PropertyName="Value" Type="DateTime" />
            </WhereParameters>
        </asp:LinqDataSource>
    </div>
</asp:Content>
