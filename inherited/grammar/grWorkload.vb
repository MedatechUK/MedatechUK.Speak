Imports System.Speech.Recognition

Public Class grWorkload : Inherits baseGrammar

    Public Overrides ReadOnly Property Initiator As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides Sub Init()

        Me.Add(New chActivity)

        Dim rep As New GrammarBuilder
        With rep
            .Append("list my")
            .Append(Me(0).Choices)

        End With
        addGrammar(rep, AddressOf Response)

    End Sub

#Region "Responses"

    Private Sub Response(Sender As Object, e As ResponseArgs)

        With TryCast(e.Args(0), tActivity)
            If .t Is Nothing Then

            Else
                If myThings.Keys.Contains(.t) Then
                    For Each o As basetask In myThings(.t).Values
                        If Not o.AssignedTo Is Nothing Then
                            If o.AssignedTo.id = PriorityUsr Then
                                syn.Speak(String.Format("You have a {0} for {1}", e.Args(0).Name, o.Customer.Name))
                            End If
                        End If
                    Next

                End If

            End If

        End With

    End Sub

#End Region

End Class
