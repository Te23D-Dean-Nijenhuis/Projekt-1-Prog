using Raylib_cs;

Raylib.InitWindow(800, 600, "The title of my window");
Raylib.SetTargetFPS(60);

while (!Raylib.WindowShouldClose())
{
  Raylib.BeginDrawing();
  
  Raylib.ClearBackground(Color.WHITE);
  
  Raylib.DrawCircle(100,100,100,Color.MAGENTA);
  
  Raylib.EndDrawing();
}