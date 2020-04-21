using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public bool isProtected;
    public bool isVisible;
    public bool isExplored;
    public bool isWall;
    public bool isDoor;
    public bool isOpen;
    public bool isReturnToRoom;

    private GameObject bottile;
    private GameObject toptile;
    private GameObject fog;
    
    private GameObject background;
    
    public List<UnitController> units;

    public int x;
    public int y;

    private const float xf = -9.5f;
    private const float yf = -9.5f;

    private void Awake()
    {
        
    }

    public void Make(int x, int y)
    {
        this.x = x;
        this.y = y;
        isVisible = false;
        isExplored = false;
        isProtected = false;
        bottile = SpriteObjectController.Instance.GetBotTile();
        toptile = SpriteObjectController.Instance.GetTopTile();
        fog = SpriteObjectController.Instance.GetExploredTile();
        units = new List<UnitController>();
    }
    public void MakeWall()
    {
        isWall = true;
        isDoor = false;
        background = SpriteObjectController.Instance.GetWall();
    }

    public void MakeFloor()
    {
        isWall = false;
        isDoor = false;
        background = SpriteObjectController.Instance.GetFloor();
    }

    public void MakeDoor()
    {
        isDoor = true;
        isOpen = false;
        isWall = false;
        background = SpriteObjectController.Instance.GetDoor();
    }

    public void MakeReturnToRoom()
    {
        isReturnToRoom = true;
    }

    public void Interact()
    {
        if (isReturnToRoom)
        {
            AudioManager.Instance.Play("door2");
            ViewController.Instance.goToRoom(true);
        }
        else if (isDoor)
        {
            AudioManager.Instance.Play("door1");
            isOpen = !isOpen; 
        }
        else if (units.Count > 0)
        {
            ItemController item = units[0].item.PickMeUp();
            GameStateController.Instance.AddToInventory(item);
        }
    }

    public bool Enter()
    {
        if (!isWall)
        {
            if (!isReturnToRoom)
            {
                if (isDoor && isOpen) return true;
                if (!isDoor && units.Count == 0) return true;
            }
            Interact();
        }
        return false;
    }

    public void MakeProtected()
    {
        isProtected = true;
    }

    public bool IsProtected()
    {
        return isProtected;
    }
    
    public void See()
    {
        isVisible = true;
        if (toptile != null) { toptile.SetActive(false); }
        if (fog != null) { fog.SetActive(false); }
        foreach (UnitController unit in units) unit.See();
        
        Explore();
    }

    public void Unsee()
    {
        isVisible = false;
        if (isExplored) {fog.SetActive(true);}
        else {toptile.SetActive(true);}
        foreach (UnitController unit in units) unit.Unsee();
    }

    public bool IsVisible()
    {
        return isVisible;
    }

    public void Explore()
    {
        isExplored = true;
    }

    public bool IsExplored()
    {
        return isExplored;
    }

    public bool IsWall()
    {
        return isWall;
    }

    public bool IsDoor()
    {
        return isDoor;
    }

    public bool IsSeeThrough()
    {
        if (!isWall) 
        {
            if (!isDoor) return true;
            if (isOpen) return true;
        }
        return false;
    }
    
    public void AddUnit(UnitController unit)
    {
        units.Add(unit);
    }

    public void RemoveUnit(UnitController unit)
    {
        units.Remove(unit);
    }

    public void Draw()
    {
        bottile.GetComponent<Transform>().position = new Vector3(x+xf,y+yf,-1);
        fog.GetComponent<Transform>().position = new Vector3(x+xf,y+yf,-6);
        toptile.GetComponent<Transform>().position = new Vector3(x+xf,y+yf,-6);
        
        fog.SetActive(false);
        toptile.SetActive(false);

        if (isExplored) fog.SetActive(true);
        else toptile.SetActive(true);

        if (isWall)
        {
            background.GetComponent<Transform>().position = new Vector3(x+xf,y+yf,-2);
        }
        else
        {
            background.GetComponent<Transform>().position = new Vector3(x+xf,y+yf,-2);
        }
    }
}
