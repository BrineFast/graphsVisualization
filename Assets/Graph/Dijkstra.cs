using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dijkstra<T>
{
    private Dictionary<GameObject, GameObject> predecessors;
    private Dictionary<GameObject, Dictionary<GameObject, List<GameObject>>> graph;

    public List<List<GameObject>> FindPathes(GameObject u, Dictionary<GameObject, Dictionary<GameObject, List<GameObject>>> graph)
    {
        this.graph = graph;
        HashSet<GameObject> used = new HashSet<GameObject>();
        Dictionary<GameObject, int> distance = new Dictionary<GameObject, int>();

        this.predecessors = new Dictionary<GameObject, GameObject>();
        foreach (var graphKey in graph.Keys)
        {
            distance.Add(graphKey, int.MaxValue);
        }
        distance[u] = 0;
        predecessors.Add(u, null);

        DijkstraAlg(used, distance);
        var andPrintPath = findAndGetPathes();
        return andPrintPath;
    }

    private void DijkstraAlg(HashSet<GameObject> used, Dictionary<GameObject, int> distance)
    {
        for (; ; )
        {
            GameObject v = null;
            foreach (GameObject e in graph.Keys)
            {
                if (!used.Contains(e) && distance[e] < int.MaxValue
                                     && (v == null || distance[v] > distance[e]))
                {
                    v = e;
                }
            }
            if (v == null)
                break;
            used.Add(v);
            foreach (GameObject e in graph.Keys)
            {
                if (!used.Contains(e) && graph[v].ContainsKey(e))
                {
                    int min = Math.Min(distance[e], distance[v] + graph[v][e].Count);
                    if (min != distance[e])
                    {
                        predecessors[e] = v;
                    }
                    distance[e] = min;
                }
            }
        }
    }

    private List<List<GameObject>> findAndGetPathes()
    {
        List<List<GameObject>> pathes = new List<List<GameObject>>();
        foreach (var key in graph.Keys)
        {
            List<GameObject> path = new List<GameObject>();
            GameObject a = key;
            while (a != null)
            {
                path.Add(a);
                a = predecessors[a];
            }
            path.Reverse();
            pathes.Add(path);
        }
        return pathes;
    }
}