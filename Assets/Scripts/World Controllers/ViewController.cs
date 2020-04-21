using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : Singleton<ViewController>
{
    private int momentsSinceInteraction;
    public enum View
    {
        Friend,
        Room,
        Computer,
        Appartment
    }

    public View cView; 
    
    private void Awake()
    {
        momentsSinceInteraction = 0;
        TimeUnitChange.timeChangeEvent += Passing;
    }

    private void Start()
    {
        goToFriend(true);
    }

    void Passing(int currentMoment)
    {
        if (currentMoment % 4 == 0) momentsSinceInteraction++;
    }

    public void goToRoom(bool ignoreDelay)
    {
        if (ignoreDelay || momentsSinceInteraction >= 1)
        {
            RoomController.Instance.doRendering(true);
            FriendController.Instance.doRendering(false);
            AppartmentController.Instance.doRendering(false);
            ComputerController.Instance.doRendering(false);
            GameStateController.Instance.allowInventoryRendering(true);
            GameStateController.Instance.allowStatusRendering(true);
            momentsSinceInteraction = 0;
            
            AudioManager.Instance.StopMusic();
            AudioManager.Instance.Play("creepyBass");
        }
    }

    public void goToFriend(bool ignoreDelay)
    {
        if (ignoreDelay || momentsSinceInteraction >= 1)
        {
            RoomController.Instance.doRendering(false);
            FriendController.Instance.doRendering(true);
            AppartmentController.Instance.doRendering(false);
            ComputerController.Instance.doRendering(false);
            GameStateController.Instance.allowInventoryRendering(true);
            GameStateController.Instance.allowStatusRendering(true);
            momentsSinceInteraction = 0;
            
            AudioManager.Instance.StopMusic();
            AudioManager.Instance.Play("curiousRock");
        }
    }

    public void goToAppartment(bool ignoreDelay)
    {
        if (ignoreDelay || momentsSinceInteraction >= 1)
        {
            PlayerController.Instance.Initialize();
            RoomController.Instance.doRendering(false);
            FriendController.Instance.doRendering(false);
            AppartmentController.Instance.doRendering(true);
            ComputerController.Instance.doRendering(false);
            GameStateController.Instance.allowInventoryRendering(true);
            GameStateController.Instance.allowStatusRendering(true);
            momentsSinceInteraction = 0;
            
            AudioManager.Instance.StopMusic();
            AudioManager.Instance.Play("droneBass");
        }
    }

    public void goToComputer(bool ignoreDelay)
    {
        if (ignoreDelay || momentsSinceInteraction >= 1)
        {
            RoomController.Instance.doRendering(false);
            FriendController.Instance.doRendering(false);
            AppartmentController.Instance.doRendering(false);
            ComputerController.Instance.doRendering(true);
            GameStateController.Instance.allowInventoryRendering(false);
            GameStateController.Instance.allowStatusRendering(true);
            GameStateController.Instance.flipUI(true);
            momentsSinceInteraction = 0;
            
            AudioManager.Instance.StopMusic();
            AudioManager.Instance.Play("computerSong");
        }
    }
    
}
