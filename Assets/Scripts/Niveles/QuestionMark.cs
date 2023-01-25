using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionMark : MonoBehaviour
{
    public float velRot = 1f;
    public float velU = 0.0025f;
    private float velUs, anguloGir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0, 0, velRot);

        velUs = velU * Mathf.Sin((anguloGir * Mathf.PI) / 180);
        anguloGir += 1f;
        if(anguloGir==360) anguloGir = 0;
        transform.Translate(0, velUs, 0, Space.World);
    }
}
