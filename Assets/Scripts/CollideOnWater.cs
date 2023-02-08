using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideOnWater : MonoBehaviour
//Everything Written By Yan Jun with help from GPT
{
    public float rotateSpeed = 300f;

    //Check whether this object car triggers the lap2obstacles
    private bool isTriggered;
    //Reference from class pointUI
    PointUI pointUI;
    void OnTriggerEnter2D(Collider2D other)
    {/*if triggered,it will rotate the car for 1.2 seconds
    Lap2Obstacles refer to water obstacle used for lap2 and lap3 
    if has tag "lap2obstacles" is tirggered,it will start triggering and 
    invoke "StopRotation" after 1.2 seconds.This will mimic a car slipping
    on the road after driving over a slippery surface such as the water puddle
    */
        if (other.CompareTag("Lap2Obstacle"))
        {
            isTriggered = true;
            Invoke("StopRotation", 1.2f);

        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {/*if isTriggered is true,it will make this gameObject.eg:<Car> rotate by using<transform.Rotate> until isTriggered is set to false.
   As such,player will see that the car will rotate if is triggered
    */
        if (isTriggered)
        {
            transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);

        }
    }

    void StopRotation()
    {
        isTriggered = false;
        /*"CancelInvoke()" is a method in the Unity game engine used to cancel a previously scheduled invocation of a method.
        This is to prevent player from adding additional score to the points*/
        CancelInvoke();
        //3 points will be deducted from the player if game object has tag "Player" as penalty for causing the car to get slippery
        if (CompareTag("Player"))
        {
            pointUI = FindObjectOfType<PointUI>();
            pointUI.point -= 3;
            pointUI.SetPointText("P:" + pointUI.point.ToString());
            transform.rotation = Quaternion.identity;
        }
    }


}
