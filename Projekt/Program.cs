using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
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

List<(int, int)> Waypoints = [(840, 120), (840, 360), (200, 360), (200, 600), (1600, 600)]; // waypoints som definerar pathen



while (!Raylib.WindowShouldClose())
{
  Raylib.BeginDrawing();

  Raylib.ClearBackground(Background);
  counter = Counter(counter);     //Tanke: ska alltid köras först

  PlaceEnemySQ(EnemySQs, counter);

  EnemySQs = Path(Waypoints, EnemySQs);

  EnemySQs = Enemies.MoveEnemySQ(EnemySQs);

  GridSize = ChangeGrid(GridSize);

  DrawPath(Waypoints);

  GRIDTEST(GridSize);

  DrawEnemySQ(EnemySQs);

  TowerCounter(TOWERS);

  Targeting(TOWERS, EnemySQs);



  TOWERPLACE(TOWERS, GridSize); //placerar ut torn

  PreviewToggle = PREVIEW(PreviewToggle, GridSize);

  TOWERDRAW(TOWERS); // ritar torn

  Attack(counter, TOWERS, EnemySQs);
  Console.Clear(); //debug
  Console.WriteLine($"TORN = {TOWERS.Count}"); //debug
  Console.WriteLine($"Counter = {counter}");
  Console.WriteLine();
  Console.WriteLine($"Antal fiender = {EnemySQs.Count}");

  Console.WriteLine($"Gridsize = {GridSize}");

  if (EnemySQs.Count > 1)
  {
    Console.WriteLine($"Waypoint = {EnemySQs[0].Waypoint}");
    Console.WriteLine($"Position = {EnemySQs[0].Position}"); //också debug

  }












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
    bool place = true; // en bool som ska vara ett kriterie för att få placera ut ett torn

    Vector2 MouseFloat = Raylib.GetMousePosition();

    int MouseIntX = 40 + GridSize * (int)(MouseFloat.X / GridSize); //ganska klottrigt men detta avrundar så 
    int MouseIntY = 40 + GridSize * (int)(MouseFloat.Y / GridSize); //att det man endast kan placera enligt ett grid system

    for (int i = 0; i < TOWERS.Count; i++)
    {
      int CheckTowerX = Math.Abs(MouseIntX - TOWERS[i].posX); //mäter distans
      int CheckTowerY = Math.Abs(MouseIntY - TOWERS[i].posY);  //mätar distans

      if (CheckTowerX < 80 && CheckTowerY < 80)
      {
        place = false;       //gör så att tornet inte kommer placeras 
        i = TOWERS.Count;    //avslutar for loopen 
      }
    }
    // (840, 120), (840, 360), (200, 360), (200, 600), (1600, 600)

    if (MouseIntX <= 840 && MouseIntX >= 0 && MouseIntY == 120)
    {
      place = false;
    }
    else if (MouseIntY <= 360 && MouseIntY >= 120 && MouseIntX == 840)
    {
      place = false;
    }
    else if (MouseIntX <= 840 && MouseIntX >= 200 && MouseIntY == 360)
    {
      place = false;
    }
    else if (MouseIntY <= 600 && MouseIntY >= 360 && MouseIntX == 200)
    {
      place = false;
    }
    else if (MouseIntX <= 1600 && MouseIntX >= 200 && MouseIntY == 600)
    {
      place = false;
    }

    if (place)
    {
      TOWERS.Add(new TOWER(MouseIntX, MouseIntY, 40));
    }


  }
  return TOWERS;
}

static bool PREVIEW(bool PreviewToggle, int GridSize)
{
  Color Red = new Color(255, 0, 0, 55);

  if (Raylib.IsKeyPressed(KeyboardKey.T))     //preview
  {
    PreviewToggle = !PreviewToggle;
  }
  Vector2 MouseFloat = Raylib.GetMousePosition();

  if (PreviewToggle)
  {
    int MouseIntX = 40 + GridSize * (int)(MouseFloat.X / GridSize); //ganska klottrigt men detta avrundar så 
    int MouseIntY = 40 + GridSize * (int)(MouseFloat.Y / GridSize); //att det man endast kan placera enligt ett grid system
    Raylib.DrawCircle(MouseIntX, MouseIntY, 40, Red);
  }

  return PreviewToggle;
}

