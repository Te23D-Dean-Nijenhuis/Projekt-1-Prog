using System.Numerics;
using Raylib_cs;

Raylib.InitWindow(1600, 800, "Geometry dashers");
Raylib.SetTargetFPS(60);
Color Background = new(0, 185, 0, 255);
int counter = 0;
bool PreviewToggle = false;
int GridSize = 80;

Color preview = new Color(0, 0, 255, 50);

List<EnemySQ> EnemySQs = [];
List<TOWER> TOWERS = [];



while (!Raylib.WindowShouldClose())
{
  Raylib.BeginDrawing();

  Raylib.ClearBackground(Background);
  counter = Counter(counter);     //Tanke: ska alltid köras först

  PlaceEnemySQ(EnemySQs,counter);

  EnemySQs = MoveEnemySQ(EnemySQs);

  GridSize = ChangeGrid(GridSize);

  DrawEnemySQ(EnemySQs);

  GRIDTEST(GridSize);

  TOWERPLACE(TOWERS, GridSize); //placerar ut torn
  
  PreviewToggle = PREVIEW(PreviewToggle, GridSize);

  TOWERDRAW(TOWERS); // ritar torn

  Console.Clear(); //debug
  Console.WriteLine($"TORN = {TOWERS.Count}"); //debug
  Console.WriteLine($"Counter = {counter}");
  Console.WriteLine();
  Console.WriteLine($"Antal fiender = {EnemySQs.Count}");

  Console.WriteLine($"Gridsize = {GridSize}");

  







  
  
  
  Raylib.EndDrawing();
}


static void GRIDTEST(int GridSize)
{
  for (int i = 0; i < 1600; i += GridSize)
  {
    Raylib.DrawLine(i, 0, i, 800, Color.Gray);

  }
  for (int i = 0; i < 800; i += GridSize)
  {
    Raylib.DrawLine(0, i, 1600, i, Color.Gray);

  }
}

static List<TOWER> TOWERPLACE(List<TOWER> TOWERS, int GridSize)
{
  if (Raylib.IsMouseButtonPressed(MouseButton.Left))
  {
    Vector2 MouseFloat = Raylib.GetMousePosition();

    int MouseIntX = 40 + GridSize * (int)(MouseFloat.X / GridSize); //ganska klottrigt men detta avrundar så 
    int MouseIntY = 40 + GridSize * (int)(MouseFloat.Y / GridSize); //att det man endast kan placera enligt ett grid system

    TOWERS.Add(new TOWER(MouseIntX, MouseIntY, 40));
    
  }
  return TOWERS;
}

static bool PREVIEW (bool PreviewToggle, int GridSize)
{
  Color Red = new Color(255, 0, 0, 55);

  if(Raylib.IsKeyPressed(KeyboardKey.T))     //preview
  {
    PreviewToggle = !PreviewToggle;
  }
  Vector2 MouseFloat = Raylib.GetMousePosition();

  if (PreviewToggle)
  {
    int MouseIntX = 40  + GridSize * (int)(MouseFloat.X / GridSize); //ganska klottrigt men detta avrundar så 
    int MouseIntY = 40  + GridSize * (int)(MouseFloat.Y / GridSize); //att det man endast kan placera enligt ett grid system
    Raylib.DrawCircle(MouseIntX, MouseIntY, 40, Red);
  }

  return PreviewToggle;
}

static void TOWERDRAW(List<TOWER> TOWERS)
{
  for (int i = 0; i < TOWERS.Count; i++)
  {
    Raylib.DrawCircle(TOWERS[i].posX,TOWERS[i].posY,TOWERS[i].radius, Color.Black); 
    Raylib.DrawCircle(TOWERS[i].posX,TOWERS[i].posY,TOWERS[i].radius - 5, Color.Blue); 
  }
}

static int Counter(int counter)
{
  counter++;
  if (counter == 60)
  {
    counter = 0;
  }
  return counter;
}

static List<EnemySQ> PlaceEnemySQ (List<EnemySQ> EnemySQs, int counter)
{
  if (counter == 0)
  {
    EnemySQs.Add(new() {rect = new Rectangle(-60, 90, 60, 60)} );
  }


  return EnemySQs;
}

static List<EnemySQ> MoveEnemySQ (List<EnemySQ> EnemySQs)
{
  for (int i = 0; i < EnemySQs.Count; i++)
  {
    EnemySQs[i].rect.X ++;
  }
  return EnemySQs;
}

static void DrawEnemySQ (List<EnemySQ> EnemySQs)
{
  for (int i = 0; i < EnemySQs.Count; i++)
  {
    Raylib.DrawRectangleRec(EnemySQs[i].rect, Color.Red);
    Raylib.DrawRectangleLinesEx(EnemySQs[i].rect, 5, Color.Black);
  }
}

static int ChangeGrid (int GridSize)
{
  if (Raylib.IsKeyPressed(KeyboardKey.Minus) && GridSize > 20)
  {
    GridSize /= 2;
  }
  if (Raylib.IsKeyPressed(KeyboardKey.Equal) && GridSize < 80) // tangenten equal på engelsk keyboard är plus på svenskt.
  {
    GridSize *= 2;
  }
  return GridSize;
}