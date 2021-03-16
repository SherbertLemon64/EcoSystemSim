using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalLogic : MonoBehaviour, IAnimalLogic
{
    public AnimalStats Stats { get; set; }

    public void Create(AnimalStats _statOne, AnimalStats _statTwo)
    {
        Stats = new AnimalStats(_statOne, _statTwo);
    }
}
