using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : AltarInteraction
{
    private MeshRenderer rend;
    private MeshCollider coll;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rend = transform.Find("PortaObri").GetComponent<MeshRenderer>();
        coll = transform.Find("PortaObri").GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void getOn()
    {
        rend.enabled = false;
        coll.enabled = false;
        audioSource.pitch = 0.5f;
        audioSource.volume = 0.5f;
        audioSource.Play();

    }

    public override void getOff()
    {
        rend.enabled = true;
        coll.enabled = true;
    }
}
