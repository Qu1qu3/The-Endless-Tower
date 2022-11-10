using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowScript : MonoBehaviour
{
    private GameObject lightA;
    int j;
    
    // Start is called before the first frame update
    void Start()
    {
        lightA = transform.Find("Area Light").gameObject;
        lightA.SetActive(true);
        InvokeRepeating("ToggleLight", 3f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ToggleLight()
    {
        if( Random.Range(1,7) <= 3 )
        {
            OffLight();
            j = Random.Range(2,6);
        }
    }

    private void OffLight()
    {
        if(j > 0)
        {
            j--;
            lightA.SetActive(false);
            Invoke("OnLight",0.1f);
        }
    }

    private void OnLight()
    {
        
        lightA.SetActive(true);
        Invoke("OffLight",0.1f);
    }
}
