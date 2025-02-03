using System.Numerics;
using Raylib_cs;

Raylib.InitWindow(1600, 800, "Geometry dashers");
Raylib.SetTargetFPS(60);
Color Background = new(0, 225, 0, 255);
int counter = 0;
bool yes = false;

Color preview = new Color(0, 0, 255, 50);

List<Square> squares = [];
List<TOWER> TOWERS = [];



while (!Raylib.WindowShouldClose())
{
  Raylib.BeginDrawing();

  Raylib.ClearBackground(Background);

  GRIDTEST();

  TOWERPLACE(TOWERS); //placerar ut torn
  yes = PREVIEW(yes);

  TOWERDRAW(TOWERS); // ritar torn

  Console.Clear(); //debug
  Console.WriteLine(TOWERS.Count); //debug
  Console.WriteLine(yes);

  







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

static void GRIDTEST()
{
  for (int i = 0; i < 1600; i += 80)
  {
    Raylib.DrawLine(i, 0, i, 800, Color.Gray);

  }
  for (int i = 0; i < 800; i += 80)
  {
    Raylib.DrawLine(0, i, 1600, i, Color.Gray);

  }
}

static List<TOWER> TOWERPLACE(List<TOWER> TOWERS)
{
  if (Raylib.IsMouseButtonPressed(MouseButton.Left))
  {
    Vector2 MouseFloat = Raylib.GetMousePosition();

    int MouseIntX = 40 + 80 * (int)(Math.Floor(MouseFloat.X / 80)); //ganska klottrigt men detta avrundar så 
    int MouseIntY = 40 + 80 * (int)(Math.Floor(MouseFloat.Y / 80)); //att det man endast kan placera enligt ett grid system

    TOWERS.Add(new TOWER(MouseIntX, MouseIntY, 40));
    
  }
  return TOWERS;
}

static bool PREVIEW (bool yes)
{
  Color Red = new Color(255, 0, 0, 55);
  
  if(Raylib.IsKeyPressed(KeyboardKey.T))     //preview
  {
    yes = !yes;
  }
  Vector2 MouseFloat = Raylib.GetMousePosition();

  if (yes)
  {
    int MouseIntX = 40 + 80 * (int)(Math.Floor(MouseFloat.X / 80)); //ganska klottrigt men detta avrundar så 
    int MouseIntY = 40 + 80 * (int)(Math.Floor(MouseFloat.Y / 80)); //att det man endast kan placera enligt ett grid system
    Raylib.DrawCircle(MouseIntX, MouseIntY, 40, Red);
  }

  return yes;
}

static void TOWERDRAW(List<TOWER> TOWERS)
{
  for (int i = 0; i < TOWERS.Count; i++)
  {
    Raylib.DrawCircle(TOWERS[i].posX,TOWERS[i].posY,TOWERS[i].radius, Color.Black); 
    Raylib.DrawCircle(TOWERS[i].posX,TOWERS[i].posY,TOWERS[i].radius - 5, Color.Blue); 
  }
}