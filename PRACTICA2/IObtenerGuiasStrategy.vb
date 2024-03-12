Public Interface IObtenerGuiasStrategy
    Function ObtenerDataDesdeExcel(Ruta As String) As List(Of ImportarGuiaLogistica)
    Function ObtenerDataDesdeAPI(fechaInicio As Date, fechaFin As Date) As List(Of ImportarGuiaLogistica)

End Interface
