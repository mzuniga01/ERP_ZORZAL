<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VentasentreFechas.aspx.cs" Inherits="ERP_GMEDINA.ReportViewer.VentasentreFechas" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" GroupTreeImagesFolderUrl="true" Height="1202px" ReportSourceID="CrystalReportSource1" ToolbarImagesFolderUrl="true" ToolPanelWidth="200px" Width="1104px" />
        <CR:CrystalReportSource runat="server" ID="CrystalReportSource1">
        <Report FileName="VentasentreFechas.rpt"></Report>
        </CR:CrystalReportSource>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="ERP_GMEDINA.Dataset.ReportesTableAdapters.UDV_Vent_Factura_VentasporFechaTableAdapter"></asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
