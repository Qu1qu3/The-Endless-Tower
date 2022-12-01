using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPorta : LaserGetter
{
    public AltarInteraction porta;

    

    public override void getOn()
    {
        porta.getOn();
    }
    public override void getOff()
    {
        porta.getOff();
    }
}

