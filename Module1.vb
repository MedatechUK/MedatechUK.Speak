Imports System.Globalization
Imports System.IO
Imports System.Reflection
Imports System.Speech
Imports System.Speech.Recognition
Imports System.Speech.Synthesis
Imports System.Xml
Imports System.Xml.Serialization

Module Module1

    Public syn As New SpeechSynthesizer
    Public sre As New SpeechRecognitionEngine(New CultureInfo("en-gb"))
    Public conversation As New Dictionary(Of String, thing)
    Public grammars As New Dictionary(Of String, baseGrammar)
    Public myThings As New myThing
    Public serialiser As New XmlSerializer(GetType(this))

    Private _Active As Boolean = False
    Public Property Active As Boolean
        Get
            Return _Active
        End Get
        Set(value As Boolean)
            _Active = value
            If _Active Then
                For Each bg As baseGrammar In grammars.Values
                    bg.LoadAsync()

                Next

            Else
                sre.UnloadAllGrammars()
                For Each bg As baseGrammar In grammars.Values
                    If bg.Initiator Then bg.LoadAsync()

                Next

            End If
        End Set
    End Property

    Public ReadOnly Property BasePath
        Get
            Return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
        End Get

    End Property

    Public ReadOnly Property BaseURL As String
        Get
            Return "https://priority.medatechuk.com/api/live/"
        End Get
    End Property

    Public ReadOnly Property PriorityUsr As String
        Get
            Return "SimonB"
        End Get
    End Property

    Sub Main()

        myThings.LoadURL("speak_Staff.ashx", {""})
        myThings.LoadURL("speak_customers.ashx", {""})

        Do While myThings.Load : Loop

        Dim r As New grReportA
        Dim a As New grActivate
        Dim w As New grWorkload

        With syn
            .SetOutputToDefaultAudioDevice()

        End With

        With sre
            .SetInputToDefaultAudioDevice()
            AddHandler .SpeechRecognized, AddressOf hRecognise

            For Each bg As baseGrammar In grammars.Values
                If bg.Initiator Then bg.LoadAsync()

            Next

            .RecognizeAsync(RecognizeMode.Multiple)

            Console.ReadKey()

        End With

    End Sub

    Private Sub hRecognise(sender As Object, e As SpeechRecognizedEventArgs)

        sre.RecognizeAsyncStop()

        With e
            For Each g As String In grammars.Keys
                If String.Compare(g, Split(.Result.Grammar.Name, ":")(0)) = 0 Then
                    If (.Result.Confidence < 0.6) Then
                        If Not grammars(g).Initiator Then syn.Speak("Sorry, I didn't catch that.")

                    Else
                        If Not grammars(g).Initiator Then syn.Speak("OK.")
                        Console.Clear()
                        grammars(g).ParseResponse(Split(.Result.Grammar.Name, ":")(1), .Result.Text)

                    End If

                End If
            Next
        End With

        sre.RecognizeAsync(RecognizeMode.Multiple)

    End Sub

End Module
