Imports System.IO
Imports System.Xml

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
        With Me
            For Each k As Type In .Keys
                For Each t As thing In Me(k).Values
                    If Not t.Loaded Then
                        t.Load()
                        Return True

                    End If

                Next
            Next

        End With
        Return False

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
        doc.Load(String.Format("{0}{1}?Usr={2}&IsRef=0&{3}", BaseURL, uri, PriorityUsr, Args(0)))
        For Each this As XmlNode In doc.SelectNodes("data/this")
            With myThings.thisPath(this)
                If Not .Exists Then
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
                    Using sr As New StreamReader(.FullName)
                        Dim T As this = serialiser.Deserialize(sr)
                        With myThings.Addthing(T)
                            .Refresh(T)

                        End With

                    End Using

                End If

            End With
        Next

    End Sub

    Public Sub LoadType(t As Type)
        If Not myThings.Keys.Contains(GetType(tContact)) Then
            For Each f As FileInfo In myThings.thisPath(GetType(tContact)).GetFiles
                Using sr As New StreamReader(f.FullName)
                    myThings.Addthing(serialiser.Deserialize(sr))
                End Using
            Next
        End If

    End Sub

    Public Sub Refresh(ByRef t As Type)




    End Sub

End Class
