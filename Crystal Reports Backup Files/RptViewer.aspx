<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptViewer.aspx.cs" Inherits="AppProj.Web.Reports.General.RptViewer" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   <div>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" Style="z-index: 0;
            left: 0px; position: absolute; top: 0px" HasCrystalLogo="False" HasDrillUpButton="False"
            HasRefreshButton="False" ToolPanelView="None" PrintMode="Pdf" OnUnload="CrystalReportViewer1_Unload" />
    </div>
    </form>
</body>
</html>
