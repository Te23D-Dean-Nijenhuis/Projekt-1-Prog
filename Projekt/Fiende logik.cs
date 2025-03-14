
public class Fiende_logik
{


    public static List<EnemySQ> MoveEnemySQ(List<EnemySQ> EnemySQs)
    {
        for (int i = 0; i < EnemySQs.Count; i++)
        {
            EnemySQs[i].rect.X += EnemySQs[i].Directions.x;
            EnemySQs[i].Position.x += EnemySQs[i].Directions.x;


            EnemySQs[i].rect.Y += EnemySQs[i].Directions.y;
            EnemySQs[i].Position.y += EnemySQs[i].Directions.y; //samma sak fast för positionen i int eftersom jag hatar floats

            if (EnemySQs[i].Hitpoints < 1)   
            {
                EnemySQs.RemoveAt(i);
            } 
        }
        return EnemySQs;
    }


}
