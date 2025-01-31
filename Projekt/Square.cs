using Raylib_cs;

public class Square
{
  public Rectangle rect;
}

public class TOWER
{
    public int posX;
    public int posY;
    public int radius;

    // Constructor to initialize the TOWER object with posX, posY, and radius
    public TOWER(int x, int y, int r)
    {
        posX = x;  
        posY = y;  
        radius = r;  
    }
}