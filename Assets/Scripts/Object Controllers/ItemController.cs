using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private TileController cTile;
    private UnitController unit;
    
    public GameObject iSprite;
    public GameObject tSprite;
    
    public bool usable;
    public bool consumable;
    public bool givable;
    public string usage;
    public string name;
    public int value;
    public int bonusValue;

    public Item ItemType;

    public enum Item
    {
        Alcohol,
        Pills,
        Water,
        Lemonade,
        Treat,
        Cheese,
        Fruit,
        Book,
        Relic
    }

    public ItemController GenerateMe(Item itemType)
    {
        usable = false;
        consumable = false;
        givable = false;
        bonusValue = 0;
        this.ItemType = itemType;
        switch (itemType)
        {
            case Item.Alcohol:
                GenerateAlcohol();
                break;
            case Item.Pills:
                GeneratePills();
                break;
            case Item.Water:
                GenerateWater();
                break;
            case Item.Lemonade:
                GenerateLemonade();
                break;
            case Item.Treat:
                GenerateTreat();
                break;
            case Item.Cheese:
                GenerateCheese();
                break;
            case Item.Fruit:
                GenerateFruit();
                break;
            case Item.Book:
                GenerateBook();
                break;
            case Item.Relic:
                GenerateRelic();
                break;
        }

        return this;
    }

    private void GenerateRelic()
    {
        throw new System.NotImplementedException();
    }

    private void GenerateBook()
    {
        throw new System.NotImplementedException();
    }

    private void GenerateFruit()
    {
        throw new System.NotImplementedException();
    }

    private void GenerateCheese()
    {
        throw new System.NotImplementedException();
    }

    private void GenerateTreat()
    {
        iSprite = SpriteObjectController.Instance.GetTreat(false);
        tSprite = SpriteObjectController.Instance.GetTreat(true);
        
        usage = "How can Jeb eat these?"; 
        name = "Treat";
        givable = true;
        consumable = true;
        value = 3;
    }

    private void GenerateLemonade()
    {
        iSprite = SpriteObjectController.Instance.GetLemonade(false);
        tSprite = SpriteObjectController.Instance.GetLemonade(true);
        
        usage = "No fizz in this heat, but it tastes nice."; 
        name = "Lemonade";
        usable = true;
        consumable = true;
        value = 3;
    }

    private void GenerateWater()
    {
        throw new System.NotImplementedException();
    }

    private void GeneratePills()
    {
        throw new System.NotImplementedException();
    }

    private void GenerateAlcohol()
    {
        throw new System.NotImplementedException();
    }

    public ItemController PutMeDown(TileController cTile)
    {
        unit = new UnitController(cTile.x,cTile.y,tSprite);
        unit.MakeItem(this);
        this.cTile = cTile;
        this.cTile.AddUnit(unit);
        iSprite.SetActive(false);

        return this;
    }
    
    public ItemController PickMeUp()
    {
        if (cTile != null) cTile.RemoveUnit(unit);
        tSprite.SetActive(false);
        
        return this;
    }
}
