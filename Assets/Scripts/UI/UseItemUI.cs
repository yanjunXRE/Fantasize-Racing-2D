using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItemUI : MonoBehaviour
{
    //All code is written by Yan Jun
    //Get the item from the ItemData array where it has a list of item functionality
    public ItemData item;
    //Declare ItemUIHandler to check whether the item is available from the item Ui at the bottom right corner
    ItemUIHandler itemUIHandler;
    //PointUI is a=used to add and store point to player
    PointUI pointUI;
    //This gameObject is declared to make sure that only player can use the item
    GameObject car;
    //This gameObject is declared to ensure that the item is used
    GameObject ItemUsed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //Press spacebar to use item by using function(UseItem())
        if ((Input.GetKeyDown(KeyCode.Space)))
        {
            //Get the carButton position
            car = GameObject.Find("P1");
            if (car.CompareTag("Player"))
            {
                //Get the FindObjectOfType which has class ItemUIHandler to reference the variable that it has from ItemUIHandler class
                itemUIHandler = FindObjectOfType<ItemUIHandler>();
                //sometimes it will have error if itemUIHandler is not found,so it prevent that from happening,we add tenary condtion to check whether itemUIHandler is found
                if (itemUIHandler != null)
                    item = itemUIHandler.GetItem;
                //Then it will call out the function use item if there is item available
                if (item != null)
                    //Then it will invoke function " UseItem" where it adds point to the player score after 0.2 seconds
                    Invoke("UseItem", 0.07f);
            }
        }


    }
    //This function UseItem() is defined for player to use the item
    void UseItem()
    {
        /*"CancelInvoke()" is a method in the Unity game engine used to cancel a previously scheduled invocation of a method.
        This is to prevent player from adding additional score to the points*/
        CancelInvoke();
        //Find the gameObject(P1) to get the carPosition to spawn item for other item than 'Mushroom Bootser'
        car = GameObject.Find("P1");
        //Find the gameObject that it has the name ItemUIPrefab 
        ItemUsed = GameObject.Find("ItemUIPrefab(Clone)");
        pointUI = FindObjectOfType<PointUI>();
        //Check if there is item(Mushroom Booster)
        if (item.name == "Mushroom Booster")
        {
            //This mushroom booster helps to boost extra points to your score by 2
            pointUI.point += 2;
        }
        //This else if statement will be called if it is item(King)
        else if (item.name == "King")
        {
            //King helps to boost extra points to your score by 5 since it is the god helping you
            pointUI.point += 5;
        }
        //else statement will be only called if neither of the if condition above is called which only left the item(Shell)
        else
        {
            //However,if red shell is used which depicts caution,it will deduct 2 points as shell is used as curse item
            pointUI.point -= 2;
        }
        pointUI.SetPointText("P:" + pointUI.point.ToString());


        //Item will be destroyed to show that the item have been used
        Destroy(ItemUsed);
        //Set item to null to show that it is empty after being used
        item = null;
    }
}
