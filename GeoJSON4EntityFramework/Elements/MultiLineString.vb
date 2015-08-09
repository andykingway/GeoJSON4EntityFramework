Imports System.Runtime.Serialization
Imports GeoJSON4EntityFramework.Base
'Imports Newtonsoft.Json

Namespace Elements
    Public Class MultiLineString
        Inherits GeoJsonGeometry(Of MultiLineString)
        Implements IGeoJsonGeometry

        '<JsonIgnore()>
        <IgnoreDataMember()>
        Public Property LineStrings As New List(Of LineString)

        <DataMember(Name:="coordinates", Order:=1)>
        Public Overrides ReadOnly Property Coordinates As Object
            Get
                'Dim result As New List(Of Object)()
                'For Each line In LineStrings
                '    result.Add(line.Coordinates)
                'Next
                'Return result
                If LineStrings.Count = 0 Then
                    Return New Double() {}
                Else
                    Dim out(LineStrings.Count - 1)()() As Double

                    Parallel.For(0, LineStrings.Count, Sub(i)
                                                           out(i) = LineStrings(i).Coordinates
                                                       End Sub)
                    Return out
                End If
            End Get
        End Property

        Public Overrides Sub CreateFromDbGeometry(inp As Entity.Spatial.DbGeometry)
            If inp.SpatialTypeName <> MyBase.TypeName Then Throw New ArgumentException
            LineStrings.Clear()

            For i As Integer = 1 To inp.ElementCount
                Dim element = inp.ElementAt(i)
                If element.SpatialTypeName <> "LineString" Then Throw New ArgumentException
                LineStrings.Add(LineString.FromDbGeometry(element))
            Next
        End Sub
    End Class
End Namespace