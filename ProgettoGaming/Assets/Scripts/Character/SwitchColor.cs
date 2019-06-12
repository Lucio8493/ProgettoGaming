using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// dato che la preda e il predatore dovranno essere di
public class SwitchColor
{

    Color preyColor = new Color(0, 0.6f, 0, 0.6f);

    Color hunterColor = new Color( 0.6f, 0, 0 , 0.6f);

    Color neutralColor = new Color(0.6f, 0.6f, 0.6f, 0.6f);


    public SwitchColor()
    {

    }
    

    public void HunterColor(GameObject o)
    {
        var Component = o.GetComponentsInChildren<Renderer>();
        foreach (var rend in Component)
        {
            Material mat = rend.material;
            mat.SetColor("_EmissionColor", hunterColor);
        }
    }


    public void PreyColor(GameObject o)
    {
        var Component = o.GetComponentsInChildren<Renderer>();
        foreach (var rend in Component)
        {
            Material mat = rend.material;
            mat.SetColor("_EmissionColor", preyColor);
        }
    }

    // il colore del personaggio che non è ne preda ne predatore rispetto al giocatore
    public void NeutralColor(GameObject o)
    {
        var Component = o.GetComponentsInChildren<Renderer>();
        foreach (var rend in Component)
        {
            Material mat = rend.material;
            mat.SetColor("_EmissionColor", neutralColor);


        }
    }
}
