using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class PlanetVertex
{
    [FormerlySerializedAs("StartVertex")] public GameObject startVertex;
    [FormerlySerializedAs("InnerVertices")] public List<InnerVertex> innerVertices;
}