Imports System.IO
Imports System.Speech.Recognition
Imports System.Xml.Serialization
Imports Speak

Public Class chTask : Inherits Speak.baseChoice

    Public Overrides Sub StaticChoice(ByRef e As List(Of thing))
        With e
            .Add(New tServiceCall)
            .Add(New tSalesOrder)
            .Add(New tTask)
            .Add(New tPhoneCall)

        End With

    End Sub

End Class