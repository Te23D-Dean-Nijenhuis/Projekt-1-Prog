using System.Diagnostics;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Diagnostics.Tracing;
using System.Numerics;
using Raylib_cs;
using Projekt;

Raylib.InitWindow(1600, 800, "Geometry dashers");
Raylib.SetTargetFPS(60);


Color Background = new(0, 185, 0, 255);

int counter = 0;

bool PreviewToggle = false;
int GridSize = 80;

Color preview = new Color(0, 0, 255, 50);

List<EnemySQ> EnemySQs = [];
List<TOWER> TOWERS = [];

List<int> SpawnAmmount = [0,0,0,0,0]; //Hur många av varje typ av fiende som ska spawnas
int RoundLenghtTime = 600; //längden av rundan ska vara 10 sekunder från början (60 * 10) 
int Roundnumber = 0;
int RoundClock;

List<(int, int)> Waypoints = [(840, 120), (840, 360), (200, 360), (200, 600), (1600, 600)]; // waypoints som definerar pathen

List<Color> EnemyHp =
[
  new Color(255, 0, 0), // 1 hp
  new Color(0, 100, 255), // 2 hp
  new Color(0, 200, 0),  // 3 hp
  new Color(255, 255, 0), //4 hp
  new Color(255, 150, 225)  //5 hp
];

List<float> SpeedMulti = [1f, 1.5f, 2f, 2.5f, 3f]; //multi på speed baserat på vilken hp, som i Bloons TD



while (!Raylib.WindowShouldClose()) //huvud loopen för spelet
{
  Raylib.BeginDrawing();

  Raylib.ClearBackground(Background);
  SpawnAmmount = Rounds.TotalEnemies(SpawnAmmount, Roundnumber);
  counter = Counter(counter);     //Tanke: ska alltid köras först

  Fiende_logik.PlaceEnemySQ(EnemySQs, counter);

  EnemySQs = Path(Waypoints, EnemySQs);  //bestämmer för var och en fiende vilken waypoint dem ska åka mot.

  EnemySQs = Fiende_logik.MoveEnemySQ(EnemySQs, Waypoints, SpeedMulti);

  GridSize = ChangeGrid(GridSize);

  DrawPath(Waypoints);

  GRIDTEST(GridSize);

  DrawEnemySQ(EnemySQs, EnemyHp);

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

  long memoryUsage = Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024);  //stulen kod

  Console.WriteLine($"Du använder: {memoryUsage} MB minne"); // debugg för eget intresse

  if (EnemySQs.Count > 1)
  {
    Console.WriteLine($"Avstånd minus steg = {Waypoints[EnemySQs[0].Waypoint].Item1 - EnemySQs[0].Position.x - (SpeedMulti[EnemySQs[0].Hitpoints - 1] * EnemySQs[0].Directions.x)}");
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

static void DrawEnemySQ(List<EnemySQ> EnemySQs, List<Color> EnemyHp)
{
  for (int i = 0; i < EnemySQs.Count; i++)
  {
    Raylib.DrawRectangleRec(EnemySQs[i].rect, EnemyHp[EnemySQs[i].Hitpoints - 1]);
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
    (int x, int y) Target = Waypoints[EnemySQs[i].Waypoint];  // hämtar från Enemy[i] en int som letar i listan waypoints för att göra en target
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
    if ((TOWERS[i].towerCounter == 60 || TOWERS[i].attackduration > 0) && EnemySQs.Count > 0 && TOWERS[i].distance < 500)
    {

      if (TOWERS[i].attackduration == 0)  // starten på attack animationen
      {
        TOWERS[i].attackduration++;
      }

      TOWERS[i].attackduration++;

      Vector2 TowerPos = new Vector2(TOWERS[i].posX, TOWERS[i].posY);
      Vector2 EnemyPos = new Vector2(EnemySQs[TOWERS[i].target].Position.x, EnemySQs[TOWERS[i].target].Position.y);

      Raylib.DrawLineEx(TowerPos, EnemyPos, 5, Color.Blue);

      if (TOWERS[i].attackduration == 10) // avslutar attack animationen
      {
        TOWERS[i].attackduration = 0;
        TOWERS[i].towerCounter = 0;
        EnemySQs[TOWERS[i].target].Hitpoints--; ;
      }




    }
  }
  return EnemySQs;
}

static List<TOWER> TowerCounter(List<TOWER> TOWERS)
{
  for (int i = 0; i < TOWERS.Count; i++)
  {
    if (TOWERS[i].towerCounter < 60)
    {
      TOWERS[i].towerCounter++;
    }
    else if (TOWERS[i].distance < 500)
    {
      TOWERS[i].towerCounter = 0;
    }
  }

  return TOWERS;
}