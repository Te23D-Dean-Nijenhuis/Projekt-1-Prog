using System.Data;
using Raylib_cs;
public class Fiende_logik
{
    public static List<EnemySQ> MoveEnemySQ(List<EnemySQ> EnemySQs, List<(int, int)> Waypoints, List<float> SpeedMulti)
    {
        for (int i = 0; i < EnemySQs.Count; i++)
        {
            if (Math.Abs(Waypoints[EnemySQs[i].Waypoint].Item1 - EnemySQs[i].Position.x) - Math.Abs(SpeedMulti[EnemySQs[i].Hitpoints - 1] * EnemySQs[i].Directions.x) < 0 || Math.Abs(Waypoints[EnemySQs[i].Waypoint].Item2 - EnemySQs[i].Position.y) - Math.Abs(SpeedMulti[EnemySQs[i].Hitpoints - 1] * EnemySQs[i].Directions.y) < 0) // om avståndet är till nästa waypoint är kortare än ett steg
            {  
                EnemySQs[i].Position = Waypoints[EnemySQs[i].Waypoint]; // sätter positionen lika waypointen istället.
                EnemySQs[i].rect.X = Waypoints[EnemySQs[i].Waypoint].Item1 - 30;
                EnemySQs[i].rect.Y = Waypoints[EnemySQs[i].Waypoint].Item2 - 30;

                EnemySQs[i].Waypoint ++; //sätter till nästa waypoint.

            } else // den flyttar bara ifall det tidigare inte har inträffat.
            {
            EnemySQs[i].rect.X += SpeedMulti[EnemySQs[i].Hitpoints -1] * EnemySQs[i].Directions.x;
            EnemySQs[i].Position.x += SpeedMulti[EnemySQs[i].Hitpoints -1] * EnemySQs[i].Directions.x;  //samma som kommentaren nedan.

            EnemySQs[i].rect.Y += SpeedMulti[EnemySQs[i].Hitpoints -1] * EnemySQs[i].Directions.y;
            EnemySQs[i].Position.y += SpeedMulti[EnemySQs[i].Hitpoints -1] * EnemySQs[i].Directions.y;  //positionen är skillt från startpunkten av varje kvadrat för detta gör matten mycket enklare för mig eftesom det personligen är lättare att utgå ifrån mitten av kvadraten.
            }

            if (EnemySQs[i].Hitpoints < 1)
            {
                EnemySQs.RemoveAt(i);
            }
        }
        return EnemySQs;
    }
    public static List<EnemySQ> PlaceEnemySQ(List<EnemySQ> EnemySQs, int counter)
    {
        if (counter == 0)
        {
            EnemySQs.Add(new() { rect = new Rectangle(-60, 90, 60, 60), Hitpoints = 5});
            EnemySQs[EnemySQs.Count - 1].Position = (-30, 120);    // dett är koordinaterna för mitten av EnemySQ

        }


        return EnemySQs;
    }
    

}
