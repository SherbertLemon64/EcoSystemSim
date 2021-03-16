using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AnimalStats
{
    public readonly float EnergyDrawMultiplier;
    public readonly byte EnergyDraw;
    public readonly byte Speed;
    public readonly byte AttackPower;
    public readonly byte Vision;
    public readonly string Species;

    public AnimalStats(AnimalStats statOne, AnimalStats statTwo)
    {
        EnergyDrawMultiplier = statOne.EnergyDrawMultiplier;
        Species = statOne.Species;

        EnergyDraw = Random.Range(1, 0) == 1 ? statOne.EnergyDraw : statTwo.EnergyDraw;
        Speed = Random.Range(1, 0) == 1 ? statOne.Speed : statTwo.Speed;
        AttackPower = Random.Range(1, 0) == 1 ? statOne.AttackPower : statTwo.AttackPower;
        Vision = Random.Range(1, 0) == 1 ? statOne.Vision : statTwo.Vision;
    }
}
