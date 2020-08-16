Imports System.IO
Imports Speak

Public Class tSalesOrder : Inherits basetask

#Region "ctor"

    Sub New()
        MyBase.New("sales order", eTaskType.servicecall)

    End Sub

    Sub New(t As this)
        MyBase.New(t)

    End Sub

#End Region

    Public Overrides ReadOnly Property url As String
        Get
            Return String.Format("priority:priform#ORDERS:{0}:live:tabulaemerge.ini", Me.id)
        End Get
    End Property

    Private _ReportedBy As tContact
    Public Property ReportedBy As tContact
        Get
            Return _ReportedBy
        End Get
        Set(value As tContact)
            _ReportedBy = value
        End Set
    End Property


    Private _SalesOrderItems As things
    Public Property SalesOrderItems As things
        Get
            Return _SalesOrderItems
        End Get
        Set(value As things)
            _SalesOrderItems = value
        End Set

    End Property

#Region "Overriden Methods"

    Public Overrides Sub Context(ParamArray args() As thing)
        'For Each t As thing In args
        '    If TypeOf (t) Is Customer Then
        '        Me.Customer = t

        '    ElseIf TypeOf (t) Is Hour Then
        '        Me.Hour = TryCast(t, Hour).Span

        '    End If
        'Next

    End Sub

    Public Overrides Sub Refresh()
        'myThings.LoadURL("speak_SalesOrderItems.ashx", {String.Format("ORDNAME={0}", Me.Name)})

    End Sub


#End Region

End Class
