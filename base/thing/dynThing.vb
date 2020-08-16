Imports Speak

Public Class dynThing : Inherits thing

    Public Overrides Sub Context(ParamArray args() As thing)
        Throw New NotImplementedException()
    End Sub

    Private _target As thing
    Public ReadOnly Property target As thing
        Get
            Return _target
        End Get
    End Property

    Sub New(name As String, Optional ByRef target As thing = Nothing)
        MyBase.New(name)
        _target = target

    End Sub

End Class
