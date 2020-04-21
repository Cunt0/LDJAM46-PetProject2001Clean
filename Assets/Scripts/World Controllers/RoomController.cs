using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RoomController : Singleton<RoomController>
{
    public GameObject Room;

    public GameObject friend;
    public GameObject friendHover;
    private bool friendReady;

    public GameObject computer;
    public GameObject computerHover;
    private bool computerReady;
    
    public GameObject slippers;
    public GameObject slippersHover;
    private bool slippersReady;
    
    void Awake()
    {
        friendHovering(false);
        computerHovering(false);
        slippersHovering(false);
    }

    void friendHovering(bool newBool)
    {
        friend.gameObject.SetActive(!newBool);
        friendHover.gameObject.SetActive(newBool);
        friendReady = newBool;
    }

    void computerHovering(bool newBool)
    {
        computer.gameObject.SetActive(!newBool);
        computerHover.gameObject.SetActive(newBool);
        computerReady = newBool;
    }

    void slippersHovering(bool newBool)
    {
        slippers.gameObject.SetActive(!newBool);
        slippersHover.gameObject.SetActive(newBool);
        slippersReady = newBool;
    }

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.Equals(friend) || hit.collider.gameObject.Equals(friendHover))
            {
                friendHovering(true);
                if (!AudioManager.Instance.GetLeftMusic("bingg").isPlaying) {AudioManager.Instance.Play("bingg");}
            }

            if (hit.collider.gameObject.Equals(computer) || hit.collider.gameObject.Equals(computerHover))
            {
                computerHovering(true);
                if (!AudioManager.Instance.GetLeftMusic("bingg").isPlaying) {AudioManager.Instance.Play("bingg");}

            }

            if (hit.collider.gameObject.Equals(slippers) || hit.collider.gameObject.Equals(slippersHover))
            {
                slippersHovering(true);
                if (!AudioManager.Instance.GetLeftMusic("bingg").isPlaying) {AudioManager.Instance.Play("bingg");}

            }
        }
        else
        {
            friendHovering(false);
            computerHovering(false);
            slippersHovering(false);
        }

        if(Input.GetMouseButton(0)){
            if (friendReady) ViewController.Instance.goToFriend(false); friendReady = false;
            if (computerReady) ViewController.Instance.goToComputer(false); computerReady = false;
            if (slippersReady) ViewController.Instance.goToAppartment(false); slippersReady = false;
        }
    }

    public void doRendering(bool newBool)
    {
        gameObject.SetActive(newBool);
        Renderer[] renderers = Room.GetComponentsInChildren<Renderer>();
        foreach(Renderer renderer in renderers)
        {
            renderer.enabled = newBool;
        }
    }
}
