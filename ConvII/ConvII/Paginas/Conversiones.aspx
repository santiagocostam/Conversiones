<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Conversiones.aspx.vb" Inherits="ConvII.Conversiones" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
                    <div>
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True"></asp:DropDownList></div>
        <div></div>
         <div>
        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True"></asp:DropDownList></div>
         <div></div>
       
        
       <div>
        <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True"></asp:DropDownList></div>
        <div></div>
        <div>
       <table class="table"><asp:GridView ID="GridDatos" runat="server"></asp:GridView> </table>
        </div>
        <div>
        <asp:Button ID="Button1" runat="server" Text="Agregar" /></div><div><asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox><div><asp:Button ID="Button2" runat="server" Text="Grabar" Visible="False" /></div>
        </div>
        </div>
    </form>
</body>
</html>
