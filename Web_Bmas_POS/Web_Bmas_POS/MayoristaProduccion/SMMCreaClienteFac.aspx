<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SMMCreaClienteFac.aspx.cs" Inherits="Web_Bmas_POS.MayoristaProduccion.SMMCreaClienteFac" %>

<%@ Register Assembly="DevExpress.Web.v22.1, Version=22.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style type="text/css">
        .auto-style6 {
            width: 35px;
        }

        .auto-style7 {
            height: 38px;
        }

        .auto-style8 {
            height: 38px;
            width: 101px;
        }

        .auto-style9 {
            width: 73px;
        }

        .auto-style10 {
            width: 73px;
            height: 3px;
        }

        .auto-style11 {
            width: 93px;
            height: 3px;
        }

        .auto-style12 {
            width: 93px;
        }

        .auto-style13 {
            width: 101px;
            height: 3px;
        }

        .auto-style14 {
            width: 101px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntMaster" runat="server">
      <div id="breadcrumbs">
        <ul>
            <li>&nbsp;<a href="../Blank.aspx">Inicio</a></li>
            <li><a>B+</a></li>
            <li><span id="current">CREA CLIENTE</span></li>
        </ul>
    </div>

    <div id="tableHeader">
        <span class="tableTitle">CREA CLIENTE</span>
        <span class="tab-end"></span>
        <%--   <div id="options">
            <a>
                <asp:ImageButton ID="cmdExcel" runat="server"
                    ImageUrl="~/App_Themes/IDA/ui/ico-file-excel.png" OnClick="cmdExcel_Click"
                    Style="width: 20px" />
                Exportar Excel</a>
        </div>--%>
    </div>
    <br />
    <div style="margin-left: 20px">
        <dx:ASPxPageControl ID="citiesTabPage" Width="100%" runat="server" CssClass="dxtcFixed" ActiveTabIndex="2" EnableHierarchyRecreation="True" Theme="iOS" OnActiveTabChanged="citiesTabPage_ActiveTabChanged">
            <TabPages>
                <dx:TabPage Text="Principal">
                    <ContentCollection>
                        <dx:ContentControl runat="server">
                            <br />
                            <div style="margin-left: 150PX">
                                <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="PRINCIPAL" Font-Size="XX-Large" Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                            </div>
                            <br />
                            <br />
                            <div>
                                <table>
                                    <tr>
                                        <td class="auto-style8">
                                            <dx:ASPxLabel ID="lblr" runat="server" Text="*Rut: " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td class="auto-style7"></td>
                                        <td class="auto-style7">
                                            <dx:ASPxTextBox runat="server" Width="200px" MaxLength="10" Height="30px" ID="txtRutCli" Theme="iOS" AutoPostBack="True" OnTextChanged="txtRutCli_TextChanged">
                                                <MaskSettings Mask="00000000-a"></MaskSettings>
                                                <ValidationSettings Display="Dynamic">
                                                    <RegularExpression ErrorText="ingrese rut" ValidationExpression="^[0-9]+-[0-9kK]{1}$"></RegularExpression>

                                                    <RequiredField IsRequired="True"></RequiredField>
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style13"></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style14">
                                            <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="*Código: " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxComboBox ID="cboSiglaCliente" runat="server" ValueType="System.String" EnableTheming="True" SelectedIndex="0" Theme="iOS" Width="66px">
                                                            <Items>
                                                                <dx:ListEditItem Selected="True" Text="C" Value="C" />
                                                                <dx:ListEditItem Text="B" Value="B" />
                                                            </Items>
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxTextBox ID="txtCodCliNew" runat="server" Width="200px" ReadOnly="true" Theme="iOS"></dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                            </table>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style13"></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style14">
                                            <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="*Razon Social: " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dx:ASPxTextBox ID="txtRazonCli" runat="server" Width="400px" Theme="iOS">
                                                <ValidationSettings Display="Dynamic">
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style13"></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style14">
                                            <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="*Nombre Fantasia: " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dx:ASPxTextBox ID="txtNombreFantasiaCli" runat="server" Width="400px" Theme="iOS">
                                                <ValidationSettings Display="Dynamic">
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style13"></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style14">
                                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="*Celular Contacto: " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dx:ASPxTextBox ID="txtCelContacto" runat="server" Width="200px" Theme="iOS" MaxLength="9">
                                                <ValidationSettings Display="Dynamic" ErrorText="Solo Numeros">
                                                    <RegularExpression ValidationExpression="\d+" />
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style13"></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style14">
                                            <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="*Grupo Cliente: " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dx:ASPxComboBox ID="cboGrupoCliente" runat="server" ValueType="System.String" DataSourceID="LqsGrupioCli" EnableTheming="True" TextField="GroupName" Theme="iOS" ValueField="GroupCode" Width="200px">
                                                <ValidationSettings Display="Dynamic">
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxComboBox>
                                            <asp:LinqDataSource ID="LqsGrupioCli" runat="server" ContextTypeName="DB_Bmas.DBML_SBO_MMetroDataContext" EntityTypeName="" TableName="OCRG">
                                            </asp:LinqDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style13"></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style14">
                                            <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="*Zona(Región): " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dx:ASPxComboBox ID="cboZona" runat="server" ValueType="System.String" DataSourceID="LqsRegion" EnableTheming="True" TextField="Name" Theme="iOS" ValueField="Code" Width="400px">
                                                <ValidationSettings Display="Dynamic">
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxComboBox>
                                            <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="DB_Bmas.DBML_SBO_MMetroDataContext" EntityTypeName="" TableName="OCST" Where="Country == @Country">
                                                <WhereParameters>
                                                    <asp:Parameter DefaultValue="CL" Name="Country" Type="String" />
                                                </WhereParameters>
                                            </asp:LinqDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style13"></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style14">
                                            <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="*E-Mail F.E: " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dx:ASPxTextBox ID="txtEmailFE" runat="server" Width="399px" Theme="iOS">
                                                <ValidationSettings Display="Dynamic" ErrorText="Ingrese Correo Valido">
                                                    <RegularExpression ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style13"></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style14">
                                            <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="*Giro: " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dx:ASPxTextBox ID="txtGirocliNew" runat="server" Width="400px" Theme="iOS">
                                                <ValidationSettings Display="Dynamic">
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style13"></td>
                                    </tr>

                                    <tr>
                                        <td class="auto-style14">
                                            <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="*Vendedor Asignado " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dx:ASPxComboBox ID="cboVendAsignado" runat="server" ValueType="System.String" Height="34px" Width="398px" DataSourceID="LqsVendedorAsig" TextField="SlpName" ValueField="SlpCode">
                                                <ValidationSettings Display="Dynamic">
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxComboBox>
                                            <asp:LinqDataSource ID="LqsVendedorAsig" runat="server" ContextTypeName="DB_Bmas.DBML_SBO_MMetroDataContext" EntityTypeName="" TableName="OSLP" Where="Active == @Active">
                                                <WhereParameters>
                                                    <asp:Parameter DefaultValue="Y" Name="Active" Type="Char" />
                                                </WhereParameters>
                                            </asp:LinqDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style13"></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style14">
                                            <dx:ASPxLabel ID="ASPxLabel23" runat="server" Text="*Tipo Negocio " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dx:ASPxComboBox ID="cboTipoNegocio" runat="server" ValueType="System.String" Height="34px" Width="398px" DataSourceID="LqsTiponegocio" TextField="IndName" ValueField="IndCode">
                                                <ValidationSettings Display="Dynamic">
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxComboBox>
                                            <asp:LinqDataSource ID="LqsTiponegocio" runat="server" ContextTypeName="DB_Bmas.DBML_SBO_MMetroDataContext" EntityTypeName="" TableName="OOND">
                                            </asp:LinqDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style13"></td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                            <br />
                            <div>
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel24" runat="server" Text="*Condicion de Pago " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td class="auto-style6"></td>
                                        <td>
                                            <dx:ASPxComboBox ID="cboCondicionPago" runat="server" ValueType="System.String" Height="34px" Width="398px" DataSourceID="LqsCondicionPago" TextField="PymntGroup" ValueField="GroupNum">
                                                <ValidationSettings Display="Dynamic">
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxComboBox>
                                            <asp:LinqDataSource ID="LqsCondicionPago" runat="server" ContextTypeName="DB_Bmas.DBML_SBO_MMetroDataContext" EntityTypeName="" TableName="OCTG">
                                            </asp:LinqDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style10"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel25" runat="server" Text="*Descuento %: " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td class="auto-style6"></td>
                                        <td>
                                            <dx:ASPxTextBox ID="txtDescuento" runat="server" Width="200px" Theme="iOS">
                                                <ValidationSettings Display="Dynamic">
                                                    <RegularExpression ValidationExpression="\d+" />
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style10"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel26" runat="server" Text="*Linea de Credito: " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td class="auto-style6"></td>
                                        <td>
                                            <dx:ASPxTextBox ID="txtLineaCredito" runat="server" Width="200px" Theme="iOS">
                                                <ValidationSettings Display="Dynamic" ErrorText="Solo Numeros">
                                                    <RegularExpression ValidationExpression="\d+" />
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style10"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel27" runat="server" Text="*Linea de Credito: " Font-Bold="true" Theme="iOS" Visible="false"></dx:ASPxLabel>
                                        </td>
                                        <td class="auto-style6"></td>
                                        <td>
                                            <dx:ASPxRadioButtonList ID="rbtNominacion" runat="server" ValueType="System.String" RepeatDirection="Horizontal" Theme="MaterialCompact" Border-BorderStyle="None" Font-Bold="True" Height="53px">
                                                <Items>
                                                    <dx:ListEditItem Text="Nominado" Value="Nominado" />
                                                    <dx:ListEditItem Text="Innominado" Value="Innominado" />
                                                </Items>

                                                <ValidationSettings Display="Dynamic">
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>

                                                <Border BorderStyle="None"></Border>
                                            </dx:ASPxRadioButtonList>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="auto-style10"></td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                            <br />
                            <div style="margin-left: 100px">
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnContinuarCli" runat="server" Text="Continuar" Theme="iOS" Height="50px" BackColor="#ff9900" OnClick="btnContinuarCli_Click">

                                                <Image IconID="scheduling_appointmentendcontinuearrow_svg_white_16x16">
                                                </Image>

                                            </dx:ASPxButton>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dx:ASPxButton ID="btnCancelar" runat="server" Text="Cancelar" Theme="iOS" Height="50px" BackColor="#cccccc" CausesValidation="False" OnClick="btnCancelar_Click">
                                                <Image IconID="outlookinspired_paymentunpaid_svg_white_32x32">
                                                </Image>
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage Text="Contacto">
                    <ContentCollection>
                        <dx:ContentControl runat="server">
                            <br />
                            <div style="margin-left: 150PX">
                                <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="CONTACTO" Font-Size="XX-Large" Font-Bold="true"></dx:ASPxLabel>
                            </div>
                            <br />
                            <br />
                            <div>
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="*Cargo: " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dx:ASPxTextBox runat="server" Width="400px" MaxLength="10" Height="30px" ID="txtCargo" Theme="iOS" AutoPostBack="True">

                                                <ValidationSettings Display="Dynamic">
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>

                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style13">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="*Nombre: " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dx:ASPxTextBox ID="txtNombreContacto" runat="server" Width="400px" Theme="iOS">
                                                <ValidationSettings Display="Dynamic">
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style13">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="*Apellido: " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dx:ASPxTextBox ID="txtApellidoContacto" runat="server" Width="400px" Theme="iOS">
                                                <ValidationSettings Display="Dynamic">
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style13"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="*Telefono 1: " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dx:ASPxTextBox ID="txtFono1" runat="server" Width="200px" Theme="iOS" MaxLength="9">
                                                <ValidationSettings Display="Dynamic" ErrorText="Solo Numeros">
                                                    <RegularExpression ValidationExpression="\d+" />
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style13"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="*Telefono 2: " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dx:ASPxTextBox ID="txtFono2" runat="server" Width="200px" Theme="iOS" MaxLength="9">
                                                <ValidationSettings Display="Dynamic" ErrorText="Solo Numeros">
                                                    <RegularExpression ValidationExpression="\d+" />
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style13"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="*E-Mail: " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dx:ASPxTextBox ID="txtEmailContacto" runat="server" Width="399px" Theme="iOS">
                                                <ValidationSettings Display="Dynamic" ErrorText="Ingrese Correo Valido">
                                                    <RegularExpression ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                            <div>
                                <dx:ASPxButton ID="btnGuardarContacto" runat="server" Text="Agregar" Theme="iOS" Height="30px" BackColor="#00bb3d" Width="100px" OnClick="btnGuardarContacto_Click">
                                    <Image IconID="iconbuilder_actions_addcircled_svg_white_16x16">
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <br />
                            <br />
                            <div>
                                <dx:ASPxGridView ID="GvDatosContacto" runat="server" AutoGenerateColumns="False" DataSourceID="LqsContacto" KeyFieldName="IDContacto" Theme="iOS">
<SettingsPopup>
<FilterControl AutoUpdatePosition="False"></FilterControl>
</SettingsPopup>
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" VisibleIndex="0">
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="IDContacto" ReadOnly="True" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                            <EditFormSettings Visible="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="CodCliente" ShowInCustomizationForm="True" Visible="False" VisibleIndex="2">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Cargo" ShowInCustomizationForm="True" VisibleIndex="3">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Nombre" ShowInCustomizationForm="True" VisibleIndex="4">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Apellido" ShowInCustomizationForm="True" VisibleIndex="5">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Telefono1" ShowInCustomizationForm="True" VisibleIndex="6">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Telefono2" ShowInCustomizationForm="True" VisibleIndex="7">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="EMail" ShowInCustomizationForm="True" VisibleIndex="8">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                </dx:ASPxGridView>
                                <asp:LinqDataSource ID="LqsContacto" runat="server" ContextTypeName="DB_Bmas.DBML_SMMWMSDataContext" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" TableName="SMM_Clientes_Contacto" Where="CodCliente == @CodCliente">
                                    <WhereParameters>
                                        <asp:SessionParameter DefaultValue="0" Name="CodCliente" SessionField="CodCliNew" Type="String" />
                                    </WhereParameters>
                                </asp:LinqDataSource>
                            </div>
                            <br />
                            <div style="margin-left: 100px">
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnContinuarContacto" runat="server" Text="Continuar" Theme="iOS" Height="50px" BackColor="#ff9900" OnClick="btnContinuarContacto_Click" CausesValidation="False">

                                                <Image IconID="scheduling_appointmentendcontinuearrow_svg_white_16x16">
                                                </Image>

                                            </dx:ASPxButton>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Cancelar" Theme="iOS" Height="50px" BackColor="#cccccc" CausesValidation="False" OnClick="ASPxButton2_Click">
                                                <Image IconID="outlookinspired_paymentunpaid_svg_white_32x32">
                                                </Image>
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage Text="Direccion">
                    <ContentCollection>
                        <dx:ContentControl runat="server">
                            <br />
                            <div style="margin-left: 150PX">
                                <dx:ASPxLabel ID="ASPxLabel28" runat="server" Text="DIRECCIÓN" Font-Size="XX-Large" Font-Bold="true"></dx:ASPxLabel>
                            </div>
                            <br />
                            <br />
                            <div>
                                <table>
                                    <tr>
                                        <td class="auto-style8">
                                            <dx:ASPxLabel ID="ASPxLabel22" runat="server" Text="Dirección: " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td class="auto-style8"></td>
                                        <td class="auto-style8">
                                            <dx:ASPxTextBox ID="txtDireccionCliNew" runat="server" Width="400px" Theme="iOS">
                                                <ValidationSettings Display="Dynamic">
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel18" runat="server" Text="Región: " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dx:ASPxComboBox ID="cboRegionNewCli" runat="server" ValueType="System.String" DataSourceID="LqsRegion" EnableTheming="True" TextField="Name" Theme="iOS" ValueField="Code" Width="400px">
                                                <ValidationSettings Display="Dynamic">
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxComboBox>
                                            <asp:LinqDataSource ID="LqsRegion" runat="server" ContextTypeName="DB_Bmas.DBML_SBO_MMetroDataContext" EntityTypeName="" TableName="OCST" Where="Country == @Country">
                                                <WhereParameters>
                                                    <asp:Parameter DefaultValue="CL" Name="Country" Type="String" />
                                                </WhereParameters>
                                            </asp:LinqDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel19" runat="server" Text="Ciudad: " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dx:ASPxTextBox ID="txtCiudadCliNew" runat="server" Width="300px" Theme="iOS">
                                                <ValidationSettings Display="Dynamic">
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel20" runat="server" Text="Comuna: " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dx:ASPxTextBox ID="txtComunaCliNew" runat="server" Width="300px" Theme="iOS">
                                                <ValidationSettings Display="Dynamic">
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel21" runat="server" Text="Tipo: " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dx:ASPxComboBox ID="cboTipoDireccionNew" runat="server" ValueType="System.String" CssClass="auto-style5" EnableTheming="True" Theme="iOS" Width="200px" SelectedIndex="0">
                                                <Items>
                                                    <dx:ListEditItem Text="Despacho" Value="S" Selected="True" />
                                                    <dx:ListEditItem Text="Facturación" Value="B" Selected="True" />
                                                </Items>
                                                <ValidationSettings Display="Dynamic">
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxComboBox>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <dx:ASPxCheckBox ID="chkDatFacturacion" runat="server" Text="Usar datos para Facturación?" Theme="iOS" Width="267px"></dx:ASPxCheckBox>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <div>
                                    <dx:ASPxButton ID="btnAgregarDir" runat="server" Text="Agregar" Theme="iOS" Height="30px" BackColor="#00bb3d" Width="100px" OnClick="btnAgregarDir_Click">
                                        <Image IconID="iconbuilder_actions_addcircled_svg_white_16x16">
                                        </Image>
                                    </dx:ASPxButton>
                                </div>
                                <br />
                            </div>
                            <br />
                            <div>
                                <dx:ASPxGridView ID="GvDatosDireccion" runat="server" AutoGenerateColumns="False" DataSourceID="LqsDireccionesCliente" KeyFieldName="CodCliente;CodDireccion" Theme="iOS">
                                    <SettingsCommandButton>
                                        <EditButton Text=" ">
                                            <Image IconID="iconbuilder_actions_edit_svg_32x32">
                                            </Image>
                                        </EditButton>
                                        <DeleteButton Text=" ">
                                            <Image IconID="iconbuilder_actions_deletecircled_svg_32x32">
                                            </Image>
                                        </DeleteButton>
                                    </SettingsCommandButton>
                                    <SettingsPopup>
                                        <FilterControl AutoUpdatePosition="False"></FilterControl>
                                    </SettingsPopup>
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" VisibleIndex="0">
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="CodCliente" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="1" Visible="False">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="CodDireccion" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="2" Visible="False">
                                            <EditFormSettings Visible="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Direccion" ShowInCustomizationForm="True" VisibleIndex="3">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Region" ShowInCustomizationForm="True" VisibleIndex="4">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Ciudad" ShowInCustomizationForm="True" VisibleIndex="5">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Comuna" ShowInCustomizationForm="True" VisibleIndex="6">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="TipoDir" ShowInCustomizationForm="True" VisibleIndex="7">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                </dx:ASPxGridView>
                                <asp:LinqDataSource ID="LqsDireccionesCliente" runat="server" ContextTypeName="DB_Bmas.DBML_SMMWMSDataContext" EntityTypeName="" TableName="SMM_Clientes_Direccion" Where="CodCliente == @CodCliente" EnableDelete="True" EnableInsert="True" EnableUpdate="True">
                                    <WhereParameters>
                                        <asp:SessionParameter DefaultValue="0" Name="CodCliente" SessionField="CodCliNew" Type="String" />
                                    </WhereParameters>
                                </asp:LinqDataSource>
                            </div>
                            <br />
                            <br />
                            <br />
                            <br />
                            <div style="margin-left: 100px">
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnCraerNw" runat="server" Text="Crear Cliente" Theme="iOS" Height="50px" BackColor="#178bff" OnClick="btnCraerNw_Click" CausesValidation="False">
                                                <Image IconID="save_saveall_svg_white_32x32">
                                                </Image>
                                            </dx:ASPxButton>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dx:ASPxButton ID="CancelarDir" runat="server" Text="Cancelar" Theme="iOS" Height="50px" BackColor="#cccccc" CausesValidation="False" OnClick="CancelarDir_Click">
                                                <Image IconID="outlookinspired_paymentunpaid_svg_white_32x32">
                                                </Image>
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
            </TabPages>
        </dx:ASPxPageControl>
    </div>
    <div>
        <dx:ASPxPopupControl ID="PopConfirmaCreaCliente" runat="server"
            HeaderText="¿Está seguro de crear Cliente?" AllowDragging="True" Theme="iOS" Width="500px" Modal="True" CloseAction="CloseButton" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle">
            <HeaderImage IconID="businessobjects_bo_attention_svg_32x32">
            </HeaderImage>
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <div style="margin-left: 25px">
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnConfirmaCreaClienteNo" runat="server" Text="Cancelar" Theme="iOS" Height="50px" Width="180px" BackColor="#cccccc" OnClick="btnConfirmaCreaClienteNo_Click" CausesValidation="False">
                                        <Image IconID="outlookinspired_cancel_svg_32x32">
                                        </Image>
                                    </dx:ASPxButton>
                                </td>
                                <td class="auto-style9"></td>
                                <td>
                                    <dx:ASPxButton ID="btnConfirmaCreaClienteSi" runat="server" Text="Aceptar" Height="50px" Theme="iOS" Width="180px" BackColor="#0075ea" OnClick="btnConfirmaCreaClienteSi_Click" CausesValidation="False">
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
        <dx:ASPxPopupControl ID="PopCancela" runat="server"
            HeaderText="¿Está seguro de Cancelar?" AllowDragging="True" Theme="iOS" Width="500px" Modal="True" CloseAction="CloseButton" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle">
            <HeaderImage IconID="businessobjects_bo_attention_svg_32x32">
            </HeaderImage>
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <div style="margin-left: 25px">
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnSiCancela" runat="server" Text="SI" Theme="iOS" Height="50px" Width="180px" BackColor="#cccccc" OnClick="btnSiCancela_Click" CausesValidation="False">
                                        <Image IconID="outlookinspired_cancel_svg_32x32">
                                        </Image>
                                    </dx:ASPxButton>
                                </td>
                                <td class="auto-style9"></td>
                                <td>
                                    <dx:ASPxButton ID="btnNoCancela" runat="server" Text="NO" Height="50px" Theme="iOS" Width="180px" BackColor="#0075ea" CausesValidation="False" OnClick="btnNoCancela_Click">
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
</asp:Content>
