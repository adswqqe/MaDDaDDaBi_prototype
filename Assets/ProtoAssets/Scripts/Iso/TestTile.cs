using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTile : MonoBehaviour
{
    public TextMesh G;
    public TextMesh H;
    public TextMesh F;

    public float g;
    public float h;
    public float f = 1f;
    
    public Vector2 vec2;
    public bool closeNode;
    public bool isBlock;

    public TestTile(Vector2 vec2)
    {
        this.vec2 = vec2;
    }
}
