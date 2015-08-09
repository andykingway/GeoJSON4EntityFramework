Imports System.Runtime.Serialization
Imports GeoJSON4EntityFramework.Base
'Imports Newtonsoft.Json

Namespace Elements
    Public Class FeatureCollection
        Inherits GeoJsonElement(Of FeatureCollection)

        '<JsonProperty(PropertyName:="features")>
        <DataMember(Name:="features", Order:=1)>
        Public Property Features As New List(Of Feature)
    End Class
End Namespace