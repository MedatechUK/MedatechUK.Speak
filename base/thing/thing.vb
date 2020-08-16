Imports System.IO
Imports System.Reflection
Imports System.Xml
Imports System.Xml.Serialization


Public MustInherit Class thing

    MustOverride Sub Context(ParamArray args() As thing)

    Overridable Sub Refresh()

    End Sub

#Region "Properties"

    Private _Loaded As Boolean = False
    Public ReadOnly Property Loaded As Boolean
        Get
            Return _Loaded
        End Get
    End Property

    Private _Description As String = Nothing
    Public Property Description As String
        Get
            Select Case _Description Is Nothing
                Case True
                    Return _Name
                Case Else
                    Return _Description
            End Select

        End Get
        Set(value As String)
            _Description = value
        End Set
    End Property

    Private _id As String
    Public ReadOnly Property id As String
        Get
            Return _id
        End Get
    End Property

    Private _Name As String
    Public Property Name As String
        Get
            Return _Name
        End Get
        Set(value As String)
            _Name = value
        End Set
    End Property

    Private _Path As String
    Public ReadOnly Property Path As String
        Get
            Return _Path
        End Get
    End Property

    Private ReadOnly Property FileName As String
        Get
            Return String.Format(
                "{0}{1}.xml",
                Path,
                id
            )
        End Get
    End Property

    Private ReadOnly Property saveFile As String
        Get
            Dim ret As New FileInfo(
                IO.Path.Combine(
                    BasePath,
                    FileName
                )
            )

            With ret.Directory
                If Not .Exists Then .Create()
            End With

            Return ret.FullName
        End Get
    End Property

    Public ReadOnly Property Ref As String
        Get
            Return String.Format("{0}{1}.xml", Path, id)
        End Get
    End Property

    Public ReadOnly Property typename As String
        Get
            Return _t.type
        End Get
    End Property

#End Region

#Region "Ctor"

    Public Sub New(Name As String)
        _Name = Name
        _id = System.Guid.NewGuid.ToString

        Dim p As Type = Me.GetType
        _Path = String.Format("{0}\", p.Name)
        While Not String.Compare(p.BaseType.Name, GetType(Object).Name) = 0
            p = p.BaseType
            _Path = String.Format("{0}\{1}", p.Name, _Path)

        End While

    End Sub

    Private _t As this
    Public Sub New(t As this)
        _t = t
        _Name = t.name
        _id = t.id


        Dim p As Type = Me.GetType
        _Path = String.Format("{0}\", p.Name)
        While Not String.Compare(p.BaseType.Name, GetType(Object).Name) = 0
            p = p.BaseType
            _Path = String.Format("{0}\{1}", p.Name, _Path)

        End While

        For Each prop As p In t.p
            Dim pr = Me.[GetType]().GetProperty(prop.name, BindingFlags.[Public] Or BindingFlags.Instance)
            If pr.CanWrite Then
                Select Case pr.PropertyType.Name
                    Case GetType(DateTime).Name
                        pr.SetValue(Me, Date.Parse(prop.value))
                    Case GetType(TimeSpan).Name
                        pr.SetValue(Me, TimeSpan.Parse(prop.value))
                    Case GetType(Int32).Name
                        pr.SetValue(Me, CInt(prop.value))
                    Case Else
                        Try
                            pr.SetValue(Me, prop.value)
                        Catch ex As Exception
                            Console.Write(ex.Message)

                        End Try

                End Select
            End If
        Next

        For Each a As thisA In _t.a
            Dim pr = Me.[GetType]().GetProperty(a.name, BindingFlags.[Public] Or BindingFlags.Instance)
            pr.SetValue(Me, New things)
        Next

        _Loaded = False

    End Sub

#End Region

#Region "Methods"

    Public Function Load() As Boolean

        For Each th As t In _t.t
            Dim pr = Me.[GetType]().GetProperty(th.name, BindingFlags.[Public] Or BindingFlags.Instance)

            If myThings(th.GetType).Keys.Contains(th.id) Then
                pr.SetValue(Me, myThings(th.GetType)(th.id))

            Else
                Return False

            End If

        Next

        For Each a As thisA In _t.a
            Dim pr = Me.[GetType]().GetProperty(a.name, BindingFlags.[Public] Or BindingFlags.Instance)
            'pr.SetValue(Me, New things)
            With pr.GetValue(Me)
                For Each q As t In a.t
                    Try
                        Dim f As Boolean = False
                        For Each t As thing In pr.GetValue(Me)
                            If t.id = q.id Then
                                f = True
                                Exit For
                            End If
                        Next
                        If Not f Then .Add(myThings(q.GetType)(q.id))

                    Catch ex As Exception
                        Return False

                    End Try

                Next

            End With

        Next

        _Loaded = True
        With New FileInfo(Me.saveFile)
            If Not .Exists Then Me.Save()

        End With

        Return True

    End Function

    Public Sub Save()
        Using sw As New StreamWriter(saveFile)
            Console.WriteLine(saveFile)

            Using xr As XmlWriter = XmlWriter.Create(sw)
                serialiser.Serialize(xr, New this(Me))

            End Using

        End Using

    End Sub

#End Region

End Class
