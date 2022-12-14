using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPorta : LaserGetter
{
    public AltarInteraction porta;

    
    public override void UpdateOn()
    {
        transform.Rotate(new Vector3(0f, 0f, 30f * Time.deltaTime));
    }
    public override void getOn()
    {
        porta.getOn();
    }
    public override void getOff()
    {
        porta.getOff();
    }
}

