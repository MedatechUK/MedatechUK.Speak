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

    Private _ReportedBy As tContact
    Public Property ReportedBy As tContact
        Get
            Return _ReportedBy
        End Get
        Set(value As tContact)
            _ReportedBy = value
        End Set
    End Property

    Private _Status As String
    Public Property Status As String
        Get
            Return _Status
        End Get
        Set(value As String)
            _Status = value
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

    Public Overrides Sub Refresh(Optional ByRef Update As this = Nothing)
        MyBase.Refresh(Update)
        If Update Is Nothing Then
            myThings.LoadURL("speak_SalesOrderItems.ashx", {String.Format("ORDNAME={0}", Me.Name)})

        Else
            myThings.LoadType(GetType(tSalesOrderItem))

        End If

    End Sub

    Public Overrides Sub Update(ByRef t As thing)
        If Not t Is Nothing Then
            t.Load()
            With TryCast(t, tSalesOrder)
                For Each c As tSalesOrderItem In .SalesOrderItems
                    If Not Me.SalesOrderItems.Contains(c) Then
                        myThings.LoadURL("speak_SalesOrderItems.ashx", {String.Format("ORDNAME={0}", Me.Name)})
                        Exit For
                    End If
                Next
            End With
        End If

    End Sub

#End Region

End Class
