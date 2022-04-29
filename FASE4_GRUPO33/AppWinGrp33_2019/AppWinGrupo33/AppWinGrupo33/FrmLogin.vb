Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Data.Sqlite

Public Class FrmLogin
    Const sTitulo = "Fase 3 - Multimedia"

    Private Sub BtnCancelar_Click(sender As Object, e As EventArgs) Handles BtnCancelar.Click
        Me.Close()
    End Sub

    Private Sub BtnRegistar_MouseHover(sender As Object, e As EventArgs) Handles BtnRegistar.MouseHover

        With TTipMensaje
            .SetToolTip(BtnRegistar, "Crear o Registrar Nuevo Usuario...")
            .ToolTipTitle = sTitulo
            .ToolTipIcon = ToolTipIcon.Info
        End With

    End Sub

    Private Sub BtnRecuperarPass_MouseHover(sender As Object, e As EventArgs) Handles BtnRecuperarPass.MouseHover
        With TTipMensaje
            .SetToolTip(BtnRecuperarPass, "Recuperar Contraseña de ingreso...")
            .ToolTipTitle = sTitulo
            .ToolTipIcon = ToolTipIcon.Info
        End With
    End Sub
    Private Sub TxtUsuario_MouseHover(sender As Object, e As EventArgs) Handles TxtUsuario.MouseHover
        With TTipMensaje
            .SetToolTip(TxtUsuario, "El codigo o usuario es tu Identificación...")
            .ToolTipTitle = sTitulo
            .ToolTipIcon = ToolTipIcon.Info
        End With
    End Sub
    Private Sub BtnAceptar_Click(sender As Object, e As EventArgs) Handles BtnAceptar.Click
        Dim sEstudiante As String = ""
        If (ValidarDatos()) Then

            If ValidarUsuario(TxtUsuario.Text.Trim(), TxtPassword.Text.Trim(), sEstudiante) Then
                MsgBox("¡Bienvenido!; " & vbCrLf & sEstudiante, vbInformation, sTitulo)
                Me.Close()
            End If
        End If
    End Sub

    Private Function ValidarDatos() As Boolean
        Dim bContinuar As Boolean
        Try

            If (TxtPassword.Text.Trim().Length > 0 And TxtUsuario.Text.Trim().Length > 0) Then
                bContinuar = True
            Else
                MsgBox("Los datos Son Obligatorios para el Ingreso... ", vbInformation, sTitulo)
                bContinuar = False
            End If

        Catch ex As Exception
            bContinuar = False
        End Try
        Return bContinuar
    End Function

    Private Function ValidarUsuario(ByVal sUsuario As String, ByVal sPass As String, ByRef sEstudiante As String) As Boolean
        Dim bValido As Boolean = False
        Dim sSqry As String
        Dim DtUsu As New DataTable
        Dim sPassDB As String
        Try
            sSqry = "select  id_registro,codigo,Nombres,Apellidos,Correo,Telefono,Password,Rol "
            sSqry += "from Estudiantes where codigo = '" & sUsuario & "'"
            DtUsu = BaseDatosLite(sSqry)
            If DtUsu.Rows.Count > 0 Then
                sPassDB = DtUsu.Rows(0)("Password").ToString.Trim()
                If (sPassDB.Equals(sPass)) Then
                    sEstudiante = Trim(DtUsu.Rows(0)("Nombres").ToString.Trim() & " " & DtUsu.Rows(0)("Apellidos").ToString.Trim())
                    bValido = True
                Else
                    bValido = False
                    MsgBox("Lo Sentimos; Usuario y/o Contraseña Incorrectos ; " & vbCrLf & "Intentalo nuevamente.", vbCritical, sTitulo)
                End If
            Else
                MsgBox("No Existen el Usuario; " & vbCrLf & "Se debe realizar el Registro Correpondiente.", vbExclamation, sTitulo)
            End If

        Catch ex As Exception
            bValido = False
            MsgBox("Ups: " & ex.Message, vbExclamation, sTitulo)
        End Try
        Return bValido
    End Function

    Private Function BaseDatosLite(ByVal SQLstr As String) As DataTable
        Dim StrPathApp = Path.GetFullPath("..\..\..\") & "BDD\BDLiteF4.sqlite" '"DBSqlF4.mdf"
        'Dim StrConeccion_0 As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""H:\UNAD\PERIODO 16-01 2022\VISUAL BASIC BASICO\APLICATIVOS VBB\VVB_FASE4_GRUPO33\AppWinGrp33_2019\AppWinGrupo33\AppWinGrupo33\BDD\DBSqlF4.mdf"";Integrated Security=True"
        'Dim StrConeccion As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""" & StrPathApp & """;Integrated Security=True"
        'Dim sOleDBConect As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & StrPathApp & ";"

        Dim DtResp As New DataTable

        Dim SqlConn As New SqliteConnection
        Dim SqlCmd As New SqliteCommand
        Dim SqlReader As SqliteDataReader

        Try

            Dim connection As String = "Data Source=" & StrPathApp & ";"
            SqlConn = New SqliteConnection(connection)
            SqlCmd = New SqliteCommand(SQLstr, SqlConn)
            SqlConn.Open()
            SqlReader = SqlCmd.ExecuteReader
            DtResp.Load(SqlReader)

        Catch ex As Exception
            DtResp = New DataTable
        Finally

            If Not IsNothing(SqlReader) Then
                SqlReader.Close()
            End If

            SqlConn.Close()
            SqlConn.Dispose()
        End Try
        Return DtResp
    End Function
    Private Function EjecutaBaseDatos(ByVal sSQL As String) As DataTable
        Dim StrPathApp = Path.GetFullPath("..\..\..\") & "BDD\DBSqlF4.mdf"
        Dim StrConeccion_0 As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""H:\UNAD\PERIODO 16-01 2022\VISUAL BASIC BASICO\APLICATIVOS VBB\VVB_FASE4_GRUPO33\AppWinGrp33_2019\AppWinGrupo33\AppWinGrupo33\BDD\DBSqlF4.mdf"";Integrated Security=True"
        Dim StrConeccion As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""" & StrPathApp & """;Integrated Security=True"
        Dim sOleDBConect As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & StrPathApp & ";"
        'Dim conn As New OleDbConnection(StrConeccion)
        Dim DtResp As New DataTable

        Dim SqlConn As SqlConnection
        Dim SqlComando As SqlCommand
        Dim SQLAdapter As SqlDataAdapter
        Dim SqlDReader As SqlDataReader

        Dim SqlComan As SqlCommand
        Dim SqlConec As SqlConnection
        Dim SqlAdap As SqlDataAdapter
        Try
            'SqlConn = New SqlConnection
            'SqlConn.ConnectionString = "Data Source=.\SQLEXPRESS;AttachDbFilename=H:\UNAD\PERIODO 16-01 2022\VISUAL BASIC BASICO\APLICATIVOS VBB\VVB_FASE4_GRUPO33\AppWinGrp33_2019\AppWinGrupo33\AppWinGrupo33\BDD\DBSqlF4.mdf;Integrated Security=True;User Instance=True"
            'SqlConn.Open()


            Dim StrConnSql As String = "Data Source=.\SQLEXPRESS;AttachDbFilename=""H:\UNAD\PERIODO 16-01 2022\VISUAL BASIC BASICO\APLICATIVOS VBB\VVB_FASE4_GRUPO33\AppWinGrp33_2019\AppWinGrupo33\AppWinGrupo33\BDD\DBSqlF4.mdf"";Integrated Security=True;User Instance=True"
            SqlConec = New SqlConnection(StrConnSql)
            SqlConec.Open()
            SqlComan = New SqlCommand(sSQL, SqlConec)
            SqlAdap = New SqlDataAdapter(SqlComan)
            SqlAdap.Fill(DtResp)

            Using con As New SqlConnection(StrConnSql)
                Using cmd As New SqlCommand(sSQL, con)
                    cmd.CommandType = CommandType.Text
                    Using sda As New SqlDataAdapter(cmd)
                        sda.Fill(DtResp)
                    End Using
                End Using
            End Using

            SqlConec.Close()
            SqlConec.Dispose()

            'SQLAdapter = New SqlDataAdapter(sSQL.ToString, SqlConn)

            'Dim cmd As New OleDbCommand(sSQL, conn)
            'Dim dr As System.Data.OleDb.OleDbDataReader

            'dr = cmd.ExecuteReader()

            'Dim SQLAdapter As New OleDbDataAdapter(sSQL.ToString, conn)


            SQLAdapter.Fill(DtResp)
            If Not SQLAdapter Is Nothing Then
                SQLAdapter.Dispose()
                SQLAdapter = Nothing
            End If
            'con.Close()
            'con.Dispose()
        Catch ex As Exception
            'con.Close()
            SqlConn.Dispose()

            DtResp = New DataTable

        End Try
        Return DtResp
    End Function

End Class
