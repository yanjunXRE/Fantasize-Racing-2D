using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CarGrading : MonoBehaviour
{
    //All code is written by YanJun
    //Declare the text from this gaming object(Header Text)
    Text gradingText;
    //Declare grade to display what grade they have depending on the point they get
    public string grade;
    // Start is called before the first frame update
    private void Awake()
    {
        gradingText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetGradingText(string text)
    {
        gradingText.text = text;
    }
}
