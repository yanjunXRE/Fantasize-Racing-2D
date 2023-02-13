using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetItem : MonoBehaviour
{
    //All code is written by Yan Jun
    //Refence from PointUI to add point for triggering the item
    PointUI pointUI;
    //create addItemPoint to add score to the player score
    int addItemPoint = 1;
    //create item list to spwan item randomly
    ItemData[] itemData;
    //Get the itemPrefab and the position
    [Header("Item prefab")]
    public GameObject itemObject;
    //Set randomNumber variable to randomize number from 0 to the number of item it has inside ItemData
    int randomNumber;
    //Set getItemName variable to see which which item does the player has
    public string getItemName;
    //Other components
    ItemUIHandler itemHandler = null;

    [Header("Spawn on")]
    public Transform spawnOnTransform;
    // Start is called before the first frame update
    void Start()
    {

        //Load ItemData from the resource file
        itemData = Resources.LoadAll<ItemData>("ItemData/");

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D item)
    {

        /*When this gameObject(Player) hit on object which has tag(Item),
         *it will then spawn random item into the item button and the item.gameObject will
         *be destroyed.As such,what player will see in the game is that player can see that the item will
         *be spawned into the item button for player to use it*/
        if (item.CompareTag("Item"))
        {
            /*It wil also destroy gameObject to make sure that this item is preninum and it will be destroyed
           * until player complete the next lap */
            Destroy(item.gameObject);
            /*To generate a random number from an array in C#, you can use the System.Random class along with the
             * Length property of the array to determine the range of possible values. (reference from chatGPT)*/
            System.Random random = new System.Random();
            /*the Next method is called with arguments 0 and array.Length, which generates a random number between 0
             * (inclusive) and itemData.Length (exclusive)
             * , which can then be used as an index to access a random element in the array.(reference from chatGPT)*/
            randomNumber = random.Next(0, itemData.Length);
            //Then it will invoke function "addPointsAndGetItem" where it adds point to the player score after 0.07 seconds
            Invoke("addPointsAndGetItem", 0.07f);
        }


    }
    void addPointsAndGetItem()
    {
        /*"CancelInvoke()" is a method in the Unity game engine used to cancel a previously scheduled invocation of a method.
        This is to prevent player from adding additional score to the points(reference from chatGPT)*/
        CancelInvoke();
        //We need to get ObjectOfType<PointUI>() in order to use the varibale inside the class
        pointUI = FindObjectOfType<PointUI>();
        //CompareTag('Player') need to be use to make sure that only player can add points
        if (CompareTag("Player"))
        {
            //(addItemPoint) where it is 1 point will be added for triggering item to encourage player to use items
            pointUI.point += addItemPoint;
            pointUI.SetPointText("P:" + pointUI.point.ToString());
            //Get the itemButton postion
            GameObject itemButtonPostion = GameObject.Find("Item");
            //set the spawnOnTransform to the itemButton psotion
            spawnOnTransform = itemButtonPostion.transform;
            /* This gameObject variable is created to check whether it has gameObject that has(ItemUIPrefab(Clone))
             * since there is only one object
             * This is to make sure that player can only use one time at a time and to ensure that player need to use
             * that particular item before instanitating new random item*/
            GameObject gotItem = GameObject.Find("ItemUIPrefab(Clone)");
            //Instanitate random item for player to use with variable randomNumber if the gotItem is empty
            if (gotItem == null)
            {
                
                GameObject instantiatedItem = Instantiate(itemObject, spawnOnTransform);
                itemHandler = instantiatedItem.GetComponent<ItemUIHandler>();
                itemHandler.SetupItem(itemData[randomNumber]);
            }


        }
    }

}
