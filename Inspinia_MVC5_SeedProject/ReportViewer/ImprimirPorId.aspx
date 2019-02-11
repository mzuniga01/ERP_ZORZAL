<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImprimirPorId.aspx.cs" Inherits="ERP_GMEDINA.ReportViewer.ImprimirPorId" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">

        </asp:ScriptManager>
    <div>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1600px" Height="900px">
       
    </rsweb:ReportViewer>
        
    </div>
    </form>
</body>
</html>
