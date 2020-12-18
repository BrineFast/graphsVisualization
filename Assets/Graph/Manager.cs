using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static GameObject startPosition = null;

    public static GameObject endPosition = null;

    public GameObject ship;
    public Material usedMaterial;
    public Material oldMaterial;

    [SerializeField]
    private List<PlanetVertex> graph = new List<PlanetVertex>();

    private List<List<GameObject>> findPathes = new List<List<GameObject>>();

    // Start is called before the first frame update
    void Start()
    {
        Dijkstra = new Dijkstra<GameObject>();
    }

    private void createGraph()
    {
        playgroundGraph = new Dictionary<GameObject, Dictionary<GameObject, List<GameObject>>>();
        foreach (var PlanetVertex in graph)
        {
            playgroundGraph.Add(PlanetVertex.startVertex, new Dictionary<GameObject, List<GameObject>>());
            foreach (InnerVertex PlanetVertexInnerVertex in PlanetVertex.innerVertices)
            {
                playgroundGraph[PlanetVertex.startVertex].Add(PlanetVertexInnerVertex.innerTop, PlanetVertexInnerVertex.path);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && oldPath != null)
        {
            if (oldPath != null)
            {
                coloredPath(oldPath, oldMaterial);
            }
            startPosition = null;
            endPosition = null;
            oldStartPosition = null;
            oldEndPosition = null;
            oldPath = null;
        }

        if (startPosition != null && startPosition != oldStartPosition)
        {
            createGraph();
            findPathes = Dijkstra.FindPathes(startPosition, playgroundGraph);
            oldStartPosition = startPosition;
            ship.transform.position = new Vector3(startPosition.transform.position.x, 200f, startPosition.transform.position.z);
        }

        if (endPosition != null && endPosition != oldEndPosition && startPosition != endPosition)
        {
            var path = findPathes.Find(e => e.Count > 0 && Equals(e[e.Count - 1], endPosition));
            if (path != null)
            {
                if (oldPath != null)
                {
                    coloredPath(oldPath, oldMaterial);
                }

                fillOldPath(path);
                coloredPath(path, usedMaterial);
                oldEndPosition = endPosition;
                startPosition = endPosition;
                endPosition = null;
            }

        }
    }

    private void fillOldPath(List<GameObject> path)
    {
        oldPath = new List<GameObject>(path);
    }

    private void coloredPath(List<GameObject> path, Material material)
    {
        for (var i = 0; i < path.Count - 1; i++)
        {
            List<GameObject> objects = graph.Find(e => Equals(e.startVertex, path[i])).
            innerVertices.Find(e => Equals(e.innerTop, path[i + 1])).path;
            List<GameObject> gameObjects = new List<GameObject>(objects);
            ColoredPlanetVertex(gameObjects, material);
        }
    }

    private static void ColoredPlanetVertex(List<GameObject> gameObjects, Material material)
    {
        foreach (var pos in gameObjects)
        {
            pos.GetComponent<MeshRenderer>().material = material;
        }
    }

    private Dictionary<GameObject, Dictionary<GameObject, List<GameObject>>> playgroundGraph;
    private Dijkstra<GameObject> Dijkstra;
    private GameObject oldStartPosition;
    private GameObject oldEndPosition;
    private List<GameObject> oldPath;
}
