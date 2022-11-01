using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holdeable : Interactable
{
    // Start is called before the first frame update
    [SerializeField] Transform holdArea;
    private FPSController player;
    public bool isHolded;
    public bool canBeHolded;
    Rigidbody rb;

    public float pickUpForce = 150.0f;

    void Start()
    {
        canBeHolded = true;
        holdArea = GameObject.Find("holdObj").transform;
        player = GameObject.Find("Mage").GetComponent<FPSController>();
        isHolded = false;
        rb = GetComponent<Rigidbody>();
    }

    public override void Interact()
    {
        if(isHolded) stopHolding();
        else if(canBeHolded) startHolding();
    }
    public void stopHolding()
    {
        
        isHolded = false;
        rb.useGravity = true;
        rb.drag = 1;
        rb.constraints &= ~RigidbodyConstraints.FreezeRotation;

        transform.parent = null;
        rb.velocity = Vector3.zero;
        player.stopHolding();
    }
    public void startHolding()
    {
        
        isHolded = true;
        rb.useGravity = false;
        rb.drag = 10;
        rb.constraints |= RigidbodyConstraints.FreezeRotation;

        transform.parent = holdArea;
        player.hold(this);

    }
    void Update()
    {
        if(isHolded) holdItem();
    }

    void holdItem()
    {
        if(Vector3.Distance(transform.position, holdArea.position) > 0.1f)
        {
            Vector3 mDic = (holdArea.position - transform.position);
            rb.AddForce(mDic * pickUpForce);
        }
    }

    public void setCanBeHolded(bool b)
    {canBeHolded = b;
    }
}
