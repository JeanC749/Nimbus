using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public float sliderValue;
    public Image imageMute;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volumenAudio",0.5f);
        AudioListener.volume = slider.value;
        CheckVolumen();
    }

    public void CheckVolumen(){
        if(sliderValue == 0){
            imageMute.enabled = true;
        }else{
            imageMute.enabled = false;
        }
    }

    public void ChangeSlider(float value){
        sliderValue = value;
        PlayerPrefs.SetFloat("volumenAudio", sliderValue);
        AudioListener.volume = slider.value;
        CheckVolumen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
