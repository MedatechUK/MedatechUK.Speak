Imports System.Speech.Recognition
Imports System.Text
Imports Speak

Public Class chHours : Inherits Speak.baseChoice
    Sub New()
        With Me
            Add(New Hour("quarter", "quarter of an hour", New TimeSpan(0, 15, 0)))
            Add(New Hour("half", "half an hour", New TimeSpan(0, 30, 0)))
            Add(New Hour("one", "one hour", New TimeSpan(1, 0, 0)))
            Add(New Hour("two", "two hours", New TimeSpan(2, 0, 0)))
            Add(New Hour("three", "three hours", New TimeSpan(3, 0, 0)))
            Add(New Hour("four", "four hours", New TimeSpan(4, 0, 0)))
            Add(New Hour("five", "five hours", New TimeSpan(5, 0, 0)))
            Add(New Hour("six", "six hours", New TimeSpan(6, 0, 0)))
            Add(New Hour("seven", "seven hours", New TimeSpan(7, 0, 0)))
            Add(New Hour("eight", "eight hours", New TimeSpan(8, 0, 0)))
            Add(New Hour("all day", "eight hours", New TimeSpan(8, 0, 0)))

        End With

    End Sub

End Class

Public Class grhours : Inherits baseGrammar

    Sub New()
        Me.Add(New chHours)

        Dim ret As New GrammarBuilder
        With ret
            .Append(Me(0).Choices)

        End With
        addGrammar(ret, AddressOf Response)

    End Sub

    Private Sub Response(Sender As Object, e As ResponseArgs)

        conversation("grReportA.chTask") = Activator.CreateInstance(conversation("grReportA.chTask").GetType)
        conversation("grReportA.chTask").Context(e.Args(0))

        With conversation("grReportA.chCustomer")
            .Context(conversation("grReportA.chTask"))
            .Save()
            syn.Speak(
                String.Format(
                    "You spent {0} on a {2} for {1}.",
                    TryCast(e.Args(0), Hour).Description,
                    .Name,
                    conversation("grReportA.chTask").Name
                )
            )

        End With
        sre.UnloadAllGrammars()
        grammars("grReportA").LoadAsync()

    End Sub

End Class
