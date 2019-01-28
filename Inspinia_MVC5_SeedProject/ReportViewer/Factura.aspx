<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Factura.aspx.cs" Inherits="ERP_GMEDINA.ReportViewer.Factura" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
            <LocalReport ReportPath="Reports\PrintingFactura.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="Factura" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="ERP_GMEDINA.Dataset.FacturaTableAdapters.tbFacturaTableAdapter" UpdateMethod="Update">
            <DeleteParameters>
                <asp:Parameter Name="Original_fact_Id" Type="Int64" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="fact_Codigo" Type="String" />
                <asp:Parameter Name="fact_Fecha" Type="DateTime" />
                <asp:Parameter Name="esfac_Id" Type="Byte" />
                <asp:Parameter Name="cja_Id" Type="Int16" />
                <asp:Parameter Name="suc_Id" Type="Int32" />
                <asp:Parameter Name="clte_Id" Type="Int32" />
                <asp:Parameter Name="pemi_NumeroCAI" Type="String" />
                <asp:Parameter Name="fact_AlCredito" Type="Boolean" />
                <asp:Parameter Name="fact_DiasCredito" Type="Int32" />
                <asp:Parameter Name="fact_PorcentajeDescuento" Type="Decimal" />
                <asp:Parameter Name="fact_Vendedor" Type="String" />
                <asp:Parameter Name="clte_Identificacion" Type="String" />
                <asp:Parameter Name="clte_Nombres" Type="String" />
                <asp:Parameter Name="fact_IdentidadTE" Type="String" />
                <asp:Parameter Name="fact_NombresTE" Type="String" />
                <asp:Parameter Name="fact_FechaNacimientoTE" Type="DateTime" />
                <asp:Parameter Name="fact_UsuarioAutoriza" Type="Int32" />
                <asp:Parameter Name="fact_FechaAutoriza" Type="DateTime" />
                <asp:Parameter Name="fact_EsAnulada" Type="Boolean" />
                <asp:Parameter Name="fact_RazonAnulado" Type="String" />
                <asp:Parameter Name="fact_UsuarioCrea" Type="Int32" />
                <asp:Parameter Name="fact_FechaCrea" Type="DateTime" />
                <asp:Parameter Name="fact_UsuarioModifica" Type="Int32" />
                <asp:Parameter Name="fact_FechaModifica" Type="DateTime" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="fact_Codigo" Type="String" />
                <asp:Parameter Name="fact_Fecha" Type="DateTime" />
                <asp:Parameter Name="esfac_Id" Type="Byte" />
                <asp:Parameter Name="cja_Id" Type="Int16" />
                <asp:Parameter Name="suc_Id" Type="Int32" />
                <asp:Parameter Name="clte_Id" Type="Int32" />
                <asp:Parameter Name="pemi_NumeroCAI" Type="String" />
                <asp:Parameter Name="fact_AlCredito" Type="Boolean" />
                <asp:Parameter Name="fact_DiasCredito" Type="Int32" />
                <asp:Parameter Name="fact_PorcentajeDescuento" Type="Decimal" />
                <asp:Parameter Name="fact_Vendedor" Type="String" />
                <asp:Parameter Name="clte_Identificacion" Type="String" />
                <asp:Parameter Name="clte_Nombres" Type="String" />
                <asp:Parameter Name="fact_IdentidadTE" Type="String" />
                <asp:Parameter Name="fact_NombresTE" Type="String" />
                <asp:Parameter Name="fact_FechaNacimientoTE" Type="DateTime" />
                <asp:Parameter Name="fact_UsuarioAutoriza" Type="Int32" />
                <asp:Parameter Name="fact_FechaAutoriza" Type="DateTime" />
                <asp:Parameter Name="fact_EsAnulada" Type="Boolean" />
                <asp:Parameter Name="fact_RazonAnulado" Type="String" />
                <asp:Parameter Name="fact_UsuarioCrea" Type="Int32" />
                <asp:Parameter Name="fact_FechaCrea" Type="DateTime" />
                <asp:Parameter Name="fact_UsuarioModifica" Type="Int32" />
                <asp:Parameter Name="fact_FechaModifica" Type="DateTime" />
                <asp:Parameter Name="Original_fact_Id" Type="Int64" />
            </UpdateParameters>
        </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
