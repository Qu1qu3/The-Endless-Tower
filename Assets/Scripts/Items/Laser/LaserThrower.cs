using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserThrower : MonoBehaviour
{
    public GameObject Laser;
    private Laser LaserScript;
    public bool isActive = true;
    // Start is called before the first frame update
    void Start()
    {
        if(!isActive) return;
        GameObject LaserS = Instantiate(Laser, transform.position, transform.rotation, transform);
        LaserScript = LaserS.GetComponent<Laser>();
        LaserScript.setPosAndDir(transform.position, transform.up);
        LaserScript.setSum(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isActive) return;
        LaserScript.setPosAndDir(transform.position, transform.forward);
    }
}
