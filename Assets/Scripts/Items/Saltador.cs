using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saltador : Interactable
{
     List <Rigidbody> currentCollisions;
     public float right = 0f;
     public float up = 0f;
     public float forward = 0f;
    // Start is called before the first frame update
    void Start()
    {
        currentCollisions = new List <Rigidbody> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        if(rb != null) currentCollisions.Add(rb);
    }

    void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        if(rb != null) currentCollisions.Remove(rb);
    }

    public override void Interact()
    {
        foreach(Rigidbody rb in currentCollisions)
        {
             rb.AddForce(transform.up * up + transform.forward * forward + transform.right * right);
        }
    }

}
