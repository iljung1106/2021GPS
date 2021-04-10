using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : Interactor
{
    public RpgItem item;
    

    
    public override void Interact()
    {
        base.Interact();

        Pick();
    }
    // Start is called before the first frame update

    public void Pick()
    {
        Debug.Log("useItem" + item.name);
        Destroy(gameObject);
    }
}
