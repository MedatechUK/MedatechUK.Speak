Public Class chActivity
    Inherits Speak.baseChoice

    Public Overrides Sub StaticChoice(ByRef e As List(Of thing))
        With e
            Add(New tActivity("service calls", GetType(tServiceCall)))
            Add(New tActivity("projects", GetType(tProject)))
            Add(New tActivity("sales orders", GetType(tSalesOrder)))
            Add(New tActivity("workload", Nothing))

        End With

    End Sub

End Class
