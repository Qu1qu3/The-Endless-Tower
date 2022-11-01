using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    public float velRot = 3.0f;
    private float velx, vely, velz;
    public float velU = 0.01f;
    private float velUs, anguloGir;
    private int[] ran = { -1, 1 };
    // Start is called before the first frame update
    void Start()
    {
        velx = velRot * Random.Range(0.2f, 0.5f) * ran[Random.Range(0, 1)];
        vely = velRot * Random.Range(0.2f, 0.5f) * ran[Random.Range(0, 1)];
        velz = velRot * Random.Range(0.2f, 0.5f) * ran[Random.Range(0, 1)];
        velUs = velU;
        InvokeRepeating("ReRandomCube", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 rot = new Vector3(transform.rotation);s
        //transform.Rotate(rot.x * velRot, rot.y, rot.x);
        transform.Rotate(velx, vely, velz);
        velUs = velU * Mathf.Sin((anguloGir * Mathf.PI) / 180);
        anguloGir += 0.5f;
        if(anguloGir==360) anguloGir = 0;
        transform.Translate(0, velUs, 0, Space.World);
        /*if(transform.position.y < 2 + offset)
        {
            velUs = velU;
        }
        else if(transform.position.y > 2.5 + offset) velUs = -velU; ;*/
    }

    void ReRandomCube()
    {
        velx = velRot * Random.Range(0.2f, 0.5f) * ran[Random.Range(0, 1)];
        vely = velRot * Random.Range(0.2f, 0.5f) * ran[Random.Range(0, 1)];
        velz = velRot * Random.Range(0.2f, 0.5f) * ran[Random.Range(0, 1)];
    }
}