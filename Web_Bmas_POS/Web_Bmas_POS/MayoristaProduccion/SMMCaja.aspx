<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SMMCaja.aspx.cs" Inherits="Web_Bmas_POS.MayoristaProduccion.SMMCaja" %>

<%@ Register Assembly="DevExpress.Web.v22.1, Version=22.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style5 {
            width: 32px;
        }

        .auto-style6 {
            width: 20px;
        }

        .auto-style7 {
            width: 40px;
        }
    </style>
    <script type='text/javascript'>
        function Forzar() {
            __doPostBack('', '');
        }
        //function DisableButton(b) {
        //    b.visible  = false;
        //    return true;
        //}
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            btnGuardar.SetEnabled(false);
        }
        function EndRequestHandler(sender, args) {
            btnGuardar.SetEnabled(true);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntMaster" runat="server">
    <div id="breadcrumbs">
        <ul>
            <li><a href="../Blank.aspx">Inicio</a></li>
            <li><a>Produccion</a></li>
            <li><span id="current">B+ CAJA</span></li>
        </ul>
    </div>
    <div id="tableHeader">
        <span class="tableTitle">B+ CAJA</span>
        <span class="tab-end"></span>
        <%--  <dx:ASPxTextBox ID="txtCuotas" runat="server" Width="170px" Theme="iOS"></dx:ASPxTextBox>--%>
    </div>
    <br />
    <br />
    <div style="margin-left: 50px">
        <table>
            <tr>
                <td>
                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="N° Preventa :" Theme="iOS" Font-Bold="true"></dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtNPreventa" runat="server" Width="170px" Theme="iOS" AutoPostBack="True" OnTextChanged="txtNPreventa_TextChanged" Height="34px">
                        <ValidationSettings Display="Dynamic">
                            <RegularExpression ValidationExpression="\d+" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </td>
                <td class="auto-style5"></td>
                <td>
                    <dx:ASPxRadioButtonList ID="rbtTipoDoc" runat="server" Caption="Tipo Documento" RepeatDirection="Horizontal" Theme="iOS" Border-BorderStyle="None" Font-Bold="true" AutoPostBack="True" OnSelectedIndexChanged="rbtTipoDoc_SelectedIndexChanged">
                        <Items>
                            <dx:ListEditItem Text="Boleta" Value="B" />
                            <dx:ListEditItem Text="Factura" Value="F" />
                        </Items>
                    </dx:ASPxRadioButtonList>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div style="margin-left: 50px">
        <dx:ASPxGridView ID="GvDatosclientes" runat="server" AutoGenerateColumns="False" DataSourceID="LqsDatosCliente" KeyFieldName="Preventa_ID" Theme="iOS">
            <Settings ShowTitlePanel="True" />
            <SettingsPopup>
                <FilterControl AutoUpdatePosition="False"></FilterControl>
            </SettingsPopup>
            <SettingsText Title="Datos Cliente" />
            <Columns>
                <dx:GridViewDataTextColumn FieldName="Preventa_ID" ReadOnly="True" Visible="False" VisibleIndex="0">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CodigoCliente" VisibleIndex="1">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="RunCliente" VisibleIndex="4">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Telefono" VisibleIndex="5">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Email" VisibleIndex="6">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="TipoVenta" VisibleIndex="7" Visible="False">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="IdUsuario" Visible="False" VisibleIndex="9">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataComboBoxColumn FieldName="GrupoCliente" VisibleIndex="8">
                    <PropertiesComboBox DataSourceID="LqsGrupoCli" TextField="GroupName" ValueField="GroupCode">
                    </PropertiesComboBox>
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataComboBoxColumn FieldName="DireccionFact" VisibleIndex="2">
                    <PropertiesComboBox DataSourceID="LqsDireccionCliente" TextField="Street" ValueField="Address">
                    </PropertiesComboBox>
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataComboBoxColumn FieldName="DireccionEnvio" VisibleIndex="3">
                    <PropertiesComboBox DataSourceID="LqsDireccionCliente" TextField="Street" ValueField="Address">
                    </PropertiesComboBox>
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataComboBoxColumn>
            </Columns>
            <Styles>
                <Header BackColor="#FF9900" Font-Bold="True" ForeColor="Black">
                </Header>
            </Styles>
        </dx:ASPxGridView>
        <asp:LinqDataSource ID="LqsDatosCliente" runat="server" ContextTypeName="DB_Bmas.DBML_SMMWMSDataContext" EntityTypeName="" TableName="SMM_Preventa" Where="Preventa_ID == @Preventa_ID">
            <WhereParameters>
                <asp:ControlParameter ControlID="txtNPreventa" DefaultValue="0" Name="Preventa_ID" PropertyName="Text" Type="Int32" />
            </WhereParameters>
        </asp:LinqDataSource>
        <asp:LinqDataSource ID="LqsGrupoCli" runat="server" ContextTypeName="DB_Bmas.DBML_SBO_MMetroDataContext" EntityTypeName="" TableName="OCRG">
        </asp:LinqDataSource>
        <asp:LinqDataSource ID="LqsDireccionCliente" runat="server" ContextTypeName="DB_Bmas.DBML_SBO_MMetroDataContext" EntityTypeName="" TableName="CRD1">
        </asp:LinqDataSource>
    </div>
    <br />
    <br />
    <div style="margin-left: 50px" id="divPickear" runat="server">
        <table>
            <tr>
                <td>
                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Codigo Producto" Theme="iOS" Font-Bold="true"></dx:ASPxLabel>
                    <dx:ASPxTextBox ID="txtDun" runat="server" Width="200px" AutoPostBack="True" OnTextChanged="txtDun_TextChanged" ViewStateMode="Disabled" CssClass="auto-style5" Theme="iOS"></dx:ASPxTextBox>
                </td>
            </tr>
        </table>
        <br />
          <div style="display:inline-block">
                 <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="Aplicar Descuento ?" Theme="iOS" Font-Bold="true"></dx:ASPxLabel>
                  <dx:ASPxTextBox ID="txtApliDescuent" runat="server" Width="170px" CssClass="auto-style13" Height="34px" Theme="iOS" Text="5"></dx:ASPxTextBox>
                
            </div>
            <div style="display:inline-block ; margin-top:15px ">
                 <dx:ASPxButton ID="btnAplicarDesc" runat="server" Text="Aplicar" Height="41px" Theme="Material" OnClick="btnAplicarDesc_Click"></dx:ASPxButton>
                
            </div>     
        <br />
        <div>
            <dx:ASPxGridView ID="GvDatos" runat="server" AutoGenerateColumns="False" DataSourceID="LinqDataSource1" EnableTheming="True" KeyFieldName="Preventa_ID;CodProducto;UniMedida" OnRowUpdating="GvDatos_RowUpdating" Theme="iOS" Width="1393px" OnRowDeleted="GvDatos_RowDeleted" OnRowUpdated="GvDatos_RowUpdated" EnableCallBacks="False" OnRowCommand="GvDatos_RowCommand">
                <SettingsPager AlwaysShowPager="True" Mode="ShowAllRecords" PageSize="90000">
                </SettingsPager>
                <SettingsEditing Mode="Batch">
                    <BatchEditSettings KeepChangesOnCallbacks="True" StartEditAction="Click" />
                </SettingsEditing>
                <Settings ShowFooter="True" />
                <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                <SettingsCommandButton>
                    <DeleteButton Text=" ">
                        <Image IconID="iconbuilder_actions_deletecircled_svg_32x32">
                        </Image>
                    </DeleteButton>
                </SettingsCommandButton>
                <SettingsPopup>
                    <FilterControl AutoUpdatePosition="False"></FilterControl>
                </SettingsPopup>
                <Columns>
                    <dx:GridViewCommandColumn ShowDeleteButton="True" VisibleIndex="0">
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="Preventa_ID" VisibleIndex="1" Visible="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="CodProducto" VisibleIndex="2">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Producto" VisibleIndex="3">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Cantidad" VisibleIndex="5" LoadReadOnlyValueFromDataModel="False">
                        <EditFormSettings Visible="True" />
                        <EditCellStyle BackColor="White">
                        </EditCellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Precio" VisibleIndex="6">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Descuento" VisibleIndex="7">
                        <PropertiesTextEdit>
                            <ValidationSettings>
                                <RegularExpression ErrorText="solo numeros" ValidationExpression="\d+" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                        <EditFormSettings Visible="True" />
                        <EditCellStyle BackColor="White">
                        </EditCellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Total" VisibleIndex="8">
                        <PropertiesTextEdit DisplayFormatString="N0">
                        </PropertiesTextEdit>
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Medida" FieldName="UniMedida" VisibleIndex="4">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataHyperLinkColumn Caption="DescEspecial" VisibleIndex="13">
                        <PropertiesHyperLinkEdit>
                            <Style HorizontalAlign="Center" VerticalAlign="Middle">
                             </Style>
                        </PropertiesHyperLinkEdit>
                        <EditFormSettings Visible="False" />
                        <DataItemTemplate>
                            <asp:ImageButton ID="DescEspecial" CommandName="cmdDescEspecial" runat="server" ImageUrl="~/Images/descuento.png" />
                        </DataItemTemplate>
                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                        </CellStyle>
                    </dx:GridViewDataHyperLinkColumn>
                    <dx:GridViewDataTextColumn Caption="Desc" FieldName="Descuento" VisibleIndex="10">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                </Columns>
                <TotalSummary>
                    <dx:ASPxSummaryItem DisplayFormat="SubTotal : {0}" FieldName="Total" ShowInColumn="Total" SummaryType="Sum" ValueDisplayFormat="N0" />
                    <dx:ASPxSummaryItem DisplayFormat="Cantidad:{0}" FieldName="CodBarra" ShowInColumn="Cod Producto" SummaryType="Count" ValueDisplayFormat="N0" />
                </TotalSummary>
                <Styles>

                    <Header BackColor="#FF9900" Font-Bold="True" ForeColor="Black">
                    </Header>

                    <FocusedRow BackColor="#FF9933">
                    </FocusedRow>
                    <Footer Font-Bold="True">
                    </Footer>
                    <BatchEditCell Font-Bold="True">
                    </BatchEditCell>
                    <BatchEditModifiedCell Font-Bold="True">
                    </BatchEditModifiedCell>
                </Styles>
            </dx:ASPxGridView>
            <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="DB_Bmas.DBML_SMMWMSDataContext" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" TableName="SMM_Preventa_Detalle" Where="Preventa_ID == @Preventa_ID" OrderBy="ByOrder desc">
                <WhereParameters>
                    <asp:ControlParameter ControlID="txtNPreventa" DefaultValue="0" Name="Preventa_ID" PropertyName="Text" Type="Int32" />
                </WhereParameters>
            </asp:LinqDataSource>
            <asp:LinqDataSource ID="LqsProductosSinDun" runat="server" ContextTypeName="DB_Bmas.DBML_SBO_MMetroDataContext" EntityTypeName="" TableName="SMM_VW_DATO_BUSCAPROD_FULL">
            </asp:LinqDataSource>
        </div>
    </div>
    <br />
    <br />
    <div style="margin-left: 50px">
        <dx:ASPxButton ID="btnVentaSinPago" runat="server" Text="Venta sin Pago" Height="26px" OnClick="btnVentaSinPago_Click" RenderMode="Link" Width="118px"></dx:ASPxButton>
    </div>
    <br />
    <br />
    <div style="margin-left: 50px">
        <table>
            <tr>
                <td>
                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Tipo Pago" Theme="iOS"></dx:ASPxLabel>
                    <dx:ASPxComboBox ID="cboTipoPago" runat="server" EnableTheming="True" Theme="iOS" Width="284px" AutoPostBack="True" OnSelectedIndexChanged="cboTipoPago_SelectedIndexChanged">
                        <Items>
                            <dx:ListEditItem Text="Efectivo" Value="1" />
                            <dx:ListEditItem Text="Debito" Value="2" />
                            <dx:ListEditItem Text="Credito" Value="3" />
                            <dx:ListEditItem Text="Transferencia" Value="4" />
                            <dx:ListEditItem Text="Cheque" Value="5" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
                <td class="auto-style6"></td>
                <td>
                    <dx:ASPxLabel ID="lblMonto" runat="server" Text="Monto" Theme="iOS"></dx:ASPxLabel>
                    <dx:ASPxTextBox ID="txtMonto" runat="server" Width="170px" Theme="iOS"></dx:ASPxTextBox>
                </td>
                <td class="auto-style6" id="stCuantoPaga" runat="server"></td>
                <td id="tdCuantoPaga" runat="server">
                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Cuanto Paga?" Theme="iOS"></dx:ASPxLabel>
                    <dx:ASPxTextBox ID="txtCuantoPaga" runat="server" Width="170px" Theme="iOS" AutoPostBack="True" OnTextChanged="txtCuantoPaga_TextChanged">
                        <ValidationSettings Display="Dynamic" ValidationGroup="b1">
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </td>
                <td class="auto-style6" id="stVuelto" runat="server"></td>
                <td id="tdVuelto" runat="server">
                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="Vuelto" Theme="iOS"></dx:ASPxLabel>
                    <dx:ASPxTextBox ID="txtVuelto" runat="server" Width="170px" Theme="iOS">
                    </dx:ASPxTextBox>
                </td>
                <td class="auto-style6" id="stNTarjeta" runat="server"></td>
                <td id="tdNTarjeta" runat="server">
                    <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="N° Tarjeta" Theme="iOS"></dx:ASPxLabel>
                    <dx:ASPxTextBox ID="txtNTarjeta" runat="server" Width="170px" Theme="iOS">
                        <ValidationSettings Display="Dynamic" ValidationGroup="b1">
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div style="margin-left: 50px">
        <table>
            <tr>
                <td id="tdCuotas" runat="server">
                    <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="Cuotas" Theme="iOS"></dx:ASPxLabel>
                    <%--  <dx:ASPxTextBox ID="txtCuotas" runat="server" Width="170px" Theme="iOS"></dx:ASPxTextBox>--%>
                    <dx:ASPxComboBox ID="cboCuotas" runat="server" EnableTheming="True" Theme="iOS" Width="90px">
                        <Items>
                            <dx:ListEditItem Text="1" Value="1" />
                            <dx:ListEditItem Text="2" Value="2" />
                            <dx:ListEditItem Text="3" Value="3" />
                        </Items>
                        <ValidationSettings Display="Dynamic" ValidationGroup="b1">
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </dx:ASPxComboBox>
                </td>
                <td class="auto-style6" id="stBoucher" runat="server"></td>
                <td id="tdBoucher" runat="server">
                    <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="N° Boucher" Theme="iOS"></dx:ASPxLabel>
                    <dx:ASPxTextBox ID="txtNBoucher" runat="server" Width="170px" Theme="iOS">
                        <ValidationSettings Display="Dynamic" ValidationGroup="b1">
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </td>
                <td class="auto-style6" id="stNCheque" runat="server"></td>
                <td id="tdNCheque" runat="server">
                    <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="N° Cheque" Theme="iOS"></dx:ASPxLabel>
                    <dx:ASPxTextBox ID="txtNcheque" runat="server" Width="170px" Theme="iOS">
                        <ValidationSettings Display="Dynamic" ValidationGroup="b1">
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </td>
                <td class="auto-style6" id="stFechaDoc" runat="server"></td>
                <td id="tdFechaDoc" runat="server">
                    <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="Fecha Documento" Theme="iOS"></dx:ASPxLabel>
                    <dx:ASPxDateEdit ID="dteFechaDocumento" runat="server" Theme="iOS">
                        <ValidationSettings Display="Dynamic" ValidationGroup="b1">
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </dx:ASPxDateEdit>
                </td>

            </tr>
        </table>
    </div>



    <div style="margin-left: 150px" id="dvGuardar" runat="server">
        <table>
            <tr>
                <td style="padding-top: 10px">
                    <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" Height="47px" Theme="iOS" BackColor="#00A636" Width="175px" OnClick="btnGuardar_Click" ValidationGroup="b1" ClientInstanceName="btnGuardar">
                        <Image IconID="iconbuilder_actions_addcircled_svg_white_32x32">
                        </Image>
                        <ClientSideEvents Click="function(s, e) {
	}" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <dx:ASPxLoadingPanel ID="LoadPago" runat="server" ClientInstanceName="LoadingPanel"
        Modal="True" Theme="iOS">
    </dx:ASPxLoadingPanel>
    <br />
    <br />
    <div style="margin-left: 50px">
        <dx:ASPxGridView ID="GvDatoPagos" runat="server" AutoGenerateColumns="False" DataSourceID="LqsDatoPagos" EnableTheming="True" KeyFieldName="ID_TipoPago" Theme="iOS" OnRowCommand="GvDatoPagos_RowCommand">
            <SettingsBehavior ConfirmDelete="True" />
            <SettingsCommandButton>
                <DeleteButton Text=" ">
                    <Image IconID="iconbuilder_actions_deletecircled_svg_32x32">
                    </Image>
                </DeleteButton>
            </SettingsCommandButton>

            <Columns>
                <dx:GridViewDataTextColumn FieldName="Preventa_ID" ReadOnly="True" Visible="False" VisibleIndex="1">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="ID_TipoPago" ReadOnly="True" Visible="False" VisibleIndex="2">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Monto" VisibleIndex="4">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="nBoucher" VisibleIndex="5">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Cuotas" VisibleIndex="6">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="NTarjeta" VisibleIndex="7">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataComboBoxColumn FieldName="TipoPago" VisibleIndex="3">
                    <PropertiesComboBox>
                        <Items>
                            <dx:ListEditItem Text="Efectivo" Value="1" />
                            <dx:ListEditItem Text="Debito" Value="2" />
                            <dx:ListEditItem Text="Credito" Value="3" />
                            <dx:ListEditItem Text="Transferencia" Value="4" />
                            <dx:ListEditItem Text="Cheque" Value="5" />
                        </Items>
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataTextColumn FieldName="NCheque" VisibleIndex="8">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Fecha Doc." FieldName="FechaDocumento" VisibleIndex="9">
                    <PropertiesTextEdit DisplayFormatString="dd-MM-yyyy">
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataHyperLinkColumn Caption="Eliminar" VisibleIndex="16">
                    <DataItemTemplate>
                        <asp:ImageButton ID="Eliminar" CommandName="cmdEliminar" runat="server" ImageUrl="~/Images/delete.png" />
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
        <asp:LinqDataSource ID="LqsDatoPagos" runat="server" ContextTypeName="DB_Bmas.DBML_SMMWMSDataContext" EntityTypeName="" TableName="SMM_Preventa_DatoPago" Where="Preventa_ID == @Preventa_ID" EnableDelete="True" EnableInsert="True" EnableUpdate="True">
            <WhereParameters>
                <asp:ControlParameter ControlID="txtNPreventa" DefaultValue="0" Name="Preventa_ID" PropertyName="Text" Type="Int32" />
            </WhereParameters>
        </asp:LinqDataSource>
    </div>
    <div style="margin-left: 250px">
        <table>
            <tr>
                <td>
                    <dx:ASPxButton ID="btnPagar" runat="server" Text="PAGAR" Height="70px" Theme="iOS" Width="180px" BackColor="#009933" OnClick="btnPagar_Click">
                        <ClientSideEvents Click="function(s, e) {
	LoadingPanel.Show();
}" />
                        <Image IconID="iconbuilder_business_dollarcircled_svg_white_32x32">
                        </Image>
                    </dx:ASPxButton>
                </td>
                <td class="auto-style7"></td>
                <td>
                    <dx:ASPxButton ID="btnAnular" runat="server" Text="ANULAR" Height="70px" Theme="iOS" Width="180px" BackColor="#ff3300" CausesValidation="False" OnClick="btnAnular_Click">
                        <Image IconID="outlookinspired_paymentunpaid_svg_white_32x32">
                        </Image>
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Modal="True" Theme="iOS">
        </dx:ASPxLoadingPanel>
        <br />
    </div>
    <div>
        <dx:ASPxPopupControl ID="popAnulaVenta" runat="server"
            HeaderText="¿Está seguro que desea anular?" AllowDragging="True" Theme="iOS" Width="500px" Modal="True" CloseAction="CloseButton" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle">
            <HeaderImage IconID="businessobjects_bo_attention_svg_32x32">
            </HeaderImage>
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <div style="margin-left: 25px">
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnNoAnula" runat="server" Text="Cancelar" Theme="iOS" Height="50px" Width="180px" BackColor="#cccccc" OnClick="btnNoAnula_Click">
                                        <Image IconID="outlookinspired_cancel_svg_32x32">
                                        </Image>
                                    </dx:ASPxButton>
                                </td>
                                <td class="auto-style9"></td>
                                <td>
                                    <dx:ASPxButton ID="btnSiAnula" runat="server" Text="Aceptar" Height="50px" Theme="iOS" Width="180px" BackColor="#0075ea" OnClick="btnSiAnula_Click">
                                        <Image IconID="iconbuilder_actions_check_svg_32x32">
                                        </Image>
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
    </div>
    <div>
        <dx:ASPxPopupControl ID="PopDesc" runat="server" AllowDragging="True" CloseAction="None" CloseAnimationType="Fade" HeaderText="Verificacion " Modal="True" PopupAction="None" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle">

            <ContentCollection>
                <dx:PopupControlContentControl>
                    <br />
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel24" runat="server" Text="Descuento" Font-Bold="true" Font-Size="Medium"></dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtDescuentoEsp" runat="server" Width="170px" Height="25px" Theme="Default">
                                        <ValidationSettings Display="Dynamic" ValidationGroup="b2">
                                            <RegularExpression ValidationExpression="\d+" />
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="Usuario" Font-Bold="true" Font-Size="Medium"></dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtUsuario" runat="server" Width="170px" Height="25px" Theme="Default">
                                        <ValidationSettings Display="Dynamic" ValidationGroup="b2">
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel23" runat="server" Text="Contraseña" Font-Bold="true" Font-Size="Medium"></dx:ASPxLabel>
                                    <asp:TextBox ID="txtContrasena" runat="server" CausesValidation="True" Height="25px" TextMode="Password" Width="170px" ValidationGroup="b2"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnVerificar" runat="server" Text="Verificar" Theme="MaterialCompact" OnClick="btnVerificar_Click" Height="45px" ValidationGroup="b2"></dx:ASPxButton>
                                </td>
                                <td class="auto-style10"></td>
                                <td>
                                    <dx:ASPxButton ID="btnCancela" runat="server" Text="Cancelar" Theme="MaterialCompact" BackColor="Red" Height="45px" OnClick="btnCancela_Click" CausesValidation="False"></dx:ASPxButton>
                                </td>
                            </tr>
                        </table>

                    </div>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
    </div>
    <div>
        <dx:ASPxPopupControl ID="popSinPago" runat="server" AllowDragging="True" CloseAction="None" CloseAnimationType="Fade" HeaderText="Verificacion " Modal="True" PopupAction="None" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle">
            <ContentCollection>
                <dx:PopupControlContentControl>
                    <br />
                    <br />
                    <div>

                        <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="Usuario" Font-Bold="true" Font-Size="Medium"></dx:ASPxLabel>
                        <dx:ASPxTextBox ID="txtUser1" runat="server" Width="170px" Height="25px" Theme="Default">
                            <ValidationSettings Display="Dynamic" ValidationGroup="z1">
                                <RequiredField IsRequired="True" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                    <br />
                    <br />
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="Contraseña" Font-Bold="true" Font-Size="Medium"></dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtPass1" runat="server" Width="170px" Height="25px" Theme="Default" Password="True">
                                        <ValidationSettings Display="Dynamic" ValidationGroup="z1">
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                    <%--<asp:TextBox ID="" runat="server" CausesValidation="True" Height="25px" TextMode="Password" Width="170px" ValidationGroup="z1"></asp:TextBox>--%>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnVerificaNoPago" runat="server" Text="Verificar" Theme="MaterialCompact" Height="45px" ValidationGroup="b4" OnClick="btnVerificaNoPago_Click"></dx:ASPxButton>
                                </td>
                                <td class="auto-style10"></td>
                                <td>
                                    <dx:ASPxButton ID="btnCancelaNoPago" runat="server" Text="Cancelar" Theme="MaterialCompact" BackColor="Red" Height="45px" CausesValidation="False" OnClick="btnCancelaNoPago_Click"></dx:ASPxButton>
                                </td>
                            </tr>
                        </table>

                    </div>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
    </div>

</asp:Content>
