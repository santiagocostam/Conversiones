Imports System
Imports System.IO
Imports System.Text
Imports System.Drawing
Imports Microsoft.Win32
Public Class Entidades
    Inherits System.Web.UI.Page
    Dim WithEvents Man As New Mantenimientos

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Page.IsPostBack) = False Then
            Me.BindGrid()
            llenarCombos()

        End If

    End Sub
    Public Sub llenarCombos()
        Dim tabla As New DataTable
        tabla = Man.verEntidadesTodas

        cmbEntidadEditar.DataSource = tabla
        cmbEntidadEditar.DataValueField = "entidadid"
        cmbEntidadEditar.DataTextField = "nombre"
        cmbEntidadEditar.DataBind()

        cmbEntidadEliminar.DataSource = tabla
        cmbEntidadEliminar.DataValueField = "entidadid"
        cmbEntidadEliminar.DataTextField = "nombre"
        cmbEntidadEliminar.DataBind()

        cmbVerEntidad.DataSource = tabla
        cmbVerEntidad.DataValueField = "entidadid"
        cmbVerEntidad.DataTextField = "nombre"
        cmbVerEntidad.DataBind()

        'Limpieza
        txtEditarEntidad.Text = ""
        txtNuevaEntidad.Text = ""

        'txtEditarEntidad.Visible = False
        'btnEditar.Visible = False

    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Dim idEntidad As Integer, siExiste As Boolean = False, sScript As String = "", sScriptBase As String = "", sScriptOracle As String = ""
        ' Dim path2 As String = "C:\eMozart\EliminarEntidad" & cmbEntidadEliminar.SelectedItem.Text & "plSQL.txt"

        siExiste = Man.verificarNombreEntidad(txtNuevaEntidad.Text)
        If siExiste = False Then
            idEntidad = Man.buscarIdEntidadMax
            sScriptBase = "INSERT INTO [dbo].[ENTIDAD] ([ENTIDADID] ,[ENTIDADNOMBRE]) VALUES (" & idEntidad & ",'" & txtNuevaEntidad.Text & "')"
            sScript = "GO" & vbCrLf &
            "INSERT INTO [dbo].[ENTIDAD]" & vbCrLf &
            " ([ENTIDADID] ,[ENTIDADNOMBRE])" & vbCrLf &
            "  VALUES (" & idEntidad & ",'" & txtNuevaEntidad.Text & "')" & vbCrLf &
            "GO"
            txtAgregarScript.Text = sScript

            Dim path As String = "C:\eMozart\AgregarEntidad" & txtNuevaEntidad.Text & ".txt"
            crearTxtScript(path, sScript)
            Man.abmEntidad(sScriptBase)
            llenarCombos()
        Else
            MsgBox("Nombre de entidad ya existente", vbCritical, "BANCOR")
            txtNuevaEntidad.Text = ""
        End If
    End Sub


    Private Sub cmbEntidadEditar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEntidadEditar.SelectedIndexChanged
        txtEditarEntidad.Visible = True
        btnEditar.Visible = True


        txtEditarScript.Text = ""

    End Sub

    Private Sub btnEditar_Click(sender As Object, e As EventArgs) Handles btnEditar.Click
        Dim siExiste As Boolean = False, sScriptBase As String = "", sScriptOracle As String = ""
        Dim path As String = "C:\eMozart\EditarEntidad" & txtEditarEntidad.Text & ".txt"
        ' Dim path2 As String = "C:\eMozart\EliminarEntidad" & cmbEntidadEliminar.SelectedItem.Text & "plSQL.txt"


        If txtEditarEntidad.Text <> "" Then
            siExiste = Man.verificarNombreEntidad(txtEditarEntidad.Text)
            If siExiste = False Then
                sScriptBase = "UPDATE [dbo].[ENTIDAD] Set  [ENTIDADNOMBRE]  = '" & txtEditarEntidad.Text & "' WHERE [ENTIDADID] =" & cmbEntidadEditar.SelectedValue & " "
                'sScriptOracle = "UPDATE ENTIDAD Set ENTIDADNOMBRE  = '" & txtEditarEntidad.Text & "' WHERE ENTIDADID =" & cmbEntidadEditar.SelectedValue & ";COMMIT; "
                txtEditarScript.Text = "USE [Conversiones_ESB]" & vbCrLf & "GO" & vbCrLf & "UPDATE [dbo].[ENTIDAD]" & vbCrLf & "SET [ENTIDADNOMBRE]  = '" & txtEditarEntidad.Text & "'" & vbCrLf & "WHERE [ENTIDADID] =" & cmbEntidadEditar.SelectedValue & "" & vbCrLf & "GO"

                crearTxtScript(path, txtEditarScript.Text)
                Man.abmEntidad(sScriptBase)

                llenarCombos()

            Else
                MsgBox("Nombre de entidad ya existente", vbCritical, "BANCOR")
                txtEditarEntidad.Text = ""
            End If


        End If
    End Sub

    Private Sub cmbEntidadEliminar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEntidadEliminar.SelectedIndexChanged

        btnEliminar.Visible = True


        txtBorrarScript.Text = ""
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Dim iReturnValue As Integer, dtSistemas As New DataTable, dtConversiones As New DataTable, sScript As String = "", sScriptBase As String = "", sScriptOracle As String = ""
        Dim path As String = "C:\eMozart\EliminarEntidad" & cmbEntidadEliminar.SelectedItem.Text & "SQLScript.txt"
        ' Dim path2 As String = "C:\eMozart\EliminarEntidad" & cmbEntidadEliminar.SelectedItem.Text & "plSQL.txt"



        ' dtSistemas = Man.verSistemas(cmbEntidadEliminar.SelectedValue)
        dtConversiones = Man.verConversionesSoloEntidad(cmbEntidadEliminar.SelectedValue)

        If dtConversiones.Rows.Count = 0 Then
            iReturnValue = MsgBox("Atención!!! Usted está a punto de borrar una entidad", MsgBoxStyle.YesNo, "Bancor")

            sScript = "USE [Conversiones_ESB] " & vbCrLf & "GO " & vbCrLf & "DELETE FROM [dbo].[ENTIDADDESCRIPCION] " & vbCrLf & "WHERE entidadId= " & cmbEntidadEliminar.SelectedValue & "" & vbCrLf & "GO" & vbCrLf
            sScript = sScript & "USE [Conversiones_ESB] " & vbCrLf & "GO " & vbCrLf & "DELETE FROM [dbo].[ENTIDADDOMINIO] " & vbCrLf & "WHERE entidadId= " & cmbEntidadEliminar.SelectedValue & "" & vbCrLf & "GO" & vbCrLf
            sScript = sScript & "USE [Conversiones_ESB] " & vbCrLf & "GO " & vbCrLf & "DELETE FROM [dbo].[ENTIDADSISTEMATIPODATOS] " & vbCrLf & "WHERE entidadId= " & cmbEntidadEliminar.SelectedValue & "" & vbCrLf & "GO" & vbCrLf
            sScript = sScript & "USE [Conversiones_ESB] " & vbCrLf & "GO " & vbCrLf & "DELETE FROM [dbo].[ENTIDAD] " & vbCrLf & "WHERE entidadId= " & cmbEntidadEliminar.SelectedValue & "" & vbCrLf & "GO" & vbCrLf

            'sScriptOracle = "DELETE FROM ENTIDAD" & vbCrLf & "WHERE entidadId= " & cmbEntidadEliminar.SelectedValue & ";" & vbCrLf
            'sScriptOracle = sScriptOracle & "DELETE FROM ENTIDADDESCRIPCION " & vbCrLf & "WHERE entidadId= " & cmbEntidadEliminar.SelectedValue & ";" & vbCrLf
            'sScriptOracle = sScriptOracle & "DELETE FROM ENTIDADDOMINIO " & vbCrLf & "WHERE entidadId= " & cmbEntidadEliminar.SelectedValue & ";" & vbCrLf
            'sScriptOracle = sScriptOracle & "DELETE FROM ENTIDADSISTEMATIPODATOS " & vbCrLf & "WHERE entidadId= " & cmbEntidadEliminar.SelectedValue & ";" & vbCrLf & "COMMIT"

            sScriptBase = "DELETE FROM [dbo].[ENTIDADDESCRIPCION] WHERE entidadId= " & cmbEntidadEliminar.SelectedValue & ";"
            sScriptBase = sScriptBase & "DELETE FROM [dbo].[ENTIDADDOMINIO] WHERE entidadId= " & cmbEntidadEliminar.SelectedValue & ";"
            sScriptBase = sScriptBase & "DELETE FROM [dbo].[ENTIDADSISTEMATIPODATOS] WHERE entidadId= " & cmbEntidadEliminar.SelectedValue & ";"
            sScriptBase = sScriptBase & "DELETE FROM [dbo].[ENTIDAD] WHERE entidadId= " & cmbEntidadEliminar.SelectedValue & ""
            'Else
            '    iReturnValue = MsgBox("Atención!!! Usted está a punto de borrar una entidad que posee conversiones", MsgBoxStyle.YesNo, "Bancor")
            '    sScript = "USE [Conversiones_ESB] " & vbCrLf & "GO " & vbCrLf & "DELETE FROM [dbo].[CONVERSION] " & vbCrLf & "WHERE entidadId= " & cmbEntidadEliminar.SelectedValue & "" & vbCrLf & "GO" & vbCrLf
            '    sScript = sScript & "USE [Conversiones_ESB] " & vbCrLf & "GO " & vbCrLf & "DELETE FROM [dbo].[ENTIDADDESCRIPCION] " & vbCrLf & "WHERE entidadId= " & cmbEntidadEliminar.SelectedValue & "" & vbCrLf & "GO" & vbCrLf
            '    sScript = sScript & "USE [Conversiones_ESB] " & vbCrLf & "GO " & vbCrLf & "DELETE FROM [dbo].[ENTIDADDOMINIO] " & vbCrLf & "WHERE entidadId= " & cmbEntidadEliminar.SelectedValue & "" & vbCrLf & "GO" & vbCrLf
            '    sScript = sScript & "USE [Conversiones_ESB] " & vbCrLf & "GO " & vbCrLf & "DELETE FROM [dbo].[ENTIDADSISTEMATIPODATOS] " & vbCrLf & "WHERE entidadId= " & cmbEntidadEliminar.SelectedValue & "" & vbCrLf & "GO" & vbCrLf
            '    sScript = sScript & "USE [Conversiones_ESB] " & vbCrLf & "GO " & vbCrLf & "DELETE FROM [dbo].[ENTIDAD] " & vbCrLf & "WHERE entidadId= " & cmbEntidadEliminar.SelectedValue & "" & vbCrLf & "GO" & vbCrLf

            '    'sScriptOracle =  "DELETE FROM ENTIDADDESCRIPCION " & vbCrLf & "WHERE entidadId= " & cmbEntidadEliminar.SelectedValue & ";" & vbCrLf
            '    'sScriptOracle = sScriptOracle & "DELETE FROM ENTIDADDOMINIO " & vbCrLf & "WHERE entidadId= " & cmbEntidadEliminar.SelectedValue & ";" & vbCrLf
            '    'sScriptOracle = sScriptOracle & "DELETE FROM ENTIDADSISTEMATIPODATOS " & vbCrLf & "WHERE entidadId= " & cmbEntidadEliminar.SelectedValue & ";" & vbCrLf
            '    'sScriptOracle = sScriptOracle & "DELETE FROM CONVERSION " & vbCrLf & "WHERE entidadId= " & cmbEntidadEliminar.SelectedValue & ";" & vbCrLf 
            '    'sScriptOracle = sScriptOracle & "DELETE FROM ENTIDAD" & vbCrLf & "WHERE entidadId= " & cmbEntidadEliminar.SelectedValue & ";" & vbCrLf & "COMMIT"

            '    sScriptBase = "DELETE FROM [dbo].[ENTIDADDESCRIPCION] WHERE entidadId= " & cmbEntidadEliminar.SelectedValue & ";"
            '    sScriptBase = sScriptBase & "DELETE FROM [dbo].[ENTIDADDOMINIO] WHERE entidadId= " & cmbEntidadEliminar.SelectedValue & ";"
            '    sScriptBase = sScriptBase & "DELETE FROM [dbo].[ENTIDADSISTEMATIPODATOS] WHERE entidadId= " & cmbEntidadEliminar.SelectedValue & ";"
            '    sScriptBase = sScriptBase & "DELETE FROM [dbo].[CONVERSION]WHERE entidadId= " & cmbEntidadEliminar.SelectedValue & ";"
            '    sScriptBase = sScriptBase & "DELETE FROM [dbo].[ENTIDAD] WHERE entidadId= " & cmbEntidadEliminar.SelectedValue & ""
        End If


        If iReturnValue = MsgBoxResult.Yes Then
            txtBorrarScript.Text = sScript
            crearTxtScript(path, txtBorrarScript.Text)
            'crearTxtScript(path,path2, txtBorrarScript.Text,sScriptOracle)
            Man.abmEntidad(sScriptBase)

            llenarCombos()

        End If

        '//// Creación txt
        'Dim savePath As String = "c:\eMozart"


        '' Create or overwrite the file.
        'Dim fileName As String = FileUpload1.FileName

        'savePath += fileName
        'FileUpload1.PostedFile.SaveAs(savePath)

        ''If (FileUpload1.HasFile) Then
        ''    savePath += fileName
        ''    FileUpload1.SaveAs(savePath)

        ''    ' Notify the user of the name the file
        ''    ' was saved under.
        ''    'UploadStatusLabel.Text = "Your file was saved as " & fileName
        ''End If



    End Sub
    'Public Sub crearTxtScript(path As String, path2 As String, textScript As String, textScriptOracle As String)
    Public Sub crearTxtScript(path As String, textScript As String)
        ' Create or overwrite the file.
        Dim fs As FileStream = File.Create(path)
        Dim info As Byte() = New UTF8Encoding(True).GetBytes(textScript)
        fs.Write(info, 0, info.Length)
        fs.Close()

        'Dim fs2 As FileStream = File.Create(path2)
        'Dim info2 As Byte() = New UTF8Encoding(True).GetBytes(textScriptOracle)
        'fs2.Write(info2, 0, info2.Length)
        'fs2.Close()
    End Sub

    Private Sub txtNuevaEntidad_TextChanged(sender As Object, e As EventArgs) Handles txtNuevaEntidad.TextChanged
        txtAgregarScript.Text = ""

    End Sub

    Private Sub cmbSeleccion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSeleccion.SelectedIndexChanged
        If cmbSeleccion.SelectedValue = 0 Then
            Panel2.Visible = True
            Panel1.Visible = False
        End If
        If cmbSeleccion.SelectedValue = 1 Then
            Panel1.Visible = True
            Panel2.Visible = False
        End If
    End Sub

    Private Sub cmbVerEntidad_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbVerEntidad.SelectedIndexChanged
        'Dim tabla As New DataTable
        'tabla = Man.verEntidadDescripcion(cmbVerEntidad.SelectedValue)
        'tabla.Columns.Add()
        'gvEntidadDetalle.DataSource = tabla
        'gvEntidadDetalle.DataBind()
        gvEntidadDetalle.PageIndex = 0
        BindGrid()
    End Sub




    ' '/// poner en el front
    ' <div>
    ' <asp:FileUpload ID = "FileUpload1" runat="server" />

    '</div>

    Private Sub BindGrid()
        Dim tabla As New DataTable
        If cmbVerEntidad.SelectedValue <> "" Then
            gvEntidadDetalle.Visible = True
            txtImagen.Visible = False
            tabla = Man.verEntidadDescripcion(cmbVerEntidad.SelectedValue)
            If tabla.Rows.Count > 0 Then
                gvEntidadDetalle.DataSource = tabla
                gvEntidadDetalle.DataBind()
            Else
                gvEntidadDetalle.Visible = False
                txtImagen.Visible = True
            End If

        Else
            gvEntidadDetalle.Visible = False
            txtImagen.Visible = True
        End If
    End Sub

    Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvEntidadDetalle.PageIndex = e.NewPageIndex
        Me.BindGrid()
    End Sub
End Class