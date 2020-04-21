using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Video;

public class FriendController : Singleton<FriendController>
{
    public GameObject Friend;
    
    public GameObject arrow;
    public GameObject arrowHover;
    public GameObject happyFace;
    public GameObject angryFace;
    private bool arrowReady;
    private int timeSinceFace;
    private int timeAway;
    
    void arrowHovering(bool newBool)
    {
        arrow.gameObject.SetActive(!newBool);
        arrowHover.gameObject.SetActive(newBool);
        arrowReady = newBool;
    }
    
    void Awake()
    {
        timeSinceFace = 0;
        DisableFaces();
        TimeUnitChange.timeChangeEvent += Passing;
    }

    public void Passing(int CurrentMoment)
    {
        timeSinceFace--;
        if (timeSinceFace == 0) DisableFaces();
        if (timeAway > 30) AngryFace(true); 
    }
    
    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.Equals(arrow) || hit.collider.gameObject.Equals(arrowHover)) {arrowHovering(true);}
        }
        else
        {
            arrowHovering(false);
        }

        if(Input.GetMouseButton(0)){
            if (arrowReady) ViewController.Instance.goToRoom(false); arrowReady = false;
        }
    }

    public void doRendering(bool newBool)
    {
        timeAway = 0;
        gameObject.SetActive(newBool);
        Renderer[] renderers = Friend.GetComponentsInChildren<Renderer>();
        foreach(Renderer renderer in renderers)
        {
            renderer.enabled = newBool;
        }
    }

    public void Consume()
    {
        HappyFace(true);
    }

    public void DisableFaces()
    {
        HappyFace(false);
        AngryFace(false);
    }

    public void HappyFace(bool newbool)
    {
        happyFace.SetActive(newbool);
        if (newbool) timeSinceFace = 10;
    }

    public void AngryFace(bool newbool)
    {
        angryFace.SetActive(newbool);
        if (newbool) timeSinceFace = -1;
    }
}
