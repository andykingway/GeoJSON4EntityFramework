Imports System.Data.Entity.Spatial
Imports System.Runtime.Serialization
Imports GeoJSON4EntityFramework.Base
'Imports Newtonsoft.Json

Namespace Elements
    Public Class Polygon
        Inherits GeoJsonGeometry(Of Polygon)
        Implements IGeoJsonGeometry

        '<JsonIgnore()>
        <IgnoreDataMember()>
        Public Property Rings As New List(Of CoordinateList)

        <DataMember(Name:="coordinates", Order:=1)>
        Public Overrides ReadOnly Property Coordinates()
            Get
                'Return Rings
                Try
                    If Rings.Count = 0 Then
                        Return New Double() {}
                    Else
                        Dim out(Rings.Count - 1)()() As Double
                        For k As Integer = 0 To Rings.Count - 1
                            Dim Points3 As CoordinateList = Rings(k)
                            If Points3.Count = 0 Then
                                Return New Double() {}
                            ElseIf Points3.Count = 1 Then
                                Throw New Exception("There must be an array of two or more points")
                            Else
                                Dim j As Integer = k
                                out(j) = New Double(Points3.Count - 1)() {}
                                Parallel.For(0, Points3.Count, Sub(i)
                                                                   out(j)(i) = Points3(i).Value
                                                               End Sub)
                            End If
                        Next
                        Return out
                    End If
                Catch ex As Exception
                    Return New Double() {}
                End Try
            End Get
        End Property

        Private Function RingToCoordinateList(ring As DbGeometry) As CoordinateList
            Dim extRingCoords As New CoordinateList()
            For i = 1 To ring.PointCount
                Dim pt = ring.PointAt(i)
                extRingCoords.Add(New Coordinate(pt.XCoordinate, pt.YCoordinate))
            Next
            Return extRingCoords
        End Function

        Public Overrides Sub CreateFromDbGeometry(inp As DbGeometry)
            If inp.SpatialTypeName <> MyBase.TypeName Then Throw New ArgumentException
            Rings.Clear()

            ' Process exterior ring
            Dim extRing = inp.ExteriorRing
            Rings.Add(RingToCoordinateList(extRing))

            ' Process interior rings (ie. holes)
            If inp.InteriorRingCount > 0 Then
                For i = 1 To inp.InteriorRingCount
                    Dim intRing = inp.InteriorRingAt(i)
                    Rings.Add(RingToCoordinateList(intRing))
                Next
            End If
        End Sub
    End Class
End Namespace