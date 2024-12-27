<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SMMCreaCliente.aspx.cs" Inherits="Web_Bmas_POS.MayoristaProduccion.SMMCreaCliente" %>

<%@ Register Assembly="DevExpress.Web.v22.1, Version=22.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntMaster" runat="server">
     <div id="breadcrumbs">
        <ul>
            <li>&nbsp;<a href="../Blank.aspx">Inicio</a></li>
            <li><a>Produccion</a></li>
            <li><span id="current">B+ CREA CLIENTE</span></li>
        </ul>
    </div>

    <div id="tableHeader">
        <span class="tableTitle">B+ CREA CLIENTE</span>
        <span class="tab-end"></span>
    </div>
    <br />
      <div style="margin-left: 20px">          
        <dx:ASPxPageControl ID="citiesTabPage" Width="100%" runat="server" CssClass="dxtcFixed" ActiveTabIndex="0" EnableHierarchyRecreation="True" Theme="iOS">
            <TabPages>
                <dx:TabPage Text="Principal">
                    <ContentCollection>
                        <dx:ContentControl runat="server">
                            <br />
                            <div>
                                <table>

                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="lblr" runat="server" Text="*Rut: " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dx:ASPxTextBox runat="server" Width="200px" MaxLength="10" Height="30px" ID="txtRutCliNew" Theme="iOS" AutoPostBack="True" OnTextChanged="txtRutCliNew_TextChanged">
                                                <MaskSettings Mask="00000000-a"></MaskSettings>

                                                <ValidationSettings Display="Dynamic">
                                                    <RegularExpression ErrorText="ingrese rut" ValidationExpression="^[0-9]+-[0-9kK]{1}$"></RegularExpression>

                                                    <RequiredField IsRequired="True"></RequiredField>
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="*Nombre: " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dx:ASPxTextBox ID="txtNombreCliNew" runat="server" Width="400px" Theme="iOS">
                                                <ValidationSettings Display="Dynamic">
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
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
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="*Razon Social: " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dx:ASPxTextBox ID="txtRazonCliNew" runat="server" Width="400px" Theme="iOS">
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="*Grupo Cliente: " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dx:ASPxComboBox ID="cboGrupoClienteNew" runat="server" ValueType="System.String" DataSourceID="LqsGrupioCli" EnableTheming="True" TextField="GroupName" Theme="iOS" ValueField="GroupCode" Width="200px">
                                                <ValidationSettings Display="Dynamic">
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxComboBox>
                                            <asp:LinqDataSource ID="LqsGrupioCli" runat="server" ContextTypeName="DB_Bmas.DBML_SBO_MMetroDataContext" EntityTypeName="" TableName="OCRG" Where="GroupCode =112 OR GroupCode =111 OR GroupCode =113 OR GroupCode =114 OR GroupCode =118">
                                            </asp:LinqDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="*Giro: " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dx:ASPxTextBox ID="txtGirocliNew" runat="server" Width="400px" Theme="iOS">
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="*Telefono: " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dx:ASPxTextBox ID="txtTelefonoCliNew" runat="server" Width="200px" Theme="iOS" MaxLength="9">
                                                <ValidationSettings Display="Dynamic">
                                                    <RegularExpression ValidationExpression="\d+" />
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="Email: " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dx:ASPxTextBox ID="txtEmailCliNew" runat="server" Width="399px" Theme="iOS">
                                                <ValidationSettings Display="Dynamic">
                                                    <RegularExpression ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
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
                <dx:TabPage Text="Direccion">
                    <ContentCollection>
                        <dx:ContentControl runat="server">
                            <br />
                            <div>
                                <table>
                                    <tr>
                                        <td class="auto-style8">
                                            <dx:ASPxLabel ID="ASPxLabel22" runat="server" Text="Dirección: " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td class="auto-style8"></td>
                                        <td class="auto-style8">
                                            <dx:ASPxTextBox ID="txtDireccionCliNew" runat="server" Width="400px" Theme="iOS" OnTextChanged="txtDireccionCliNew_TextChanged">
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel18" runat="server" Text="Región: " Font-Bold="true" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dx:ASPxComboBox ID="cboRegionNewCli" runat="server" ValueType="System.String" DataSourceID="LqsRegion" EnableTheming="True" TextField="Name" Theme="iOS" ValueField="Code" Width="400px"></dx:ASPxComboBox>
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
                                                <ValidationSettings ValidationGroup="AgregarDir">
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
                                                    <dx:ListEditItem Text="Envío" Value="S" Selected="True" />
                                                    <dx:ListEditItem Text="Facturación" Value="B" Selected="True" />
                                                </Items>
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
                                    <dx:ASPxButton ID="btnAgregarDir" runat="server" Text="Agregar" Theme="iOS" Height="30px" BackColor="#00bb3d" Width="100px" OnClick="btnAgregarDir_Click" ValidationGroup="AgregarDir">
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
                            <div style="margin-left: 100px">
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnCraerNw" runat="server" Text="Crear Cliente" Theme="iOS" Height="50px" BackColor="#178bff" OnClick="btnCrearNewCli_Click">
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
                                    <dx:ASPxButton ID="btnConfirmaCreaClienteSi" runat="server" Text="Cancelar" Theme="iOS" Height="50px" Width="180px" BackColor="#cccccc" OnClick="btnConfirmaCreaClienteSi_Click">
                                        <Image IconID="outlookinspired_cancel_svg_32x32">
                                        </Image>
                                    </dx:ASPxButton>
                                </td>
                                <td class="auto-style9"></td>
                                <td>
                                    <dx:ASPxButton ID="btnConfirmaCreaClienteNo" runat="server" Text="Aceptar" Height="50px" Theme="iOS" Width="180px" BackColor="#0075ea" OnClick="btnConfirmaCreaClienteNo_Click">
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
