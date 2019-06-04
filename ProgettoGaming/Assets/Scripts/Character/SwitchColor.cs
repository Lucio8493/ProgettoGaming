using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// dato che la preda e il predatore dovranno essere di
public class SwitchColor
{
    public SwitchColor()
    {

    }

    public void HunterColor(GameObject o)
    {
        var Component = o.GetComponentsInChildren<Renderer>();
        foreach (var rend in Component)
        {
            Material mat = rend.material;
            mat.SetColor("_EmissionColor", Color.red);
        }
    }


    public void PreyColor(GameObject o)
    {
        var Component = o.GetComponentsInChildren<Renderer>();
        foreach (var rend in Component)
        {
            Material mat = rend.material;
            mat.SetColor("_EmissionColor", Color.green);
        }
    }

    // il colore del personaggio che non è ne preda ne predatore rispetto al giocatore
    public void NeutralColor(GameObject o)
    {
        var Component = o.GetComponentsInChildren<Renderer>();
        foreach (var rend in Component)
        {
            Material mat = rend.material;
            mat.SetColor("_EmissionColor", Color.white);


        }
    }
}
