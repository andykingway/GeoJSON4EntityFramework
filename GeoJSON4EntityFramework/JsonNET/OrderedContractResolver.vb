'Imports Newtonsoft.Json
'Imports Newtonsoft.Json.Serialization

'Namespace JsonNet
'    Public Class OrderedContractResolver
'        Inherits DefaultContractResolver

'        Protected Overrides Function CreateProperties(type As Type, memberSerialization As MemberSerialization) As IList(Of JsonProperty)
'            Return (From p In MyBase.CreateProperties(type, memberSerialization)
'                    Let order As Byte = IIf(p.Order Is Nothing, 99, p.Order)
'                    Order By order, p.PropertyName Select p).ToList
'        End Function
'    End Class
'End Namespace