Imports System.IO

Public MustInherit Class baseChoice : Inherits List(Of thing) : Implements IDisposable

    Public Overridable ReadOnly Property ChoiceType As Type
        Get
            Throw New NotSupportedException
        End Get
    End Property

    Public Overridable Sub StaticChoice(ByRef e As List(Of thing))
        Throw New NotSupportedException

    End Sub

    Public Overridable Function Filter(ByRef t As thing) As Boolean
        Throw New NotSupportedException

    End Function

    Sub New(Dyn As Boolean)

    End Sub

    Sub New()

        Me.Clear()

        Try
            StaticChoice(Me)

        Catch
            Dim p As Type
            Try
                p = ChoiceType

            Catch ex As Exception
                Throw ex

            End Try

            Dim _Path As String = String.Format("{0}\", p.Name)
            While Not String.Compare(p.BaseType.Name, GetType(Object).Name) = 0
                _Path = String.Format("{0}\{1}", ChoiceType.BaseType.Name, _Path)
                p = p.BaseType
            End While

            For Each f As FileInfo In New DirectoryInfo(IO.Path.Combine(BasePath, _Path)).GetFiles
                Using sr As New StreamReader(f.FullName)
                    myThings.Addthing(serialiser.Deserialize(sr))

                End Using

            Next

            Do While myThings.Load : Loop

            For Each t As thing In myThings(ChoiceType).Values
                Try
                    If Filter(t) Then Me.Add(t)

                Catch ex As Exception
                    Me.Add(t)

                End Try

            Next

        End Try

    End Sub

    Public Function Choices() As System.Speech.Recognition.Choices

        Dim ret As New System.Speech.Recognition.Choices
        For Each c As thing In Me
            ret.Add(c.Name)

        Next
        Return ret

    End Function

    Public ReadOnly Property Name As String
        Get
            Return Me.GetType().Name
        End Get
    End Property

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
