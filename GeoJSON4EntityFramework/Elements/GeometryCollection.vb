Imports System.Data.Entity.Spatial
Imports System.Runtime.Serialization
Imports GeoJSON4EntityFramework.Base
'Imports Newtonsoft.Json

Namespace Elements
    Public Class GeometryCollection
        Inherits GeoJsonElement(Of GeometryCollection)
        Implements IGeoJsonGeometry

        '<JsonProperty(PropertyName:="geometries")>
        <DataMember(Name:="geometries", Order:=1)>
        Public Property Geometries As New List(Of IGeoJsonGeometry)

        Shared Function FromDbGeometry(inp As DbGeometry) As IGeoJsonGeometry
            Dim obj As New GeometryCollection()
            obj.CreateFromDbGeometry(inp)
            Return obj
        End Function

        Public Sub CreateFromDbGeometry(inp As DbGeometry)
            If inp.SpatialTypeName <> "GeometryCollection" Then Throw New ArgumentException
            Geometries.Clear()

            For i As Integer = 1 To inp.ElementCount
                Dim element = inp.ElementAt(i)
                Select Case element.SpatialTypeName
                    Case "MultiPolygon"
                        Geometries.Add(MultiPolygon.FromDbGeometry(element))
                    Case "Polygon"
                        Geometries.Add(Polygon.FromDbGeometry(element))
                    Case "Point"
                        Geometries.Add(Point.FromDbGeometry(element))
                    Case "MultiPoint"
                        Geometries.Add(MultiPoint.FromDbGeometry(element))
                    Case "LineString"
                        Geometries.Add(LineString.FromDbGeometry(element))
                    Case "MultiLineString"
                        Geometries.Add(MultiLineString.FromDbGeometry(element))
                End Select
            Next
        End Sub
    End Class
End Namespace