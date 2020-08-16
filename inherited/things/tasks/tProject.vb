Imports Speak
Imports System.IO

Public Class tProject : Inherits basetask

#Region "ctor"

    Sub New()
        MyBase.New("project hours", eTaskType.project)

    End Sub

    Sub New(t As this)
        MyBase.New(t)

    End Sub

#End Region

    Public Overrides ReadOnly Property url As String
        Get
            Return String.Format("priority:priform#DOCUMENTS_p:{0}:live:tabulaemerge.ini", Me.id)
        End Get

    End Property

    Private _ProjWBS As things
    Public Property ProjWBS As things
        Get
            Return _ProjWBS
        End Get
        Set(value As things)
            _ProjWBS = value
        End Set

    End Property

#Region "Overriden Methods"

    Public Overrides Sub Context(ParamArray args() As thing)
        'For Each t As thing In args
        '    If TypeOf (t) Is Customer Then
        '        Me.Customer = t

        '    ElseIf TypeOf (t) Is Hour Then
        '        Me.Hour = TryCast(t, Hour).Span

        '    End If
        'Next

    End Sub

    Public Overrides Sub Refresh()
        'myThings.LoadURL("speak_ProjWBS.ashx", {String.Format("ProjNo={0}", Me.Name)})

    End Sub

#End Region


End Class
