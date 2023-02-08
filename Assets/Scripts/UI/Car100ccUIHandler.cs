using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Car100ccUIHandler : MonoBehaviour
{//All code is written By Yan Jun with reference to CarUIHandler.cs
    [Header("Car details")]
    public Image carImage;

    //Other components
    Animator animator = null;


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    //SetupCar function is used to add sprite from the carData selected
    public void SetupCar(Car100ccData carData)
    {
        carImage.sprite = carData.CarUISprite;
    }
    /*This function below start animation by animating from the right if the 
     varaible isAppearingOnRightSide is true or if different other condition
    it will animate from the left
    This function is called when player start to select their vehicle when click
    at the left of right button*/
    public void StartCarEntranceAnimation(bool isAppearingOnRightSide)
    {
        if (isAppearingOnRightSide)
            animator.Play("100cc appear from right");
        else animator.Play("100cc appear from left");
    }
    /*Similar to StartCarEntranceAnimation() function,it also animate car for the selection,
     except this animation will disappear from tne right or left depeding on the boolean of
    variable isExitingOnRightSide*/
    public void StartCarExitAnimation(bool isExitingOnRightSide)
    {
        if (isExitingOnRightSide)
            animator.Play("100cc disappear from right");
        else animator.Play("100cc disappear from left");
    }

    //Events
    public void OnCarExitAnimationCompleted()
    {
        Destroy(gameObject);
    }
}
