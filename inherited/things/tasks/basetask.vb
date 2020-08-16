Imports System.Text
Imports Speak

Public Enum eTaskType
    servicecall
    salesorder
    project
    task
    phonecall

End Enum

Public MustInherit Class basetask : Inherits thing

#Region "CTOR"
    Sub New(Name As String, taskType As eTaskType)
        MyBase.New(Name)
        _tasktype = taskType
        _TaskDate = Now

    End Sub
    Sub New(t As this)
        MyBase.New(t)
        Select Case Type.GetType(t.type)
            Case GetType(tSalesOrder)
                _tasktype = eTaskType.salesorder

            Case GetType(tServiceCall)
                _tasktype = eTaskType.servicecall

            Case GetType(tProject)
                _tasktype = eTaskType.project

        End Select

    End Sub

#End Region

    MustOverride ReadOnly Property url As String

    Public ReadOnly Property Plural As tplural
        Get
            Select Case Me.TaskType
                Case eTaskType.project
                    Return New tplural("project", "projects")

                Case eTaskType.salesorder
                    Return New tplural("sales order", "sales orders")

                Case eTaskType.servicecall
                    Return New tplural("service call", "service calls")

                Case Else
                    Return New tplural("task", "tasks")

            End Select
        End Get
    End Property

    Private _tasktype As eTaskType
    Public ReadOnly Property TaskType As eTaskType
        Get
            Return _tasktype
        End Get
    End Property

    Private _TaskDate As Date
    Public Property TaskDate As Date
        Get
            Return _TaskDate
        End Get
        Set(value As Date)
            _TaskDate = value
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

    Private _AssignedTo As tStaff
    Public Property AssignedTo As tStaff
        Get
            Return _AssignedTo
        End Get
        Set(value As tStaff)
            _AssignedTo = value
        End Set
    End Property

    Private _Status As String
    Public Property Status As String
        Get
            Return _Status
        End Get
        Set(value As String)
            _Status = value
        End Set
    End Property

    Public Sub Choice(ByRef d As dynChoice)
        d.Add(
            New dynThing(
                String.Format(
                    "{0} ending {1} {2} {3}",
                    Plural.Singular,
                    Right(id, 3).Substring(0, 1),
                    Right(id, 3).Substring(1, 1),
                    Right(id, 3).Substring(2, 1)
                ), Me
            )
        )
        d.Add(
            New dynThing(
                String.Format(
                    "{0} {1} {2} {3}",
                    Plural.Singular,
                    Right(id, 3).Substring(0, 1),
                    Right(id, 3).Substring(1, 1),
                    Right(id, 3).Substring(2, 1)
                ), Me
            )
        )
    End Sub

    Public Function Ending() As String
        Dim str As New StringBuilder
        With str
            .AppendFormat(
                "ending {0} {1} {2}",
                Right(id, 3).Substring(0, 1),
                Right(id, 3).Substring(1, 1),
                Right(id, 3).Substring(2, 1))
            Return str.ToString

        End With
    End Function


End Class
