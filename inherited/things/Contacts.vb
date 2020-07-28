Imports Speak
Imports System
Imports System.IO

Public Class tContact : Inherits thing

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

    Private _HomePhone As String
    Public Property HomePhone As String
        Get
            Return _HomePhone
        End Get
        Set(value As String)
            _HomePhone = value
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

    Private _Customer As Customer
    Public Property Customer As Customer
        Get
            Return _Customer
        End Get
        Set(value As Customer)
            _Customer = value
        End Set

    End Property

#End Region

#Region "Methods"

    Public Overrides Sub Context(ParamArray args() As thing)
        'For Each t As thing In args
        '    If TypeOf (t) Is basetask Then
        '        Me.Tasks.Add(t)

        '    End If

        'Next

    End Sub

#End Region

End Class
