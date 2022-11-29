using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PiedraNivel2 : Interactable
{
    public string desc = "Interact ";
    FPSController player;
    GameObject PilarPiedra;
    GameObject contenedorTexto;
    GameObject historia;
    TMP_Text texto;
    // Start is called before the first frame update
    void Start()
    {
        SetDescription(desc);
        player = GameObject.Find("Mage").GetComponent<FPSController>();
        PilarPiedra = GameObject.Find("PilarAux");
        //panelImagen = transform.Find("PilarPiedra/DialogoPiedra");

        historia = PilarPiedra.transform.Find("ParaDialogo/Historia").gameObject;
        contenedorTexto = PilarPiedra.transform.Find("ParaDialogo/Historia/Textito").gameObject;
        texto = contenedorTexto.GetComponent<TMP_Text>();
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return) && historia.activeSelf)
        {
            historia.SetActive(false);
        }
    }

    // Update is called once per frame
    public override void Interact()
    {
        //Debug.Log ("Portal Triggering Mal");
        player.canShoot1 = true;
        player.canShoot2 = true;
        if (historia != null)
        {
            historia.SetActive(true);
            texto.SetText("ALELUYA");
        }
    }
}