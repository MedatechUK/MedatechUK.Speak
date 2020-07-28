Imports Speak

Public Class tPhoneCall : Inherits basetask

    Sub New()
        MyBase.New("phone call", eTaskType.servicecall)


    End Sub

    Sub New(t As this)
        MyBase.New(t)

    End Sub

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
