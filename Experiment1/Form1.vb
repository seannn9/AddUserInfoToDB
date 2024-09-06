Imports MySql.Data.MySqlClient
Public Class Form1
    Dim connection As MySqlConnection
    Dim cmd As MySqlCommand

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connection = New MySqlConnection
        connection.ConnectionString = "server=localhost;username=root;password=;database=experiment1"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            connection.Open()
            ConnectionStatus.Text = "Connected"
            ConnectionStatus.ForeColor = Color.Green
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            If (connection.State = ConnectionState.Closed) Then
                MsgBox("Please connect to the database first", vbExclamation, Title:="Warning")
            ElseIf String.IsNullOrEmpty(TextBox1.Text) Or
                    String.IsNullOrEmpty(TextBox2.Text) Or
                     String.IsNullOrEmpty(TextBox3.Text) Or
                      String.IsNullOrEmpty(TextBox4.Text) Then
                MsgBox("Please fill in all input fields", vbExclamation, Title:="Warning")
            Else
                If connection.State = ConnectionState.Closed Then
                    connection.Open()
                End If

                cmd = New MySqlCommand("INSERT INTO student (FIRST_NAME, MIDDLE_NAME, LAST_NAME, NICKNAME) 
                    values (@FIRST_NAME, @MIDDLE_NAME, @LAST_NAME, @NICKNAME)", connection)
                cmd.Parameters.AddWithValue("@FIRST_NAME", TextBox1.Text)
                cmd.Parameters.AddWithValue("@MIDDLE_NAME", TextBox2.Text)
                cmd.Parameters.AddWithValue("@LAST_NAME", TextBox3.Text)
                cmd.Parameters.AddWithValue("@NICKNAME", TextBox4.Text)
                Dim i As Integer = cmd.ExecuteNonQuery
                If i > 0 Then
                    MsgBox("Successfully Added Record", vbInformation, Title:="Success")
                Else
                    MsgBox("Failed to Add Record", vbExclamation, Title:="Error")
                End If
                connection.Close()
                ConnectionStatus.Text = "Disconnected"
                ConnectionStatus.ForeColor = Color.DarkRed
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If String.IsNullOrEmpty(TextBox1.Text) And
                String.IsNullOrEmpty(TextBox2.Text) And
                 String.IsNullOrEmpty(TextBox3.Text) And
                  String.IsNullOrEmpty(TextBox4.Text) Then
            MsgBox("Already cleared", vbInformation, Title:="Warning")
        Else
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
        End If
    End Sub
End Class
