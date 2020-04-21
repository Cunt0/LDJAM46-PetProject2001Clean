using System;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    private int x;
    private int y;
    private TileController cTile;
    private UnitController unit;
    private GameObject pSprite;
    private int visionRange = 5;
    private int movementCooldown;

    private int momentsSinceInteraction;

    public void Awake()
    {
        momentsSinceInteraction = 0;
        movementCooldown = 0;
        TimeUnitChange.timeChangeEvent += Passing;
    }
    
    public void Initialize()
    {
        CreateVision(this.x,this.y,visionRange);
    }

    public void PutMeDown(int x, int y, TileController cTile)
    {
        pSprite = SpriteObjectController.Instance.GetPerson();
        this.x = x;
        this.y = y;
        
        unit = new UnitController(this.x,this.y,pSprite);
        unit.MakePlayer(this);
        this.cTile = cTile;
        this.cTile.AddUnit(unit);
    }

    public void MoveMe(int x, int y)
    {
        cTile.RemoveUnit(unit);
        RemoveVision(this.x,this.y,visionRange);
        this.x = x;
        this.y = y;
        
        cTile = MapController.Instance.cMap[this.x, this.y];
        cTile.AddUnit(unit);
        unit.Move(this.x,this.y);
        CreateVision(this.x,this.y,visionRange);
    }
    void Update()
    {
        movementCooldown--;
        if (movementCooldown < 0)
        {
            if (Input.GetKey(KeyCode.UpArrow)) Try(0,1);
            else if (Input.GetKey(KeyCode.DownArrow)) Try(0,-1);
            else if (Input.GetKey(KeyCode.LeftArrow)) Try(-1,0);
            else if (Input.GetKey(KeyCode.RightArrow)) Try(1, 0);
            else if (Input.GetKey(KeyCode.Space)) Try(0,0);
        }
        
    }
    private void Passing(int currentMoment)
    {
        momentsSinceInteraction++;
    }

    private void Try(int dx, int dy)
    {
        int x = this.x + dx;
        int y = this.y + dy;

        bool moved = false;
        if (y >= 0 && y < 20 && x >= 0 && x < 20)
        {
            TileController dTile = MapController.Instance.cMap[x, y];
            if (Input.GetKeyDown(KeyCode.LeftControl)) dTile.Interact();
            else moved = dTile.Enter();
            //if (dTile.isDoor) AudioManager.Instance.Play("door1");
            if (moved) MoveMe(x,y);
        }
        
        //Time Passes (cats, snails move and so on)
        movementCooldown = 30;
    }
    
    public delegate bool PlotFunction(int x, int y);
    public bool CheckVision(int x, int y)
    {
        if (x >= 0 && x <= 19 && y >= 0 && y <= 19)
        {
            TileController tile = MapController.Instance.cMap[x, y];
            tile.See();
            if (MapController.Instance.cMap[x, y].IsSeeThrough()) return true;
        }
        return false;
    }
    public static void Line(int x0, int y0, int x1, int y1, PlotFunction plot)
    {
        double dX = (x1 - 0.5f) - (x0 - 0.5f);
        double dY = (y1 - 0.5f) - (y0 - 0.5f);
        int steps = (int) Math.Max(Math.Abs(dX), Math.Abs(dY));
        
        double xStep = (double)dX/(double)steps;
        double yStep = (double)dY/(double)steps;
        double x = (double) x0 - 0.5f;
        double y = (double) y0 - 0.5f;
        int ix;
        int iy;
        
        for (int i = 0; i < steps; i++)
        {
            ix = (int) Math.Ceiling(x);
            iy = (int) Math.Ceiling(y);
            if (!plot(ix, iy)) return;
            x += xStep;
            y += yStep;
        }
    }
    
    private void CreateVision(int x, int y, int range)
    {
        int xmin = x - range;
        int ymin = y - range;
        int xmax = x + range;
        int ymax = y + range;

        for (int xt = xmin; xt <= xmax; xt++)
        {
            for (int yt = ymin; yt <= ymax; yt++)
            {
                int dx = xt - x;
                int dy = yt - y;
                double radius = Math.Sqrt((double) ((dx * dx) + (dy * dy)));
                int radiusInt = Decimal.ToInt32(Math.Round((decimal)radius));
                if (radiusInt == range) Line(x,y,xt,yt,CheckVision);
            } 
        } 
    }
    
    private void RemoveVision(int x, int y, int range)
    {
        int xmin = Math.Max(x - range,0);
        int ymin = Math.Max(y - range,0);
        int xmax = Math.Min(x + range,19);
        int ymax = Math.Min(y + range,19);

        for (int xt = xmin; xt <= xmax; xt++)
        {
            for (int yt = ymin; yt <= ymax; yt++)
            {
                TileController tile = MapController.Instance.cMap[xt,yt];
                tile.Unsee();
            } 
        } 
    }
}
