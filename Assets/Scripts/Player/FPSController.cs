using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour {

    public float jumpForce = 8;
    public float drag = 240f;
    public bool lockCursor;
    public float mouseSensitivity = 10;

    private bool isHolding;
    private Holdeable holdingObject;

    public bool canShoot1 = true;
    public bool canShoot2 = true;
    public bool canShootP = true;
    public PlayerPortal playerPortal;
    public PlayerPortalPasado playerPortalPasado;
    public PlayerInteract playerInteract;
    private GameObject jumpCheck;
    public Vector3 jumpCheckSize;

    [SerializeField] private bool isPasado;
    [SerializeField] private float puntoMedio;


    Camera cam;
    
    public float velocidad;
    private int multVel;
    private float multVel2;
    private float multAir;
    Rigidbody Rbody;
    float rotX;
    Vector3 moveDirection;
    private bool isGround;

    void Start () {
        jumpCheck = transform.Find("jumpCheck").gameObject;
        isHolding = false;
        holdingObject = null;
        playerPortal.Initialize();
        playerPortalPasado.Initialize();
        cam = Camera.main;
        if (lockCursor) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        Rbody = GetComponent<Rigidbody>();
        puntoMedio = (GameObject.Find("LayoutPasado").transform.position.x + GameObject.Find("Layout").transform.position.x)/2;
        
    }

    void Update () {
        isPasado = transform.position.x < puntoMedio;
        isGround = Physics.SphereCast(transform.position, 0.3f, Vector3.down, out RaycastHit hit, 1.2f);
        readInput();
        if(isGround) multAir = 1f; else multAir = 0.3f;
        
        
        
        float  h = mouseSensitivity * Input.GetAxis("Mouse X") * Time.fixedDeltaTime;
        float  v = mouseSensitivity * Input.GetAxis("Mouse Y") * Time.fixedDeltaTime;
        transform.Rotate(0,h,0);
        
        rotX = (cam.transform.eulerAngles.x > 269) ? cam.transform.eulerAngles.x - 360 : cam.transform.eulerAngles.x;
        if(rotX - v < 90f && rotX - v > -90f)
        {   
            cam.transform.Rotate(-v,0,0);
        }
    }
    void FixedUpdate()
    {
        movePlayer();
    }


    void readInput()
    {
        if(Input.GetKey(KeyCode.LeftShift)) {multVel = 1;} else {multVel = 1;}
        if(Input.GetKeyDown(KeyCode.Space)) jump();
        if(Input.GetMouseButtonDown(0) && !isHolding && canShoot1) shootPortal(0);
        if(Input.GetMouseButtonDown(1) && !isHolding && canShoot2) shootPortal(1);
        if(Input.GetKeyDown(KeyCode.Q) && !isHolding && canShootP) shootPortal(2);
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(isHolding) holdingObject.stopHolding();
            else playerInteract.tryToInteract();
        }
        
    }
    void movePlayer()
    {
        //Debug.Log (moveDirection.normalized, Rbody);
        moveDirection = transform.forward * Input.  GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        if (isGround) applyDrag(-Rbody.velocity);
        
        //if(moveDirection.x == 0f) Rbody.velocity = new Vector3(0f, Rbody.velocity.y, Rbody.velocity.z);
        //if(moveDirection.z == 0f) Rbody.velocity = new Vector3(Rbody.velocity.x, Rbody.velocity.y, 0f);
        Vector2 velocitySides = new Vector2(Rbody.velocity.x, Rbody.velocity.z);
        if(velocitySides.magnitude > 3f)
        {
            multVel2 = 0.5f;
            //velocitySides = Vector2.ClampMagnitude(velocitySides, 3f);
            //Rbody.velocity = new Vector3(velocitySides.x,Rbody.velocity.y,velocitySides.y);
        }
        else multVel2 = 1f;
        Rbody.AddForce(moveDirection.normalized * velocidad * multVel * multVel2 *multAir, ForceMode.VelocityChange );
        
    }

    private void shootPortal(int p)
    {
        if(isPasado) playerPortalPasado.shootPortal(p);
        else playerPortal.shootPortal(p);
    }
    private void jump()
    {
        //Debug.Log (isGround);
        if(isGround)
        {
            Rbody.AddForce(Vector2.up * jumpForce);
        }
    }

    private void applyDrag(Vector3 dir)
    {
        if(Rbody.velocity.magnitude != 0)
        {
            Rbody.AddForce(dir * drag * Time.fixedDeltaTime);
        }
    }

    public void hold(Holdeable obj)
    {
        holdingObject = obj;
        isHolding = true;
    }

    public void stopHolding()
    {
        holdingObject = null;
        isHolding = false;
    }

    
    

}