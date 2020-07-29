

Public Class Banter : Inherits List(Of String)

    Private Random As New Random

    Sub New(ParamArray args() As String)
        For Each str As String In args
            Me.Add(str)
        Next

    End Sub

    Public Function Response() As String
        Return Me(Random.Next(0, Me.Count - 1))

    End Function


End Class
