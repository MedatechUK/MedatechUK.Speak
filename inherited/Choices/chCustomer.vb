Imports System.IO
Imports System.Speech.Recognition
Imports System.Xml.Serialization
Imports Speak

Public Class chCustomer : Inherits Speak.baseChoice

    Public Overrides ReadOnly Property ChoiceType As Type
        Get
            Return GetType(Customer)

        End Get
    End Property

End Class