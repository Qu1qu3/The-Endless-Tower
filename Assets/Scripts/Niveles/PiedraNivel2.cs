using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiedraNivel2 : Interactable
{
    FPSController player;
    GameObject panelImagen;
    // Start is called before the first frame update
    void Start()
    {   
        player = GameObject.Find("Mage").GetComponent<FPSController>();
    }

    // Update is called once per frame
    public override void Interact()
    {
        //Debug.Log ("Portal Triggering Mal");
        player.canShoot1 = true;
        player.canShoot2 = true;

        panelImagen = transform.Find("DialogoPiedra/Panel").gameObject;
        Debug.Log(transform.Find("DialogoPiedra/Panel"));
        panelImagen.SetActive(true);
    }
}
