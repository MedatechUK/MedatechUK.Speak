Imports Speak
Imports System
Imports System.IO
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

    Private _ServiceCalls As things
    Public Property ServiceCalls As things
        Get
            Return _ServiceCalls
        End Get
        Set(value As things)
            _ServiceCalls = value
        End Set

    End Property

    Private _SalesOrders As things
    Public Property SalesOrders As things
        Get
            Return _SalesOrders
        End Get
        Set(value As things)
            _SalesOrders = value
        End Set

    End Property

    Private _Projects As things
    Public Property Projects As things
        Get
            Return _Projects
        End Get
        Set(value As things)
            _Projects = value
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

    Public Overrides Sub Refresh(Optional ByRef Update As this = Nothing)
        MyBase.Refresh(Update)
        If Update Is Nothing Then
            myThings.LoadURL("speak_Contacts.ashx", {String.Format("CUSTNAME={0}", Me.CustName)})
            myThings.LoadURL("speak_ServiceCall.ashx", {String.Format("CUSTNAME={0}", Me.CustName)})
            myThings.LoadURL("speak_SalesOrder.ashx", {String.Format("CUSTNAME={0}", Me.CustName)})
            myThings.LoadURL("speak_projects.ashx", {String.Format("CUSTNAME={0}", Me.CustName)})

        Else
            With myThings
                .LoadType(GetType(tContact))
                .LoadType(GetType(tServiceCall))
                .LoadType(GetType(tProject))
                .LoadType(GetType(tSalesOrder))

            End With

        End If

    End Sub

    Public Overrides Sub Update(ByRef t As thing)
        If Not t Is Nothing Then
            t.Load()
            With TryCast(t, Customer)
                For Each c As tContact In .Contacts
                    If Not Me.Contacts.Contains(c) Then
                        myThings.LoadURL("speak_Contacts.ashx", {String.Format("CUSTNAME={0}", Me.CustName)})
                        Exit For
                    End If
                Next
                For Each c As tServiceCall In .ServiceCalls
                    If Not Me.ServiceCalls.Contains(c) Then
                        myThings.LoadURL("speak_ServiceCall.ashx", {String.Format("CUSTNAME={0}", Me.CustName)})
                        Exit For
                    End If
                Next

                For Each c As tSalesOrder In .SalesOrders
                    If Not Me.SalesOrders.Contains(c) Then
                        myThings.LoadURL("speak_SalesOrder.ashx", {String.Format("CUSTNAME={0}", Me.CustName)})
                        Exit For
                    End If
                Next

                For Each c As tProject In .Projects
                    If Not Me.Projects.Contains(c) Then
                        myThings.LoadURL("speak_projects.ashx", {String.Format("CUSTNAME={0}", Me.CustName)})
                        Exit For
                    End If
                Next

            End With

        End If

    End Sub

#End Region

End Class
