Imports Speak

Public Class Hour : Inherits thing

    Private _Description As String
    Public Overrides Property Description As String
        Get
            Return _Description
        End Get
        Set(value As String)
            _Description = value
        End Set
    End Property

    Private _t As TimeSpan
    Public ReadOnly Property Span As TimeSpan
        Get
            Return _t
        End Get
    End Property

    Sub New()
        MyBase.New("")
    End Sub

    Sub New(Name As String, Description As String, t As TimeSpan)
        MyBase.New(Name)
        _Description = Description
        _t = t
    End Sub

    Sub New(t As this)
        MyBase.New(t)

    End Sub

    Public Overrides Sub Context(ParamArray args() As thing)
        Throw New NotImplementedException()

    End Sub

End Class
