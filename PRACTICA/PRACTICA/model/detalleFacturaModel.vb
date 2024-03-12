Public Class detalleFacturaModel

    Public Property DetalleFacturaId As Integer
        Public Property Cantidad As Integer
        Public Property Producto As String
        Public Property Precio As Decimal

    Public ReadOnly Property Total As Decimal
        Get
            Return Cantidad * Precio
        End Get
    End Property
End Class
