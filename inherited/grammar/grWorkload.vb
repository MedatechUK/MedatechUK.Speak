Imports System.Speech.Recognition

Public Class grWorkload : Inherits baseGrammar

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
        addGrammar(rep, AddressOf Response)



        Dim rep2 As New GrammarBuilder
        With rep2
            .Append("list")
            .Append(Me(0).Choices)
            .Append("for")
            .Append(Me(1).Choices)

        End With
        addGrammar(rep2, AddressOf Response2)

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

    Private Sub Response2(Sender As Object, e As ResponseArgs)
        With TryCast(e.Args(1), Customer)
            syn.Speak(String.Format("{0} has", .Name))
            syn.Speak(String.Format("{0}", TryCast(e.Args(0), tActivity).Plural.Describe(.ServiceCalls.Count)))
        End With


    End Sub

#End Region

End Class
