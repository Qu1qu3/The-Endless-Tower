using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lr;
    
    public Vector3 startPoint;
    public Vector3 direction;

    private int lMask;

    public GameObject LaserPrefab;
    private GameObject OtherLaser;
    private Laser OtherLaserScript;
    public bool summoning;

    // Start is called before the first frame update
    void Start()
    {
        summoning = false;
        lr = GetComponent<LineRenderer>();
        lMask = 1 << 20;
        lMask = ~lMask;
        startPoint = transform.position;
        direction = Vector3.up;
    }
    public void setSum(bool b)
    {
        summoning = b;
    }
    /*public void OnEnable()
    {
        lr = GetComponent<LineRenderer>();
        lMask = (1 << 20);
        lMask = ~lMask;
        startPoint = transform.position;
        direction = Vector3.up;
    }*/
    public void setPosAndDir(Vector3 p, Vector3 d)
    {
        startPoint = p;
        direction = d;
    }
    // Update is called once per frame
    void Update()
    {

        lr.SetPosition(0, startPoint);
        RaycastHit hit;
        if(Physics.Raycast(startPoint, direction, out hit, 6000, lMask))
        {
            if(hit.collider)
            {
                lr.SetPosition(1, hit.point);
            }

            if(hit.transform.tag == "Portal")
            {
                GameObject portal = hit.transform.gameObject;
                PortalScript portalScript = portal.GetComponent<PortalScript>();
                Vector3 LocalPos = portal.transform.InverseTransformPoint(hit.point);
                Vector3 OtherPos = portalScript.OtherPortal.transform.TransformPoint(-new Vector3(LocalPos.x, -LocalPos.y, LocalPos.z));

                Vector3 LocalDir = portal.transform.InverseTransformDirection(direction);
                Vector3 OtherDir = portalScript.OtherPortal.transform.TransformDirection(-new Vector3(LocalPos.x, -LocalPos.y, LocalPos.z));
                //OtherDir = Quaternion.FromToRotation(portal.transform.rotation, portalScript.OtherPortal.transform.rotation);
                if(OtherLaser == null)
                {
                    OtherLaser = Instantiate(LaserPrefab, transform.position, transform.rotation, transform);
                    OtherLaserScript = OtherLaser.GetComponent<Laser>();
                    summoning = true;
                }
                Debug.Log(OtherLaser);
                OtherLaserScript.setPosAndDir(OtherPos, OtherDir);
            }
            else //if(OtherLaser != null)
            {
                Destroy(OtherLaser);
                OtherLaserScript = null;
                summoning = false;
            }

        }
        else lr.SetPosition(1, direction * 5000);
    }
}
