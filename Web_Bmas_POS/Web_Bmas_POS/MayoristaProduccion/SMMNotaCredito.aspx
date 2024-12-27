<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SMMNotaCredito.aspx.cs" Inherits="Web_Bmas_POS.MayoristaProduccion.SMMNotaCredito" %>

<%@ Register Assembly="DevExpress.Web.v22.1, Version=22.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style5 {
            width: 146px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntMaster" runat="server">
    <div id="breadcrumbs">
        <ul>
            <li>&nbsp;<a href="../Blank.aspx">Inicio</a></li>
            <li><a>Produccion</a></li>
            <li><span id="current">B+ NOTA CREDITO</span></li>
        </ul>
    </div>
    <div id="tableHeader">
        <span class="tableTitle">B+ NOTA CREDITO</span>
        <span class="tab-end"></span>
    </div>
    <br />
    <div style="margin-left: 150px">
        <dx:aspxradiobuttonlist id="rbTipoDoc" runat="server" enabletheming="True" itemspacing="5px" repeatdirection="Horizontal" theme="iOS" width="313px" border-borderstyle="None" autopostback="True" enableviewstate="False">
            <items>
                <dx:listedititem text="BOLETA" value="B" />
                <dx:listedititem text="FACTURA" value="F" />
            </items>
            <validationsettings validationgroup="a1" display="Dynamic" errortext="Seleccione" setfocusonerror="True">
                <errorimage iconid="iconbuilder_security_warningcircled1_svg_32x32">
                </errorimage>
                <requiredfield isrequired="True" errortext="Seleccione" />
            </validationsettings>

            <border borderstyle="None"></border>
        </dx:aspxradiobuttonlist>
    </div>
    <div style="margin-left: 150px">
        <table>
            <tr>
                <td>
                    <dx:aspxlabel id="ASPxLabel1" runat="server" text="Folio" theme="iOS"></dx:aspxlabel>
                    <dx:aspxtextbox id="txtfolio" runat="server" width="170px" theme="iOS">
                        <validationsettings display="Dynamic" validationgroup="a1">
                            <requiredfield isrequired="True" />
                        </validationsettings>
                    </dx:aspxtextbox>
                </td>
                <td></td>
                <td style="padding-top: 15px">
                    <dx:aspxbutton id="btnBuscar" runat="server" text="Buscar" height="47px" theme="iOS" width="154px" onclick="btnBuscar_Click" validationgroup="a1">
                        <image iconid="iconbuilder_actions_zoom_svg_white_32x32">
                        </image>
                    </dx:aspxbutton>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <div>
        <dx:aspxgridview id="GvDatosCliente" runat="server" datasourceid="LqsDatosCliente" autogeneratecolumns="False" keyfieldname="ID_NotaCredito" theme="iOS">
            <settingspopup>
                <filtercontrol autoupdateposition="False"></filtercontrol>
            </settingspopup>
            <settings showtitlepanel="True" />
            <settingstext title="Datos Cliente" />
            <columns>
                <dx:gridviewdatatextcolumn fieldname="ID_NotaCredito" readonly="True" visible="False" visibleindex="0">
                    <editformsettings visible="False" />
                </dx:gridviewdatatextcolumn>
                <dx:gridviewdatatextcolumn fieldname="FolioDocumento" visibleindex="1">
                </dx:gridviewdatatextcolumn>
                <dx:gridviewdatatextcolumn fieldname="TipoDocumento" visibleindex="2">
                </dx:gridviewdatatextcolumn>
                <dx:gridviewdatatextcolumn fieldname="CodCliente" visibleindex="3">
                </dx:gridviewdatatextcolumn>
                <dx:gridviewdatatextcolumn fieldname="NombreCliente" visibleindex="4">
                </dx:gridviewdatatextcolumn>
                <dx:gridviewdatatextcolumn fieldname="Direccion_Despacho" visibleindex="5">
                </dx:gridviewdatatextcolumn>
                <dx:gridviewdatatextcolumn fieldname="Direccion_Facturacion" visibleindex="6">
                </dx:gridviewdatatextcolumn>
                <dx:gridviewdatadatecolumn fieldname="FechaDocumento" visibleindex="7">
                </dx:gridviewdatadatecolumn>
            </columns>
            <styles>
                <header backcolor="#FF9900" font-bold="True" forecolor="Black">
                </header>
            </styles>
        </dx:aspxgridview>
        <asp:LinqDataSource ID="LqsDatosCliente" runat="server" EntityTypeName="" ContextTypeName="DB_Bmas.DBML_SMMWMSDataContext" TableName="SMM_NotaCredito_DatoCliente" Where="ID_NotaCredito == @ID_NotaCredito">
            <WhereParameters>
                <asp:SessionParameter DefaultValue="0" Name="ID_NotaCredito" SessionField="IdNota" Type="Int32" />
            </WhereParameters>
        </asp:LinqDataSource>
    </div>
    <br />
    <br />
    <div>
        <dx:aspxgridview id="GvDatosCompra" runat="server" autogeneratecolumns="False" datasourceid="LqsDatosCompra" keyfieldname="ID_NotaCredito;CodProducto" theme="iOS" onrowdeleting="GvDatosCompra_RowDeleting" onrowupdating="GvDatosCompra_RowUpdating">
            <settingsediting mode="Batch">
                <batcheditsettings keepchangesoncallbacks="True" starteditaction="Click" />
            </settingsediting>
            <settings showtitlepanel="True" />
            <settingstext title="Datos Compra" />
            <settings showfooter="True" />
            <settingsbehavior allowfocusedrow="True" confirmdelete="True" />
            <settingscommandbutton>
                <deletebutton text=" ">
                    <image iconid="iconbuilder_actions_deletecircled_svg_32x32">
                    </image>
                </deletebutton>
            </settingscommandbutton>
            <settingspopup>
                <filtercontrol autoupdateposition="False"></filtercontrol>
            </settingspopup>
            <columns>
                <dx:gridviewcommandcolumn showdeletebutton="True" visibleindex="0">
                </dx:gridviewcommandcolumn>
                <dx:gridviewdatatextcolumn fieldname="ID_NotaCredito" readonly="True" visible="False" visibleindex="1">
                    <editformsettings visible="False" />
                </dx:gridviewdatatextcolumn>
                <dx:gridviewdatatextcolumn fieldname="CodProducto" readonly="True" visibleindex="2">
                    <editformsettings visible="False" />
                </dx:gridviewdatatextcolumn>
                <dx:gridviewdatatextcolumn fieldname="Producto" visibleindex="3">
                    <editformsettings visible="False" />
                </dx:gridviewdatatextcolumn>
                <dx:gridviewdatatextcolumn fieldname="Cantidad" visibleindex="4">
                    <propertiestextedit displayformatstring="N2">
                    </propertiestextedit>
                </dx:gridviewdatatextcolumn>
                <dx:gridviewdatatextcolumn fieldname="Precio" visibleindex="5">
                    <propertiestextedit displayformatstring="N2">
                    </propertiestextedit>
                    <editformsettings visible="False" />
                </dx:gridviewdatatextcolumn>
                <dx:gridviewdatatextcolumn fieldname="Total" visibleindex="7">
                    <editformsettings visible="False" />
                </dx:gridviewdatatextcolumn>
                <dx:gridviewdatatextcolumn fieldname="Descuento" visibleindex="6">
                </dx:gridviewdatatextcolumn>
            </columns>
            <totalsummary>
                <dx:aspxsummaryitem displayformat="SubTotal : {0}" fieldname="Total" showincolumn="Total" summarytype="Sum" valuedisplayformat="N0" />
            </totalsummary>
            <styles>
                <header backcolor="#FF9900" font-bold="True" forecolor="Black">
                </header>
                <focusedrow backcolor="#FF9933">
                </focusedrow>
                <footer font-bold="True">
                </footer>
                <batcheditcell font-bold="True">
                </batcheditcell>
                <batcheditmodifiedcell font-bold="True">
                </batcheditmodifiedcell>
            </styles>
        </dx:aspxgridview>
        <asp:LinqDataSource ID="LqsDatosCompra" runat="server" ContextTypeName="DB_Bmas.DBML_SMMWMSDataContext" EntityTypeName="" TableName="SMM_NotaCredito_DatoCompra" Where="ID_NotaCredito == @ID_NotaCredito" EnableDelete="True" EnableInsert="True" EnableUpdate="True">
            <WhereParameters>
                <asp:SessionParameter DefaultValue="0" Name="ID_NotaCredito" SessionField="IdNota" Type="Int32" />
            </WhereParameters>
        </asp:LinqDataSource>
    </div>
    <br />
    <br />
    <div style="margin-left: 150px">
        <table>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style5"></td>
                <td style="padding-top: 15px">
                    <dx:aspxbutton id="btnConfirma" runat="server" text="Confirma" theme="iOS" height="45px" width="166px" onclick="btnConfirma_Click">
                        <image iconid="diagramicons_check_svg_white_32x32">
                        </image>
                    </dx:aspxbutton>
                </td>
                <td></td>
                <td style="padding-top: 15px">
                    <dx:aspxbutton id="btnAnula" runat="server" text="Anula" theme="iOS" height="45px" width="159px" backcolor="#ff3300" onclick="btnAnula_Click">
                        <image iconid="iconbuilder_actions_deletecircled_svg_white_32x32">
                        </image>
                    </dx:aspxbutton>
                </td>

            </tr>
        </table>
    </div>
    <div>
        <dx:aspxpopupcontrol id="popAnulaNota" runat="server"
            headertext="¿Está seguro que desea anular?" allowdragging="True" theme="iOS" width="500px" modal="True" closeaction="CloseButton" popuphorizontalalign="WindowCenter" popupverticalalign="Middle">
            <headerimage iconid="businessobjects_bo_attention_svg_32x32">
            </headerimage>
            <contentcollection>
                <dx:popupcontrolcontentcontrol runat="server">
                    <div style="margin-left: 25px">
                        <table>
                            <tr>
                                <td>
                                    <dx:aspxbutton id="btnNoAnula" runat="server" text="Cancelar" theme="iOS" height="50px" width="180px" backcolor="#cccccc" onclick="btnNoAnula_Click">
                                        <image iconid="outlookinspired_cancel_svg_32x32">
                                        </image>
                                    </dx:aspxbutton>
                                </td>
                                <td class="auto-style9"></td>
                                <td>
                                    <dx:aspxbutton id="btnSiAnula" runat="server" text="Aceptar" height="50px" theme="iOS" width="180px" backcolor="#0075ea" onclick="btnSiAnula_Click">
                                        <image iconid="iconbuilder_actions_check_svg_32x32">
                                        </image>
                                    </dx:aspxbutton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </dx:popupcontrolcontentcontrol>
            </contentcollection>
        </dx:aspxpopupcontrol>
    </div>
</asp:Content>
