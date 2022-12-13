using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collidAntiMagic : MonoBehaviour
{
    private GameObject player;
    private CapsuleCollider playerColider;
    public PortalScript[] Portal;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Mage");
        //playerColider = player.GetComponent<CapsuleCollider>();
        //BoxCollider col = GetComponent<BoxCollider>();
        //Physics.IgnoreCollision(playerColider, col, true);

        Portal = new PortalScript[6];
        //Debug.Log("InitShoot");
        Portal[0] = GameObject.Find("Portal1").GetComponent<PortalScript>();
        Portal[1] = GameObject.Find("Portal2").GetComponent<PortalScript>();
        Portal[2] = GameObject.Find("PortalTiempo").GetComponent<PortalScript>();
        Portal[3] = GameObject.Find("Portal1Pasado").GetComponent<PortalScript>();
        Portal[4] = GameObject.Find("Portal2Pasado").GetComponent<PortalScript>();
        Portal[5] = GameObject.Find("PortalTiempoPasado").GetComponent<PortalScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        foreach(PortalScript ps in Portal)
        {
            ps.delPortal();
        }
    }
}
