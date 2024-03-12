Imports System.Net.Http
Imports System.Collections.Generic
Imports Newtonsoft.Json


Public Class ApiPromesaStrategy
    Implements IObtenerGuiasStrategy

    ' URL de la API
    Private ReadOnly _urlApi As String

    Public Sub New(urlApi As String)
        _urlApi = urlApi
    End Sub

    Public Function ObtenerDataDesdeExcel(Ruta As String) As List(Of ImportarGuiaLogistica) Implements IObtenerGuiasStrategy.ObtenerDataDesdeExcel
        Throw New NotImplementedException("Esta función no está implementada en ApiPromesaStrategy")

    End Function

    Public Function ObtenerDataDesdeAPI(fechaInicio As Date, fechaFin As Date) As List(Of ImportarGuiaLogistica) Implements IObtenerGuiasStrategy.ObtenerDataDesdeAPI
        Dim datos As New List(Of ImportarGuiaLogistica)()

        ' Aquí colocas la lógica para hacer la solicitud a la API y obtener los datos con las fechas especificadas
        ' Por ejemplo, puedes usar HttpClient para hacer la solicitud HTTP
        Using cliente As New HttpClient()
            ' Construye la URL de la API con las fechas de inicio y fin
            Dim urlCompleta As String = $"{_urlApi}?fechaInicio={fechaInicio.ToString("yyyy-MM-dd")}&fechaFin={fechaFin.ToString("yyyy-MM-dd")}"

            ' Realiza la solicitud GET a la API
            Dim respuesta As HttpResponseMessage = cliente.GetAsync(urlCompleta).Result

            ' Verifica si la solicitud fue exitosa
            If respuesta.IsSuccessStatusCode Then
                ' Lee el contenido de la respuesta como una lista de ImportarGuiaLogistica (debes implementar la lógica para deserializar los datos según el formato de la respuesta de tu API)
                ' Por ejemplo, si la respuesta está en formato JSON, puedes usar JsonConvert de la librería Newtonsoft.Json para deserializar los datos
                Dim contenido As String = respuesta.Content.ReadAsStringAsync().Result
                ' Deserializa el contenido JSON en una lista de ImportarGuiaLogistica
                datos = JsonConvert.DeserializeObject(Of List(Of ImportarGuiaLogistica))(contenido)
            Else
                ' Si la solicitud no fue exitosa, lanza una excepción o maneja el error según corresponda
                Throw New Exception($"Error al obtener datos desde la API: {respuesta.StatusCode} - {respuesta.ReasonPhrase}")
            End If
        End Using

        Return datos
    End Function
End Class
