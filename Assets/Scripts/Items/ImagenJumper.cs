using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagenJumper : MonoBehaviour
{
    public float cG = 1f;
    private float multVel;
    public float velChange = 0.001f;
    private SpriteRenderer image;
    private float anguloGir = 0f;
    // Start is called before the first frame update
    void Start()
    {
        image = this.GetComponent<SpriteRenderer>();
        image.color = new Color(0f, 0.9f, 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRGB();
        image.color = new Color( 0f, cG, 1f, 1f);
    }

    private void UpdateRGB()
    {
        float a = 0.48f * Mathf.Sin((anguloGir * Mathf.PI) / 180) + 0.5f;
        if(a >= 0.30f && a <= 0.60f) multVel = 1;
        else multVel = 1;
        anguloGir += velChange * multVel;
        if(anguloGir > 360) anguloGir -= 360;
        cG = a;
    }
}
