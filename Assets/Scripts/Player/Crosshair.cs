using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public float velz = 3f;
    public float velChange = 0.001f;
    public float cX, cZ = 0f;   
    public float cY = 1f;
    private float multVel;
    private float anguloGir = 0f;
    private UnityEngine.UI.Image image;
    // Start is called before the first frame update
    void Start()
    {
        cY = 0.9f;
        image = this.GetComponent<UnityEngine.UI.Image>();
        image.color = new Color(0f, 0.9f, 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f,0f,velz);
        UpdateRGB();
        UpdateColor();

        
    }

    private void UpdateColor()
    {
        image.color = new Color(cX, cY, cZ, 1f);
    }

    private void UpdateRGB()
    {
        float a = 0.5f * Mathf.Sin((anguloGir * Mathf.PI) / 180) + 0.5f;
        float b = 0.5f * Mathf.Sin(((anguloGir+180f) * Mathf.PI) / 180) + 0.5f;
        if(a >= 0.30f && a <= 0.60f) multVel = 10f;
        else multVel = 1;
        anguloGir += velChange * multVel;
        if(anguloGir > 360) anguloGir -= 360;
        cX = a;
        cZ = a;
        cY = b;
    }

}
