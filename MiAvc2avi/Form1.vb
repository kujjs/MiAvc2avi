Imports System.IO

Public Class Form1

    Inherits System.Windows.Forms.Form
    Private Sub Form1_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'Dim procesos As Process()
        'procesos = Process.GetProcessesByName("avc2avi")

        '        If procesos.Length > 0 Then
        'For i = procesos.Length - 1 To 0 Step -1
        'procesos(i).CloseMainWindow()
        'Next
        'End If
        ' thread.Abort()
        Dim pProcess() As Process = System.Diagnostics.Process.GetProcessesByName("avc2avi")

        For Each p As Process In pProcess
            p.Kill()
        Next
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        
    End Sub

 
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListView1.CheckBoxes = True
    End Sub

    Dim directorio As String

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles btnInput.Click
        Dim openFileDialog1 As New OpenFileDialog()

        'openFileDialog1.InitialDirectory = "F:\Downloads"
        openFileDialog1.Filter = "h264 files (*.h264)|*.h264"
        openFileDialog1.FilterIndex = 1

        openFileDialog1.Multiselect = True


        If (openFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK) Then
            Dim file As String
            Dim a As Int16 = 0
            For Each file In openFileDialog1.SafeFileNames
                ListView1.Items.Add(file)
                ListView1.Items(a).SubItems.Add("----")
                ListView1.Items(a).Checked = True
                a = a + 1
            Next
            TextBox3.Text = IO.Path.GetDirectoryName(openFileDialog1.FileName)
            directorio = (IO.Path.GetDirectoryName(openFileDialog1.FileName))

        End If
    End Sub

    Dim thread As Threading.Thread
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Control.CheckForIllegalCrossThreadCalls = False
        thread = New Threading.Thread(AddressOf llenar)
        Button3.Enabled = False
        thread.Start("inicio")

    End Sub

    Dim a As Int16 = 0

    Public Sub convetir(ByVal fotograma As String, ByVal intput As String, ByVal salida As String)
        Try
            ' MsgBox(" -f " & fotograma & " -i """ & intput & """ -o """ & salida & """")
            'MsgBox(C)
            'ProgressBar1.Value = 0
            Dim comando As String = " -f " & fotograma & " -i """ & intput & """ -o """ & salida & """"
            Dim process As New Process()
            Dim FileName As String = "avc2avi.exe"
            Dim Arguments As String = comando
            process.StartInfo.UseShellExecute = False
            process.StartInfo.RedirectStandardOutput = True
            process.StartInfo.RedirectStandardError = True
            process.StartInfo.CreateNoWindow = True
            process.StartInfo.FileName = FileName
            process.StartInfo.Arguments = Arguments
            process.Start()
            If process.Responding = True Then
                ListView1.Items(C).SubItems(1).Text = "PROCESANDO"

            End If

            Dim output As String = process.StandardOutput.ReadToEnd()
            ListView1.Items(C).SubItems(1).Text = "FINALIZADO"
            ListView1.Items(C).Checked = False




        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub

    Dim C As Int16 = 0
    Private Sub llenar(ByVal estado As String)
        'MsgBox(estado)

        If estado = "cancelado" Then
            For Each itema As ListViewItem In ListView1.Items
                If ListView1.Items(a).SubItems(1).Text = "FINALIZADO" Or ListView1.Items(a).Checked = False Then
                Else

                    ListView1.Items(a).SubItems(1).Text = "CANCELADO"
                End If
                a = a + 1

            Next
            thread.Abort()
            Button3.Enabled = True
        Else
            For Each item As ListViewItem In ListView1.Items
                'MsgBox(a)
                'MsgBox(item.Checked)
                ' If ListView1.Items(C).Checked = True Then
                ' If item.Checked = False Then Continue For
                'Dim b As Integer = ListView1.Items.Count

                'Dim ejecutando As Process() = Process.GetProcessesByName("avc2avi")
                If item.Checked = False Then
                    C = C + 1
                    Continue For
                    
                    ' End If
                Else

                    Dim intput As String = directorio & "\" & item.Text
                    Try
                        Dim path As String = TextBox3.Text & "\" & item.Text & ".avi"
                        Dim fs As FileStream = File.Create(path)
                        fs.Close()


                        convetir(TextBox2.Text, intput, path)

                        Button3.Enabled = True

                    Catch ex As Exception
                        MsgBox(ex.Message)

                    End Try


                End If
                C = C + 1
            Next
        End If
        C = 0
        a = 0

    End Sub




    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            ' Configuración del FolderBrowserDialog  
            With FolderBrowserDialog1

                .Reset() ' resetea  

                ' leyenda  
                .Description = " Seleccionar una carpeta "
                ' Path " Mis documentos "  
                .SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

                ' deshabilita el botón " crear nueva carpeta "  
                .ShowNewFolderButton = True
                '.RootFolder = Environment.SpecialFolder.Desktop
                '.RootFolder = Environment.SpecialFolder.StartMenu  

                Dim ret As DialogResult = .ShowDialog ' abre el diálogo  

                ' si se presionó el botón aceptar ...  
                If ret = Windows.Forms.DialogResult.OK Then

                    'Dim nFiles As ObjectModel.ReadOnlyCollection(Of String)

                    'nFiles = My.Computer.FileSystem.GetFiles(.SelectedPath)

                    'MsgBox("Total de archivos: " & CStr(nFiles.Count), _
                    '                       MsgBoxStyle.Information)
                    TextBox3.Text = .SelectedPath
                End If

                .Dispose()

            End With
        Catch oe As Exception
            MsgBox(oe.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub rbDefecto_CheckedChanged(sender As Object, e As EventArgs) Handles rbDefecto.CheckedChanged
        TextBox3.Enabled = False
        Button1.Enabled = False
        TextBox3.Text = directorio
    End Sub

    Private Sub rbdistinta_CheckedChanged(sender As Object, e As EventArgs) Handles rbdistinta.CheckedChanged
        TextBox3.Enabled = True
        Button1.Enabled = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        llenar("cancelado")
        Dim pProcess() As Process = System.Diagnostics.Process.GetProcessesByName("avc2avi")

        For Each p As Process In pProcess
            p.Kill()
        Next

        'ListView1.Items(a).SubItems(1).Text = "CANCELADO"

    End Sub

   
    Private Sub ListView1_MouseDown(sender As Object, e As MouseEventArgs) Handles ListView1.MouseDown
        If e.Button = MouseButtons.Right Then
            If ListView1.Items.Count <= 0 Then
                ContextMenuStrip1.Show(Cursor.Position)
                ContextMenuStrip1.Visible = True
                DeleteToolStripMenuItem.Enabled = False
            Else
                ContextMenuStrip1.Show(Cursor.Position)
                ContextMenuStrip1.Visible = True
                DeleteToolStripMenuItem.Enabled = True
            End If

           
        End If
    End Sub


    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        Dim x As Integer
        x = ListView1.FocusedItem.Index
        MsgBox(x)
        ListView1.Items(x).Remove()

    End Sub
End Class