static void TOWERDRAW(List<TOWER> TOWERS)
{
  for (int i = 0; i < TOWERS.Count; i++)
  {
    Raylib.DrawCircle(TOWERS[i].posX, TOWERS[i].posY, TOWERS[i].radius, Color.Black);
    Raylib.DrawCircle(TOWERS[i].posX, TOWERS[i].posY, TOWERS[i].radius - 5, Color.Blue);
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

static List<EnemySQ> PlaceEnemySQ(List<EnemySQ> EnemySQs, int counter)
{
  if (counter == 0)
  {
    EnemySQs.Add(new() { rect = new Rectangle(-60, 90, 60, 60) });
    EnemySQs[EnemySQs.Count - 1].Position = (-30, 120);    // dett är koordinaterna för mitten av EnemySQ

  }


  return EnemySQs;
}



static void DrawEnemySQ(List<EnemySQ> EnemySQs)
{
  for (int i = 0; i < EnemySQs.Count; i++)
  {
    Raylib.DrawRectangleRec(EnemySQs[i].rect, Color.Red);
    Raylib.DrawRectangleLinesEx(EnemySQs[i].rect, 5, Color.Black);
  }
}

static int ChangeGrid(int GridSize)
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

static List<EnemySQ> Path(List<(int, int)> Waypoints, List<EnemySQ> EnemySQs)
{
  for (int i = 0; i < EnemySQs.Count; i++)
  {
    (int x, int y) Target = Waypoints[EnemySQs[i].Waypoint];
    EnemySQs[i].Directions = (Math.Sign(Target.x - EnemySQs[i].Position.x), Math.Sign(Target.y - EnemySQs[i].Position.y));

    if (EnemySQs[i].Directions.x == 0 && EnemySQs[i].Directions.y == 0)
    {
      EnemySQs[i].Waypoint++;
    }

  }
  return EnemySQs;
}

static void DrawPath(List<(int x, int y)> Waypoints)
{
  Raylib.DrawRectangle(0, Waypoints[0].y - 35, 875, 70, Color.DarkGray);
  Raylib.DrawRectangle(Waypoints[0].x - 35, Waypoints[0].y - 35, 70, 310, Color.DarkGray);
  Raylib.DrawRectangle(Waypoints[2].x - 35, Waypoints[2].y - 35, 710, 70, Color.DarkGray);
  Raylib.DrawRectangle(Waypoints[2].x - 35, Waypoints[2].y - 35, 70, 310, Color.DarkGray);
  Raylib.DrawRectangle(Waypoints[3].x - 35, Waypoints[3].y - 35, 1600, 70, Color.DarkGray);
}

static List<TOWER> Targeting(List<TOWER> TOWERS, List<EnemySQ> EnemySQs)
{
  double distanceConfirm = 0;
  for (int i = 0; i < TOWERS.Count; i++)
  {
    if (TOWERS[i].towerCounter == 60)
    {

    for (int ii = 0; ii < EnemySQs.Count; ii++)   //går igenom listan av fiender för att hitta kortaste avståndet till en fiende
    {
      double distanceTarget = Math.Sqrt(Math.Pow(TOWERS[i].posX - EnemySQs[ii].Position.x, 2) + Math.Pow(TOWERS[i].posY - EnemySQs[ii].Position.y, 2));
      if (ii == 0)
      {
        distanceConfirm = distanceTarget;
        TOWERS[i].target = ii;
        TOWERS[i].distance = distanceConfirm;
      }
      if (distanceTarget < distanceConfirm)
      {
        distanceConfirm = distanceTarget;
        TOWERS[i].target = ii;
        TOWERS[i].distance = distanceConfirm;
      }
    }
    }
  }
  return TOWERS;
}

static List<EnemySQ> Attack(int counter, List<TOWER> TOWERS, List<EnemySQ> EnemySQs)
{

    for (int i = 0; i < TOWERS.Count; i++)
    {
      if (TOWERS[i].towerCounter == 60 || TOWERS[i].attackduration > 0)
      {

        if (TOWERS[i].attackduration == 0)
        {
          TOWERS[i].attackduration ++;
        }

        TOWERS[i].attackduration ++;

        if (TOWERS[i].attackduration == 10)
        {
          TOWERS[i].attackduration = 0;
          TOWERS[i].towerCounter = 0;
          EnemySQs.RemoveAt(TOWERS[i].target);
        }

      Vector2 TowerPos = new Vector2(TOWERS[i].posX, TOWERS[i].posY);
      Vector2 EnemyPos = new Vector2(EnemySQs[TOWERS[i].target].Position.x, EnemySQs[TOWERS[i].target].Position.y);

      Raylib.DrawLineEx(TowerPos, EnemyPos, 5, Color.Blue);


      
      }
    }
    return EnemySQs;
  }
  
static List<TOWER> TowerCounter (List<TOWER> TOWERS)
{
  for (int i = 0; i < TOWERS.Count; i++)
  {
    if (TOWERS[i].towerCounter < 60)
    {
    TOWERS[i].towerCounter ++;
    } else if (TOWERS[i].distance < 300)
    {
      TOWERS[i].towerCounter = 0;
    }
  }

 return TOWERS;
}


