Imports System.Speech.Recognition

Public Class grActivate : Inherits baseGrammar

    Public Overrides ReadOnly Property Initiator As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides Sub Init()

        Me.Add(New chActivate)

        Dim rep As New GrammarBuilder
        With rep
            .Append(Me(0).Choices)

        End With
        addGrammar(rep, AddressOf Response)

    End Sub

#Region "Responses"

    Private Sub Response(Sender As Object, e As ResponseArgs)
        With TryCast(e.Args(0), Initiator)
            Select Case .Active
                Case True
                    syn.Speak(
                        .Response.Response
                    )
                Case Else
                    syn.Speak(
                        .Response.Response
                    )

            End Select

            Active = .Active

        End With

    End Sub

#End Region

End Class
