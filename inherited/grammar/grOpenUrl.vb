Imports System.Speech.Recognition

Public Class grOpenUrl : Inherits baseGrammar

    Public Overrides ReadOnly Property Initiator As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides Sub Init()

        Me.Add(New chCustomer)

        Dim rep2 As New GrammarBuilder
        With rep2
            .Append("open")
            .Append(Me(0).Choices)

        End With
        addGrammar(rep2, AddressOf Response2)

    End Sub

#Region "Responses"

    Private Sub Response(Sender As Object, e As ResponseArgs)
        For Each cust As Customer In myThings(GetType(Customer)).Values
            'syn.Speak(cust.CustReport(e.Args(0), True))

        Next

    End Sub

    Private Sub Response2(Sender As Object, e As ResponseArgs)


    End Sub

#End Region

End Class
