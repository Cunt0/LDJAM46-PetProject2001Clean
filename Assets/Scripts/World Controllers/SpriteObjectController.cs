using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Serialization;

public class SpriteObjectController : Singleton<SpriteObjectController>
{
    public GameObject TopTile;
    public GameObject ExploredTile;
    public GameObject BotTile;
    
    public GameObject Wall;
    public GameObject Floor;
    public GameObject Door;

    public GameObject Crate;

    public GameObject Person;
    public GameObject Snail;
    public GameObject Cat;

    public GameObject Alcohol;
    public GameObject Pills;
    public GameObject Water;
    public GameObject Lemonade;
    public GameObject Treat;
    public GameObject Cheese;
    public GameObject Fruit;
    public GameObject Book;
    public GameObject Relic;
    public GameObject ItemFloor;

    public void MakeChild(GameObject newParent, GameObject orphan)
    {
        orphan.transform.parent = newParent.transform;
        orphan.GetComponent<Renderer>().enabled = AppartmentController.Instance.Appartment.GetComponent<Renderer>().enabled;
    }
    
    public GameObject GetTopTile()
    {
        GameObject SpriteObject = Instantiate(TopTile);
        MakeChild(MapController.Instance.map,SpriteObject);
        return SpriteObject;
    }
    public GameObject GetExploredTile()
    {
        GameObject SpriteObject = Instantiate(ExploredTile);
        MakeChild(MapController.Instance.map,SpriteObject);
        return SpriteObject;
    }
    public GameObject GetBotTile()
    {
        GameObject SpriteObject = Instantiate(BotTile);
        MakeChild(MapController.Instance.map,SpriteObject);
        return SpriteObject;
    }
    public GameObject GetWall()
    {
        GameObject SpriteObject = Instantiate(Wall);
        MakeChild(MapController.Instance.map,SpriteObject);
        return SpriteObject;
    }
    public GameObject GetFloor()
    {
        GameObject SpriteObject = Instantiate(Floor);
        MakeChild(MapController.Instance.map,SpriteObject);
        return SpriteObject;
    }
    public GameObject GetDoor()
    {
        GameObject SpriteObject = Instantiate(Door);
        MakeChild(MapController.Instance.map,SpriteObject);
        return SpriteObject;
    }
    public GameObject GetCrate()
    {
        GameObject SpriteObject = Instantiate(Crate);
        MakeChild(MapController.Instance.map,SpriteObject);
        return SpriteObject;
    }
    public GameObject GetPerson()
    {
        GameObject SpriteObject = Instantiate(Person);
        MakeChild(MapController.Instance.map,SpriteObject);
        return SpriteObject;
    }
    public GameObject GetSnail()
    {
        GameObject SpriteObject = Instantiate(Snail);
        MakeChild(MapController.Instance.map,SpriteObject);
        return SpriteObject;
    }
    public GameObject GetCat()
    {
        GameObject SpriteObject = Instantiate(Cat);
        MakeChild(MapController.Instance.map,SpriteObject);
        return SpriteObject;
    }

    public GameObject GetAlcohol(bool map)
    {
        GameObject SpriteObject;
        if (map)
        {
            SpriteObject = Instantiate(ItemFloor);
            MakeChild(MapController.Instance.map,SpriteObject);
        }
        else
        {
            SpriteObject = Instantiate(Alcohol);
            MakeChild(GameStateController.Instance.inventoryUI,SpriteObject);
        }
        return SpriteObject;
    }

    public GameObject GetPills(bool map)
    {
        GameObject SpriteObject;
        if (map)
        {
            SpriteObject = Instantiate(ItemFloor);
            MakeChild(MapController.Instance.map,SpriteObject);
        }
        else
        {
            SpriteObject = Instantiate(Pills);
            MakeChild(GameStateController.Instance.inventoryUI,SpriteObject);
        }
        return SpriteObject;
    }
    public GameObject GetWater(bool map)
    {
        GameObject SpriteObject;
        if (map)
        {
            SpriteObject = Instantiate(ItemFloor);
            MakeChild(MapController.Instance.map,SpriteObject);
        }
        else
        {
            SpriteObject = Instantiate(Water);
            MakeChild(GameStateController.Instance.inventoryUI,SpriteObject);
        }
        return SpriteObject;
    }
    public GameObject GetLemonade(bool map)
    {
        GameObject SpriteObject;
        if (map)
        {
            SpriteObject = Instantiate(ItemFloor);
            MakeChild(MapController.Instance.map,SpriteObject);
        }
        else
        {
            SpriteObject = Instantiate(Lemonade);
            MakeChild(GameStateController.Instance.inventoryUI,SpriteObject);
        }
        return SpriteObject;
    }
    public GameObject GetTreat(bool map)
    {
        GameObject SpriteObject;
        if (map)
        {
            SpriteObject = Instantiate(ItemFloor);
            MakeChild(MapController.Instance.map,SpriteObject);
        }
        else
        {
            SpriteObject = Instantiate(Treat);
            MakeChild(GameStateController.Instance.inventoryUI,SpriteObject);
        }
        return SpriteObject;
    }
    public GameObject GetCheese(bool map)
    {
        GameObject SpriteObject;
        if (map)
        {
            SpriteObject = Instantiate(ItemFloor);
            MakeChild(MapController.Instance.map,SpriteObject);
        }
        else
        {
            SpriteObject = Instantiate(Cheese);
            MakeChild(GameStateController.Instance.inventoryUI,SpriteObject);
        }
        return SpriteObject;
    }
    public GameObject GetFruit(bool map)
    {
        GameObject SpriteObject;
        if (map)
        {
            SpriteObject = Instantiate(ItemFloor);
            MakeChild(MapController.Instance.map,SpriteObject);
        }
        else
        {
            SpriteObject = Instantiate(Fruit);
            MakeChild(GameStateController.Instance.inventoryUI,SpriteObject);
        }
        return SpriteObject;
    }
    public GameObject GetBook(bool map)
    {
        GameObject SpriteObject;
        if (map)
        {
            SpriteObject = Instantiate(ItemFloor);
            MakeChild(MapController.Instance.map,SpriteObject);
        }
        else
        {
            SpriteObject = Instantiate(Book);
            MakeChild(GameStateController.Instance.inventoryUI,SpriteObject);
        }
        return SpriteObject;
    }
    public GameObject GetRelic(bool map)
    {
        GameObject SpriteObject;
        if (map)
        {
            SpriteObject = Instantiate(ItemFloor);
            MakeChild(MapController.Instance.map,SpriteObject);
        }
        else
        {
            SpriteObject = Instantiate(Relic);
            MakeChild(GameStateController.Instance.inventoryUI,SpriteObject);
        }
        return SpriteObject;
    }
}
