GeoJSON4EntityFramework
=======================
Create GeoJSON from Entity Framework Spatial Data

Add support for "LingString", "MultiLineString", "GeometryCollection", and Polygons with holes.
Remove EntityFramework 5 support to simplify personal usage.
Remove build in Json.Net serializer.
Use System.Runtime.Serialization attributes to decorate properties needs to be serialized.
You need plug your own Json serializer.

GeoJSON Specs:
http://geojson.org/geojson-spec.html
