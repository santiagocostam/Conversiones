<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Entidades.aspx.vb" Inherits="ConvII.Entidades" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<div>
       
</div>
<div>
<table style="width:100%;border-style:solid">
  <tr>
    <th style="width:33%">Nueva Entidad</th>
    <th style="width:33%">Editar Entidad</th>
    <th style="width:33%">Borrar Entidad</th>
  </tr>
  <tr>
    <td>
      <asp:TextBox ID="txtNuevaEntidad" runat="server" Width="350px"></asp:TextBox><asp:Button ID="btnAgregar" runat="server" Text="Agregar" /></td>
      <td><asp:DropDownList ID="cmbEntidadEditar" runat="server" AutoPostBack="True" Width="400px"></asp:DropDownList></td>
       <td><asp:DropDownList ID="cmbEntidadEliminar" runat="server" AutoPostBack="True" Width="400px"></asp:DropDownList></td>
  </tr>
    <tr><td>

        </td>
        <td>
            <asp:TextBox ID="txtEditarEntidad" runat="server" Visible="False" Width="300"></asp:TextBox>
            <asp:Button ID="btnEditar" runat="server" Text="Editar" Visible="False" />
        </td>
         <td>
            <asp:Button ID="btnEliminar" runat="server" Text="Borrar" Visible="False" />
        </td>
    </tr>
    <tr><td> <font color="white"> -</font></td>
    </tr>
  <tr>
     
      <td> <asp:TextBox ID="txtAgregarScript" runat="server" TextMode="MultiLine" Height="300px" Width="400px"></asp:TextBox>
        <asp:Label ID="lblAgregarScript" runat="server" Text=""></asp:Label></td>
      <td>
          <asp:TextBox ID="txtEditarScript" runat="server" TextMode="MultiLine" Height="300px" Width="400px"></asp:TextBox>
        <asp:Label ID="lblEditarScript" runat="server" Text=""></asp:Label></td>
       <td>
           <asp:TextBox ID="txtBorrarScript" runat="server" TextMode="MultiLine" Height="300px" Width="400px"></asp:TextBox>
        <asp:Label ID="lblBorrarScript" runat="server" Text=""></asp:Label></td>
  </tr>
    

</table>
    

</div>


</asp:Content>
