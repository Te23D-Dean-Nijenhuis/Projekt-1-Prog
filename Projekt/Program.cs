using Raylib_cs;

Raylib.InitWindow(800, 800, "The title of my window");
Raylib.SetTargetFPS(60);

int x = 10;
int y = 100;
int width = 50;
int height = 50;

List<int> coordinater = [];

Rectangle fyra = new Rectangle (x,y,width,height);

while (!Raylib.WindowShouldClose())
{
  Raylib.BeginDrawing();
  Raylib.ClearBackground(Color.Green);


  ballong(x, y, width, height, fyra);

  x++;




  Raylib.EndDrawing();
}


static (int x, int y, int widht, int height) ballong(int x, int y, int width, int height, Rectangle fyra)
{
  fyr
  Raylib.DrawRectangle(x, y, width, height, Color.Red);
  Raylib.DrawRectangleLinesEx(fyra, 5, Color.Black);
  return (x,y,width,height);
}