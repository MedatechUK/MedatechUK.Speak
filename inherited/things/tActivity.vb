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

    Sub New(Name As String, t As Type)
        MyBase.New(Name)
        _t = t
    End Sub

    Sub New(t As this)
        MyBase.New(t)

    End Sub

#End Region

End Class
