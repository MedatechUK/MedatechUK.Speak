Imports System.IO
Imports System.Text
Imports System.Xml
Imports System.Dynamic

Public Class myThing : Inherits Dictionary(Of Type, Dictionary(Of String, thing))

    Public Function Addthing(t As this) As thing
        With Me
            If Not .Keys.Contains(t.GetType) Then
                .Add(t.GetType, New Dictionary(Of String, thing))

            End If
        End With

        With Me(t.GetType)
            If Not .Keys.Contains(t.id) Then
                .Add(t.id, Activator.CreateInstance(t.GetType, {t}))

            End If

        End With

        Return Me(t.GetType)(t.id)

    End Function

    Public Function Load() As Boolean

        Dim ret As Boolean = False
        Using tmp As New disposableList
            For Each k As Type In Me.Keys
                For Each t As thing In Me(k).Values
                    If Not t.Loaded Then
                        tmp.Add(t)
                    End If
                Next
            Next
            For Each t As thing In tmp
                If Not t.Load() Then ret = True
            Next
        End Using

        Return ret

    End Function

    Public Function thisPath(this As XmlNode) As FileInfo

        Dim _Path As String
        Dim p As Type = Type.GetType(this.Attributes("type").Value)
        _Path = String.Format("{0}\{1}.xml", p.Name, this.Attributes("id").Value)
        While Not String.Compare(p.BaseType.Name, GetType(Object).Name) = 0
            p = p.BaseType
            _Path = String.Format("{0}\{1}", p.Name, _Path)

        End While

        Return New FileInfo(
            IO.Path.Combine(
                BasePath,
                _Path
            )
        )

    End Function

    Public Function thisPath(T As Type) As DirectoryInfo

        Dim _Path As String = String.Format("{0}\", T.Name)
        While Not String.Compare(T.BaseType.Name, GetType(Object).Name) = 0
            _Path = String.Format("{0}\{1}", T.BaseType.Name, _Path)
            T = T.BaseType
        End While

        Return New DirectoryInfo(IO.Path.Combine(BasePath, _Path))

    End Function

    Public Function IsThing(ByRef o As Type) As Boolean

        Dim p As Type = o
        Do
            If p Is GetType(thing) Then Return True
            p = p.BaseType

        Loop Until String.Compare(p.Name, GetType(Object).Name) = 0
        Return False

    End Function

    Public Sub LoadURL(uri As String, ParamArray Args As String())

        Dim doc As New XmlDocument
        doc.Load(String.Format("{0}api/live/{1}?Usr={2}&IsRef=0&{3}", BaseURL, uri, PriorityUsr.id, Args(0)))
        For Each this As XmlNode In doc.SelectNodes("data/this")

            If Not Me(Type.GetType(this.Attributes("type").Value)).Keys.Contains(this.Attributes("id").Value) Then
                Using str As New MemoryStream
                    Using sw As New StreamWriter(str)
                        sw.Write(this.OuterXml)
                        sw.Flush()
                        str.Position = 0
                        With myThings.Addthing(serialiser.Deserialize(str))
                            .Refresh()
                        End With
                    End Using
                End Using

            Else
                Dim changes As Boolean = False
                For Each t As XmlNode In this.SelectNodes("a/t")
                    If Not refLoaded(t) Then
                        changes = True
                        Exit For
                    End If
                Next
                If changes Then
                    With Me(Type.GetType(this.Attributes("type").Value))(this.Attributes("id").Value)
                        .Refresh()
                    End With

                End If

            End If

        Next

    End Sub

    Private Function refLoaded(node As XmlNode) As Boolean
        If Me.Keys.Contains(Type.GetType(node.Attributes("type").Value)) Then
            With Me(Type.GetType(node.Attributes("type").Value))
                For Each i As thing In .Values
                    If String.Compare(i.Ref, node.Attributes("ref").Value) = 0 Then
                        Return True
                    End If
                Next

            End With
        End If
        Return False

    End Function

    Public Sub LoadType(t As Type)
        If Not myThings.Keys.Contains(t) Then
            myThings.Add(t, New Dictionary(Of String, thing))
        End If
        With myThings.thisPath(t)
            If Not .Exists Then .Create()
            For Each f As FileInfo In .GetFiles
                Using sr As New StreamReader(f.FullName)
                    myThings.Addthing(serialiser.Deserialize(sr))

                End Using
            Next

        End With

    End Sub

    Public Sub Refresh(ByRef t As Type)

    End Sub


    Public Function ListFormat(List As Dictionary(Of String, things), act As tActivity, Format As String) As String

        Dim str As New StringBuilder
        With str
            For Each k As String In List.Keys
                If k = List.Keys.Last And List.Count > 1 Then
                    .AppendLine()
                    .AppendFormat("{0} ", " and ")
                Else
                    If List.Count > 1 And Not (k = List.Keys.First) Then .AppendLine()
                End If

                If act.t Is Nothing Then
                    If List(k).Count > 1 Then
                        .AppendFormat(Format, TryCast(List(k).First, basetask).Plural.Describe(List(k).Count), k)
                    Else
                        .AppendFormat(Format, TryCast(List(k).First, basetask).Plural.Describe(List(k).Count) & " " & TryCast(List(k).First, basetask).Ending, k)
                    End If
                Else
                    If List(k).Count > 1 Then
                        .AppendFormat(Format, act.Plural.Describe(List(k).Count), k)
                    Else
                        .AppendFormat(Format, act.Plural.Describe(List(k).Count) & " " & TryCast(List(k).First, basetask).Ending, k)
                    End If
                End If

            Next
        End With

        Return str.ToString

    End Function

    Public Function ListFormat(List As things, Optional IndefiniteArticle As Boolean = False)
        Dim str As New StringBuilder
        With str
            For Each t As thing In List
                If t Is List.Last And List.Count > 1 Then
                    .AppendLine
                    .AppendFormat("{0} ", " and ")
                Else
                    If List.Count > 1 And Not (t Is List.First) Then .AppendLine()
                End If

                With t
                    If IndefiniteArticle Then
                        Select Case t.Description.Substring(0, 1).ToLower
                            Case "a", "e", "i", "o", "u"
                                str.AppendFormat("an {0}", t.Description)
                            Case Else
                                str.AppendFormat("a {0}", t.Description)
                        End Select

                    Else
                        str.AppendFormat("{0}", t.Description)

                    End If

                End With

            Next
        End With

        Return str.ToString
    End Function

End Class
