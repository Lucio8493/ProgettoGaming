using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIVisibility : AbstractVisibility
{

    public override void SetInvisible()
    {
        
        var children = this.GetComponentsInParent<Transform>();
        foreach (var child in children)
        {
            if (child.name == "HunterArrow" || child.name == "PreyArrow" || child.name == "Sword")
            {
                child.gameObject.SetActive(false);
            }
            else if (child.name == "Body1" || child.name == "Rat")
            {
                child.GetComponentInParent<SkinnedMeshRenderer>().enabled = false;
            }
        }
    }

    public override void SetVisible()
    {
        var children = this.GetComponentsInParent<Transform>();
        foreach (var child in children)
        {
            if (child.name == "Sword")
            {
                child.gameObject.SetActive(true);
            }
            else if (child.name == "Body1" || child.name == "Rat")
            {
                child.GetComponentInParent<SkinnedMeshRenderer>().enabled = true;
            }
        }
    }

}
