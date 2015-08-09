Imports System.Data.Entity.Spatial
Imports System.Runtime.Serialization
Imports GeoJSON4EntityFramework.Base
'Imports Newtonsoft.Json

Namespace Elements
    Public Class MultiPolygon
        Inherits GeoJsonGeometry(Of MultiPolygon)
        Implements IGeoJsonGeometry

        '<JsonIgnore()>
        <IgnoreDataMember()>
        Public Property Polygons As New List(Of Polygon)

        <DataMember(Name:="coordinates", Order:=1)>
        Public Overrides ReadOnly Property Coordinates As Object
            Get
                'Dim result As New List(Of Object)()
                'For Each poly In Polygons
                '    result.Add(poly.Coordinates)
                'Next
                'Return result
                If Polygons.Count = 0 Then
                    Return New Double() {}
                Else
                    Dim out(Polygons.Count - 1)()()() As Double

                    Parallel.For(0, Polygons.Count, Sub(i)
                                                        out(i) = Polygons(i).Coordinates
                                                    End Sub)
                    Return out
                End If
            End Get
        End Property

        Public Overrides Sub CreateFromDbGeometry(inp As DbGeometry)
            If inp.SpatialTypeName <> "MultiPolygon" Then Throw New ArgumentException
            Polygons.Clear()

            For i As Integer = 1 To inp.ElementCount
                Dim element = inp.ElementAt(i)
                If element.SpatialTypeName <> "Polygon" Then Throw New ArgumentException
                Polygons.Add(Polygon.FromDbGeometry(element))
            Next
        End Sub
    End Class
End Namespace