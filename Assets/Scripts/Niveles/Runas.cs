using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runas : MonoBehaviour
{
    private Renderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<Renderer>();
    }

    public void setActive(float i)
    {
        meshRenderer = GetComponent<Renderer>();
        meshRenderer.material.SetFloat("_active", i);
    }
}
