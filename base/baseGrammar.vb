Imports System.Speech.Recognition
Imports System.Text

Public Class GrammarHandler

    Private _Grammar As GrammarBuilder
    Public ReadOnly Property Grammar As GrammarBuilder
        Get
            Return _Grammar
        End Get
    End Property

    Private _handler As EventHandler
    Public ReadOnly Property Handler As EventHandler
        Get
            Return _handler
        End Get
    End Property

    Public Sub New(Grammar As GrammarBuilder, handler As EventHandler)
        _Grammar = Grammar
        _handler = handler

    End Sub

End Class

Public Class ResponseArgs : Inherits EventArgs

    Private _args() As thing
    Public ReadOnly Property Args() As thing()
        Get
            Return _args
        End Get

    End Property

    Sub New(ParamArray Args() As thing)
        _args = Args

    End Sub

End Class

Public MustInherit Class baseGrammar : Inherits List(Of baseChoice)

    Public MustOverride Sub Init()

    Public Overridable ReadOnly Property Initiator As Boolean
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property Name As String
        Get
            Return Me.GetType().Name
        End Get
    End Property

    Sub New()
        grammars.Add(Me.Name, Me)
        Init()

    End Sub

    Public Property conv(field As String) As thing
        Get
            If InStr(field, ".") > 0 Then
                Return conversation(field)
            Else
                Return conversation(String.Format("{0}.{1}", Me.Name, field))

            End If
        End Get
        Set(value As thing)
            If InStr(field, ".") > 0 Then
                conversation(field) = value
            Else
                conversation(String.Format("{0}.{1}", Me.Name, field)) = value
            End If
        End Set
    End Property

    Private _GrammarSet As New Dictionary(Of Integer, GrammarHandler)
    Public Sub addGrammar(Grammar As GrammarBuilder, ByRef handler As EventHandler)
        _GrammarSet.Add(_GrammarSet.Count, New GrammarHandler(Grammar, handler))

    End Sub

    Public Sub LoadAsync()

        If _GrammarSet.Count = 0 Then Init()

        Dim delg As New List(Of Grammar)
        For Each g As Grammar In sre.Grammars
            If String.Compare(Split(g.Name, ":")(0), Me.GetType().Name) = 0 Then
                delg.Add(g)
            End If
        Next

        Do
            Try
                For Each g As Grammar In delg
                    sre.UnloadGrammar(g)
                Next
                Exit Do

            Catch ex As Exception
                Threading.Thread.Sleep(100)

            End Try

        Loop

        Dim i As Integer = 0
        With _GrammarSet
            For Each k As Integer In .Keys
                Dim g = New Grammar(.Item(k).Grammar)
                g.Name = String.Format("{0}:{1}", Me.GetType().Name, k.ToString)
                sre.LoadGrammarAsync(g)
                Console.WriteLine(_GrammarSet(k.ToString).Grammar.DebugShowPhrases)
                Console.WriteLine()

                i += 1
            Next

        End With

    End Sub

    Public Sub ParseResponse(GrammarSet As Integer, Text As String)

        Dim g As GrammarBuilder = _GrammarSet(GrammarSet).Grammar
        Dim d As New Dictionary(Of Integer, Object)
        Dim l As List(Of String)
        Dim resp(Me.Count - 1) As thing

        Dim s As String = g.DebugShowPhrases
        While InStr(s, "[") > 0
            If s.Substring(0, InStr(s, "[") - 1).Length > 0 Then
                Dim str As String = Trim(s.Substring(0, InStr(s, "[") - 1))
                Try
                    d.Add(d.Keys.Max + 1, str)
                Catch
                    d.Add(1, str)
                End Try
                s = Trim(s.Substring(InStr(s, "[")))

            End If

            l = New List(Of String)
            For Each v As String In Split(s.Substring(0, InStr(s, "]") - 1), ",")
                l.Add(Trim(v))
            Next
            Try
                d.Add(d.Keys.Max + 1, l)
            Catch
                d.Add(1, l)
            End Try
            s = Trim(s.Substring(InStr(s, "]")))

        End While
        If s.Length > 0 Then
            l = New List(Of String)
            l.Add(Trim(s))
            Try
                d.Add(d.Keys.Max + 1, l)
            Catch
                d.Add(1, l)
            End Try
        End If

        For i As Integer = 1 To d.Keys.Max
            Select Case d(i).GetType
                Case GetType(String)
                    Text = Trim(Text.Substring(d(i).Length - 1))

                Case Else
                    Dim f As Boolean = False
                    For q As Integer = 0 To Me.Count - 1
                        For Each this As thing In Me(q)
                            Try
                                If String.Compare(this.Name, Text.Substring(0, this.Name.Length), True) = 0 Then
                                    f = True
                                    resp(q) = this
                                    If Not conversation.Keys.Contains(String.Format("{0}.{1}", Me.Name, Me(q).Name)) Then
                                        conversation.Add(String.Format("{0}.{1}", Me.Name, Me(q).Name), this)
                                    Else
                                        conversation(String.Format("{0}.{1}", Me.Name, Me(q).Name)) = this
                                    End If

                                    Text = Trim(Text.Substring(this.Name.Length))
                                    Exit For

                                End If
                            Catch : End Try

                        Next
                        If f Then Exit For

                    Next

            End Select

        Next
        For q As Integer = 0 To Me.Count - 1
            If resp(q) Is Nothing Then
                If conversation.Keys.Contains(String.Format("{0}.{1}", Me.Name, Me(q).Name)) Then
                    resp(q) = conversation(String.Format("{0}.{1}", Me.Name, Me(q).Name))
                End If
            End If
        Next

        Dim ev As EventHandler = _GrammarSet(GrammarSet).Handler
        _GrammarSet.Clear()
        Me.Clear()
        ev.Invoke(Me, New ResponseArgs(resp))
        LoadAsync()

    End Sub

End Class
