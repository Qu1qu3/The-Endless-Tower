using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collidAntiMagic : MonoBehaviour
{
    private GameObject player;
    private CapsuleCollider playerColider;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Mage");
        playerColider = player.GetComponent<CapsuleCollider>();
        BoxCollider col = GetComponent<BoxCollider>();
        Physics.IgnoreCollision(playerColider, col, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
