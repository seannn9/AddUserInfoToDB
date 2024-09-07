Imports MySql.Data.MySqlClient
Public Class Form1
    Dim connection As MySqlConnection
    Dim cmd As MySqlCommand
    Dim database_name, table_name As New String("experiment1")

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connection = New MySqlConnection
        connection.ConnectionString = "server=localhost;username=root;password=;database='" & database_name & "'"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If connection.State = ConnectionState.Closed Then
                connection.Open()
                ConnectionStatus.Text = "Connected"
                ConnectionStatus.ForeColor = Color.Green
                Button1.Text = "Disconnect to Database"
            Else
                connection.Close()
                ConnectionStatus.Text = "Disconnected"
                ConnectionStatus.ForeColor = Color.DarkRed
                Button1.Text = "Connect to Database"
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            If (connection.State = ConnectionState.Closed) Then 'Check if connection is closed when user tries to add a record
                MsgBox("Please connect to the database first", vbExclamation, Title:="Warning")
            ElseIf String.IsNullOrEmpty(TextBox1.Text) Or 'Check if any of the text box is empty when user tries to add a record
                    String.IsNullOrEmpty(TextBox2.Text) Or
                     String.IsNullOrEmpty(TextBox3.Text) Or
                      String.IsNullOrEmpty(TextBox4.Text) Then
                MsgBox("Please fill in all input fields", vbExclamation, Title:="Warning")
            Else
                If connection.State = ConnectionState.Closed Then
                    connection.Open()
                End If

                cmd = New MySqlCommand("INSERT INTO " & table_name & " (FIRST_NAME, MIDDLE_NAME, LAST_NAME, NICKNAME) 
                    values (@FIRST_NAME, @MIDDLE_NAME, @LAST_NAME, @NICKNAME)", connection)
                cmd.Parameters.AddWithValue("@FIRST_NAME", TextBox1.Text)
                cmd.Parameters.AddWithValue("@MIDDLE_NAME", TextBox2.Text)
                cmd.Parameters.AddWithValue("@LAST_NAME", TextBox3.Text)
                cmd.Parameters.AddWithValue("@NICKNAME", TextBox4.Text)
                Dim i As Integer = cmd.ExecuteNonQuery
                If i > 0 Then
                    MsgBox("Successfully Added Record To " & table_name & "", vbInformation, Title:="Success")
                Else
                    MsgBox("Failed to Add Record To " & table_name & "", vbExclamation, Title:="Error")
                End If
                TextBox1.Clear()
                TextBox2.Clear()
                TextBox3.Clear()
                TextBox4.Clear()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Clear all the text box 
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
    End Sub
End Class
