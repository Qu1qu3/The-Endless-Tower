using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaminoRunas : MonoBehaviour
{
    public float isActive;
    private Runas[] runArray;
    // Start is called before the first frame update
    void Start()
    {
        runArray = GetComponentsInChildren<Runas>();
        setRunas(isActive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setRunas(float r)
    {
        foreach(Runas rs in runArray)
        {
            rs.setActive(r);
        }
    }
}
