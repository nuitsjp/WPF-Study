<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThrowError.aspx.cs" Inherits="EchoUaWebApp.ThrowError" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<script lang="JavaScript">
<!--
function throwError(){
    throw new Error("エラー発生");
}
// -->
</script>
<body onload="throwError()">
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
