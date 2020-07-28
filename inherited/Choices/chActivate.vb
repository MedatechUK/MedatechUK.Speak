Imports System.IO
Imports System.Speech.Recognition
Imports System.Xml.Serialization
Imports Speak

Public Class chActivate : Inherits Speak.baseChoice

    Public Overrides Sub StaticChoice(ByRef e As List(Of thing))

        Dim startBanter As New Banter({"hi there", "hey", "hi", "I'm here", "I'm listening", "hello dave"})
        Dim thanksBanter As New Banter({"you're very welcome", "you're welcome", "welcome", "sure", "no problem"})

        With e
            Add(New Initiator("priority", True, startBanter))
            Add(New Initiator("hey priority", True, startBanter))
            Add(New Initiator("thanks priority", False, thanksBanter))
            Add(New Initiator("thank you priority", False, thanksBanter))
            Add(New Initiator("ok priority", False, New Banter({"ok", "thanks", "cool"})))
            Add(New Initiator("priority stop", False, New Banter({"ok"})))

        End With

    End Sub

End Class