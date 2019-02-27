﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImprimirEntradaDevolucion.aspx.cs" Inherits="ERP_GMEDINA.ReportViewer.ImprimirEntradaDevolucion" %>

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
        <rsweb:ReportViewer ID="ReportViewer1" runat="server"  Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1600px" Height="900px">
            <localreport reportpath="Reports\ImprimirEntradaDevolucion.rdlc">
                <datasources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="DataSet1" />
                </datasources>
            </localreport>

        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="ERP_GMEDINA.EntradaTableAdapters.SDP_tbentradaImprimirCompra_SelectTableAdapter">
            <SelectParameters>
                <asp:Parameter DefaultValue="2" Name="TipoEntrada" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="ERP_GMEDINA.EntradaTableAdapters.SDP_tbentradaImprimirCompra_SelectTableAdapter"></asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
