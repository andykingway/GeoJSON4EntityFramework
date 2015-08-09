Imports System.Runtime.Serialization
'Imports Newtonsoft.Json

Namespace Base
    <DataContract()>
    Public MustInherit Class GeoJsonElement(Of T)

        '<JsonProperty(PropertyName:="type", Order:=1)>
        <DataMember(Name:="type", Order:=0)>
        Public ReadOnly Property TypeName As String
            Get
                Return GetType(T).Name
            End Get
        End Property
    End Class
End Namespace