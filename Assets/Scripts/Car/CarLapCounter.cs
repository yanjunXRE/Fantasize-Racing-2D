using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CarLapCounter : MonoBehaviour
{

    public Text carPositionText;

    int passedCheckPointNumber = 0;//indiciate the checkpointNumber to ensure that the checkpoint is in order
    float timeAtLastPassedCheckPoint = 0;

    int numberOfPassedCheckpoints = 0;

    int lapsCompleted = 0;//this variable lapsCompleted shows how many laps has the player complete

    const int lapsToComplete = 3;/*Total amount of lap that player need to complete.For example,if
     lapToComplete is set to 3,it mean that player need to complete 3 laps*/

    public bool isRaceCompleted = false;//It is to show whether the race is completed 

    int carPosition = 0;//indicate car postiion

    bool isHideRoutineRunning = false;
    float hideUIDelayTime;

    //Other components
    LapCounterUIHandler lapCounterUIHandler;
    /*Line 32 to 49 is written by Yan Jun
    Get PointUI class*/
    PointUI pointUI;
    //Get CarGrading class
    CarGrading carGrading;
    //recreated a new lap1Obstacle for lap3 (lap1Obstacles is made up of barrel)
    public GameObject createLap1Obstacles;
    //Create a new lap2Obstacle for lap2 and lap3(lap2Obstacles is made up of water)*/
    public GameObject createLap2Obstacles;
    //Create a batch of item that will be used for the lap*/
    public GameObject createItem;
    //to spawn location of the prefab
    Vector3 spawnPosition;
    //to set the rotation of the prefab
    Quaternion spawnRotation;
    //to create new object of the prefab
    GameObject newObject;
    //object that will destroyed
    string[] destroyObjectLap1;
    //Events
    public event Action<CarLapCounter> OnPassCheckpoint;

    void Start()
    {
        if (CompareTag("Player"))
        {
            lapCounterUIHandler = FindObjectOfType<LapCounterUIHandler>();
            lapCounterUIHandler.SetLapText($"LAP {lapsCompleted + 1}/{lapsToComplete}");
            /*Line62 to 63 is written By Yan Jun
             * As seen from the line below,it will set the text of point UI to (P:0) by findingObjectType<PointUI>
             * if point is set to 0*/
            pointUI = FindObjectOfType<PointUI>();
            pointUI.SetPointText("P:" + pointUI.point.ToString());
        }
    }

    public void SetCarPosition(int position)
    {
        carPosition = position;

    }

    public int GetNumberOfCheckpointsPassed()
    {
        return numberOfPassedCheckpoints;
    }
    public float GetTimeAtLastCheckPoint()
    {
        return timeAtLastPassedCheckPoint;
    }

    public bool IsRaceCompleted()
    {
        return isRaceCompleted;
    }

    IEnumerator ShowPositionCO(float delayUntilHidePosition)
    {
        hideUIDelayTime += delayUntilHidePosition;

        carPositionText.text = carPosition.ToString();
        //Line 99 to 118 is written by Yan Jun
        /*This condition is to add point depending on which psotion you finish it in
        1st place =10 points
        2nd place =5 points
        3rd place =-2 points 
        4th place =-5 points 
        */
        if (CompareTag("Player"))
        {
            if (carPositionText.text == "1")
            {
                pointUI.point += 10;
            }
            else if (carPositionText.text == "2")
            {
                pointUI.point += 5;
            }
            else if (carPositionText.text == "3")
            {
                pointUI.point -= 2;
            }
            else
            {
                pointUI.point -= 5;
            }
            pointUI.SetPointText("P:" + pointUI.point.ToString());
        }
        carPositionText.gameObject.SetActive(true);

        if (!isHideRoutineRunning)
        {
            isHideRoutineRunning = true;

            yield return new WaitForSeconds(hideUIDelayTime);
            carPositionText.gameObject.SetActive(false);

            isHideRoutineRunning = false;
        }

    }


    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("CheckPoint"))
        {
            //Once a car has completed the race we don't need to check any checkpoints or laps. 
            if (isRaceCompleted)
                return;

            CheckPoint checkPoint = collider2D.GetComponent<CheckPoint>();

            //Make sure that the car is passing the checkpoints in the correct order. The correct checkpoint must have exactly 1 higher value than the passed checkpoint
            if (passedCheckPointNumber + 1 == checkPoint.checkPointNumber)
            {
                passedCheckPointNumber = checkPoint.checkPointNumber;

                numberOfPassedCheckpoints++;

                //Store the time at the checkpoint
                timeAtLastPassedCheckPoint = Time.time;

                if (checkPoint.isFinishLine)
                {
                    passedCheckPointNumber = 0;
                    lapsCompleted++;


                    //Line 166 to 206 is written by Yan Jun
                    /* The variable(destroyObjects) is the type of the elements in the array(destroyObjectLap1).
                     * The type of element is automatically inferred based on the type of the elements in the array.
                     * In each iteration of the loop, the element variable takes on the value of the next element in the array.
                     * The purpose of iteration is to destroy every object in the array that is used in lap1 so that we can 
                     * easiy show item that is used in lap2 or lap3*/
                    string[]destroyObjectLap1 = new string[] { "ItemBoxLap1(50cc)", "Lap1Obstacle" };
                    foreach (var destroyObjects in destroyObjectLap1)
                    {
                        GameObject objectDestroyed = GameObject.Find(destroyObjects);
                        Destroy(objectDestroyed);

                    }

                    //It is assumed that these two game objects represent different obstacles in a racing game
                    if (createLap1Obstacles != null && createLap2Obstacles != null )
                    {
                        if (lapsCompleted == 2)
                        {/*if player has completed 2 laps,createLap1Obstacles will be instantiated.
                         * from the player perspective, player will see obstacles that used to appear in
                         * lap1 will be seen again at lap3*/
                            spawnPosition = new Vector3(0, 0, 0);
                            spawnRotation = Quaternion.identity;
                            newObject = Instantiate(createLap1Obstacles, spawnPosition, spawnRotation);
                        }
                        else
                        {

                            /*if player did complete 2 laps,thenit will call the else statement,
                             * obstacle inside lap2Obstacle will instantiated for the second
                             * and third laps,which mean that player will have to go through lap2Obstacles*/
                            spawnPosition = new Vector3(0, 0, 0);
                            spawnRotation = Quaternion.identity;
                            newObject = Instantiate(createLap2Obstacles, spawnPosition, spawnRotation);
                        }
                       
                    }
                    if (createItem != null)
                    {
                        /*Then it will also instantiate object which as createItem to refresh every item for 
                        * the particular lap.This is to ensure that every player got a chance to use item to 
                        * race interesting 
                        */
                        spawnPosition = new Vector3(0, 0, 0);
                        spawnRotation = Quaternion.identity;
                        newObject = Instantiate(createItem, spawnPosition, spawnRotation);
                    }
                    //to check whether the lap is completed
                    if (lapsCompleted >= lapsToComplete)
                        isRaceCompleted = true;
                    //to update the lap they should be going through

                    if (!isRaceCompleted && lapCounterUIHandler != null)
                        lapCounterUIHandler.SetLapText($"LAP {lapsCompleted + 1}/{lapsToComplete}");


                }


                //Invoke the passed checkpoint event
                OnPassCheckpoint?.Invoke(this);

                //Now show the cars position as it has been calculated but only do it when a car passes through the finish line
                if (isRaceCompleted)
                {
                    StartCoroutine(ShowPositionCO(100));

                    if (CompareTag("Player"))
                    {
                        GameManager.instance.OnRaceCompleted();

                        GetComponent<CarInputHandler>().enabled = false;
                        GetComponent<CarAIHandler>().enabled = true;
                        GetComponent<AStarLite>().enabled = true;
                        /*this object is declared to show player that they are controlled by ai
                        after finishing the race*/
                        //Line 237 to 264 is written by Yan Jun
                        gameObject.tag = "AI";

                        //carGrading class is declared by finding objectofType<CarGrading>

                        carGrading = FindObjectOfType<CarGrading>();
                        /*Player will then be accessed on their skill on racing
                         *As such, if player point is more than 34 inclusive,player will earn grade"Excellent"*/
                        if (pointUI.point >= 34)
                        {
                            carGrading.grade = "Excellent";
                        }
                        //If player point is between 22 inclusive and 34,player will earn grade"Good"
                        else if (pointUI.point >= 22 && pointUI.point < 34)
                        {
                            carGrading.grade = "Good";
                        }
                        //If player point is between 12 inclusive and 22,player will earn grade"Pass"
                        else if (pointUI.point >= 12 && pointUI.point < 22)
                        {
                            carGrading.grade = "Pass";
                        }
                        //If player score anything below 11,player will lose the race
                        else
                        {
                            carGrading.grade = "Fail";
                        }
                        carGrading.SetGradingText(carGrading.grade);
                    }
                }
                else if (checkPoint.isFinishLine) StartCoroutine(ShowPositionCO(1.5f));
            }
        }
    }
}
