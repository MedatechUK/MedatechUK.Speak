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

    End Sub

#End Region

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

    Private _Description As String
    Public Shadows Property Description As String
        Get
            Return _Description
        End Get
        Set(value As String)
            _Description = value
        End Set
    End Property

End Class
