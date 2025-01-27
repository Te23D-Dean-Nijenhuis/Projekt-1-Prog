using Raylib_cs;

Raylib.InitWindow(1600, 800, "Geomatry dashers");
Raylib.SetTargetFPS(60);
Color Background = new(0,225,0,255);
Color Red = new(255,0,0,255);
int counter = 0;

List<Square> squares = [];


while (!Raylib.WindowShouldClose())
{
  Raylib.BeginDrawing();
  
  Raylib.ClearBackground(Background);

  if(counter == 0)
  {
  squares.Add(new() {posX = -50, posY = 7, width = 50, height = 50});
  }

  for (int i = 0; i < squares.Count; i++)
  {
  squares[i].posX ++;
  Place(squares[i]); 
  }

  counter++;
  if(counter == 60)
  {
    counter = 0;
  }


  

  
  Raylib.EndDrawing();
}


static void Place(Square square)
{
  Raylib.DrawRectangle(square.posX, square.posY, square.width, square.height, Color.Red);
  Raylib.DrawRectangleLinesEx(new Rectangle(square.posX, square.posY, square.width, square.height), 5, Color.Black);
}

