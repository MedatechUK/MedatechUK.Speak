Imports System.IO
Imports System.Speech.Recognition
Imports System.Xml.Serialization
Imports Speak

Public Class chHours : Inherits Speak.baseChoice

    Public Overrides ReadOnly Property ChoiceType As Type
        Get
            Return GetType(Hour)

        End Get
    End Property

    Public Overrides Sub StaticChoice(ByRef e As List(Of thing))
        With e
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