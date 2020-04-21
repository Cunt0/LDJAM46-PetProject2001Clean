using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapController : Singleton<MapController>
{
    public GameObject map;
    private List<TileController[,]> maps;
    public TileController[,] cMap;
    private void Awake()
    {
        maps = new List<TileController[,]>();
        cMap = mapCreator(20);
        maps.Add(cMap);
    }

    private TileController[,] mapCreator(int n)
    {
        TileController[,] map = new TileController[n,n];

        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                TileController tile = gameObject.AddComponent<TileController>();
                tile.Make(x,y);
                tile.MakeWall();
                map[x, y] = tile;
            }
        }

        addStart(map);

        addCorridors(map,20);

        addRooms(map, 12);

        addItems(map);

        foreach (TileController tile in map)
        {
            tile.Draw();
        }
        
        return map;
    }

    private int crawlAsLongAsPossible(int n, int x, int y, int xStep, int yStep, TileController[,] map)
    {
        if (n <= 0) return n;
        if (x > 0 && x < 19 && y > 0 && y < 19)
        {
            if (!map[x, y].IsProtected())
            {
                map[x,y].MakeFloor();
                n = crawlAsLongAsPossible(n-1,x + xStep, y + yStep, xStep, yStep, map);
            }
        }
        return n;
    }
    
    private bool roomCrawler(int xn, int yn, int x, int y, int xStep, int yStep, bool xmain, TileController[,] map)
    {
        if (xn < 0 && yn < 0) return true;
        if (hasInVicinity(x, y, 0, map)) return true;
        if (x > 0 && x < 19 && y > 0 && y < 19)
        {
            //Mutate:
            int r = Random.Range(0, 20 + 1);
            if (r == 0) xn += Random.Range(-1,1+1);
            if (r == 1) yn += Random.Range(-1,1+1);
            if (r == 2) xStep = Random.Range(-1,1+1);
            if (r == 3) yStep = Random.Range(-1,1+1);

            if (x + xStep < 0 || x + xStep > 19 || y + yStep < 0 || y + yStep > 19) return true;
            if (xmain)
            {
                if (xn > 0) if (!map[x + xStep, y].IsWall()) return true; roomCrawler(xn - 1, yn, x + xStep, y,xStep,yStep,xmain,map);
                if (yn > 0) if (!map[x, y + yStep].IsWall()) return true; roomCrawler(xn, yn - 1, x, y + yStep,xStep,yStep,xmain,map);
            }
            else
            {
                if (yn > 0) if (!map[x + xStep, y].IsWall()) roomCrawler(xn, yn - 1, x, y + yStep,xStep,yStep,xmain,map);
                if (xn > 0) if (!map[x, y + yStep].IsWall()) roomCrawler(xn - 1, yn, x + xStep, y,xStep,yStep,xmain,map);
            }
            
            map[x,y].MakeFloor();
        }
        return true;
    }

    private bool hasInVicinity(int x, int y, int mode, TileController[,] map)
    {
        bool found = false;
        // mode 0: protected, mode 1: air, mode 2: door
        int minx = Math.Max(x - 1,0);
        int miny = Math.Max(y - 1,0);
        int maxx = Math.Min(x + 1,19);
        int maxy = Math.Min(y + 1,19);
        for (int ix = minx; ix <= maxx; ix++)
        {
            for (int iy = miny; iy <= maxy; iy++)
            {
                if (ix != x || iy != y)
                {
                    switch (mode)
                    {
                        case 0:
                            found = found || map[ix,iy].IsProtected();
                            break;
                        case 1:
                            found = found || (!map[ix,iy].IsWall() && !map[ix,iy].IsDoor());
                            break;
                        case 2:
                            found = found || map[ix, iy].IsDoor();
                            break;
                    }
                }
            }
        }

        return found;
    }

    private bool addItem(ItemController.Item itemType, TileController[,] map)
    {
        List<TileController> possibleLocations = new List<TileController>();
        foreach (TileController tile in map)
        {
            if (!tile.IsWall() && !tile.IsProtected() && !tile.isDoor) possibleLocations.Add(tile);
        }
        if (possibleLocations.Count == 0) return false;
        TileController cTile = possibleLocations[Random.Range(0, possibleLocations.Count-1+1)];
        
        ItemController item = gameObject.AddComponent<ItemController>();
        item.GenerateMe(itemType);
        item.PutMeDown(cTile);
        
        return true;
    }

    private bool addItems(TileController[,] map)
    {
        for (int i = 0; i < 5; i++) { addItem(ItemController.Item.Lemonade,map);}
        for (int i = 0; i < 5; i++) { addItem(ItemController.Item.Treat,map);}

        return true;
    }
    
    private bool addRoom(TileController[,] map, int n, int dev)
    {
        List<TileController> possibleStarts = new List<TileController>();
        foreach (TileController tile in map)
        {
            if (tile.IsWall())
            {
                if (!hasInVicinity(tile.x,tile.y,0,map) && hasInVicinity(tile.x, tile.y, 1, map) && !hasInVicinity(tile.x,tile.y,2,map)) possibleStarts.Add(tile);
            }
        }

        if (possibleStarts.Count == 0) return false;
        TileController cTile = possibleStarts[Random.Range(0, possibleStarts.Count-1+1)];
        
        
        
        int x = cTile.x;
        int y = cTile.y;
        int xn = Random.Range(n - dev, n + dev + 1);
        int yn = Random.Range(n - dev, n + dev + 1);
        int rStep = 1;
        if (Random.Range(0, 1 + 1) == 0) rStep *= -1;
        
        if (x - 1 > 0 && x + 1 < 19)
        {
            if (map[x - 1, y].IsWall() && !map[x + 1, y].IsWall())
            {
                cTile.MakeDoor();
                roomCrawler(xn,yn,x-1,y,-1,rStep,true,map);
            } else if (!map[x - 1, y].IsWall() && map[x + 1, y].IsWall())
            {
                cTile.MakeDoor();
                roomCrawler(xn,yn,x+1,y,1,rStep,true,map);
            }
        } else if (y - 1 > 0 && y + 1 < 19)
        {
            if (map[x, y-1].IsWall() && !map[x, y+1].IsWall())
            {
                cTile.MakeDoor();
                roomCrawler(xn,yn,x,y-1,rStep,-1,false,map);
            } else if (!map[x, y-1].IsWall() && map[x, y+1].IsWall())
            {
                cTile.MakeDoor();
                roomCrawler(xn,yn,x,y+1,rStep,1,false,map);
            }
        }
        
        
        
        return true;
    }
    private void addRooms(TileController[,] map, int n)
    {
        for (int i = 0; i < n; i++) addRoom(map,6,2);
    }
    
    private bool addCorridor(TileController[,] map, int n, int dev)
    {
        List<TileController> possibleStarts = new List<TileController>();
        foreach (TileController tile in map) if (!tile.IsProtected() && !tile.IsWall()) possibleStarts.Add(tile);
        if (possibleStarts.Count == 0) return false;
        TileController cTile = possibleStarts[Random.Range(0, possibleStarts.Count-1+1)];

        int xStep = 0;
        int yStep = 0;
        if (Random.Range(0, 1+1) == 0) xStep = 1; else yStep = 1;
        
        int length = Random.Range(n - dev, n + dev +1);
        int first = Random.Range(0, length+1);
        length = crawlAsLongAsPossible(first, cTile.x, cTile.y, xStep, yStep, map);
        crawlAsLongAsPossible(length, cTile.x, cTile.y, -xStep, -yStep, map);
        
        return true;
    }
    
    private void addCorridors(TileController[,] map, int n)
    {
        for (int i = 0; i < n; i++) addCorridor(map, 15, 5);
    }

    private void addStart(TileController[,] map)
    {
        int ix = 0;
        int iy = Random.Range(0,15+1);
        
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                TileController tile = map[ix+x, iy+y];
                tile.MakeProtected();
                tile.Explore();
            }
        }
        
        for (int x = 1; x < 3; x++)
        {
            for (int y = 1; y < 3; y++)
            {
                TileController tile = map[ix+x, iy+y];
                tile.MakeFloor();
            }
        }
        
        TileController entranceTile = map[ix+3, iy+2];
        entranceTile.MakeDoor();
        entranceTile.MakeReturnToRoom();
        TileController landingTile = map[ix + 4, iy + 2];
        landingTile.MakeFloor();
        PlayerController.Instance.PutMeDown(ix+4,iy+2,landingTile);
    }
}
