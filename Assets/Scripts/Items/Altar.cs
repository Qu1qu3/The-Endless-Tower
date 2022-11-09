using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{

    [SerializeField] private GameObject Cube;
    private Transform posCube;
    private Holdeable holdCube;
    private Rigidbody rbCube;
    private GameObject light;
    [SerializeField] private bool isCentered;
    public float velTraslate = 0.01f;

    public float velRot = 3.0f;
    private float velx, vely, velz;
    public float rangeUp = 0.01f;
    private float velUs, anguloGir;
    public float velUp = 0.5f;
    private int[] ran = { -1, 1 };

    public AltarInteraction interactionable;
    // Start is called before the first frame update
    void Start()
    {
        isCentered = false;
        posCube = transform.Find("PosCube");
        light = transform.Find("LightA").gameObject;

        velx = velRot * Random.Range(0.2f, 0.5f) * ran[Random.Range(0, 1)];
        vely = velRot * Random.Range(0.2f, 0.5f) * ran[Random.Range(0, 1)];
        velz = velRot * Random.Range(0.2f, 0.5f) * ran[Random.Range(0, 1)];
        velUs = rangeUp;
    }

    // Update is called once per frame
    void Update()
    {
        if( Cube != null ) UpdateCube();
        
    }
    void UpdateCube()
    {
        if(!isCentered) moveToCenter();
        if(isCentered && holdCube.isHolded) quitCube();
        if(isCentered) UpdateCubeRotation();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag != "ActivadorAltar") return;
        if (Cube != null) return;
        //Debug.Log("aa");
        Cube = other.gameObject;
        holdCube = Cube.GetComponent<Holdeable>();
        rbCube = Cube.GetComponent<Rigidbody>();
        holdCube.stopHolding();
        holdCube.setCanBeHolded(false);
        rbCube.useGravity = false;
        rbCube.constraints |= RigidbodyConstraints.FreezePosition;
        
    }


    private void quitCube()
    {
        rbCube.constraints &= ~RigidbodyConstraints.FreezePosition;
        CancelInvoke("ReRandomCube");
        interactionable.getOff();
        isCentered = false;
        Cube = null;
        holdCube = null;
        rbCube = null;
        Cube = null;
        light.SetActive(false);
    }
    private void moveToCenter()
    {
        Vector3 movement = posCube.position - Cube.transform.position;
        Cube.transform.Translate(movement * velTraslate, Space.World);
        
        if(movement.magnitude <= 0.01f)
        {
            holdCube.setCanBeHolded(true);
            anguloGir = 0f;
            InvokeRepeating("ReRandomCube", 1f, 1f);
            isCentered = true;
            light.SetActive(true);
            interactionable.getOn();
        }
    }

    private void UpdateCubeRotation()
    {
        Cube.transform.Rotate(velx, vely, velz);
        velUs = rangeUp * Mathf.Sin((anguloGir * Mathf.PI) / 180);
        anguloGir += velUp;
        if(anguloGir >= 360) anguloGir -= 360;
        Cube.transform.Translate(0, velUs, 0, Space.World);
    }

    private void ReRandomCube()
    {
        velx = velRot * Random.Range(0.2f, 0.5f) * ran[Random.Range(0, 1)];
        vely = velRot * Random.Range(0.2f, 0.5f) * ran[Random.Range(0, 1)];
        velz = velRot * Random.Range(0.2f, 0.5f) * ran[Random.Range(0, 1)];
    }
}
