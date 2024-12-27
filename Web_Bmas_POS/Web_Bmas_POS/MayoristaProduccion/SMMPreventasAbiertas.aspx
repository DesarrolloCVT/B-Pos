<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SMMPreventasAbiertas.aspx.cs" Inherits="Web_Bmas_POS.MayoristaProduccion.SMMPreventasAbiertas" %>

<%@ Register Assembly="DevExpress.Web.v22.1, Version=22.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">
        function printReport() {
            window.open("Report.aspx", "PrintingFrame")
            var frameElement = document.getElementById("FrameToPrint");
            frameElement.addEventListener("load", function (e) {
                if (frameElement.contentDocument.contentType !== "text/html")
                    frameElement.contentWindow.print();
            });
        }
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntMaster" runat="server">
      <div id="breadcrumbs">
        <ul>
            <li><a href="../Blank.aspx">Inicio</a></li>
            <li><a>Produccion</a></li>
            <li><span id="current">PREVENTAS</span></li>
        </ul>
    </div>

    <div id="tableHeader">
        <span class="tableTitle">PREVENTAS</span>
        <span class="tab-end"></span>
        <%--  <dx:ASPxTextBox ID="txtCuotas" runat="server" Width="170px" Theme="iOS"></dx:ASPxTextBox>--%>
    </div>
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
        <dx:ASPxGridView ID="GvDatos" runat="server" AutoGenerateColumns="False" KeyFieldName="Preventa_ID" Theme="iOS" OnRowCommand="GvDatos_RowCommand">
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
            <Settings ShowFilterRow="True" />
            <SettingsBehavior AllowFocusedRow="True" />
            <SettingsSearchPanel CustomEditorID="tbToolbarSearch" />
            <Columns>
                <dx:GridViewCommandColumn ShowClearFilterButton="True" VisibleIndex="0">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn Caption="N° Preventa" FieldName="Preventa_ID" ReadOnly="True" VisibleIndex="1">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CodigoCliente" VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="RunCliente" VisibleIndex="5">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="TipoVenta" VisibleIndex="8">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Estado" VisibleIndex="11">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="FechaRegistro" VisibleIndex="13">
                    <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy HH:mm">
                    </PropertiesDateEdit>                    
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn FieldName="TotalPreventa" VisibleIndex="10">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataComboBoxColumn Caption="Usuario" FieldName="NombreUsuario" VisibleIndex="12">
                </dx:GridViewDataComboBoxColumn>
                   <dx:GridViewDataHyperLinkColumn Caption="Reimprimir" VisibleIndex="13">
                        <PropertiesHyperLinkEdit>
                            <Style HorizontalAlign="Center" VerticalAlign="Middle">
                             </Style>
                        </PropertiesHyperLinkEdit>
                        <EditFormSettings Visible="False" />
                        <DataItemTemplate>
                            <asp:ImageButton ID="reimprimir" CommandName="cmdReimprimir" runat="server" ImageUrl="~/Images/printing.png" />
                        </DataItemTemplate>
                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                        </CellStyle>
                    </dx:GridViewDataHyperLinkColumn>
            </Columns>
            <Styles>
                <Header BackColor="#FF9900" Font-Bold="True" ForeColor="Black">
                </Header>
            </Styles>
        </dx:ASPxGridView>
            <iframe id="FrameToPrint" name="PrintingFrame" style="position: absolute; left: -10000px; top: -10000px;"></iframe>
    </div>
</asp:Content>
