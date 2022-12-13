using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerPortalPasado
{
    private LayerMask mask;
    PortalScript[] Portal;
    PortalScript[] PortalFuturo;
    
    public PortalScript Portal1;
    public PortalScript Portal2;
    public PortalScript PortalTiempo;
    public PortalScript PortalTiempoPasado;
    public PortalScript Portal1Pasado;
    public PortalScript Portal2Pasado;

    private Vector3 layoutDiff;


    public void Initialize()
    {
        layoutDiff = GameObject.Find("LayoutPasado").transform.position - GameObject.Find("Layout").transform.position;
        Portal = new PortalScript[3];
        PortalFuturo = new PortalScript[3];
        //Debug.Log("InitShoot");
        Portal[0] = Portal1Pasado;
        Portal[1] = Portal2Pasado;
        Portal[2] = PortalTiempoPasado;

        PortalFuturo[0] = Portal1;
        PortalFuturo[1] = Portal2;
        PortalFuturo[2] = PortalTiempo;

        mask = LayerMask.GetMask("Wall"); 
    }
    void Update()
    {
        
    }
    public void shootPortal(int p)
    {
        
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity)) //, 1 << 9
        {
            if(hit.transform.gameObject.layer != LayerMask.NameToLayer("Wall")) return;
            //Debug.Log("Hit " + p);
            //Debug.Log(Portal[p].transform.forward + "  PF");
            if(hit.normal.z == - Portal[p].transform.forward.z && hit.normal.z != 0f) 
            {
                Portal[p].transform.Rotate(0f,0f,180f);
                //Debug.Log("True");
            }
                
            Portal[p].transform.rotation = Quaternion.FromToRotation(Portal[p].transform.forward, hit.normal) * Portal[p].transform.rotation;
            //Debug.Log(hit.normal + "  HN");
            if(hit.normal.y <= 0.001f)
            {
               Portal[p].transform.localEulerAngles = new Vector3(0f, Portal[p].transform.localEulerAngles.y, 0f);
               
            }
            //Debug.Log("TrueH " + hit.normal.y); 
            Portal[p].transform.position = hit.point + Portal[p].transform.forward * 0.001f;
            Portal[p].terrainBehind = hit.collider.GetComponent<MeshCollider>();

            //Mover Portal del futuro
            PortalFuturo[p].transform.position = Portal[p].transform.position - layoutDiff;
            PortalFuturo[p].transform.rotation = Portal[p].transform.rotation;
            PortalFuturo[p].getOwnCollider();
            
            Portal[p].setActive(true);
            Portal[p].checkOtherPortal();
            Portal[p].OtherPortal.GetComponent<PortalScript>().checkOtherPortal();

            PortalFuturo[p].setActive(true);
            PortalFuturo[p].checkOtherPortal();
            PortalFuturo[p].OtherPortal.GetComponent<PortalScript>().checkOtherPortal();
        }
    }

}
