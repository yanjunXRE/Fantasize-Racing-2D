using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spawn100ccCar : MonoBehaviour
{
    int numberOfCarsSpawned = 0;
    // Start is called before the first frame update
    void Start()
    {////Written by Yan Jun with reference from SpawnCar.cs
        //set the gameobejct spawnpoints with the object that has the tag("SpawnPoint")
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        //Ensure that the spawn points are sorted by name
        spawnPoints = spawnPoints.ToList().OrderBy(s => s.name).ToArray();

        //Load the Car100ccData data located in the "Car100ccData/" folder
        Car100ccData[] Car100ccDatas = Resources.LoadAll<Car100ccData>("Car100ccData/");

        //Driver info where it loads data from the GameManager's driver list
        List<DriverInfo> driverInfoList = new List<DriverInfo>(GameManager.instance.GetDriverList());

        //Sort the drivers based on last position from the DriverList info
        driverInfoList = driverInfoList.OrderBy(s => s.lastRacePosition).ToList();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Transform spawnPoint = spawnPoints[i].transform;

            if (driverInfoList.Count == 0)
                return;

            DriverInfo driverInfo = driverInfoList[0];

            int selectedCarID = driverInfo.carUniqueID;

            //Find the selected 100cc car
            foreach (Car100ccData Car100ccData in Car100ccDatas)
            {
                //We found the car data for the player
                /*For each spawn point, the function instantiates a car game object with the prefab matching
                 * the car unique ID of the current driver in the DriverInfo list*/
                if (Car100ccData.CarUniqueID == selectedCarID)
                {

                    GameObject car = Instantiate(Car100ccData.CarPrefab, spawnPoint.position, spawnPoint.rotation);

                    car.name = driverInfo.name;

                    car.GetComponent<CarInputHandler>().playerNumber = driverInfo.playerNumber;
                    //The car's input handling and AI behavior is enabled or disabled based on whether the driver is an AI or a player.
                    if (driverInfo.isAI)
                    {
                        car.GetComponent<CarInputHandler>().enabled = false;
                        car.tag = "AI";
                    }
                    else
                    {
                        car.GetComponent<CarAIHandler>().enabled = false;
                        car.GetComponent<AStarLite>().enabled = false;
                        car.tag = "Player";
                    }

                    numberOfCarsSpawned++;

                    break;
                }
            }

            //Remove the spawned driver
            driverInfoList.Remove(driverInfo);
        }
    }
    //the function provides a method to return the number of cars spawned
    public int GetNumberOfCarsSpawned()
    {
        return numberOfCarsSpawned;
    }

}
