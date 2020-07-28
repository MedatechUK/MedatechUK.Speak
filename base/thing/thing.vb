Imports System.IO
Imports System.Reflection
Imports System.Xml
Imports System.Xml.Serialization


Public MustInherit Class thing

    MustOverride Sub Context(ParamArray args() As thing)

    Private _Update As thing
    Overridable Sub Refresh(Optional ByRef Update As this = Nothing)

        If Not Update Is Nothing Then
            _Update = Activator.CreateInstance(Me.GetType, {Update})

        End If

    End Sub

    Overridable Sub Update(ByRef t As thing)

    End Sub

#Region "Properties"

    Private _Loaded As Boolean = False
    Public ReadOnly Property Loaded As Boolean
        Get
            Return _Loaded
        End Get
    End Property

    Public Overridable Property Description As String
        Get
            Return Name
        End Get
        Set(value As String)

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

        _Loaded = (t.t.Count = 0 And t.a.Count = 0)

    End Sub

    Sub Load()

        _Loaded = True

        For Each th As t In _t.t
            Dim pr = Me.[GetType]().GetProperty(th.name, BindingFlags.[Public] Or BindingFlags.Instance)

            If myThings(pr.PropertyType).Keys.Contains(th.id) Then
                pr.SetValue(Me, myThings(pr.PropertyType)(th.id))

            Else
                If New FileInfo(IO.Path.Combine(BasePath, th.ref)).Exists Then
                    Using sr As New StreamReader(IO.Path.Combine(BasePath, th.ref))
                        pr.SetValue(Me,
                            myThings.Addthing(serialiser.Deserialize(sr))
                        )
                    End Using

                Else
                    Console.WriteLine("Bad data.")

                End If

            End If

        Next

        For Each a As thisA In _t.a
            Dim pr = Me.[GetType]().GetProperty(a.name, BindingFlags.[Public] Or BindingFlags.Instance)
            'pr.SetValue(Me, New things)
            With pr.GetValue(Me)
                For Each q As t In a.t
                    Try
                        .Add(myThings(q.GetType)(q.id))

                    Catch ex As Exception
                        If New FileInfo(IO.Path.Combine(BasePath, q.ref)).Exists Then
                            Using sr As New StreamReader(IO.Path.Combine(BasePath, q.ref))
                                .Add(
                                    myThings.Addthing(serialiser.Deserialize(sr))
                                )

                            End Using

                        Else
                            Console.WriteLine("Bad data.")

                        End If

                    End Try

                Next

            End With

        Next

        If Not _Update Is Nothing Then
            Update(_Update)
            Me.Save()
        End If

    End Sub

#End Region

#Region "Methods"

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
