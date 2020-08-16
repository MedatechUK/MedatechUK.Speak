Imports System.Speech.Recognition
Imports System.Text

Public Class grWorkload : Inherits baseGrammar

    Private _my As Boolean = False

    Public Overrides ReadOnly Property Initiator As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides Sub Init()

        Me.Add(New chActivity)
        Me.Add(New chCustomer)

        Dim rep As New GrammarBuilder
        With rep
            .Append("list my")
            .Append(Me(0).Choices)

        End With
        addGrammar(rep, AddressOf ListMy)

        Dim rep3 As New GrammarBuilder
        With rep3
            .Append("list my")
            .Append(Me(0).Choices)
            .Append("for")
            .Append(Me(1).Choices)

        End With
        addGrammar(rep3, AddressOf ListMyFor)

        Dim rep2 As New GrammarBuilder
        With rep2
            .Append("list")
            .Append(Me(0).Choices)
            .Append("for")
            .Append(Me(1).Choices)

        End With
        addGrammar(rep2, AddressOf ListFor)

        If PriorityUsr.CurrentTask IsNot Nothing Then
            Dim rep4 As New GrammarBuilder
            With rep4
                .Append("end task")

            End With
            addGrammar(rep4, AddressOf endTask)
        End If

    End Sub

#Region "Responses"

    Private Sub ListMy(Sender As Object, e As ResponseArgs)

        _my = True

        Dim t As New List(Of Customer)
        For Each cust As Customer In myThings(GetType(Customer)).Values
            Dim rep As things = cust.CustReport(e.Args(0), _my)
            If rep.Count > 0 Then
                t.Add(cust)
            End If
        Next

        If t.Count = 0 Then
            Using B As New Banter(
                    String.Format("there are no {0} assigned to you", TryCast(e.Args(0), tActivity).Plural.Plural),
                    String.Format("sorry, you have no {0} assigned", TryCast(e.Args(0), tActivity).Plural.Plural)
                )
                syn.Speak(B.Response)

            End Using

        Else
            Using d As New dynChoice()
                Dim strs As New List(Of String)
                For Each c As Customer In t
                    Dim list = c.TaskByType(_my)
                    strs.Add(String.Format("{0} for {1}", myThings.ListFormat(list, e.Args(0), "{0} "), c.Name))
                    For Each k As String In list.Keys
                        If list(k).Count = 1 Then
                            d.Add(New dynThing(String.Format("{0} {1}", TryCast(list(k).First, basetask).Plural.Singular, TryCast(list(k).First, basetask).Ending), list(k).First))

                        End If
                    Next

                Next

                Dim str As New StringBuilder
                str.Append("you have ")
                For Each s As String In strs
                    If s Is strs.Last And strs.Count > 1 Then
                        str.AppendLine.AppendFormat("{0} ", " and ")
                    Else
                        If Not (s Is strs.Last) Then str.AppendLine()
                    End If
                    str.AppendFormat("{0}", s)

                Next

                syn.Speak(str.ToString)

                Init()
                If d.Count > 0 Then
                    Me.Add(d)
                    Dim resp2 As New GrammarBuilder
                    With resp2
                        .Append("open")
                        .Append(d.Choices)

                    End With
                    addGrammar(resp2, AddressOf openTask)
                End If

            End Using

        End If

    End Sub

    Private Sub ListMyFor(Sender As Object, e As ResponseArgs)
        _my = True
        listforlistmy(Sender, e)

    End Sub

    Private Sub ListFor(Sender As Object, e As ResponseArgs)
        _my = False
        listforlistmy(Sender, e)

    End Sub

    Private Sub listforlistmy(Sender As Object, e As ResponseArgs)

        With TryCast(e.Args(1), Customer)
            Dim rep As things = .CustReport(e.Args(0))

            If rep.Count = 0 Then
                Using B As New Banter(
                    String.Format("I have no {0} for {1}", TryCast(e.Args(0), tActivity).Plural.Plural, .Name),
                    String.Format("there are no {0} for {1}", TryCast(e.Args(0), tActivity).Plural.Plural, .Name),
                    String.Format("No, I can't find any {0} for {1}", TryCast(e.Args(0), tActivity).Plural.Plural, .Name),
                    String.Format("Sorry, I can't find any {0} for {1}", TryCast(e.Args(0), tActivity).Plural.Plural, .Name),
                    String.Format("{1} have no {0}, sorry", TryCast(e.Args(0), tActivity).Plural.Plural, .Name)
                    )
                    syn.Speak(B.Response)

                End Using

            Else
                Select Case TryCast(e.Args(0), tActivity).t
                    Case GetType(tProject)

                        Dim list As things = .CustReport(e.Args(0), _my)
                        syn.Speak(String.Format("{0} have {1}", .Name, myThings.ListFormat(list, True)))

                        Init()
                        Using d As New dynChoice()
                            For Each T As thing In list
                                d.Add(New dynThing(TryCast(T, tProject).Description, T))
                            Next
                            Me.Add(d)

                            Dim resp3 As New GrammarBuilder
                            With resp3
                                .Append("open")
                                .Append(d.Choices)

                            End With
                            addGrammar(resp3, AddressOf openTask)

                            Dim resp As New GrammarBuilder
                            With resp
                                .Append("list wbs for")
                                .Append(d.Choices)

                            End With
                            addGrammar(resp, AddressOf listWBS)

                            Dim resp2 As New GrammarBuilder
                            With resp2
                                .Append("wbs for")
                                .Append(d.Choices)

                            End With
                            addGrammar(resp2, AddressOf listWBS)

                        End Using

                    Case Nothing ' Workload

                        Dim list = .TaskByType(_my)
                        syn.Speak(String.Format("{0} have {1}", .Name, myThings.ListFormat(list, e.Args(0), "{0},")))

                        Init()
                        ListAndOpen(list)

                    Case Else

                        Dim list = .TaskByStatus(e.Args(0), _my)
                        syn.Speak(String.Format("{0} have {1}", .Name, myThings.ListFormat(list, e.Args(0), "{0} in status {1},")))
                        If TryCast(e.Args(0), tActivity).t = GetType(tProject) Then
                            syn.Speak(String.Format("{0}.", myThings.ListFormat(list.First.Value)))

                        End If

                        Init()
                        ListAndOpen(list)


                End Select
            End If
        End With

    End Sub

    Private Sub listWBS(Sender As Object, e As ResponseArgs)
        Using w As New dynChoice()
            With TryCast(TryCast(e.Args(2), dynThing).target, tProject)
                syn.Speak(
                    String.Format(
                          "{0}'s {1} has wbs codes for {2}.",
                          .Customer.Name,
                          .Description,
                          myThings.ListFormat(.ProjWBS)
                      )
                  )

                For Each wbs As tProjWBS In .ProjWBS
                    w.Add(New dynThing(wbs.Description, wbs))

                Next

            End With

            With TryCast(e.Args(1), Customer)

                Dim list As things = .CustReport(e.Args(0), _my)
                Init()
                Me.Add(w)
                Using d As New dynChoice()
                    For Each T As thing In list
                        d.Add(New dynThing(TryCast(T, tProject).Description, T))
                    Next
                    Me.Add(d)

                    Dim resp3 As New GrammarBuilder
                    With resp3
                        .Append("open")
                        .Append(d.Choices)

                    End With
                    addGrammar(resp3, AddressOf openTask)

                    Dim resp As New GrammarBuilder
                    With resp
                        .Append("list wbs for")
                        .Append(d.Choices)

                    End With
                    addGrammar(resp, AddressOf listWBS)

                    Dim resp4 As New GrammarBuilder
                    With resp4
                        .Append("work on")
                        .Append(w.Choices)

                    End With
                    addGrammar(resp4, AddressOf workon)

                    Dim resp5 As New GrammarBuilder
                    With resp5
                        .Append("report for")
                        .Append(w.Choices)

                    End With
                    addGrammar(resp5, AddressOf reportfor)

                End Using

            End With

        End Using
    End Sub

    Private Sub ListInStatus(Sender As Object, e As ResponseArgs)
        With TryCast(e.Args(1), Customer)
            Dim list = .TaskByStatus(e.Args(0), e.Args(2), _my)
            Dim str As New StringBuilder
            Dim ch As New List(Of String)
            With str
                .AppendFormat("{0} have ", e.Args(1).Name)
                For Each t As basetask In list.First.Value

                    If String.Compare(t.id, list.First.Value.Last.id) = 0 And list.First.Value.Count > 1 Then
                        .AppendFormat("{0} ", " and ")
                    Else
                        If list.First.Value.Count > 1 And Not String.Compare(t.id, list.First.Value.First.id) = 0 Then .AppendLine()
                    End If

                    .AppendFormat("one {0} ending {1} {2} {3}", t.Plural.Singular, Right(t.id, 3).Substring(0, 1), Right(t.id, 3).Substring(1, 1), Right(t.id, 3).Substring(2, 1)).AppendLine()

                Next
                syn.Speak(.ToString)

            End With

            Init()
            Using d As New dynChoice()
                For Each t As basetask In list.First.Value
                    t.Choice(d)
                Next
                Me.Add(d)

                Dim resp As New GrammarBuilder
                With resp
                    .Append("open")
                    .Append(d.Choices)

                End With
                addGrammar(resp, AddressOf openTask)

                ListAndOpen(.TaskByStatus(e.Args(0), _my))

            End Using

        End With

    End Sub

    Private Sub openTask(Sender As Object, e As ResponseArgs)
        With TryCast(TryCast(e.Args(2), dynThing).target, basetask)
            Process.Start(.url)

            Init()
            Select Case .TaskType
                Case eTaskType.salesorder
                    With TryCast(TryCast(e.Args(2), dynThing).target, tSalesOrder)
                        Using d As New dynChoice()
                            For Each t As tSalesOrderItem In .SalesOrderItems
                                t.Choice(d)
                            Next
                            Me.Add(d)

                            Dim resp4 As New GrammarBuilder
                            With resp4
                                .Append("work on")
                                .Append(d.Choices)

                            End With
                            addGrammar(resp4, AddressOf workon)

                            Dim resp5 As New GrammarBuilder
                            With resp5
                                .Append("report for")
                                .Append(d.Choices)

                            End With
                            addGrammar(resp5, AddressOf reportfor)



                        End Using

                    End With

                Case eTaskType.servicecall
                    workon(Sender, e)

            End Select
        End With

    End Sub

    Private Sub workon(Sender As Object, e As ResponseArgs)

        With PriorityUsr
            If .CurrentTask IsNot Nothing Then
                ' Close current report and save
                .CurrentTask.EndReport()
                ' Add the current report to the task log
                .TaskLog.Add(.CurrentTask)
                ' Create a new task
                .CurrentTask = New tReport(TryCast(e.Args.Last, dynThing).target)

            Else
                ' Create a new task
                .CurrentTask = New tReport(TryCast(e.Args.Last, dynThing).target)

            End If
            ' Save the user
            .Save()
        End With

        With TryCast(e.Args.Last, dynThing)
            Select Case .target.GetType
                Case GetType(tServiceCall)
                    With TryCast(.target, tServiceCall)
                        syn.Speak(String.Format("you are working on the {0} {1} for {2}.", .Plural.Singular, .Ending, .Customer.Name))
                        PriorityUsr.CurrentTask.Description = String.Format("a {0} {1} for {2}.", .Plural.Singular, .Ending, .Customer.Name)

                    End With

                Case GetType(tProjWBS)
                    With TryCast(.target, tProjWBS)
                        syn.Speak(String.Format("you are working on {0} for {1}'s {2}.", .Description, .Customer.Name, .Project.Description))
                        PriorityUsr.CurrentTask.Description = String.Format("the {0} wbs of {1}'s {2}.", .Description, .Customer.Name, .Project.Description)

                    End With

                Case GetType(tSalesOrderItem)
                    With TryCast(.target, tSalesOrderItem)
                        syn.Speak(String.Format("you are working on line {0} of {1} {2} for {3}.", .Line.ToString, .SalesOrder.Plural.Singular, .SalesOrder.Ending, .Customer.Name))
                        PriorityUsr.CurrentTask.Description = String.Format("line {0} of {1} {2} for {3}.", .Line.ToString, .SalesOrder.Plural.Singular, .SalesOrder.Ending, .Customer.Name)

                    End With

                Case Else

            End Select

        End With

        Init()

    End Sub

    Private Sub reportfor(Sender As Object, e As ResponseArgs)
        With e
            syn.Speak(
                String.Format(
                    "How many hours did you spend on the {0} for {1}?",
                    .Args(2).Description,
                    .Args(1).Name
                )
            )

            Init()
            Using d As New dynChoice
                With d
                    .Add(New Hour("quarter", "quarter of an hour", e.Args(2), New TimeSpan(0, 15, 0)))
                    .Add(New Hour("half", "half an hour", e.Args(2), New TimeSpan(0, 30, 0)))
                    .Add(New Hour("one", "one hour", e.Args(2), New TimeSpan(1, 0, 0)))
                    .Add(New Hour("two", "two hours", e.Args(2), New TimeSpan(2, 0, 0)))
                    .Add(New Hour("three", "three hours", e.Args(2), New TimeSpan(3, 0, 0)))
                    .Add(New Hour("four", "four hours", e.Args(2), New TimeSpan(4, 0, 0)))
                    .Add(New Hour("five", "five hours", e.Args(2), New TimeSpan(5, 0, 0)))
                    .Add(New Hour("six", "six hours", e.Args(2), New TimeSpan(6, 0, 0)))
                    .Add(New Hour("seven", "seven hours", e.Args(2), New TimeSpan(7, 0, 0)))
                    .Add(New Hour("eight", "eight hours", e.Args(2), New TimeSpan(8, 0, 0)))
                    .Add(New Hour("all day", "eight hours", e.Args(2), New TimeSpan(8, 0, 0)))

                End With

                With Me
                    .Add(d)

                    Dim ret As New GrammarBuilder
                    With ret
                        .Append(d.Choices)

                    End With
                    addGrammar(ret, AddressOf reporttask)

                End With

            End Using

        End With
    End Sub

    Private Sub reporttask(Sender As Object, e As ResponseArgs)
        With PriorityUsr
            .TaskLog.Add(New tReport(TryCast(TryCast(e.Args(2), Hour).Task, dynThing).target, TryCast(e.Args(2), Hour).Span))

            ' Save the user
            .Save()

        End With

        With TryCast(TryCast(e.Args(2), Hour).Task, dynThing)
            Select Case .target.GetType
                Case GetType(tServiceCall)
                    With TryCast(.target, tServiceCall)
                        syn.Speak(
                            String.Format(
                                "I recorded {0} on a {1} for {2}.",
                                TryCast(e.Args(2), Hour).Description,
                                .Plural.Singular,
                                .Customer.Name
                            )
                        )

                    End With

                Case GetType(tProjWBS)
                    With TryCast(.target, tProjWBS)
                        syn.Speak(
                            String.Format(
                                "I recorded {0} on {1}'s {2}.",
                                TryCast(e.Args(2), Hour).Description,
                                .Customer.Name,
                                .Project.Description
                            )
                        )

                    End With

                Case GetType(tSalesOrderItem)
                    With TryCast(.target, tSalesOrderItem)
                        syn.Speak(
                            String.Format(
                            "I recorded {0} on a {1} for {2}.",
                            TryCast(e.Args(2), Hour).Description,
                            .SalesOrder.Plural.Singular,
                            .Customer.Name
                        )
                    )

                    End With

                Case Else

            End Select

        End With

    End Sub

    Private Sub endTask(Sender As Object, e As ResponseArgs)
        With PriorityUsr
            If .CurrentTask IsNot Nothing Then
                ' Close current report and save
                .CurrentTask.EndReport()

                Dim str As New StringBuilder
                Select Case .CurrentTask.Spent.Hours
                    Case 0
                    Case 1
                        str.AppendFormat("{0} hour", .CurrentTask.Spent.Hours.ToString)
                    Case Else
                        str.AppendFormat("{0} hours", .CurrentTask.Spent.Hours.ToString)
                End Select

                Select Case .CurrentTask.Spent.Minutes - (.CurrentTask.Spent.Hours * 60)
                    Case 0
                        str.Append(New Banter({"no time at all", "zero minutes", "less than a minute", "a new yourk minute"}).Response)
                    Case 1
                        str.AppendFormat("{0} minute", .CurrentTask.Spent.Minutes - (.CurrentTask.Spent.Hours * 60).ToString)
                    Case Else
                        str.AppendFormat("{0} minutes", .CurrentTask.Spent.Minutes - (.CurrentTask.Spent.Hours * 60).ToString)
                End Select
                syn.Speak(String.Format("I recorded {0} against {1}.", str.ToString, .CurrentTask.Description))


                ' Add the current report to the task log
                .TaskLog.Add(.CurrentTask)

                ' Clear the current task
                .CurrentTask = Nothing

                ' Save the user
                .Save()

            Else
                syn.Speak("I'm not currently recording a task.")

            End If

        End With

    End Sub

