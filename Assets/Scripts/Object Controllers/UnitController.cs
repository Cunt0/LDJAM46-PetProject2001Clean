using UnityEngine;

public class UnitController
{
    private GameObject unit;
    public ItemController item;
    public PlayerController player;

    public int x;
    public int y;

    private const float xf = -9.5f;
    private const float yf = -9.5f;

    private void Awake()
    {
        
    }

    public UnitController(int x, int y, GameObject unit)
    {
        this.x = x;
        this.y = y;
        this.unit = unit;
    }

    public void MakeItem(ItemController item)
    {
        this.item = item;
    }

    public void MakePlayer(PlayerController player)
    {
        this.player = player;
    }

    public void Move(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    
    public void See()
    {
        unit.GetComponent<Transform>().position = new Vector3(x+xf,y+yf,-4);
        unit.SetActive(true);
    }

    public void Unsee()
    {
        unit.SetActive(false);
    }
}