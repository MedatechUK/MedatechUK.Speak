Imports Speak

Public Class tActivity : Inherits thing

    Public Overrides Sub Context(ParamArray args() As thing)
        Throw New NotImplementedException()
    End Sub

#Region "ctor"

    Private _t As Type
    Public Property t As Type
        Get
            Return _t
        End Get
        Set(value As Type)
            _t = value
        End Set
    End Property

    Private _plural As tplural
    Public Property Plural As tplural
        Get
            Return _plural
        End Get
        Set(value As tplural)
            _plural = value
        End Set
    End Property

    Sub New(Name As String, plural As tplural, t As Type)
        MyBase.New(Name)
        _plural = plural
        _t = t

    End Sub

    Sub New(t As this)
        MyBase.New(t)

    End Sub

#End Region

End Class
