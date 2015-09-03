'Public Sub convetir(ByVal fotograma As String, ByVal intput As String, ByVal salida As String)
'
'Try
'Dim comando As String = " -f " & fotograma & " -i """ & intput & """ -o """ & salida & """"
'Dim process As New Process()
'Dim FileName As String = "avc2avi.exe"
'Dim Arguments As String = comando
''process.StartInfo.CreateNoWindow = True
'process.StartInfo.UseShellExecute = False
'process.StartInfo.RedirectStandardOutput = True
'process.StartInfo.RedirectStandardError = True
'process.StartInfo.CreateNoWindow = False
'process.StartInfo.FileName = FileName
'process.StartInfo.Arguments = Arguments

'MsgBox(FileName & Arguments)
'process.StartInfo.WorkingDirectory = WorkingDirectory
'process.Start()
'If process.Responding = True Then
'ListView1.Items(a).SubItems(1).Text = "PROCESANDO"
'
'End If
'
'Dim output As String = Process.StandardOutput.ReadToEnd()
'MsgBox(output, MsgBoxStyle.Information)
''Dim io As String = process.StandardOutput.ReadLine
'For i As Integer = 0 To 100
'ProgressBar1.Value = i
'Next
'MsgBox(output)
' If process.HasExited = True Then
'        ListView1.Items(a).SubItems(1).Text = "FINALIZADO"
' End If

'txtResults.Text = output & "n1"
'       a = a + 1
'  Catch ex As Exception
'txtResults.Text = "ERROR"
'     MsgBox(ex.Message)
'    End Try





'End Sub