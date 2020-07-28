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

    Public Overrides Sub Refresh(Optional ByRef Update As this = Nothing)
        MyBase.Refresh(Update)
        If Update Is Nothing Then
            myThings.LoadURL("speak_ProjWBS.ashx", {String.Format("ProjNo={0}", Me.Name)})

        Else
            myThings.LoadType(GetType(tProjWBS))

        End If

    End Sub

    Public Overrides Sub Update(ByRef t As thing)
        If Not t Is Nothing Then
            t.Load()
            With TryCast(t, tProject)
                For Each c As tProjWBS In .ProjWBS
                    If Not Me.ProjWBS.Contains(c) Then
                        myThings.LoadURL("speak_ProjWBS.ashx", {String.Format("ProjNo={0}", Me.Name)})
                        Exit For
                    End If
                Next
            End With
        End If

    End Sub

#End Region


End Class
