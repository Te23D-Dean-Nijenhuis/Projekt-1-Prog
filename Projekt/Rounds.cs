using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        RoundLenghtTime = 600 + 120*RoundNumber; // längden av rundan ökar med 2 sekunder varje runda. Första runda är 100 sek

        return RoundLenghtTime;
    }

    public static int RoundClock (int RoundLenghtTime, int RoundClock, bool AllKilled)  // en klocka som räknar tiden på rundan;
    {
        if (RoundClock < RoundLenghtTime) //räknar upp klockan så länge den är mindre än hur lång rundan ska vara.
        {
            RoundClock ++;
        } else if (RoundClock == RoundLenghtTime && AllKilled) //när klockan är slut OCH alla är dödade klocka = 0
        {
            RoundClock = 0;
        }
        return RoundClock;
    }

    public static List<float> SpawnTimer (List<float> SpawnTimers, int RoundLenghtTime, List<int> SpawnAmmount)
    {
        for (int i = 0; i < SpawnTimers.Count; i++)
        {
            SpawnTimers[i] = RoundLenghtTime/SpawnAmmount[i];
        }

        return SpawnTimers;
    }

    public static void SpawnEnemies (List<EnemySQ> EnemySQs, int counter, List<int> SpawnAmmount, bool AllKilled)
    {
        if(!AllKilled)
        {
        for (int i = 0; i < 4; i++)
        {  
            if()
        }
        }
    
    }

}
