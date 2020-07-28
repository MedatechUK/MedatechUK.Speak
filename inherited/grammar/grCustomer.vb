Imports System.Speech.Recognition
Imports System.Text
Imports Speak

Public Class grCustomer : Inherits baseGrammar

    Sub New()
        Me.Add(New chCustomer)

        Dim ret As New GrammarBuilder
        With ret
            .Append("for")
            .Append(Me(0).Choices)

        End With
        addGrammar(ret, AddressOf Response)

    End Sub

    Private Sub Response(Sender As Object, e As ResponseArgs)
        With e
            conversation("grReportA.chTask").Context(.Args(0))
            syn.Speak(
                String.Format(
                    "How many hours did you spend on the {0} for {1}?",
                    TryCast(conversation("grReportA.chTask"), basetask).Name,
                    TryCast(conversation("grReportA.chTask"), basetask).Customer.Name
                )
            )

            sre.UnloadAllGrammars()
            grammars("grhours").LoadAsync()

        End With

    End Sub

End Class
