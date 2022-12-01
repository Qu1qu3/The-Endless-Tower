using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PiedraNivel2 : Interactable
{
    FPSController player;
    public GameObject PilarPiedra;
    GameObject contenedorTexto;
    GameObject historia;
    TMP_Text texto;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Mage").GetComponent<FPSController>();
        PilarPiedra = transform.root.gameObject;
        Debug.Log(PilarPiedra);
        //panelImagen = transform.Find("PilarPiedra/DialogoPiedra");

        historia = PilarPiedra.transform.Find("ParaDialogo/Historia").gameObject;
        contenedorTexto = PilarPiedra.transform.Find("ParaDialogo/Historia/Textito").gameObject;
        texto = contenedorTexto.GetComponent<TMP_Text>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && historia.activeSelf)
        {
            historia.SetActive(false);
            player.activeHist = false;
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
            player.activeHist = true;
            texto.SetText("Recuerdas que se pueden hacer portales con los clicks del ratón");
        }
    }
}