using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{

    public Slider slider;
    public Text displayText;



    public void setSlider(float a)
    {
        slider.value = a;
    }

        // Start is called before the first frame update
    void Start()
    {
        displayText.text = "Hunger";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
