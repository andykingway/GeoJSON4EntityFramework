Imports System.Runtime.Serialization
Imports GeoJSON4EntityFramework.Base
'Imports Newtonsoft.Json

Namespace Elements
    Public Class LineString
        Inherits GeoJsonGeometry(Of LineString)
        Implements IGeoJsonGeometry

        '<JsonIgnore()>
        <IgnoreDataMember()>
        Public Property Points As New CoordinateList

        <DataMember(Name:="coordinates", Order:=1)>
        Public Overrides ReadOnly Property Coordinates()
            Get
                Try
                    If Points.Count = 0 Then
                        Return New Double() {}
                    ElseIf Points.Count = 1 Then
                        Throw New Exception("There must be an array of two or more points")
                    Else
                        Dim out(Points.Count - 1)() As Double
                        'out = New Double(Points.Count - 1)() {}
                        Parallel.For(0, Points.Count, Sub(i)
                                                          out(i) = Points(i).Value
                                                      End Sub)
                        Return out
                    End If
                Catch ex As Exception
                    Return New Double() {}
                End Try
            End Get
        End Property

        Public Overrides Sub CreateFromDbGeometry(inp As Entity.Spatial.DbGeometry)
            If inp.SpatialTypeName <> MyBase.TypeName Then Throw New ArgumentException
            Points.Clear()

            For i As Integer = 1 To inp.PointCount
                Dim point = inp.PointAt(i)
                Points.AddNew(point.XCoordinate, point.YCoordinate)
            Next
        End Sub
    End Class
End Namespace