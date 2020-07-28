Imports Speak

Public Class tProjWBS : Inherits thing

#Region "ctor"

    Sub New(Name As String)
        MyBase.New(Name)

    End Sub

    Sub New(t As this)
        MyBase.New(t)

    End Sub

#End Region

    Private _Project As tProject
    Public Property Project As tProject
        Get
            Return _Project
        End Get
        Set(value As tProject)
            _Project = value
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

#Region "overriden Methods"

    Public Overrides Sub Context(ParamArray args() As thing)
        Throw New NotImplementedException()
    End Sub

#End Region

End Class
