using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Planet
{
    [FormerlySerializedAs("StartVertex")] public GameObject startVertex;
    [FormerlySerializedAs("InnerVertices")] public List<InnerVertex> innerVertices;
}