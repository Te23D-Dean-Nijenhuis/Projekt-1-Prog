using System.Numerics;
using System.Xml.Serialization;
using Raylib_cs;

Raylib.InitWindow(800, 800, "The title of my window");
Raylib.SetTargetFPS(60);

int x = 0;
int y = 390;
int xh = 1;
int yh = 1;

while (!Raylib.WindowShouldClose())
{
  Raylib.BeginDrawing();
  
  Raylib.ClearBackground(Color.White);

  (x, y, xh, yh) = test(x, y, xh, yh);
  
  Raylib.EndDrawing();
}

static (int, int, int, int) test(int x, int y, int xh, int yh)
{
    Raylib.DrawRectangle(x,y,20,20,Color.Black);
    x += xh;
    y += yh;

    if(y >= 780 || y <= 0)
    {
        yh *= -1;
    }
    if(x >= 780 || x <= 0)
    {
        xh *= -1;
    }



    return (x, y, xh, yh);

}