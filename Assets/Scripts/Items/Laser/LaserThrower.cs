using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserThrower : MonoBehaviour
{
    public GameObject Laser;
    private Laser LaserScript;
    // Start is called before the first frame update
    void Start()
    {
        GameObject LaserS = Instantiate(Laser, transform.position, transform.rotation, transform);
        Debug.Log("a "+ LaserS);
        LaserScript = LaserS.GetComponent<Laser>();
        Debug.Log("b "+ LaserScript);
        LaserScript.setPosAndDir(transform.position, transform.up);
        LaserScript.setSum(false);
    }

    // Update is called once per frame
    void Update()
    {
        LaserScript.setPosAndDir(transform.position, transform.forward);
    }
}
