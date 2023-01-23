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
    private bool summoning;
    public bool getting;
    private LaserGetter lasGet;

    // Start is called before the first frame update
    void Start()
    {
        //Iniciamos variables
        getting = false;
        summoning = false;
        lr = GetComponent<LineRenderer>();

        lMask = 1 << 20;
        lMask = ~lMask;

        startPoint = transform.position;
        direction = Vector3.up;
    }
    public void setSum(bool b)
    {
        //summoning indica si este rayo laser esta generando otro al llegar a un portal
        summoning = b;
    }
    
    public void setPosAndDir(Vector3 p, Vector3 d)
    {
        startPoint = p;
        direction = d;
    }
    
    void OnDestroy()
    {
        if(getting)
        {
            getting = false;
            lasGet.getOff();
        }
    }
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
                Vector3 OtherDir = portalScript.OtherPortal.transform.TransformDirection(-new Vector3(LocalDir.x, -LocalDir.y, LocalDir.z));

                //Crear otro laser que depende de este si aun no existe uno
                if(OtherLaser == null)
                {
                    OtherLaser = Instantiate(LaserPrefab, transform.position, transform.rotation, transform);
                    OtherLaserScript = OtherLaser.GetComponent<Laser>();
                    summoning = true;
                }
                //Actualizar la posicion y direccion del otro laser
                OtherLaserScript.setPosAndDir(OtherPos, OtherDir);
            }
            else
            {
                //Si no encuentra un portal, no tiene que haber otro laser
                if(summoning)
                {
                    Destroy(OtherLaser);
                    OtherLaserScript = null;
                    summoning = false;
                }
                
            }

            //Si encuentra un objeto que reaccione a los laseres, activa su funcion.
            if(hit.transform.tag == "LaserGetter")
            {
                lasGet = hit.transform.gameObject.GetComponent<LaserGetter>();
                lasGet.UpdateOn();
                if(!getting)
                {
                    getting = true;
                    lasGet.getOn();
                }
                
                
            }
            else
            {
                if(getting)
                {
                    getting = false;
                    lasGet.getOff();
                }
            }


        }
        else lr.SetPosition(1, direction * 5000);
    }
}
