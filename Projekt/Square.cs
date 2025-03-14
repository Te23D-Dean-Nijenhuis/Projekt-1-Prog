using System.Diagnostics.Metrics;
using Raylib_cs;

public class EnemySQ
{
  public int Hitpoints;
  public int Speed;
  public int Waypoint = 0;
  public int target;
  public (int x, int y) targetPos;
  public (int x, int y) Position;
  public (int x, int y) Directions;
  public Rectangle rect;
}

public class TOWER
{
    public int towerCounter = 0;
    public int target;
    public double distance;
    public int attackduration = 0;
    public int posX;
    public int posY;
    public int radius;

    // Constructor som initierar 
    public TOWER(int x, int y, int r)
    {
        posX = x;  
        posY = y;  
        radius = r;  
    }
}