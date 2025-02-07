using System.Diagnostics.Metrics;
using Raylib_cs;

public class EnemySQ
{
  public int Speed;
  public int Waypoint = 0;
  public (int x, int y) Position;

  public (int x, int y) Directions;
  
  public Rectangle rect;
}

public class TOWER
{
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