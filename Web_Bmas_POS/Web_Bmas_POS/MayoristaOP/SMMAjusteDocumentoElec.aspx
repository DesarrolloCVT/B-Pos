<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SMMAjusteDocumentoElec.aspx.cs" Inherits="Web_Bmas_POS.MayoristaOP.SMMAjusteDocumentoElec" %>

<%@ Register Assembly="DevExpress.Web.v22.1, Version=22.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntMaster" runat="server">
    <div id="breadcrumbs">
        <ul>
            <li><a href="../Blank.aspx">Inicio</a></li>
            <li><a>WMS</a></li>
            <li><span id="current">Ajuste Documento</span></li>
        </ul>
    </div>

    <div id="tableHeader">
        <span class="tableTitle">Ajuste Documento</span>
        <span class="tab-end"></span>
    </div>
    <br />
    <br />
    <div style="margin-left: 150px">
        <table>
            <tr>
                <td>
                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="N° Preventa"></dx:ASPxLabel>
                    <dx:ASPxTextBox ID="txtPrevID" runat="server" Width="170px" Theme="iOS">
                        <ValidationSettings Display="Dynamic" ValidationGroup="a1">
                            <RegularExpression ValidationExpression="\d+" />
                            <RequiredField ErrorText="ingrese numero de preventa" IsRequired="True" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </td>
                <td></td>
                <td>
                    <dx:ASPxButton ID="BtnBuscar" runat="server" Text="Buscar" Height="48px" Theme="iOS" Width="73px" OnClick="BtnBuscar_Click"></dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <div>
        <dx:ASPxButton ID="btnAjustar" runat="server" Text="Ajustar" Height="46px" OnClick="btnAjustar_Click" ValidationGroup="a1" Width="167px"></dx:ASPxButton>
    </div>
    <div>
        <dx:ASPxGridView ID="GvDatos" runat="server" AutoGenerateColumns="False" DataSourceID="LqsDatosAjuste" KeyFieldName="Preventa_ID;ItemCode">
            <SettingsPager PageSize="100">
            </SettingsPager>
            <SettingsPopup>
                <FilterControl AutoUpdatePosition="False"></FilterControl>
            </SettingsPopup>
            <Columns>
                <dx:GridViewDataTextColumn Caption="N° Preventa" FieldName="Preventa_ID" VisibleIndex="0">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Cod.Producto" FieldName="ItemCode" VisibleIndex="1">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Producto" FieldName="ItemName" VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="StockSala" VisibleIndex="3">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CantUnidades" VisibleIndex="7">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Faltante" VisibleIndex="8">
                </dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>
        <asp:LinqDataSource ID="LqsDatosAjuste" runat="server" ContextTypeName="DB_Bmas.DBML_SBO_MMetroDataContext" EntityTypeName="" TableName="SMM_Ajuste_Documento_Electronico" Where="Preventa_ID == @Preventa_ID">
            <WhereParameters>
                <asp:ControlParameter ControlID="txtPrevID" DefaultValue="0" Name="Preventa_ID" PropertyName="Text" Type="Int32" />
            </WhereParameters>
        </asp:LinqDataSource>
    </div>
</asp:Content>
