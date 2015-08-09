Imports System.Data.Entity.Spatial
Imports System.Runtime.Serialization
'Imports Newtonsoft.Json

Namespace Base
    <DataContract()>
    Public MustInherit Class GeoJsonGeometry(Of T)
        Inherits GeoJsonElement(Of T)

        '<JsonProperty(PropertyName:="coordinates")>
        <DataMember(Name:="coordinates", Order:=1)>
        Public MustOverride ReadOnly Property Coordinates() As Object

        '<JsonProperty(PropertyName:="bbox", Order:=5, NullValueHandling:=NullValueHandling.Ignore)>
        '<DataMember(Name:="bbox", Order:=5, EmitDefaultValue:=False)>
        'Public Property BoundingBox As Double()

        Public MustOverride Sub CreateFromDbGeometry(inp As DbGeometry)

        Public Shared Function FromDbGeometry(inp As DbGeometry) As GeoJsonGeometry(Of T)
            Dim obj As GeoJsonGeometry(Of T) = CTypeDynamic(Activator.CreateInstance(Of T)(), GetType(T))

            'obj.BoundingBox = New Double(3) {
            '    inp.Envelope.PointAt(1).YCoordinate,
            '    inp.Envelope.PointAt(1).XCoordinate,
            '    inp.Envelope.PointAt(3).YCoordinate,
            '    inp.Envelope.PointAt(3).XCoordinate
            '}
            obj.CreateFromDbGeometry(inp)
            Return obj
        End Function
    End Class
End Namespace