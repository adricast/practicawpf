Imports System.ComponentModel

Imports System.Collections.ObjectModel


Public Class facturaViewModel
    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Private _factura As facturaModel

    Public Property Factura As facturaModel
        Get
            Return _factura
        End Get
        Set(value As facturaModel)
            _factura = value
            OnPropertyChanged("Factura")
        End Set
    End Property

    Public Sub New()
        ' Inicializar la factura y sus detalles
        Factura = New facturaModel()
        AddHandler Factura.Detalles.CollectionChanged, AddressOf Detalles_CollectionChanged
    End Sub


    Private Sub Detalles_CollectionChanged(sender As Object, e As System.Collections.Specialized.NotifyCollectionChangedEventArgs)
        ' Recalcular el total de la factura cada vez que cambian los detalles
        RecalcularTotal()
    End Sub

    Private Sub RecalcularTotal()
        ' Calcular el total sumando los totales de los detalles
        Dim total As Decimal = 0
        For Each detalle As detalleFacturaModel In Factura.Detalles
            total += detalle.Total
        Next
        ' Llamar al método de la factura para actualizar el total
        Factura.ActualizarTotal(total)
    End Sub

    Protected Sub OnPropertyChanged(ByVal propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub
End Class
