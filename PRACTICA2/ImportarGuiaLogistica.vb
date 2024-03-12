Public Class ImportarGuiaLogistica
    Private _cliente As String
    Public Property Cliente As String
        Get
            Return _cliente
        End Get
        Set(value As String)
            _cliente = value
        End Set
    End Property

    Private _tipoDePago As String
    Public Property TipoDePago As String
        Get
            Return _tipoDePago
        End Get
        Set(value As String)
            _tipoDePago = value
        End Set
    End Property

    Private _ciudad As String
    Public Property Ciudad As String
        Get
            Return _ciudad
        End Get
        Set(value As String)
            _ciudad = value
        End Set
    End Property

    Private _codigo As String
    Public Property Codigo As String
        Get
            Return _codigo
        End Get
        Set(value As String)
            _codigo = value
        End Set
    End Property

    Private _descripcion As String
    Public Property Descripcion As String
        Get
            Return _descripcion
        End Get
        Set(value As String)
            _descripcion = value
        End Set
    End Property

    Private _cantidad As Integer
    Public Property Cantidad As Integer
        Get
            Return _cantidad
        End Get
        Set(value As Integer)
            _cantidad = value
        End Set
    End Property

    ' Constructor por defecto
    Public Sub New()
    End Sub

    ' Constructor con parámetros
    Public Sub New(ByVal cliente As String, ByVal tipoDePago As String, ByVal ciudad As String, ByVal codigo As String, ByVal descripcion As String, ByVal cantidad As Integer)
        Me.Cliente = cliente
        Me.TipoDePago = tipoDePago
        Me.Ciudad = ciudad
        Me.Codigo = codigo
        Me.Descripcion = descripcion
        Me.Cantidad = cantidad
    End Sub



End Class
