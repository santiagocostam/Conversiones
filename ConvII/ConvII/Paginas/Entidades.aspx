﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Entidades.aspx.vb" Inherits="ConvII.Entidades" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<div>
       
</div>
<div>
<table style="width:100%;border-style:solid">
  <tr>
    <th style="width:33%"><font face="Verdana" color="E74617">Nueva Entidad</font></th>
    <th style="width:33%"><font face="Verdana" color="E74617">Editar Entidad</font></th>
    <th style="width:33%"><font face="Verdana" color="E74617">Borrar Entidad</font></th>
  </tr>
  <tr>
    <td>
      <asp:TextBox ID="txtNuevaEntidad" runat="server" Width="325px"></asp:TextBox>
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" BackColor="#5cb85c" Font-Bold="true" Font-Size="Small" ForeColor="White" width="70"/></td>
      <td><asp:DropDownList ID="cmbEntidadEditar" runat="server" AutoPostBack="True" Width="402px"></asp:DropDownList></td>
       <td><asp:DropDownList ID="cmbEntidadEliminar" runat="server" AutoPostBack="True" Width="333px"></asp:DropDownList>
           <asp:Button ID="btnEliminar" runat="server" Text="Borrar" BackColor="#5cb85c" Font-Bold="true" Font-Size="Small" ForeColor="White" width="70"/></td>
  </tr>
    <tr><td>

        </td>
        <td>
            <asp:TextBox ID="txtEditarEntidad" runat="server" Width="320"/>
             <asp:Button ID="btnEditar" runat="server" width="70" Text="Editar" BackColor="#5cb85c" Font-Bold="true" Font-Size="Small" ForeColor="White" />
        </td>
        
            
        
    </tr>
    <tr><td> <font color="white"> -</font></td>
    </tr>
  <tr>
     
      <td>
          <asp:TextBox ID="txtAgregarScript" runat="server" TextMode="MultiLine" Height="280px" Width="400px"></asp:TextBox>
        </td>
      <td>
          <asp:TextBox ID="txtEditarScript" runat="server" TextMode="MultiLine" Height="280px" Width="400px"></asp:TextBox>
        </td>
       <td>
           <asp:TextBox ID="txtBorrarScript" runat="server" TextMode="MultiLine" Height="280px" Width="400px"></asp:TextBox>
       </td>
  </tr>
    

</table>
    

</div>


</asp:Content>
