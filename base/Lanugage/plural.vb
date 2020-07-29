Public Class tplural

    Private _singular As String
    Public Property Singular As String
        Get
            Return _singular
        End Get
        Set(value As String)
            _singular = value
        End Set
    End Property

    Private _plural As String
    Public Property Plural As String
        Get
            Return _plural
        End Get
        Set(value As String)
            _plural = value
        End Set
    End Property

    Sub New(Singular As String, Plural As String)
        _singular = Singular
        _plural = Plural

    End Sub

    Public Function Describe(count As Integer) As String
        Select Case count
            Case 0
                Return String.Format("no {0}", _plural)

            Case 1
                Select Case _singular.Substring(0, 1).ToLower
                    Case "a", "e", "i", "o", "u", "h"
                        Return String.Format("{0} {1}", New Banter({"one", "an"}).Response, _singular)
                    Case Else
                        Return String.Format("{0} {1}", New Banter({"one", "a"}).Response, _singular)
                End Select

            Case 2
                Return String.Format("{0} {1}", New Banter({"two", "a couple of", "a pair of"}).Response, _plural)

            Case 6
                Return String.Format("{0} {1}", New Banter({"six", "half a dozen"}).Response, _plural)

            Case 6
                Return String.Format("{0} {1}", New Banter({"twelve", "a dozen"}).Response, _plural)

            Case Else
                Return String.Format("{0} {1}", count.ToString, _plural)

        End Select

    End Function

End Class
