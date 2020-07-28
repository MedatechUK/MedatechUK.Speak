Imports Speak

Public Class Initiator : Inherits thing

    Private _active As Boolean = False
    Public Property Active As Boolean
        Get
            Return _active
        End Get
        Set(value As Boolean)
            _active = value
        End Set
    End Property

    Sub New(Name As String, active As Boolean, resp As Banter)
        MyBase.New(Name)
        _active = active
        _response = resp

    End Sub

    Private _response As Banter
    Public Property Response As Banter
        Get
            Return _response
        End Get
        Set(value As Banter)
            _response = value
        End Set
    End Property

    Public Overrides Sub Context(ParamArray args() As thing)
        Throw New NotImplementedException()

    End Sub

End Class
