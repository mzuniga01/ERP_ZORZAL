<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InventarioNumeraciones.aspx.cs" Inherits="ERP_GMEDINA.ReportViewer.InventarioNumeraciones" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div style="height: 254px">
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ReportSourceID="CrystalReportSource1" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="Reports\InventarioNumeraciones.rpt">
            </Report>
        </CR:CrystalReportSource>
    </div>
    </div>
    </form>
</body>
</html>
