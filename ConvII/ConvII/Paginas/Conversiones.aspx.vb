Public Class Conversiones
    Inherits System.Web.UI.Page

    Dim WithEvents Man As New Mantenimientos
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If (Page.IsPostBack) = False Then
            Dim tabla As New DataTable
            tabla = Man.verDominios

            DropDownList1.DataSource = tabla
            DropDownList1.DataValueField = "dominioid"
            DropDownList1.DataTextField = "dominionombre"
            DropDownList1.DataBind()
        End If


    End Sub


    Private Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged


        Dim tabla As New DataTable
        tabla = Man.verEntidades(DropDownList1.SelectedValue)
        DropDownList2.DataSource = tabla
        DropDownList2.DataValueField = "entidadid"
        DropDownList2.DataTextField = "entidadnombre"
        DropDownList2.DataBind()

    End Sub

    Private Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged
        Dim tabla As New DataTable
        tabla = Man.verSistemas(DropDownList2.SelectedValue)
        DropDownList3.DataSource = tabla
        DropDownList3.DataValueField = "SISTEMAID_DESTINO"
        DropDownList3.DataTextField = "SISTEMANOMBRE"
        DropDownList3.DataBind()

    End Sub

    Private Sub DropDownList3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList3.SelectedIndexChanged
        Dim tabla As New DataTable
        tabla = Man.verConversiones(DropDownList3.SelectedValue.ToString, DropDownList2.SelectedValue)
        tabla.Columns.Add()
        GridDatos.DataSource = tabla
        GridDatos.DataBind()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Visible = True
        Button2.Visible = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Insert


        'Llanar combo

    End Sub

End Class