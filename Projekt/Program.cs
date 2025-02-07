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

    PlaceEnemySQ(EnemySQs,counter);
  
    EnemySQs = Path( Waypoints,EnemySQs);

    EnemySQs = MoveEnemySQ(EnemySQs);

    GridSize = ChangeGrid(GridSize);

    DrawPath(Waypoints);

    GRIDTEST(GridSize);

    DrawEnemySQ(EnemySQs);


    TOWERPLACE(TOWERS, GridSize); //placerar ut torn
    
    PreviewToggle = PREVIEW(PreviewToggle, GridSize);

    TOWERDRAW(TOWERS); // ritar torn

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
        int CheckTowerX =   Math.Abs(MouseIntX - TOWERS[i].posX); //mäter distans
        int CheckTowerY =   Math.Abs(MouseIntY- TOWERS[i].posY);  //mätar distans

        if (CheckTowerX < 80 && CheckTowerY < 80)
        {
          place = false;       //gör så att tornet inte kommer placeras 
          i = TOWERS.Count;    //avslutar for loopen 
        }
      }

      if (place)
      {
        TOWERS.Add(new TOWER(MouseIntX, MouseIntY, 40));
      }

      
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
      EnemySQs[EnemySQs.Count - 1].Position = (-30,120);    // dett är koordinaterna för mitten av EnemySQ

    }


    return EnemySQs;
  }

  static List<EnemySQ> MoveEnemySQ (List<EnemySQ> EnemySQs)
  {
    for (int i = 0; i < EnemySQs.Count; i++)
    {
      EnemySQs[i].rect.X += EnemySQs[i].Directions.x;
      EnemySQs[i].Position.x += EnemySQs[i].Directions.x;


      EnemySQs[i].rect.Y += EnemySQs[i].Directions.y;
      EnemySQs[i].Position.y += EnemySQs[i].Directions.y; //samma sak fast för positionen i int eftersom jag hatar floats
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

  static List<EnemySQ> Path (List<(int, int)> Waypoints, List<EnemySQ> EnemySQs)
  {
    for (int i = 0; i < EnemySQs.Count; i++)
    {
      (int x, int y) Target = Waypoints[EnemySQs[i].Waypoint];
      EnemySQs[i].Directions = ( Math.Sign(Target.x - EnemySQs[i].Position.x), Math.Sign(Target.y -EnemySQs[i].Position.y) );
      
      if(EnemySQs[i].Directions.x == 0 && EnemySQs[i].Directions.y == 0)
      {
        EnemySQs[i].Waypoint ++;
      }
      
    }
    return EnemySQs;
  }

static void DrawPath (List<(int x, int y)> Waypoints)
{
  Raylib.DrawRectangle(0, Waypoints[0].y - 35, 875, 70, Color.DarkGray);
  Raylib.DrawRectangle(Waypoints[0].x - 35, Waypoints[0].y - 35, 70, 310, Color.DarkGray);
  Raylib.DrawRectangle(Waypoints[2].x - 35, Waypoints[2].y - 35, 710, 70, Color.DarkGray);
  Raylib.DrawRectangle(Waypoints[2].x - 35, Waypoints[2].y - 35, 70, 310, Color.DarkGray);
  Raylib.DrawRectangle(Waypoints[3].x - 35, Waypoints[3].y - 35, 1600, 70, Color.DarkGray);
}