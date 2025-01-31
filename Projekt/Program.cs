using Raylib_cs;

Raylib.InitWindow(1600, 800, "Geomatry dashers");
Raylib.SetTargetFPS(60);
Color Background = new(0, 225, 0, 255);
Color Red = new(255, 0, 0, 255);
int counter = 0;

List<Square> squares = [];


while (!Raylib.WindowShouldClose())
{
  Raylib.BeginDrawing();

  Raylib.ClearBackground(Background);



  

  /*if (counter == 0)
  {
    squares.Add(new() { rect = new Rectangle(-50, 7, 50, 50) });
  }

  for (int i = 0; i < squares.Count; i++)
  {
    squares[i].rect.X+=5;
    Place(squares[i]);
  }

  counter++;
  if (counter == 60)
  {
    counter = 0;
  }
  */

  Raylib.EndDrawing();
}


static void Place(Square square)
{
  Raylib.DrawRectangleRec(square.rect, Color.Red);
  Raylib.DrawRectangleLinesEx(square.rect, 5, Color.Black);
}

