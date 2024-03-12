Imports System.Collections.ObjectModel
Imports System.ComponentModel

Public Class facturaModel
    Public Property FacturaId As Integer
    Public Property Fecha As DateTime
    Public Property Cliente As String

    Public Property Detalles As ObservableCollection(Of detalleFacturaModel) ' Usar ObservableCollection en lugar de List

    ' Calcula el total sumando los totales de todos los detalles
    Private _total As Decimal
    Public ReadOnly Property Total As Decimal
        Get
            Return _total
        End Get
    End Property
    Public Sub New()
        Detalles = New ObservableCollection(Of detalleFacturaModel)()
        ActualizarTotal()
    End Sub

    Public Sub ActualizarTotal(Optional nuevoTotal As Decimal = 0)
        ' Si se proporciona un nuevo total, actualizarlo, de lo contrario, recalcular
        If nuevoTotal = 0 Then
            _total = Detalles.Sum(Function(detalle) detalle.Total)
        Else
            _total = nuevoTotal
        End If
        ' Notificar cambios en la propiedad Total
        OnPropertyChanged("Total")
    End Sub
    Public Event PropertyChanged As PropertyChangedEventHandler
    Protected Sub OnPropertyChanged(ByVal propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub
End Class
