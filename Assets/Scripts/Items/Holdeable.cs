using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holdeable : Interactable
{
    public string desc = "Grab item ";
    // Start is called before the first frame update
    [SerializeField] Transform holdArea;
    private FPSController player;
    public bool isHolded;
    public bool canBeHolded;
    Rigidbody rb;
    public Material[] originalMaterials;
    private Material newMat;
    Renderer meshRenderer;
    private CapsuleCollider playerColider;
    private Collider thisCollider;
    public float lerpDuration = 0.5f;

    public float pickUpForce = 20.0f;

    void Start()
    {
        thisCollider = GetComponent<Collider>();
        newMat = Resources.Load<Material>("Material/blueTransparent");
        meshRenderer = GetComponent<Renderer>();
        originalMaterials = meshRenderer.materials;
        SetDescription(desc);
        canBeHolded = true;
        holdArea = GameObject.Find("holdObj").transform;
        player = GameObject.Find("Mage").GetComponent<FPSController>();
        playerColider = player.GetComponent<CapsuleCollider>();
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

        //meshRenderer.materials = originalMaterials;
        StartCoroutine(Lerp(1f,0f));
        Physics.IgnoreCollision(playerColider, thisCollider, false);
    }
    public void startHolding()
    {
        
        isHolded = true;
        rb.useGravity = false;
        rb.drag = 10;
        rb.constraints |= RigidbodyConstraints.FreezeRotation;

        transform.parent = holdArea;
        player.hold(this);

        //var materialsCopy = meshRenderer.materials;
        //materialsCopy[0] = newMat;
        //meshRenderer.materials = materialsCopy;
        StartCoroutine(Lerp(0f,1f));
        Physics.IgnoreCollision(playerColider, thisCollider, true);
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
            rb.AddForce(mDic * pickUpForce, ForceMode.Force);
        }
    }

    public void setCanBeHolded(bool b)
    {
        canBeHolded = b;
    }

    IEnumerator Lerp(float startValue, float endValue)
    {
        float timeElapsed = 0;
        float slider;
        while (timeElapsed < lerpDuration)
        {
            slider = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            meshRenderer.material.SetFloat("_SliderOn", slider);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        slider = endValue;
    }
}
