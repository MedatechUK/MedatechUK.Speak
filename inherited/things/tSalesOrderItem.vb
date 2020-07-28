Imports Speak

Public Class tSalesOrderItem : Inherits thing

#Region "ctor"

    Sub New(Name As String)
        MyBase.New(Name)

    End Sub

    Sub New(t As this)
        MyBase.New(t)

    End Sub

#End Region

    Private _Line As Integer
    Public Property Line As Integer
        Get
            Return _Line
        End Get
        Set(value As Integer)
            _Line = value
        End Set
    End Property

    Private _DueDate As Date
    Public Property DueDate As Date
        Get
            Return _DueDate
        End Get
        Set(value As Date)
            _DueDate = value
        End Set
    End Property

    Private _Description As String
    Public Shadows Property Description As String
        Get
            Return _Description
        End Get
        Set(value As String)
            _Description = value
        End Set
    End Property

    Private _SalesOrder As tSalesOrder
    Public Property SalesOrder As tSalesOrder
        Get
            Return _SalesOrder
        End Get
        Set(value As tSalesOrder)
            _SalesOrder = value
        End Set
    End Property

    Private _Customer As Customer
    Public Property Customer As Customer
        Get
            Return _Customer
        End Get
        Set(value As Customer)
            _Customer = value
        End Set
    End Property

#Region "overriden Methods"

    Public Overrides Sub Context(ParamArray args() As thing)
        Throw New NotImplementedException()
    End Sub

#End Region

End Class
