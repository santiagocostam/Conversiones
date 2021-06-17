Imports System.Data.SqlClient
Public Class Mantenimientos
    Inherits Conexiones
    Event Errores(Descripcion As String)

    Public Function verDominios() As DataTable
        Dim Query As String = "Select dominioid,dominionombre from dominio"

        Try
            conectar()
            Dim cmd As New SqlCommand(Query, conn)
            cmd.CommandType = CommandType.Text
            Dim resultado As SqlDataReader = cmd.ExecuteReader
            Dim tablas As DataTable = New DataTable
            tablas.Load(resultado)
            Return tablas

        Catch ex As Exception
            RaiseEvent Errores(ex.Message)
            Return Nothing
        Finally
            desconectar()
        End Try
    End Function
    Public Function verEntidades(ByVal idDominio As Integer) As DataTable
        Dim Query As String = "Select entidad.entidadid,entidad.entidadnombre from entidad,entidaddominio where entidad.entidadid=entidaddominio.entidadid and entidaddominio.dominioid=" & idDominio & ""

        Try
            conectar()
            Dim cmd As New SqlCommand(Query, conn)
            cmd.CommandType = CommandType.Text
            Dim resultado As SqlDataReader = cmd.ExecuteReader
            Dim tablas As DataTable = New DataTable
            tablas.Load(resultado)
            Return tablas

        Catch ex As Exception
            RaiseEvent Errores(ex.Message)
            Return Nothing
        Finally
            desconectar()
        End Try
    End Function
    Public Function verEntidadesTodas() As DataTable
        'Dim Query As String = "SELECT [ENTIDADID],([ENTIDADNOMBRE]+[ENTIDADID]) as nombre FROM [Conversiones_ESB].[dbo].[ENTIDAD] order by [ENTIDADNOMBRE]"
        Dim query As String = "SELECT [ENTIDADID],concat_ws (' - ',[ENTIDADid],[ENTIDADNOMBRE]) as nombre FROM [Conversiones_ESB].[dbo].[ENTIDAD] order by [ENTIDADNOMBRE]"

        Try
            conectar()
            Dim cmd As New SqlCommand(Query, conn)
            cmd.CommandType = CommandType.Text
            Dim resultado As SqlDataReader = cmd.ExecuteReader
            Dim tablas As DataTable = New DataTable
            tablas.Load(resultado)
            Return tablas

        Catch ex As Exception
            RaiseEvent Errores(ex.Message)
            Return Nothing
        Finally
            desconectar()
        End Try
    End Function
    Public Function verSistemas(ByVal idEntidad As Integer) As DataTable
        Dim Query As String = "SELECT 
      a.[SISTEMAID_DESTINO]
	  ,b.SISTEMANOMBRE
  FROM [Conversiones_ESB].[dbo].[CONVERSION] a, SISTEMA b
  where a.ENTIDADID=" & idEntidad & " and a.SISTEMAID_DESTINO=b.SISTEMAID
  group by a.[SISTEMAID_DESTINO]
	  ,b.SISTEMANOMBRE "

        Try
            conectar()
            Dim cmd As New SqlCommand(Query, conn)
            cmd.CommandType = CommandType.Text
            Dim resultado As SqlDataReader = cmd.ExecuteReader
            Dim tablas As DataTable = New DataTable
            tablas.Load(resultado)
            Return tablas

        Catch ex As Exception
            RaiseEvent Errores(ex.Message)
            Return Nothing
        Finally
            desconectar()
        End Try
    End Function
    Public Function verConversiones(ByVal idSistema As String, ByVal idEntidad As Integer) As DataTable
        Dim Query As String = "SELECT DISTINCT [ENTIDADID]
      ,a.[SISTEMAID_DESTINO]
      ,a.[VALOR_DESTINO]
	  ,b.SISTEMANOMBRE as Destino
	  ,a.[SISTEMAID_ORIGEN]
	  ,a.[VALOR_ORIGEN]
	  ,(select SISTEMANOMBRE  from sistema where SISTEMAID=a.SISTEMAID_ORIGEN and SISTEMAID='740') as Origen
  FROM [Conversiones_ESB].[dbo].[CONVERSION] a, SISTEMA b
  where a.ENTIDADID=" & idEntidad & " and a.SISTEMAID_DESTINO=b.SISTEMAID and b.SISTEMAID='" & idSistema & "' "

        Try
            conectar()
            Dim cmd As New SqlCommand(Query, conn)
            cmd.CommandType = CommandType.Text
            Dim resultado As SqlDataReader = cmd.ExecuteReader
            Dim tablas As DataTable = New DataTable
            tablas.Load(resultado)
            Return tablas

        Catch ex As Exception
            RaiseEvent Errores(ex.Message)
            Return Nothing
        Finally
            desconectar()
        End Try
    End Function
    Public Function verConversionesSoloEntidad(ByVal idEntidad As Integer) As DataTable
        Dim Query As String = "SELECT DISTINCT [ENTIDADID]
      ,a.[SISTEMAID_DESTINO]
      ,a.[VALOR_DESTINO]
	  ,b.SISTEMANOMBRE as Destino
	  ,a.[SISTEMAID_ORIGEN]
	  ,a.[VALOR_ORIGEN]
	  ,(select SISTEMANOMBRE  from sistema where SISTEMAID=a.SISTEMAID_ORIGEN and SISTEMAID='740') as Origen
  FROM [Conversiones_ESB].[dbo].[CONVERSION] a, SISTEMA b
  where a.ENTIDADID=" & idEntidad & " "

        Try
            conectar()
            Dim cmd As New SqlCommand(Query, conn)
            cmd.CommandType = CommandType.Text
            Dim resultado As SqlDataReader = cmd.ExecuteReader
            Dim tablas As DataTable = New DataTable
            tablas.Load(resultado)
            Return tablas

        Catch ex As Exception
            RaiseEvent Errores(ex.Message)
            Return Nothing
        Finally
            desconectar()
        End Try
    End Function
    Public Function buscarIdEntidadMax()

        Dim Query As String = "SELECT max([ENTIDADID]) FROM [Conversiones_ESB].[dbo].[ENTIDAD]"

        Try
            conectar()
            Dim cmd As New SqlCommand(Query, conn)
            cmd.CommandType = CommandType.Text
            Dim resultado As SqlDataReader = cmd.ExecuteReader
            Dim tablas As DataTable = New DataTable
            Dim idEntidad As Integer = 1
            tablas.Load(resultado)
            If tablas.Rows.Count > 0 Then
                idEntidad = tablas(0)(0) + 1
            End If

            Return idEntidad

        Catch ex As Exception
            RaiseEvent Errores(ex.Message)
            Return Nothing
        Finally
            desconectar()
        End Try
    End Function
    Public Function verificarNombreEntidad(ByVal nombreEntidad As String)

        Dim Query As String = "SELECT [ENTIDADNOMBRE] FROM [Conversiones_ESB].[dbo].[ENTIDAD] where [ENTIDADNOMBRE] ='" & nombreEntidad & "'"

        Try
            conectar()
            Dim cmd As New SqlCommand(Query, conn)
            cmd.CommandType = CommandType.Text
            Dim resultado As SqlDataReader = cmd.ExecuteReader
            Dim tablas As DataTable = New DataTable
            Dim siExiste As Boolean = False
            tablas.Load(resultado)
            If tablas.Rows.Count > 0 Then
                siExiste = True
            End If

            Return siExiste

        Catch ex As Exception
            RaiseEvent Errores(ex.Message)
            Return Nothing
        Finally
            desconectar()
        End Try
    End Function
    Public Sub abmEntidad(ByVal txtQuery As String)

        Try
            conectar()


            Dim cmd As New SqlCommand(txtQuery, conn)
            cmd.CommandType = CommandType.Text
            cmd.CommandText = txtQuery
            cmd.ExecuteNonQuery()



        Catch ex As Exception
            RaiseEvent Errores(ex.Message)

        Finally
            desconectar()
        End Try
    End Sub
    Public Function verEntidadDescripcion(ByVal idEntidad As Integer)

        Dim Query As String = "select ENTIDADDESCRIPCION.DESCRIPCION,DOMINIO.DOMINIONOMBRE,sistema.SISTEMAID,sistema.SISTEMANOMBRE" &
        " from entidad,DOMINIO,SISTEMA,ENTIDADDESCRIPCION,ENTIDADDOMINIO,ENTIDADSISTEMATIPODATOS" &
        " where ENTIDAD.ENTIDADID=ENTIDADDESCRIPCION.ENTIDADID and ENTIDAD.ENTIDADID=ENTIDADDOMINIO.ENTIDADID and" &
        " ENTIDAD.ENTIDADID=ENTIDADSISTEMATIPODATOS.ENTIDADID and ENTIDAD.ENTIDADID=" & idEntidad & ""

        Try
            conectar()
            Dim cmd As New SqlCommand(Query, conn)
            cmd.CommandType = CommandType.Text
            Dim resultado As SqlDataReader = cmd.ExecuteReader
            Dim tablas As DataTable = New DataTable

            tablas.Load(resultado)
            Return tablas

        Catch ex As Exception
            RaiseEvent Errores(ex.Message)
            Return Nothing
        Finally
            desconectar()
        End Try
    End Function
End Class
