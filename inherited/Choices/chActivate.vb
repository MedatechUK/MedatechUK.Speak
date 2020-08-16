Imports System.IO
Imports System.Speech.Recognition
Imports System.Xml.Serialization
Imports Speak

Public Class chActivate : Inherits Speak.baseChoice

    Public Overrides Sub StaticChoice(ByRef e As List(Of thing))

        With e
            Add(New Initiator("priority", True, New Banter({"hi there", "hi", "hello", "hello dave"})))
            Add(New Initiator("hey priority", True, New Banter({"hey there", "hey", "hi"})))
            Add(New Initiator("thanks priority", False, New Banter({"you're welcome", "no problem", "no worries"})))
            Add(New Initiator("thank you priority", False, New Banter({"you're very welcome", "no, thank you", "no worries", "you're welcome"})))
            Add(New Initiator("ok priority", False, New Banter({"ok", "thanks", "cool"})))
            Add(New Initiator("stop priority", False, New Banter({"ok"})))

        End With

    End Sub

End Class