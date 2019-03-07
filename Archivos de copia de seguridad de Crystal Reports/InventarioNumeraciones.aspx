<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InventarioNumeraciones.aspx.cs" Inherits="ERP_GMEDINA.ReportViewer.InventarioNumeraciones" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div style="height: 254px">

        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="Reports\InventarioNumeraciones.rpt">
            </Report>
        </CR:CrystalReportSource>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ReportSourceID="CrystalReportSource1" />

    </div>
    </div>
    </form>
</body>
</html>
