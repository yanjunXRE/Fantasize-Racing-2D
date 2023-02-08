using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//All code is written by Yan Jun with reference to the CarData.cs
/*The class has public properties that return the values of the attributes. The class also has the [CreateAssetMenu] attribute, 
 * which allows creating instances of the class as assets in the Unity Editor by clicking Assets > Create > Item Data.*/
[CreateAssetMenu(fileName = "New Item Data", menuName = "Item Data", order = 51)]
/*The class defintion ItemData is a scriptable object for game development
 *The class extends the ScriptableObject class, allowing instances of the class to be saved as assets in the project.*/
public class ItemData : ScriptableObject
{
    [SerializeField]
    private int itemUniqueID = 0;

    [SerializeField]
    private Sprite itemUISprite;

    [SerializeField]
    private GameObject itemPrefab;

    public int ItemUniqueID
    {
        //An integer value that represents a unique identifier for the item
        get { return itemUniqueID; }
    }
    public Sprite ItemUISprite
    {//A Sprite object that represents the car's image in the game's UI.
        get { return itemUISprite; }
    }
    public GameObject ItemPrefab
    {//A GameObject that represents the car's prefab in the game scene.
        get { return itemPrefab; }
    }

}
