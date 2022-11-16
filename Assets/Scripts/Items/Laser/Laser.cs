using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lr;
    
    public Vector3 startPoint;
    public Vector3 direction;

    private int lMask;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lMask = (1 << 20);
        lMask = ~lMask;
    }

    public void Init(Vector3 p, Vector3 d)
    {
        
        lr = GetComponent<LineRenderer>();
        lMask = (1 << 20);
        lMask = ~lMask;
    }
    // Update is called once per frame
    void UpdateLaser(Vector3 p, Vector3 d)
    {
        startPoint = p;
        direction = d;
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
                PortalScript ps = hit.transform.gameObject.GetComponent<PortalScript>();
            }
        }
        else lr.SetPosition(1, transform.forward * 5000);
    }
}
