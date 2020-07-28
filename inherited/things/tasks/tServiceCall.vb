Imports Speak

Public Class tServiceCall : Inherits basetask

#Region "ctor "

    Sub New()
        MyBase.New("service call", eTaskType.servicecall)


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

    Public Overrides Sub Context(ParamArray args() As thing)
        'For Each t As thing In args
        '    If TypeOf (t) Is Customer Then
        '        Me.Customer = t

        '    ElseIf TypeOf (t) Is Hour Then
        '        Me.Hour = TryCast(t, Hour).Span

        '    End If
        'Next

    End Sub

End Class
