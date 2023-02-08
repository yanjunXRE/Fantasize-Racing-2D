using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PointUI : MonoBehaviour
{//All code is written by Yan Jun
    Text pointText;
    //point variable is created to store point given to player
    public int point;
    private void Awake()
    {
        pointText = GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    /*When the race start, the variable
    point will set to 0.As seen from player perspective,
    player will see 'P:0' as a reference of the point given*/
    {
        point = 0;

    }


    public void SetPointText(string text)
    {
        pointText.text = text;
    }


}
