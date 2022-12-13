using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    
    public Transform OtherPortal;
    private PortalScript oPS;
    private Camera playerCam;
    private Camera portalCam;
    private Camera OtherPortalCam;
    private GameObject player;
    private GameObject cylinderCam;
    public Vector3 initPos;

    private Transform holdObjPlayer;
    private Vector3 originalHoldPos;

    public MeshCollider terrainBehind;
    private CapsuleCollider playerColider;

    private bool isOpen;
    Rigidbody playerRbody;

    public bool isActive = false;
    public GameObject go;
    [SerializeField]
    private LayerMask lMaskIgnore = 10;
    private int lMask;    
    // Start is called before the first frame update
    void Awake()
    {
        
    }
    void Start()
    {
        lMask = ~(0 << 32);
        lMask &= ~(1 << 10);
        lMask &= ~(1 << 22);
        switch(gameObject.name) 
            {
            case "Portal1":
                // code block
            case "Portal1Pasado":
                lMask &= ~(1 << 29);
                break;
            case "Portal2":
                // code block
            case "Portal2Pasado":
                lMask &= ~(1 << 30);
                break;
            case "PortalTiempo":
                // code block
            case "PortalTiempoPasado":
                lMask &= ~(1 << 31);
                break;
            default:
                break;
            }
        transform.Find("CameraPortal").gameObject.GetComponent<Camera>().cullingMask = lMask;
        cylinderCam = transform.Find("Cylinder").gameObject;
        getOwnCollider();
        player = GameObject.Find("Mage");
        
        playerCam = Camera.main;
        portalCam = GetComponentsInChildren<Camera>()[0];
        OtherPortalCam = OtherPortal.GetComponentsInChildren<Camera>()[0];
        go = OtherPortal.Find("CylinderSG").gameObject;
        playerColider = player.GetComponent<CapsuleCollider>();
        isOpen = false;
        playerRbody = player.GetComponent<Rigidbody>();
        oPS = OtherPortal.GetComponent<PortalScript>();
        //holdObjPlayer = GameObject.Find("holdObj").transform;
        //originalHoldPos = holdObjPlayer.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

        // Quaternion.Inverse(transform.rotation) *
        Quaternion direccion =  Quaternion.Inverse(transform.rotation) * Camera.main.transform.rotation;
            //Debug.Log (direccion.eulerAngles.x +"  "+ direccion.eulerAngles.y + 180+"  "+ direccion.eulerAngles.z); direccion.eulerAngles.x
        OtherPortalCam.transform.localEulerAngles = new Vector3(direccion.eulerAngles.x, direccion.eulerAngles.y + 180, direccion.eulerAngles.z);
        //OtherPortalCam.transform.LookAt(OtherPortal.position);
        
        Vector3 distancia = transform.InverseTransformPoint(Camera.main.transform.position);  //+ new Vector3(0f,0.6f,-0.5f)
        OtherPortalCam.transform.localPosition = - new Vector3(distancia.x, -distancia.y, distancia.z);
        setNearClipPlane();
        distancePlayer();
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log ("Portal Triggering Mal");
        if (other.tag == "Player")
        {
           // Debug.Log ("Portal Triggering");
            Vector3 PlayerFromPortal = transform.InverseTransformPoint(player.transform.position);
            

            if(PlayerFromPortal.z < 0.015)
            {
                Quaternion ftoR = Quaternion.FromToRotation(transform.forward, OtherPortal.transform.forward);
                //if(ftoR == new Quaternion(1f,0f,0f,0f)) ftoR = new Quaternion(0f,1f,0f,0f);
                Vector3 vel = -playerRbody.velocity;
                Vector3 vel2 = -transform.InverseTransformDirection(playerRbody.velocity);
                /*player.transform.position = OtherPortal.position + new Vector3(-PlayerFromPortal.x,
                                                                                +PlayerFromPortal.y,
                                                                                -PlayerFromPortal.z);*/
                
                player.transform.position = OtherPortal.position + ftoR * PlayerFromPortal;
                                                                                
                //Debug.Log ("Portal Triggering Be");
                //Quaternion ttt = Quaternion.Inverse(transform.rotation) * player.transform.rotation;

                
                player.transform.eulerAngles = Vector3.up * (OtherPortal.eulerAngles.y - (transform.eulerAngles.y - player.transform.eulerAngles.y) + 180);
                playerCam.transform.localEulerAngles = Vector3.right * (OtherPortal.eulerAngles.x + Camera.main.transform.localEulerAngles.x);
                

                
                

                vel2 = new Vector3(vel2.x, -vel2.y, vel2.z);

                //playerRbody.velocity = OtherPortal.transform.TransformDirection(vel2);

                playerRbody.velocity = vel2.magnitude * OtherPortal.forward;
                //Debug.Log (ftoR);

                //holdObjPlayer.position =  OtherPortal.position + OtherPortal.transform.forward;
                //Invoke("setOldPos", 0.5f);
            }
        }
    }

    void distancePlayer()
    {
        float distVec = transform.InverseTransformPoint(player.transform.position).magnitude;
        //Debug.Log (distVec);
        if(distVec < 0.9f) 
        {
            isOpen = true;
            if(oPS.getActive()) Physics.IgnoreCollision(playerColider, terrainBehind, true);
        }
        else if(isOpen) 
        {
            isOpen = false;
            Physics.IgnoreCollision(playerColider, terrainBehind, false);
        }
    }
    void setNearClipPlane()
    {
        Transform clipPlane = transform;
        int dot = System.Math.Sign(Vector3.Dot(clipPlane.forward, transform.position - portalCam.transform.position));
        Vector3 camSpacePos = portalCam.worldToCameraMatrix.MultiplyPoint(clipPlane.position);
        Vector3 camSpaceNormal = portalCam.worldToCameraMatrix.MultiplyVector(clipPlane.forward) * dot;
        float camSpaceDst = -Vector3.Dot (camSpacePos, camSpaceNormal);
        Vector4 clipPlaneCameraSpace = new Vector4(camSpaceNormal.x, camSpaceNormal.y, camSpaceNormal.z, camSpaceDst);

        portalCam.projectionMatrix = playerCam.CalculateObliqueMatrix(clipPlaneCameraSpace);
    }

    public void getOwnCollider()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -transform.forward, out hit, 0.1f, 1 << 9))
        {
            terrainBehind = hit.collider.GetComponent<MeshCollider>();
        }
    }

    public void setActive(bool t)
    {
        isActive = t;
    }

    public bool getActive()
    {
        return isActive;
    }

    public void checkOtherPortal()
    {
        cylinderCam.SetActive(oPS.getActive());
    }

    public void delPortal()
    {
        transform.position = initPos;
        setActive(false);
    }
    /*void setOldPos()
    {
        holdObjPlayer.localPosition = originalHoldPos;
    }*/
}
