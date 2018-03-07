<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StringCompress.aspx.cs"
    Inherits="WebTest.StringCompress" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td>
                <asp:Button ID="btnCompressed" runat="server" Text="压缩" 
                    onclick="btnCompressed_Click" />
                长度：<asp:Label ID="lblOriginalLength" runat="server" Text="0"></asp:Label>
            </td>
            <td>
                长度：<asp:Label ID="lblCompressed" runat="server" Text="0"></asp:Label>
                压缩率：<asp:Label ID="lblCompressRate" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:Button ID="btnDeCompressed" runat="server" Text="解压" 
                    onclick="btnDeCompressed_Click" />
                长度：<asp:Label ID="lblDeCompressed" runat="server" Text="0"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtOriginal" runat="server" Height="404px" Width="320px" 
                    TextMode="MultiLine"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtCompressed" runat="server" Height="404px" Width="320px" 
                    TextMode="MultiLine"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtDeCompressed" runat="server" Height="404px" Width="320px" 
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div>
    </div>
    </form>
</body>
</html>
