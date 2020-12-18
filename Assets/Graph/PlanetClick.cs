using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetClick : MonoBehaviour
{
    public GameObject gameManager;
    private Manager manager;
    private void Start()
    {
        manager = gameManager.GetComponent<Manager>();
    }

    private void OnMouseDown()
    {
        if (Manager.startPosition == null)
        {
            Manager.startPosition = gameObject;
        }
        else if (Manager.endPosition == null || Manager.endPosition != gameObject)
        {
            Manager.endPosition = gameObject;
        }
    }
}