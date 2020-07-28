Imports System.Speech.Recognition

Public Class grReportA : Inherits baseGrammar

    Sub New()
        MyBase.New
        init()

    End Sub

    Public Overrides Sub Init()

        Me.Add(New chTask)
        Me.Add(New chCustomer)

        Dim rep As New GrammarBuilder
        With rep
            .Append("report a")
            .Append(Me(0).Choices)
            .Append("for")
            .Append(Me(1).Choices)

        End With
        addGrammar(rep, AddressOf Response)

        Dim rep2 As New GrammarBuilder
        With rep2
            .Append("report a")
            .Append(Me(0).Choices)

        End With
        addGrammar(rep2, AddressOf Response2)

    End Sub

#Region "Responses"
    ''' <summary>
    ''' Report A {0} for {1}
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub Response(Sender As Object, e As ResponseArgs)

        With e
            syn.Speak(
                String.Format(
                    "How many hours did you spend on the {0} for {1}?",
                    .Args(0).Name,
                    .Args(1).Name
                )
            )

            With Me
                .Add(New chHours)

                Dim ret As New GrammarBuilder
                With ret
                    .Append(Me(0).Choices)

                End With
                addGrammar(ret, AddressOf Response3)

            End With

        End With

    End Sub

    ''' <summary>
    ''' Report a {0}
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub Response2(Sender As Object, e As ResponseArgs)
        With e
            syn.Speak(
                String.Format(
                    "Who is the {0} for?",
                    .Args(0).Name
                )
            )

            With Me
                .Add(New chCustomer)

                Dim ret As New GrammarBuilder
                With ret
                    .Append(Me(0).Choices)

                End With
                addGrammar(ret, AddressOf Response4)

            End With

        End With

    End Sub

    ''' <summary>
    ''' How many hours did you spend on the {0} for {1}?
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub Response3(Sender As Object, e As ResponseArgs)

        With conv("chTask")
            .Context(e.Args(0))
            .Context(conv("chCustomer"))
            .Save()

        End With

        With conv("chCustomer")
            .Context(conv("chTask"))
            .Save()

            syn.Speak(
                String.Format(
                    "You spent {0} on a {2} for {1}.",
                    TryCast(e.Args(0), Hour).Description,
                    .Name,
                    conv("chTask").Name
                )
            )

        End With

    End Sub

    ''' <summary>
    ''' Who is the {0} for?
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub Response4(Sender As Object, e As ResponseArgs)
        With e
            With conv("chTask")
                .Context(e.Args(0))

            End With
            syn.Speak(
                String.Format(
                    "How many hours did you spend on the {0} for {1}?",
                    TryCast(conv("chTask"), basetask).Name,
                    TryCast(conv("chTask"), basetask).Customer.Name
                )
            )

            With Me
                .Add(New chHours)

                Dim ret As New GrammarBuilder
                With ret
                    .Append(Me(0).Choices)

                End With
                addGrammar(ret, AddressOf Response3)

            End With

        End With

    End Sub

#End Region

End Class
