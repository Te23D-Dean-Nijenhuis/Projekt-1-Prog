using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
namespace Projekt;


public class Rounds
{
    public static List<int> TotalEnemies (List<int> SpawnAmmount, int Roundnumber)  //definerar/ändrar listan av antal fiender som ska spawna
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

    public static List<int> SpawnClocks (List<float> SpawnTimers, List<int> SpawnTimersClocks) //kör klockorna till spawning
    {
        for (int i = 0; i < 4; i++)
        {
            SpawnTimersClocks[i] ++;
            if (SpawnTimersClocks[i] > SpawnTimers[i])
            {
                SpawnTimersClocks[i] = 0;
            }
        }
        return SpawnTimersClocks;
    }

    public static void SpawnEnemies (List<EnemySQ> EnemySQs, List<int> SpawnAmmount, bool AllKilled, List<int> SpawnTimersClocks, List<float> SpawnTimers)
    {
        if(!AllKilled)
        {
        for (int i = 0; i < 4; i++)
        {  
            if(SpawnTimersClocks[i]+ 1 >= SpawnTimers[i]) //när spawntimerclock + 1 merlikamed spawntimers så spawnas en fiende med HP = i      +1 eftersom detta gör så att en fiende hinner spawna innan klockan resetas, men intervallen blir fortfarande detsamma.
            {
                Fiende_logik.PlaceEnemySQ(EnemySQs, i);
            }
        }
        }
    
    }

}
