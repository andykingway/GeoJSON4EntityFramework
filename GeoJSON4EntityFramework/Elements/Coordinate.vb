'Imports Newtonsoft.Json

Namespace Elements
    '<JsonConverter(GetType(CoordinateConverter))>
    Public Class Coordinate

        Sub New()
            MyBase.New()
        End Sub

        Sub New(_x As Double, _y As Double)
            X = _x
            Y = _y
        End Sub

        Public ReadOnly Property Value As Double()
            Get
                Return New Double() {X, Y}
            End Get
        End Property

        '<JsonIgnore()>
        Public Property X As Double

        '<JsonIgnore()>
        Public Property Y As Double
    End Class
End Namespace