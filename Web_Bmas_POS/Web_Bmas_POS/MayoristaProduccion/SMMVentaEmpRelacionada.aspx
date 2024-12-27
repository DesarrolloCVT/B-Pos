<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SMMVentaEmpRelacionada.aspx.cs" Inherits="Web_Bmas_POS.MayoristaProduccion.SMMVentaEmpRelacionada" %>

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
            <li>&nbsp;<a href="../Blank.aspx">Inicio</a></li>
            <li><a>Produccion</a></li>
            <li><span id="current">B+ PREVENTA EMPRESA RELACIONADA</span></li>
        </ul>
    </div>
      <div id="tableHeader">
        <span class="tableTitle">B+ EMPRESA RELACIONADA</span>
        <span class="tab-end"></span>
    </div>
        <div style="margin-left: 500px">
        <div style="display: flex; align-items: center;" class="auto-style14">
            <div style="display: inline-block">
                <dx:ASPxLabel ID="ASPxLabel25" runat="server" Text="N° Preventa"></dx:ASPxLabel>
                <dx:ASPxTextBox ID="txtNPreventaImp" runat="server" Width="170px" Theme="iOS"></dx:ASPxTextBox>
            </div>
            <div style="display: inline-block">
                <dx:ASPxButton ID="btnReimprimir" runat="server" Text="Reimprimir Preventa" Height="50px" Theme="iOS" OnClick="btnReimprimir_Click" Width="146px">
                    <Image IconID="print_print_svg_16x16">
                    </Image>
                </dx:ASPxButton>
            </div>

        </div>
    </div>
       <div style="margin-left: 100px">
        <dx:ASPxRoundPanel ID="roundCliente" runat="server" ShowCollapseButton="true" Width="200px" HeaderText="CLIENTE" LoadContentViaCallback="True" Theme="iOS" AllowCollapsingByHeaderClick="True">
            <HeaderImage IconID="businessobjects_bo_employee_svg_16x16">
            </HeaderImage>
            <PanelCollection>
                <dx:PanelContent>
                    <div>
                        <%--<dx:ASPxButtonEdit ID="txtBusCliente" runat="server" OnButtonClick="ASPxButtonEdit1_ButtonClick" Theme="iOS" Width="530px" Border-BorderColor="#0099ff" AutoPostBack="True" OnTextChanged="txtBusCliente_TextChanged" NullText="Buscar Cliente">
                            <Buttons>
                                <dx:EditButton ToolTip="Editar" Width="25px">
                                    <Image IconID="iconbuilder_actions_edit_svg_16x16">
                                    </Image>
                                </dx:EditButton>
                                <dx:EditButton ToolTip="Agregar" Width="25px">
                                    <Image IconID="iconbuilder_actions_add_svg_16x16">
                                    </Image>
                                </dx:EditButton>
                            </Buttons>
                            <ButtonStyle>
                                <BorderBottom BorderStyle="Double" />
                            </ButtonStyle>
                            <Border BorderColor="#0099FF"></Border>
                        </dx:ASPxButtonEdit>--%>
                        <dx:ASPxComboBox ID="cboBusCliente" runat="server" ValueType="System.String" Theme="iOS" DataSourceID="LqsEmpRelacionadas" OnSelectedIndexChanged="cboBusCliente_SelectedIndexChanged" ValueField="RutCliente" Width="397px" AutoPostBack="True">
                            <Columns>
                                <dx:ListBoxColumn Caption="Run" FieldName="RutCliente">
                                </dx:ListBoxColumn>
                                <dx:ListBoxColumn Caption="Nombre " FieldName="NombreCliente" Width="400px">
                                </dx:ListBoxColumn>
                            </Columns>
                            <ValidationSettings Display="Dynamic" ValidationGroup="a1">
                                <RequiredField IsRequired="True" />
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                        <asp:LinqDataSource ID="LqsEmpRelacionadas" runat="server" ContextTypeName="DB_Bmas.DBML_SMMWMSDataContext" EntityTypeName="" TableName="SMM_VW_INFO_CLIENTE_PREVENTA" Where="CodGrupCliente == @CodGrupCliente">
                            <WhereParameters>
                                <asp:Parameter DefaultValue="103" Name="CodGrupCliente" Type="Int16" />
                            </WhereParameters>
                        </asp:LinqDataSource>
                    </div>
                    <br />
                    <div runat="server" id="divDatos_Client">
                        <div>
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Código Cliente" Theme="iOS" Font-Bold="true"></dx:ASPxLabel>
                            <dx:ASPxTextBox ID="txtCodigoCliente" runat="server" Width="400px" Theme="iOS" Text="C66666666-6" ReadOnly="true"></dx:ASPxTextBox>
                        </div>
                        <div>
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Nombre Cliente" Theme="iOS" Font-Bold="true"></dx:ASPxLabel>
                            <dx:ASPxTextBox ID="txtNombreCliente" runat="server" Width="400px" Theme="iOS" NullText="Nombre Cliente">
                                <Border BorderColor="#0099FF"></Border>
                            </dx:ASPxTextBox>
                        </div>
                        <div>
                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Identificador" Theme="iOS" Font-Bold="true"></dx:ASPxLabel>
                            <dx:ASPxTextBox ID="txtIdentificador" runat="server" Width="400px" Theme="iOS" Text="66666666-6" ReadOnly="true"></dx:ASPxTextBox>
                        </div>
                        <div>
                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="Dirección envío" Theme="iOS" Font-Bold="true"></dx:ASPxLabel>
                            <dx:ASPxComboBox ID="cboDireccionEnvio" runat="server" ValueType="System.String" Theme="iOS" Width="525px" DataSourceID="direccionDespacho" ValueField="CodDireccion">
                                <Columns>
                                    <dx:ListBoxColumn Caption="Direccion" FieldName="Street">
                                    </dx:ListBoxColumn>
                                    <dx:ListBoxColumn Caption="Comuna" FieldName="County">
                                    </dx:ListBoxColumn>
                                    <dx:ListBoxColumn Caption="Region" FieldName="City">
                                    </dx:ListBoxColumn>
                                </Columns>
                            </dx:ASPxComboBox>
                            <asp:LinqDataSource ID="direccionDespacho" runat="server" ContextTypeName="DB_Bmas.DBML_SMMWMSDataContext" EntityTypeName="" TableName="VW_DIRECCIONES_CLIENTES_SAP" Where="AdresType == @AdresType &amp;&amp; CardCode == @CardCode">
                                <WhereParameters>
                                    <asp:Parameter DefaultValue="S" Name="AdresType" Type="Char" />
                                    <asp:ControlParameter ControlID="txtCodigoCliente" DefaultValue="C66666666-6" Name="CardCode" PropertyName="Text" Type="String" />
                                </WhereParameters>
                            </asp:LinqDataSource>
                        </div>
                        <div>
                            <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="Dirección facturación" Theme="iOS" Font-Bold="true"></dx:ASPxLabel>
                            <dx:ASPxComboBox ID="cboDireccionFacturacion" runat="server" ValueType="System.String" Theme="iOS" Width="525px" DataSourceID="direccionFacturacion" ValueField="CodDireccion">
                                <Columns>
                                    <dx:ListBoxColumn Caption="Direccion" FieldName="Street">
                                    </dx:ListBoxColumn>
                                    <dx:ListBoxColumn Caption="Comuna" FieldName="County">
                                    </dx:ListBoxColumn>
                                    <dx:ListBoxColumn Caption="Region" FieldName="City">
                                    </dx:ListBoxColumn>
                                </Columns>
                            </dx:ASPxComboBox>
                        </div>
                        <div>
                            <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="Télefono" Theme="iOS" Font-Bold="true"></dx:ASPxLabel>
                            <dx:ASPxTextBox ID="txtTelefono" runat="server" Width="400px" Theme="iOS" Text="984321168" ReadOnly="true"></dx:ASPxTextBox>
                        </div>
                        <div>
                            <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="Email" Theme="iOS" Font-Bold="true"></dx:ASPxLabel>
                            <dx:ASPxTextBox ID="txtEmail" runat="server" Width="400px" Theme="iOS" ReadOnly="true" NullText="Email"></dx:ASPxTextBox>
                        </div>
                        <div>
                            <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="Grupo Cliente" Theme="iOS" Font-Bold="true"></dx:ASPxLabel>
                            <dx:ASPxComboBox ID="cboGrupoCliente" runat="server" ValueType="System.String" Theme="iOS" Width="400px" ReadOnly="true" DataSourceID="LqsGrupoCliente" TextField="GroupName" ValueField="GroupCode"></dx:ASPxComboBox>
                            <asp:LinqDataSource ID="LqsGrupoCliente" runat="server" ContextTypeName="DB_Bmas.DBML_SBO_MMetroDataContext" EntityTypeName="" TableName="OCRG" Where="GroupCode == @GroupCode">
                                <WhereParameters>
                                    <asp:SessionParameter DefaultValue="112" Name="GroupCode" SessionField="GrupoCli" Type="Int16" />
                                </WhereParameters>
                            </asp:LinqDataSource>
                        </div>                       
                    </div>
                    <br />
                    <div style="margin-left: 140px">
                        <dx:ASPxButton ID="btnPickear" runat="server" Text="PICKEAR" Theme="iOS" Height="59px" Width="249px" BackColor="#FF9900" OnClick="btnPickear_Click" ValidationGroup="a1">
                            <Image IconID="iconbuilder_shopping_package_svg_white_32x32">
                            </Image>
                        </dx:ASPxButton>
                        <asp:LinqDataSource ID="direccionFacturacion" runat="server" ContextTypeName="DB_Bmas.DBML_SMMWMSDataContext" EntityTypeName="" TableName="VW_DIRECCIONES_CLIENTES_SAP" Where="AdresType == @AdresType &amp;&amp; CardCode == @CardCode">
                            <WhereParameters>
                                <asp:Parameter DefaultValue="B" Name="AdresType" Type="Char" />
                                <asp:ControlParameter ControlID="txtCodigoCliente" DefaultValue="C66666666-6" Name="CardCode" PropertyName="Text" Type="String" />
                            </WhereParameters>
                        </asp:LinqDataSource>
                    </div>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxRoundPanel>
    </div>
      <br />
    <br />
    <div style="margin-left: 50px ; display: flex; align-items: center; width: 1200px; height: 200px; background-color: #d7d7d7; border-color: black; border-style: solid; border-width: 2px; justify-content:flex-start" runat="server" id="divTotalVis" visible="false" >
        <div style="display: inline-block ;vertical-align:central" >
             <dx:ASPxLabel ID="ASPxLabel10" runat="server"  Text="TOTAL: " Font-Size="100px" Font-Bold="True" Border-BorderStyle="None" ReadOnly="true" BackColor="#d4d4d4" Font-Names="Calibri"></dx:ASPxLabel>
        </div>
        <div display: inline-block">
              <dx:ASPxTextBox ID="txtTotal" runat="server"  Font-Size="150px" Font-Bold="True" Border-BorderStyle="None" ReadOnly="true" DisplayFormatString="N0" BackColor="#d7d7d7" Font-Names="Calibri" Width="937px"></dx:ASPxTextBox>
        </div>      
    </div>
    <br />
    <br />
    <div style="margin-left: 50px" id="divPickear" runat="server">
        <div style="display:flex; align-items:center">
            <div style="display:inline-block; margin-right:100px">
                 <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Codigo Producto" Theme="iOS" Font-Bold="true"></dx:ASPxLabel>
                 <dx:ASPxTextBox ID="txtDun" runat="server" Width="200px" AutoPostBack="True" OnTextChanged="txtDun_TextChanged" ViewStateMode="Disabled" CssClass="auto-style5" Theme="iOS"></dx:ASPxTextBox>
            </div>
            <div style="display:inline-block">
                 <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="Aplicar Descuento ?" Theme="iOS" Font-Bold="true"></dx:ASPxLabel>
                  <dx:ASPxTextBox ID="txtApliDescuent" runat="server" Width="170px" CssClass="auto-style13" Height="34px" Theme="iOS" Text="5"></dx:ASPxTextBox>
                
            </div>
            <div style="display:inline-block ; margin-top:15px ">
                 <dx:ASPxButton ID="btnAplicarDesc" runat="server" Text="Aplicar" Height="41px" Theme="Material" OnClick="btnAplicarDesc_Click"></dx:ASPxButton>
            </div>           
        </div>
         <dx:ASPxLabel ID="lblCantidadProd" runat="server" Text="" Theme="iOS" Font-Bold="true" Font-Size="Medium"></dx:ASPxLabel>
        <br />
      <%--  <div  style="display:flex; align-items:center">
            <div style="display:inline-block">
                 <dx:ASPxComboBox ID="cboProductosinDun" runat="server" Caption="Busca Producto" DataSourceID="LqsProductosSinDun" EnableTheming="True" Theme="iOS" Width="703px" ValueField="Codigo" Font-Bold="True">
                            <Columns>
                                <dx:ListBoxColumn FieldName="ItemCode" Width="150px" Caption="CodProducto">
                                </dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="ItemName" Width="650px" Caption="Producto">
                                </dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="Price" Width="120px" Caption="Precio">
                                </dx:ListBoxColumn>
                                <dx:ListBoxColumn Caption="Medida" FieldName="UomCode">
                                </dx:ListBoxColumn>
                            </Columns>
                            <ClearButton DisplayMode="Always">
                            </ClearButton>
                        </dx:ASPxComboBox>
            </div>
            <div style="display:inline-block">
               <dx:ASPxButton ID="btnAgregaProdSinDun" runat="server" Height="34px" OnClick="btnAgregaProdSinDun_Click" Theme="iOS">
                            <Image IconID="iconbuilder_actions_addcircled_svg_white_16x16">
                            </Image>
                        </dx:ASPxButton>
            </div>          
        </div>--%>


        <br />
        <div>
            <dx:ASPxGridView ID="GvDatos" runat="server" ViewStateMode="Disabled" AutoGenerateColumns="False" DataSourceID="LinqDataSource1" EnableTheming="True" KeyFieldName="Preventa_ID;CodProducto;UniMedida" OnRowUpdating="GvDatos_RowUpdating" Theme="iOS" Width="1467px" OnRowDeleted="GvDatos_RowDeleted" OnRowCommand="GvDatos_RowCommand" EnableCallBacks="False" OnRowUpdated="GvDatos_RowUpdated" EnableViewState="False">
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
                    <dx:ASPxSummaryItem DisplayFormat="Cantidad : {0}" FieldName="CodBarra" ShowInColumn="Cod Producto" SummaryType="Count" ValueDisplayFormat="N0" />
                </TotalSummary>
                <Styles>
                    <Header BackColor="#FF9900" Font-Bold="True">
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
                    <asp:SessionParameter DefaultValue="0" Name="Preventa_ID" SessionField="IdPreventa" Type="Int32" />
                </WhereParameters>
            </asp:LinqDataSource>
            <asp:LinqDataSource ID="LqsProductosSinDun" runat="server" ContextTypeName="DB_Bmas.DBML_SBO_MMetroDataContext" EntityTypeName="" TableName="SMM_VW_DATO_BUSCAPROD_FULL">
            </asp:LinqDataSource>
        </div>
        <div style="margin-left: 150px;display:flex; align-items:center">
            <div style="display:inline-block ; margin-right:25px">
                <dx:ASPxButton ID="btnCrear" runat="server" Text="CREAR" Height="50px" Theme="iOS" Width="150px" OnClick="btnCrear_Click">
                            <Image IconID="iconbuilder_shopping_shoppingcart_svg_white_32x32">
                            </Image>
                        </dx:ASPxButton>
                        <iframe id="FrameToPrint" name="PrintingFrame" style="position: absolute; left: -10000px; top: -10000px;"></iframe>
            </div>
            <div style="display:inline-block">
                     <dx:ASPxButton ID="btnAnular" runat="server" Text="ANULAR" Height="50px" Theme="iOS" Width="172px" BackColor="Red" OnClick="btnAnular_Click">
                            <Image IconID="iconbuilder_actions_deletecircled_svg_white_32x32">
                            </Image>
                        </dx:ASPxButton> 
            </div>       
        </div>
    </div>
    <div>
    </div>
    <div>
        <dx:ASPxPopupControl ID="popAnulaPreventa" runat="server"
            HeaderText="¿Está seguro que desea anular?" AllowDragging="True" Theme="iOS" Width="500px" Modal="True" CloseAction="CloseButton" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle">
            <HeaderImage IconID="businessobjects_bo_attention_svg_32x32">
            </HeaderImage>
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <div style="margin-left: 25px;display:flex; align-items:center">
                        <div style="display:inline-block;margin-right:25px">
                             <dx:ASPxButton ID="btnNoAnula" runat="server" Text="Cancelar" Theme="iOS" Height="50px" Width="180px" BackColor="#cccccc" OnClick="btnNoAnula_Click">
                                        <Image IconID="outlookinspired_cancel_svg_32x32">
                                        </Image>
                                    </dx:ASPxButton>
                        </div>
                        <div style="display:inline-block">
                           <dx:ASPxButton ID="btnSiAnula" runat="server" Text="Aceptar" Height="50px" Theme="iOS" Width="180px" BackColor="#0075ea" OnClick="btnSiAnula_Click">
                                        <Image IconID="iconbuilder_actions_check_svg_32x32">
                                        </Image>
                                    </dx:ASPxButton>
                        </div>                       
                    </div>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
    </div>
    <div>
    </div>
    <div>
        <dx:ASPxPopupControl ID="PopDesc" runat="server" AllowDragging="True" CloseAction="None" CloseAnimationType="Fade" HeaderText="Verificacion " Modal="True" PopupAction="None" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle">

            <ContentCollection>
                <dx:PopupControlContentControl>
                    <br />
                    <div style="margin-left:10px">
                        <div>
                           <dx:ASPxLabel ID="ASPxLabel24" runat="server" Text="Descuento" Font-Bold="true" Font-Size="Medium"></dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtDescuentoEsp" runat="server" Width="170px" Height="25px" Theme="Default">
                                        <ValidationSettings Display="Dynamic" ValidationGroup="b2">
                                            <RegularExpression ValidationExpression="\d+" />
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                        </div>
                        <div>
                         <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="Usuario" Font-Bold="true" Font-Size="Medium"></dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtUsuario" runat="server" Width="170px" Height="25px" Theme="Default">
                                        <ValidationSettings Display="Dynamic" ValidationGroup="b2">
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                        </div>
                        <div>                           
                                    <dx:ASPxLabel ID="ASPxLabel23" runat="server" Text="Contraseña" Font-Bold="true" Font-Size="Medium"></dx:ASPxLabel>
                                    <asp:TextBox ID="txtContrasena" runat="server" CausesValidation="True" Height="25px" TextMode="Password" Width="170px" ValidationGroup="b2"></asp:TextBox>
                             
                        </div>                                         
                    </div>
                    <br />
                     <div style="margin-left: 15px;display:flex; align-items:center">
                            <div style="display:inline-block;margin-right:25px">
                                 <dx:ASPxButton ID="btnVerificar" runat="server" Text="Verificar" Theme="MaterialCompact" OnClick="btnVerificar_Click" Height="45px" ValidationGroup="b2"></dx:ASPxButton>
                            </div>
                            <div style="display:inline-block">
                                  <dx:ASPxButton ID="btnCancela" runat="server" Text="Cancelar" Theme="MaterialCompact" BackColor="Red" Height="45px" OnClick="btnCancela_Click" CausesValidation="False"></dx:ASPxButton>
                            </div>
                             
                        </div>   
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
    </div>
</asp:Content>
