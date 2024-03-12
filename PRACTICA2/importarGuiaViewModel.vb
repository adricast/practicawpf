Imports System.Collections.ObjectModel
Imports System.ComponentModel

Public Class importarGuiaViewModel
    Implements INotifyPropertyChanged

    ' Lista de opciones para el ComboBox
    Public Property Estrategias As ObservableCollection(Of String)

    ' Propiedad para almacenar la estrategia seleccionada
    Private _estrategiaSeleccionada As String



    Public Property EstrategiaSeleccionada As String
        Get
            Return _estrategiaSeleccionada
        End Get
        Set(value As String)
            _estrategiaSeleccionada = value
            OnPropertyChanged("EstrategiaSeleccionada")

            ' Cambiar la visibilidad del PanelStack según la estrategia seleccionada
            If value = "Desde archivo" Then
                PanelStackVisible = Visibility.Collapsed

            Else
                PanelStackVisible = Visibility.Visible
            End If

            ' Notificar a la interfaz de usuario que el comando de importación puede haber cambiado de estado

        End Set
    End Property

    Private _panelStackVisible As Visibility


    Public Property PanelStackVisible As Visibility
        Get
            Return _panelStackVisible
        End Get
        Set(value As Visibility)
            _panelStackVisible = value
            OnPropertyChanged("PanelStackVisible")
            If value = Visibility.Collapsed Then
                PanelUnoVisible = Visibility.Collapsed
                PanelDosVisible = Visibility.Visible
            Else
                PanelUnoVisible = Visibility.Visible
                PanelDosVisible = Visibility.Collapsed
            End If
        End Set
    End Property
    Private _rutaArchivo As String
    Public Property RutaArchivo As String
        Get
            Return _rutaArchivo
        End Get
        Set(value As String)
            _rutaArchivo = value
            OnPropertyChanged("RutaArchivo")
        End Set
    End Property
    ' Estrategias disponibles
    Private ReadOnly _archivoStrategy As ArchivoStrategy
    Private ReadOnly _apiPromesaStrategy As ApiPromesaStrategy

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Private Sub OnPropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Private _panelUnoVisible As Visibility
    Public Property PanelUnoVisible As Visibility
        Get
            Return _panelUnoVisible
        End Get
        Set(value As Visibility)
            _panelUnoVisible = value
            OnPropertyChanged("PanelUnoVisible")
        End Set
    End Property

    Private _panelDosVisible As Visibility
    Public Property PanelDosVisible As Visibility
        Get
            Return _panelDosVisible
        End Get
        Set(value As Visibility)
            _panelDosVisible = value
            OnPropertyChanged("PanelDosVisible")
        End Set
    End Property
    Private _importarCommand As RelayCommand
    Public ReadOnly Property ImportarCommand As RelayCommand
        Get
            If _importarCommand Is Nothing Then
                _importarCommand = New RelayCommand(AddressOf Importar, AddressOf PuedeImportar)
            End If
            Return _importarCommand
        End Get
    End Property

    Private Function PuedeImportar(parameter As Object) As Boolean
        ' Verificar si la estrategia seleccionada es "Archivo"
        Return EstrategiaSeleccionada = "Archivo"
    End Function

    Private Sub Importar(parameter As Object)
        ' Verificar si se puede importar desde archivo
        If PuedeImportar(Nothing) Then
            ' Lógica para importar desde archivo utilizando la ruta proporcionada
            _archivoStrategy.ObtenerDataDesdeExcel(RutaArchivo)
        End If
    End Sub

    Public Sub New()

        Estrategias = New ObservableCollection(Of String)()
        Estrategias.Add("Desde archivo")
        Estrategias.Add("Desde API Promesa")
        EstrategiaSeleccionada = "Desde archivo"
        PanelStackVisible = Visibility.Collapsed

    End Sub





End Class
