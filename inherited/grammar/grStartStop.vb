Imports System.Speech.Recognition
Imports Speak

Public Class chBoolean : Inherits Speak.baseChoice

    Sub New()
        With Me
            '.Add(New thing("on"))
            '.Add(New thing("silent"))
        End With

    End Sub

End Class

Public Class grStartStop : Inherits baseGrammar

    Sub New()
        Me.Add(New chBoolean)

        Dim ret As New GrammarBuilder
        With ret
            .Append("speech")
            .Append(Me(0).Choices)
        End With
        addGrammar(ret)

    End Sub

    Public Overrides Sub Response(GrammarSet As Integer, ParamArray Args() As thing)
        Throw New NotImplementedException()
    End Sub

End Class
