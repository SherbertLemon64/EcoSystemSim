using System;
using System.Collections.Generic;
using Data.Util;
using UnityEngine;

namespace UnityTemplateProjects
{
	public class LearningController : MonoBehaviour
	{
		public Vector2Int size;
		private int simultaniousWorlds;
		public int worldSeparationDistance;

		public GameObject World;

		public static GameObject[] Worlds;

		public static Movement[] Animals;

		public static List<Movement> AliveAnimalsObjects = new List<Movement>();
		
		public void Start()
		{
			simultaniousWorlds = size.x * size.y;
			
			Worlds = new GameObject[simultaniousWorlds];
			Animals = new Movement[simultaniousWorlds];

			for (int x = 0, i = 0; x < size.x; x++)
			{
				for (int y = 0; y < size.y; y++, i++)
				{
					GameObject world = Instantiate(World, new Vector3(x * worldSeparationDistance,0,y * worldSeparationDistance), Quaternion.identity);
					Worlds[i] = world;
					Movement animal = world.GetComponentInChildren<Movement>();
					Animals[i] = animal;
					AliveAnimalsObjects.Add(animal);
				}
			}
		}
		
		
		public static void ResetWorlds(Movement FinalMovement)
		{
			NeuralNetwork BaseNet = FinalMovement.nn;
			AliveAnimalsObjects.Remove(FinalMovement);
			
			foreach (Movement a in Animals)
			{
				a.ResetPosition();
				a.nn = new NeuralNetwork(BaseNet);
				a.gameObject.SetActive(true);
				AliveAnimalsObjects.Add(a);
			}
		}
		
	}
}