Imports System.Runtime.Serialization
Imports GeoJSON4EntityFramework.Base
'Imports Newtonsoft.Json

Namespace Elements
    Public Class Point
        Inherits GeoJsonGeometry(Of Point)
        Implements IGeoJsonGeometry

        '<JsonIgnore()>
        <IgnoreDataMember()>
        Public Property Coordinate As New Coordinate(0, 0)

        <DataMember(Name:="coordinates", Order:=1)>
        Public Overrides ReadOnly Property Coordinates As Object
            Get
                Return Coordinate.Value
            End Get
        End Property

        Public Overrides Sub CreateFromDbGeometry(inp As Entity.Spatial.DbGeometry)
            If inp.SpatialTypeName <> MyBase.TypeName Then Throw New ArgumentException
            Coordinate = New Coordinate(inp.XCoordinate, inp.YCoordinate)
        End Sub
    End Class
End Namespace