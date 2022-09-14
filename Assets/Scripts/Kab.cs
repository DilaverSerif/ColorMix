using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kab : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    
    public void SetColor(Color color)
    {
        meshRenderer.materials[0].color = color;
    }
}
