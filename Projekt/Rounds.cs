using System;
using System.Collections.Generic;
using System.Data;
namespace Projekt;


public class Rounds
{
    public static List<int> TotalEnemies (List<int> SpawnAmmount, int Roundnumber)  //definerar/ändrar listan ovan
    {
        SpawnAmmount[0] = 20 + 5*Roundnumber;

        SpawnAmmount[1] = 5*Roundnumber - 10;

        SpawnAmmount[2] = 5*Roundnumber - 20;

        SpawnAmmount[3] = 5*Roundnumber - 30;

        SpawnAmmount[4] = 5*Roundnumber - 40;


        return SpawnAmmount;
    }

    public static int RoundTime (int RoundLenghtTime, int RoundNumber) // ändrar längden av rundan.
    {
        RoundLenghtTime = 600 + 120*RoundNumber; // längden av rundan ökar med 2 sekunder varje runda.

        return RoundLenghtTime;
    }

    public static int RoundClock (int RoundLenghtTime, int RoundClock)
    {
        if ()
        {
            RoundClock ++;
        }
    }

    public static void LimitSpawnEnemies (List<EnemySQ> EnemySQs, int )
    {
        PlaceEnemySQ(List<EnemySQ> EnemySQs, int counter)
    }

}
