﻿Public Class chActivity
    Inherits Speak.baseChoice

    Public Overrides Sub StaticChoice(ByRef e As List(Of thing))
        With e
            Add(New tActivity("service calls", New tplural("service call", "service calls"), GetType(tServiceCall)))
            Add(New tActivity("projects", New tplural("project", "projects"), GetType(tProject)))
            Add(New tActivity("sales orders", New tplural("sales order", "sales orders"), GetType(tSalesOrder)))
            Add(New tActivity("workload", New tplural("task", "tasks"), Nothing))

        End With

    End Sub

End Class
