Imports Speak

Public Class tReport : Inherits thing

    Private _started As Boolean
    Private _starttime As DateTime
    Public Property starttime As DateTime
        Get
            Return _starttime
        End Get
        Set(value As DateTime)
            _starttime = value
        End Set
    End Property

    Sub New(ByRef task As thing)
        MyBase.New("report")
        _task = task
        _starttime = Now
        _started = True

        Save()

    End Sub
    Sub New(ByRef task As thing, Spent As TimeSpan)
        MyBase.New("report")
        _task = task
        _Spent = Spent

        Save()

    End Sub

    Sub New(t As this)
        MyBase.New(t)

    End Sub
    Sub New(Name As String)
        MyBase.New(Name)

    End Sub

    Private _Spent As TimeSpan
    Public Property Spent As TimeSpan
        Get
            If Not _started Then
                Return _Spent
            Else
                Return New TimeSpan(Now.Ticks - _starttime.Ticks)
            End If
        End Get
        Set(value As TimeSpan)
            _Spent = value
        End Set
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

    Public Overrides Sub Context(ParamArray args() As thing)
        Throw New NotImplementedException()
    End Sub

    Public Sub EndReport()
        _Spent = New TimeSpan(Now.Ticks - _starttime.Ticks)
        Save()

    End Sub

End Class
