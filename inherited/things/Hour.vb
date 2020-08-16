Imports Speak

Public Class Hour : Inherits thing

    Private _t As TimeSpan
    Public ReadOnly Property Span As TimeSpan
        Get
            Return _t
        End Get
    End Property

    Private _task As thing
    Public Property Task As thing
        Get
            Return _task
        End Get
        Set(value As thing)
            _task = value
        End Set
    End Property

    Sub New()
        MyBase.New("")
    End Sub

    Sub New(Name As String, Description As String, task As thing, t As TimeSpan)
        MyBase.New(Name)
        Me.Description = Description
        _task = task
        _t = t
    End Sub

    Sub New(t As this)
        MyBase.New(t)

    End Sub

    Public Overrides Sub Context(ParamArray args() As thing)
        Throw New NotImplementedException()

    End Sub

End Class