#End Region

#Region "Functions"

    Private Sub ListAndOpen(List As Dictionary(Of String, things))

        Using c As New dynChoice()
            Using b As New dynChoice()
                Using d As New dynChoice()
                    For Each k As String In List.Keys
                        If List(k).Count = 1 Then
                            TryCast(List(k).First, basetask).Choice(b)
                            Select Case TryCast(List(k).First, basetask).TaskType
                                Case eTaskType.servicecall
                                    TryCast(List(k).First, basetask).Choice(c)

                            End Select

                        Else
                            d.Add(New dynThing(k, List(k).First))

                        End If
                    Next

                    If d.Count > 0 Then
                        Me.Add(d)
                        Dim resp As New GrammarBuilder
                        With resp
                            .Append("list")
                            .Append(d.Choices)

                        End With
                        addGrammar(resp, AddressOf ListInStatus)
                    End If

                End Using

                If b.Count > 0 Then
                    Me.Add(b)
                    Dim resp2 As New GrammarBuilder
                    Using d As New dynChoice()
                        With resp2
                            .Append("open")
                            .Append(b.Choices)

                        End With
                    End Using
                    addGrammar(resp2, AddressOf openTask)
                End If

                If c.Count > 0 Then
                    Dim resp4 As New GrammarBuilder
                    With resp4
                        .Append("work on")
                        .Append(b.Choices)

                    End With
                    addGrammar(resp4, AddressOf workon)

                    Dim resp5 As New GrammarBuilder
                    With resp5
                        .Append("report for")
                        .Append(b.Choices)

                    End With
                    addGrammar(resp5, AddressOf reportfor)

                End If


            End Using
        End Using
    End Sub

#End Region

End Class
