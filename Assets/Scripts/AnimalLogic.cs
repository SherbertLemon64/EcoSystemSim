using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalLogic : MonoBehaviour, IAnimalLogic
{
    public float Energy;
    public AnimalStats Stats { get; set; }

    public Movement MoveScript;

    public NeuralNetwork nn;

    public void Start()
    {
        MoveScript = gameObject.GetComponent<Movement>();

        nn = new NeuralNetwork(new[] {12, 5, 2});
    }

    public void Create(AnimalStats _statOne, AnimalStats _statTwo)
    {
        Stats = new AnimalStats(_statOne, _statTwo);
    }

    public void Update()
    {
        Energy -= Stats.EnergyDraw * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision.collider.gameObject.name == "Walls")
        {
            TurnOffGameObject();
        } else if (_collision.collider.gameObject.layer == 8)
        {
            Energy += 20;
        }

        if (Energy <= 0)
        {
            TurnOffGameObject();
        }
    }

    private void TurnOffGameObject()
    {
        gameObject.SetActive(false);
        if (LearningController.AliveAnimalsObjects.Count == 1)
        {
            LearningController.ResetWorlds(MoveScript);
        }
        else
        {
            LearningController.AliveAnimalsObjects.Remove(MoveScript);
        }
    }

}
