using System;
using System.Collections.Generic;
namespace Projekt;


public class Rounds
{
    public List<int> SpawnAmmount = [0,0,0,0,0]; //Hur många av varje typ av fiende som ska spawnas
    public static List<int> TotalEnemies (List<int> SpawnAmmount, int Roundnumber)  //definerar/ändrar listan ovan
    {
        SpawnAmmount[0] = 20 + 5*Roundnumber;

        SpawnAmmount[1] = 5*Roundnumber - 10;

        SpawnAmmount[2] = 5*Roundnumber - 20;

        SpawnAmmount[3] = 5*Roundnumber - 30;

        SpawnAmmount[4] = 5*Roundnumber - 40;


        return SpawnAmmount;
    }
    public int RoundLenghtTime = 600; //längden av rundan ska vara 10 sekunder från början (60 * 10)
    public static int RoundTime (int RoundLenghtTime, int RoundNumber) // ändrar längden av rundan.
    {
        RoundLenghtTime = 600 + 120*RoundNumber; // längden av rundan ökar med 2 sekunder varje runda.

        return RoundLenghtTime;
    }
/*
    public static int RunRound (List<EnemySQ> EnemySQs, int RoundLenghtTime)
    {
        Fiende_logik.MoveEnemySQ(List<EnemySQ> EnemySQs);


        return RoundLenghtTime; //TEMP
    } 
    */
}
