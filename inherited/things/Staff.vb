Imports Speak
Imports System
Imports System.IO

Public Class tStaff : Inherits thing

#Region "ctor"
    Sub New(Name As String)
        MyBase.New(Name)

    End Sub

    Sub New(t As this)
        MyBase.New(t)

    End Sub

#End Region

#Region "Properties"

    Private _Position As String
    Public Property Position As String
        Get
            Return _Position
        End Get
        Set(value As String)
            _Position = value
        End Set
    End Property

    Private _Phone As String
    Public Property Phone As String
        Get
            Return _Phone
        End Get
        Set(value As String)
            _Phone = value
        End Set
    End Property

    Private _Mobile As String
    Public Property Mobile As String
        Get
            Return _Mobile
        End Get
        Set(value As String)
            _Mobile = value
        End Set
    End Property

    Private _Email As String
    Public Property Email As String
        Get
            Return _Email
        End Get
        Set(value As String)
            _Email = value
        End Set
    End Property

    Private _CurrentTask As tReport
    Public Property CurrentTask As tReport
        Get
            Return _CurrentTask
        End Get
        Set(value As tReport)
            _CurrentTask = value
        End Set
    End Property

    Private _TaskLog As New things
    Public Property TaskLog As things
        Get
            Return _TaskLog
        End Get
        Set(value As things)
            _TaskLog = value
        End Set

    End Property

#End Region

#Region "Methods"

    Public Overrides Sub Context(ParamArray args() As thing)


    End Sub

#End Region

End Class
