Imports Speak
Imports System
Imports System.IO
Imports System.Text
Imports System.Xml


Public Class Customer : Inherits thing

#Region "ctor"
    Sub New(Name As String)
        MyBase.New(Name)

    End Sub

    Sub New(t As this)
        MyBase.New(t)

    End Sub

#End Region

#Region "Properties"

    Private _CustName As String
    Public Property CustName As String
        Get
            Return _CustName
        End Get
        Set(value As String)
            _CustName = value
        End Set
    End Property

    Private _Description As String
    Public Shadows Property Description As String
        Get
            Return _Description
        End Get
        Set(value As String)
            _Description = value
        End Set
    End Property

    Private Property _Phone As String
    Public Property Phone As String
        Get
            Return _Phone
        End Get
        Set(value As String)
            _Phone = value
        End Set
    End Property

    Private _Zip As String
    Public Property Zip As String
        Get
            Return _Zip
        End Get
        Set(value As String)
            _Zip = value
        End Set
    End Property

    Private _AssignedTo As tStaff
    Public Property AssignedTo As tStaff
        Get
            Return _AssignedTo
        End Get
        Set(value As tStaff)
            _AssignedTo = value
        End Set
    End Property

    Private _Contacts As things
    Public Property Contacts As things
        Get
            Return _Contacts
        End Get
        Set(value As things)
            _Contacts = value
        End Set

    End Property

    Private _tasks As things
    Public Property tasks As things
        Get
            Return _tasks
        End Get
        Set(value As things)
            _tasks = value
        End Set

    End Property

#End Region

#Region "Methods"

    Public Overrides Sub Context(ParamArray args() As thing)
        'For Each t As thing In args
        '    If TypeOf (t) Is basetask Then


        '    End If

        'Next

    End Sub

    Public Overrides Sub Refresh()
        myThings.LoadURL("speak_Contacts.ashx", {String.Format("CUSTNAME={0}", Me.CustName)})
        myThings.LoadURL("speak_ServiceCall.ashx", {String.Format("CUSTNAME={0}", Me.CustName)})
        myThings.LoadURL("speak_SalesOrder.ashx", {String.Format("CUSTNAME={0}", Me.CustName)})
        myThings.LoadURL("speak_projects.ashx", {String.Format("CUSTNAME={0}", Me.CustName)})

    End Sub

#End Region

    Public ReadOnly Property TaskByStatus(act As tActivity, status As dynThing, Optional OwnerOnly As Boolean = False) As Dictionary(Of String, things)
        Get
            Select Case act.t
                Case GetType(tSalesOrder)
                    Return ByStatus(eTaskType.salesorder, status, OwnerOnly)

                Case GetType(tServiceCall)
                    Return ByStatus(eTaskType.servicecall, status, OwnerOnly)

                Case GetType(tProject)
                    Return ByStatus(eTaskType.project, status, OwnerOnly)

                Case Else
                    Return Nothing

            End Select

        End Get
    End Property

    Public ReadOnly Property TaskByStatus(act As tActivity, Optional OwnerOnly As Boolean = False) As Dictionary(Of String, things)
        Get
            Select Case act.t
                Case GetType(tSalesOrder)
                    Return ByStatus(eTaskType.salesorder, Nothing, OwnerOnly)

                Case GetType(tServiceCall)
                    Return ByStatus(eTaskType.servicecall, Nothing, OwnerOnly)

                Case GetType(tProject)
                    Return ByStatus(eTaskType.project, Nothing, OwnerOnly)

                Case Else
                    Return Nothing

            End Select

        End Get
    End Property

    Public ReadOnly Property TaskByType(Optional OwnerOnly As Boolean = False) As Dictionary(Of String, things)
        Get
            Return ByType(OwnerOnly)

        End Get
    End Property

    Private Function ByStatus(e As eTaskType, status As dynThing, OwnerOnly As Boolean) As Dictionary(Of String, things)
        Dim ret As New Dictionary(Of String, things)
        For Each t As basetask In Me.tasks

            Dim add As Boolean = False
            While True
                If Not e = t.TaskType Then Exit While
                If (t.AssignedTo Is Nothing) Then
                    If (OwnerOnly) Then Exit While
                End If
                If Not (t.AssignedTo Is PriorityUsr) And (OwnerOnly) Then Exit While
                If (Not status Is Nothing) Then
                    If (Not status.Name = t.Status) Then Exit While
                End If

                add = True
                Exit While
            End While

            If add Then
                If Not ret.Keys.Contains(t.Status) Then ret.Add(t.Status, New things)
                ret(t.Status).Add(t)
            End If

        Next
        Return ret
    End Function

    Private Function ByType(OwnerOnly As Boolean) As Dictionary(Of String, things)

        Dim ret As New Dictionary(Of Integer, things)
        Dim ret2 As New Dictionary(Of String, things)

        For Each t As basetask In Me.tasks
            If (t.AssignedTo Is Nothing) Then
                If (Not OwnerOnly) Then
                    If Not ret.Keys.Contains(t.TaskType) Then ret.Add(t.TaskType, New things)
                    ret(t.TaskType).Add(t)
                End If
            ElseIf (Not OwnerOnly Or (t.AssignedTo Is PriorityUsr)) Then
                If Not ret.Keys.Contains(t.TaskType) Then ret.Add(t.TaskType, New things)
                ret(t.TaskType).Add(t)

            End If
        Next
        For Each K As eTaskType In ret.Keys
            Dim key As String
            Select Case ret(K).Count
                Case 1
                    key = TryCast(ret(K).First, basetask).Plural.Singular
                Case Else
                    key = TryCast(ret(K).First, basetask).Plural.Plural
            End Select
            ret2.Add(key, New things)
            For Each i As basetask In ret(K)
                ret2(key).Add(i)
            Next

        Next
        Return ret2

    End Function

    Public Function CustReport(act As tActivity, Optional ownerOnly As Boolean = False) As things

        Dim ret As New things
        For Each T As basetask In Me.tasks
            If ((act.t Is T.GetType) Or (act.t Is Nothing)) Then
                If (T.AssignedTo Is Nothing) Then
                    If (Not ownerOnly) Then ret.Add(T)

                ElseIf (Not ownerOnly Or (t.AssignedTo Is PriorityUsr)) Then
                    ret.Add(T)

                End If

            End If

        Next
        Return ret

    End Function

End Class
