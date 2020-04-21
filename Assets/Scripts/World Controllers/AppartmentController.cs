using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppartmentController : Singleton<AppartmentController>
{
    public GameObject Appartment;
    void Awake()
    {
        
    }

    public void doRendering(bool newBool)
    {
        gameObject.SetActive(newBool);
        Renderer[] renderers = Appartment.GetComponentsInChildren<Renderer>();
        foreach(Renderer renderer in renderers)
        {
            renderer.enabled = newBool;
        }
    }
}
