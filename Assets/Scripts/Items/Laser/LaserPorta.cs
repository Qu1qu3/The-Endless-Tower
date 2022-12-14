using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPorta : LaserGetter
{
    public AltarInteraction porta;
    public CaminoRunas runes;
    
    public override void UpdateOn()
    {
        transform.Rotate(new Vector3(0f, 0f, 30f * Time.deltaTime));
    }
    public override void getOn()
    {
        porta.getOn();
        if(runes) runes.setRunas(1);
    }
    public override void getOff()
    {
        porta.getOff();
        if(runes) runes.setRunas(0);
    }
}

