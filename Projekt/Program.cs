using Raylib_cs;

Raylib.InitWindow(1600, 800, "Geomatry dashers");
Raylib.SetTargetFPS(60);
Color Background = new(0,225,0,255);

List<Square> squares = [];



while (!Raylib.WindowShouldClose())
{
  Raylib.BeginDrawing();
  
  Raylib.ClearBackground(Background);
  


  
  Raylib.EndDrawing();
}


static void Place()
{
  Raylib.DrawRectangle(Square.posX, Square.posY, Square.width, Square.height)

}

