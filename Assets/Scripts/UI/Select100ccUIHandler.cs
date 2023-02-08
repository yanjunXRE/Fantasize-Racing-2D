using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Select100ccUIHandler : MonoBehaviour
{//All code is written By Yan Jun with reference from SelectUIHandler.cs
    [Header("Car100cc prefab")]
    public GameObject car100ccPrefab;

    [Header("Spawn on")]
    public Transform spawnOnTransform;

    bool isChangingCar = false;

    Car100ccData[] carDatas;

    int selectedCarIndex = 0;

    //Other components
    Car100ccUIHandler carUIHandler = null;

    // Start is called before the first frame update
    void Start()
    {
        //Load the Car100ccData data from the resource file
        carDatas = Resources.LoadAll<Car100ccData>("Car100ccData/");
        /*The StartCoroutine method returns upon the first yield return,
         * however you can yield the result, which waits until the coroutine
         * has finished execution where it executes the function(SpawnCarCO(true)
         */
        StartCoroutine(SpawnCarCO(true));
    }

    // Update is called once per frame
    void Update()
    {   /*if leftarrow is pressed,OnPreviousCar() function will be triggered*/
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            OnPreviousCar();
        }
        /*if rightarrow is pressed,OnNextCar() function will be triggered*/
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            OnNextCar();
        }
        /*if space is pressed,OnSelectCar() function will be triggered*/
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSelectCar();
        }
    }

    public void OnPreviousCar()
    {
        if (isChangingCar)
            return;
        /*This variable will then minus the number by1 as to instantiate object depending on the array element*/
        selectedCarIndex--;
        /*if the selectedCarIndex is less than 0,it will set the value to the biggest value depending on the number of
         element it has in the array*/
        if (selectedCarIndex < 0)
            selectedCarIndex = carDatas.Length - 1;
        /*The SpawnCarCo(true) will be called with the boolean variable isCarAppearingOnRightSide set to true,
        which mean that the animation will appear from the right side*/
        StartCoroutine(SpawnCarCO(true));
    }

    public void OnNextCar()
    {
        if (isChangingCar)
            return;
        /*This variable will then plus the number by1 as to instantiate object depending on the array element*/
        selectedCarIndex++;
        /*if the selectedCarIndex is more than 0 the number of
      element it has in the array,it will set the value to the smallest value it has in the array which is 0*/
        if (selectedCarIndex > carDatas.Length - 1)
            selectedCarIndex = 0;
        /*The SpawnCarCo(true) will be called with the boolean variable isCarAppearingOnRightSide set to false,
        which mean that the animation will appear from the left side*/
        StartCoroutine(SpawnCarCO(false));
    }

    public void OnSelectCar()
    {
        GameManager.instance.ClearDriversList();

        GameManager.instance.AddDriverToList(1, "P1", carDatas[selectedCarIndex].CarUniqueID, false);

        //Create a new list of spacecraft
        List<Car100ccData> uniqueCars = new List<Car100ccData>(carDatas);

        //Remove the spacecraft that player has selected
        uniqueCars.Remove(carDatas[selectedCarIndex]);

        string[] names = { "Freddy", "Eddy", "Teddy", "Buddy", "Luddy", "Puddy" };
        List<string> uniqueNames = names.ToList<string>();

        //AI drivers will be added 
        /*This foor lop start with variable i set to 2,the reason why i is set to 2 
        is beacuse P1 has its id set as 1.
        It will loop with the last number, 4 in varibale i
        The driverName and carData will be randomized for array number 0 to the number of element in the array it has 
        for the variable
        */
        for (int i = 2; i < 5; i++)
        {
            string driverName = uniqueNames[Random.Range(0, uniqueNames.Count)];
            uniqueNames.Remove(driverName);

            Car100ccData carData = uniqueCars[Random.Range(0, uniqueCars.Count)];

            GameManager.instance.AddDriverToList(i, driverName, carData.CarUniqueID, true);

        }

        SceneManager.LoadScene("Course100cc");
    }
    /*This IEnumerator will start to call when the player trigger OnPreviousCar() and OnNextCar()
    repespctively
     */
    IEnumerator SpawnCarCO(bool isCarAppearingOnRightSide)
    {
        isChangingCar = true;
        /*This condition check whether the carUIHandler is null,if it is not empty,it will start to exit
         animation so that new GameObject can be instantiated or the UI will be buggy as player might choose
        the wrong vehicle that they choose*/
        if (carUIHandler != null)
            carUIHandler.StartCarExitAnimation(!isCarAppearingOnRightSide);

        GameObject instantiated100ccCar = Instantiate(car100ccPrefab, spawnOnTransform);

        carUIHandler = instantiated100ccCar.GetComponent<Car100ccUIHandler>();
        carUIHandler.SetupCar(carDatas[selectedCarIndex]);
        /*When carUIHandler is instantiated,it will start to appear animation for player's vehicle that 
         they selected*/
        carUIHandler.StartCarEntranceAnimation(isCarAppearingOnRightSide);
        /*reason for yield and coroutine is because we want the animation to complete
        before spawn anohter 100cc spacecraft*/
        yield return new WaitForSeconds(0.4f);

        isChangingCar = false;
    }
}
