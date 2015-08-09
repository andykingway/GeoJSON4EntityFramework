Imports System.Data.Entity.Spatial
Imports System.Runtime.Serialization
Imports GeoJSON4EntityFramework.Base
'Imports Newtonsoft.Json

Namespace Elements
    Public Class Feature
        Inherits GeoJsonElement(Of Feature)

        '<JsonProperty(PropertyName:="id", Order:=2)>
        <DataMember(Name:="id", Order:=1)>
        Public Property ID As String

        '<JsonProperty(PropertyName:="properties", Order:=3)>
        <DataMember(Name:="properties", Order:=2)>
        Public Property Properties As New Dictionary(Of String, Object)

        '<JsonProperty(PropertyName:="geometry", Order:=4, NullValueHandling:=NullValueHandling.Include)>
        <DataMember(Name:="geometry", Order:=3, IsRequired:=True)>
        Public Property Geometry As IGeoJsonGeometry
        '<JsonConverter(GetType(GenericListConverter(Of IGeoJsonGeometry)))>
        'Public Property Geometry As New List(Of IGeoJsonGeometry)

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ParamArray Geometries() As IGeoJsonGeometry)
            MyBase.New()
            Geometry = Geometries.ToList
        End Sub

        Public Shared Function FromDbGeometry(inp As DbGeometry) As Feature
            Dim f As New Feature

            Select Case inp.SpatialTypeName
                Case "MultiPolygon"
                    f.Geometry = MultiPolygon.FromDbGeometry(inp)
                'f.Geometry.Add(MultiPolygon.FromDbGeometry(inp))
                Case "Polygon"
                    f.Geometry = Polygon.FromDbGeometry(inp)
                'f.Geometry.Add(Polygon.FromDbGeometry(inp))
                Case "Point"
                    f.Geometry = Point.FromDbGeometry(inp)
                'f.Geometry.Add(Point.FromDbGeometry(inp))
                Case "MultPoint"
                    f.Geometry = MultiPoint.FromDbGeometry(inp)
                'f.Geometry.Add(MultiPoint.FromDbGeometry(inp))
                Case "GeometryCollection"
                    f.Geometry = GeometryCollection.FromDbGeometry(inp)
                'f.Geometry.Add(GeometryCollection.FromDbGeometry(inp))
                Case "LineString"
                    f.Geometry = LineString.FromDbGeometry(inp)
                'f.Geometry.Add(LineString.FromDbGeometry(inp))
                Case "MultiLineString"
                    f.Geometry = MultiLineString.FromDbGeometry(inp)
                    'f.Geometry.Add(MultiLineString.FromDbGeometry(inp))
            End Select

            Return f
        End Function
    End Class
End Namespace