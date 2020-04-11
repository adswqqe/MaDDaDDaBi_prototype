using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathScript : MonoBehaviour
{
    [SerializeField]
    GameObject nextTarget;

    public GameObject GetTarget()
    {
        if (nextTarget != null)
            return nextTarget;
        else
        {
            GameObject go = new GameObject();
            go.name = "END";
            return go;
        }
    }
}
