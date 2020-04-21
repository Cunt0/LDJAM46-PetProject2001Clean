using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameStateController : Singleton<GameStateController>
{


    public int Authority;
    public int Clarity;
    public const int ClarityCap = 2001;
    public int Hunger;
    public const int HungerCap = 10;
    public int Thirst;
    public const int ThirstCap = 10;
    public int Drunkenness;
    public const int DrunkennessCap = 10;
    public int LemonadeCounter;
    public int messageTimer = 0;
    public int messageTimerCap = 10;

    private int eR;

    public GameObject inventoryUI;
    public GameObject[] InventorySlots;
    public GameObject statsUI;

    public GameObject statusF;
    public Text statusT;

    public ItemController[] inventory;

    private bool showingInvetnory;
    private bool showingStatus;

    private bool inventoryRenderingPossible;
    private bool statusRenderingPossible;

    private string currentMessage;
    public Queue<string> messages;

    private void Awake()
    {
        messages = new Queue<string>();

        inventoryRenderingPossible = false;
        statusRenderingPossible = false;
        doInventoryRendering(false);
        doStatusRendering(false);
        
        statusT = statusF.GetComponent<Text>();
        inventory = new ItemController[10];
        for (int i = 0; i < inventory.Length; i++) { inventory[i] = null;} 
        
        Clarity = 0;
        Hunger = 0;
        Thirst = 0;
        Authority = 0;
        LemonadeCounter = 100;
        TimeUnitChange.timeChangeEvent += Passing;
    }

    public void Passing(int CurrentMoment)
    {
        if (CurrentMoment % 2000 + eR == 0)
        {
            DoEvent();
        }

        if (CurrentMoment % 600 == 0)
        {
            DoHunger();
        }

        if (CurrentMoment % 300 == 0)
        {
            DoThirst();
        }

        if (CurrentMoment % 10 == 0)
        {
            DoClarity();
            DoLemonade();
        }

        messageTimer++;
        if (messageTimer > messageTimerCap) NextMessage();
    }

    private void DoEvent()
    {
        eR = Random.Range(-1000, 1000);
    }

    private void DoHunger()
    {
        Hunger++;
        if (Hunger > HungerCap) {Clarity += 500; Hunger = HungerCap;}
    }

    private void DoLemonade()
    {
        LemonadeCounter--;
        if (LemonadeCounter == 0)
        {
            ItemController lemonade = statusF.AddComponent<ItemController>();
            lemonade.GenerateMe(ItemController.Item.Lemonade);
            AddToInventory(lemonade);
        }
    }

    public bool AddToInventory(ItemController item)
    {
        bool found = false;
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null) {inventory[i] = item; found = true; break; }
        }

        item.iSprite.AddComponent<BoxCollider2D>();

        if (found)
        {
            ImposeMessage("Added " + item.name + " to inventory");
            updateInventory();
            return true;
        }
        
        ImposeMessage("Couldn't add " + item.name + " to inventory");
        return false;
    }

    private ItemController RemoveFromInventory(int i)
    {
        ItemController item = inventory[i];
        inventory[i] = null;
        updateInventory();
        return item;
    }
    
    private void ConsumeFromInventory(ItemController item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].Equals(item))
            {
                inventory[i] = null;
                updateInventory();
                Destroy(item);
                Destroy(item.iSprite);
            }
        }
    }

    private void DoThirst()
    {
        Thirst++;
        if (Thirst > ThirstCap) DoThirstEnd();
        Thirst = ThirstCap;
    }

    private void DoClarity()
    {
        Clarity++;
        if (Clarity > ClarityCap) {DoClarityEnd(); Clarity = ClarityCap;}
        
    }

    private void DoDrunkenNess()
    {
        if (Drunkenness > 0) Drunkenness--;
        if (Drunkenness > DrunkennessCap) {Clarity -= 100; Drunkenness = DrunkennessCap;}
    }

    private void DoThirstEnd()
    {

    }

    private void DoClarityEnd()
    {

    }

    private void DoMessages(bool doMessages)
    {
        if (doMessages && currentMessage != null)
        {
            statusT.text = currentMessage;
        }
        else
        {
            statusT.text = "clarity: " + Clarity + " | " +
                           "thirst: " + Thirst + " | " +
                           "hunger: " + Hunger + " | " +
                           "authority: " + Authority;
        }
    }

    public void allowInventoryRendering(bool renderPossible)
    {
        inventoryRenderingPossible = renderPossible;
        if (!renderPossible) doInventoryRendering(false);
    }

    public void doInventoryRendering(bool newBool)
    {
        newBool = inventoryRenderingPossible && newBool;
        Renderer[] renderers = inventoryUI.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = newBool;
        }
    }

    public void showInventory(bool newBool)
    {
        if (newBool)
        {
            updateInventory();
            doInventoryRendering(true);
        }
        else
        {
            doInventoryRendering(false);
        }
    }
    
    public void allowStatusRendering(bool renderPossible)
    {
        statusRenderingPossible = renderPossible;
        if (!renderPossible) doInventoryRendering(false);
    }

    public void doStatusRendering(bool newBool)
    {
        newBool = statusRenderingPossible && newBool;
        Renderer[] renderers = statsUI.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers) renderer.enabled = newBool;
        Canvas[] canvases = statsUI.GetComponentsInChildren<Canvas>();
        foreach (Canvas canvas in canvases) canvas.enabled = newBool;
    }

    public void showStatus(bool newBool)
    {
        if (newBool)
        {
            doStatusRendering(true);
        }
        else
        {
            doStatusRendering(false);
        }
    }

    public void updateInventory()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] != null)
            {
                Vector3 v3 = InventorySlots[i].GetComponent<Transform>().position;
                inventory[i].iSprite.SetActive(true);
                inventory[i].iSprite.GetComponent<Renderer>().enabled = showingInvetnory;
                v3[2] = v3[2] - 1;
                inventory[i].iSprite.GetComponent<Transform>().position = v3;
            }
        }
    }

    public void flipUI(bool up)
    {
        if (up)
        {
            Vector3 v3I = inventoryUI.GetComponent<Transform>().position;
            v3I[1] = 8;
            inventoryUI.GetComponent<Transform>().position = v3I;
        
            Vector3 v3S = statsUI.GetComponent<Transform>().position;
            v3S[1] = 9.5f;
            statsUI.GetComponent<Transform>().position = v3S;
        }
        else
        {
            Vector3 v3I = inventoryUI.GetComponent<Transform>().position;
            v3I[1] = -8;
            inventoryUI.GetComponent<Transform>().position = v3I;
        
            Vector3 v3S = statsUI.GetComponent<Transform>().position;
            v3S[1] = -9.5f;
            statsUI.GetComponent<Transform>().position = v3S;
        }
        
    }

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null && hit.collider.gameObject.Equals(statsUI))
        {
            DoMessages(false);
        }
        else
        {
            DoMessages(true);
        }

        if (hit.collider != null && hit.collider.gameObject.Equals(inventoryUI))
        {
            if (!showingInvetnory) 
            {
                showInventory(true);
                showingInvetnory = true;
            }
        } else
        {
            if (showingInvetnory)
            {
                showInventory(false);
                showingInvetnory = false;
            }
        }

        if (!showingStatus && statusRenderingPossible)
        {
            showStatus(true);
            showingStatus = true;
        } else if (showingStatus && !statusRenderingPossible)
        {
            showStatus(false);
            showingStatus = false;
        }

        if (hit.collider != null)
        {
            GameObject col = hit.collider.gameObject; 
            foreach (ItemController item in inventory)
            {
                if (item != null && item.iSprite.Equals(col))
                {
                    if (!showingInvetnory) 
                    {
                        showInventory(true);
                        showingInvetnory = true;
                        currentMessage = item.name;
                    }
                    else
                    {
                        currentMessage = item.name;
                    }

                    if (Input.GetMouseButton(0))
                    {
                        if (item.givable && ViewController.Instance.cView.Equals(ViewController.View.Friend))
                        {
                            FriendController.Instance.Consume();
                            ConsumeFromInventory(item);
                        }
                        else if (item.consumable)
                        {
                            Consume(item);
                            ConsumeFromInventory(item);
                        }
                    }
                }
            }
        }
    }

    public void ImposeMessage(string message)
    {
        messageTimer = 0;
        currentMessage = message;
    }
    
    public void AddMessage(string message)
    {
        messages.Enqueue(message);
    }

    public void NextMessage()
    {
        currentMessage = null;
        if (messages.Count > 0) ImposeMessage(messages.Dequeue());
    }

    private void Consume(ItemController item)
    {
        switch (item.ItemType)
        {
            case ItemController.Item.Lemonade:
                Thirst = Math.Max(Thirst - item.value, 0);
                if (LemonadeCounter < 0) LemonadeCounter = 200;
                else LemonadeCounter -= 50;
                ImposeMessage(item.usage);
                break;
            case ItemController.Item.Treat:
                Hunger = Math.Max(Hunger - item.value, 0);
                ImposeMessage(item.usage);
                break;
        }
    }
}
