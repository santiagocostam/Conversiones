Public Class Entidades
    Inherits System.Web.UI.Page
    Dim WithEvents Man As New Mantenimientos
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Page.IsPostBack) = False Then
            llenarCombos()

        End If

    End Sub
    Public Sub llenarCombos()
        Dim tabla As New DataTable
        tabla = Man.verEntidadesTodas

        cmbEntidadEditar.DataSource = tabla
        cmbEntidadEditar.DataValueField = "entidadid"
        cmbEntidadEditar.DataTextField = "entidadnombre"
        cmbEntidadEditar.DataBind()

        cmbEntidadEliminar.DataSource = tabla
        cmbEntidadEliminar.DataValueField = "entidadid"
        cmbEntidadEliminar.DataTextField = "entidadnombre"
        cmbEntidadEliminar.DataBind()

    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Dim idEntidad As Integer, siExiste As Boolean = False

        siExiste = Man.verificarNombreEntidad(txtNuevaEntidad.Text)
        If siExiste = False Then
            idEntidad = Man.buscarIdEntidadMax
            lblAgregarScript.Text = "USE [Conversiones_ESB]" & vbCrLf & "GO" & vbCrLf & "INSERT INTO [dbo].[ENTIDAD]" & vbCrLf & " ([ENTIDADID] ,[ENTIDADNOMBRE])" & vbCrLf & "  VALUES (" & idEntidad & ",'" & txtNuevaEntidad.Text & "') GO"
        Else
            MsgBox("Nombre de entidad ya existente", vbCritical, "BANCOR")
            txtNuevaEntidad.Text = ""
        End If
    End Sub


    Private Sub cmbEntidadEditar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEntidadEditar.SelectedIndexChanged
        txtEditarEntidad.Visible = True
        btnEditar.Visible = True
    End Sub

    Private Sub btnEditar_Click(sender As Object, e As EventArgs) Handles btnEditar.Click
        Dim siExiste As Boolean = False


        If txtEditarEntidad.Text <> "" Then
            siExiste = Man.verificarNombreEntidad(txtEditarEntidad.Text)
            If siExiste = False Then
                lblEditarScript.Text = "USE [Conversiones_ESB]" & vbCrLf & "GO" & vbCrLf & "UPDATE [dbo].[ENTIDAD]" & vbCrLf & "SET [ENTIDADNOMBRE]  = '" & txtEditarEntidad.Text & "'" & vbCrLf & "WHERE [ENTIDADID] =" & cmbEntidadEditar.SelectedValue.ToString & "   GO"
            Else
                MsgBox("Nombre de entidad ya existente", vbCritical, "BANCOR")
                txtEditarEntidad.Text = ""
            End If
        End If
    End Sub

    'Private Sub cmbEntidadEliminar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEntidadEliminar.SelectedIndexChanged
    '    txtEditarEntidad.Visible = True
    '    btnEditar.Visible = True
    'End Sub
End Class