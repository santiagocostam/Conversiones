<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="ConvII._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   

            <div class="navbar navbar-inverse navbar-fixed-top">
            <div class="container">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" runat="server" href="~/"><font color="orange">Menú Principal</font></a>
                </div>
                <div class="navbar-collapse collapse">
                    <ul class="nav navbar-nav">
                        <li><a runat="server" href="~/">Sistemas</a></li>
                        <li><a runat="server" href="~/Paginas/Entidades.aspx">Entidades</a></li>
                        <li><a runat="server" href="~/Contact">Conversiones</a></li>
                    </ul>
                </div>
            </div>
        </div>
    <div>
<asp:TextBox ID="TextBox1" runat="server" Height="275" BorderWidth="0"></asp:TextBox>
    </div>
    

</asp:Content>
