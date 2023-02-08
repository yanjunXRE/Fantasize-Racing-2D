using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//All code is written by Yan Jun with reference to the CarData.cs

/*The class has public properties that return the values of the attributes. The class also has the [CreateAssetMenu] attribute, 
 * which allows creating instances of the class as assets in the Unity Editor by clicking Assets > Create > Car100cc Data.*/
[CreateAssetMenu(fileName = "New Car100cc Data", menuName = "Car100cc Data", order = 51)]
/*The class defintion Car100cData is a scriptable object for game development
 The class extends the ScriptableObject class, allowing instances of the class to be saved as assets in the project.*/
public class Car100ccData : ScriptableObject
{
    [SerializeField]
    private int carUniqueID = 0;

    [SerializeField]
    private Sprite carUISprite;

    [SerializeField]
    private GameObject carPrefab;

    public int CarUniqueID
    {//An integer value that represents a unique identifier for the car
        get { return carUniqueID; }
    }
    public Sprite CarUISprite
    {//A Sprite object that represents the car's image in the game's UI.
        get { return carUISprite; }
    }
    public GameObject CarPrefab
    {//A GameObject that represents the car's prefab in the game scene.
        get { return carPrefab; }
    }

}
