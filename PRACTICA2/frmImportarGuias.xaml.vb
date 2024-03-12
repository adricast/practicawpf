Imports Microsoft.Win32

Class MainWindow
    Private Sub btnExaminar_Click(sender As Object, e As RoutedEventArgs) Handles btnExaminar.Click
        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.Filter = "Archivos de Excel (*.xlsx)|*.xlsx|Todos los archivos (*.*)|*.*"

        If openFileDialog.ShowDialog() = True Then
            Dim rutaArchivo As String = openFileDialog.FileName
            ' Aquí puedes hacer lo que necesites con la ruta del archivo seleccionado
            Dim viewModel As importarGuiaViewModel = DirectCast(Me.DataContext, importarGuiaViewModel)
            viewModel.RutaArchivo = rutaArchivo
        End If
    End Sub
End Class
