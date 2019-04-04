using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustSliderValue : MonoBehaviour
{
    public FloatVariable value;

    public Slider mainSlider;
    [HideInInspector]
    public PersistentStateController psc;

    private void Awake()
    {
        mainSlider.value = value.GetFloat();
        switch (value.name)
        {
            case "Sensitivity":
                break;
            default:
                GameObject.Find("CameraContainer").GetComponent<CameraOperator>().baseSens = value.GetFloat();
                break;
        }
    }

    public void Start()
    {
        //Adds a listener to the main slider and invokes a method when the value changes.
        mainSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        psc = GameObject.Find("PersistentStateController").GetComponent<PersistentStateController>();
    }

    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()
    {
        value.SetFloat(mainSlider.value);

        switch (value.name)
        {
            case "Volume":
                foreach (Sound s in psc.musicSounds.objectSounds)
                {
                    psc.musicSounds.SetVolume(s.name, value.GetFloat());
                }
                break;
            case "Sensitivity":
                GameObject.Find("CameraContainer").GetComponent<CameraOperator>().baseSens = value.GetFloat();
                break;
            default:
                break;
        }
    }

    private void OnEnable()
    {
        mainSlider.value = value.GetFloat();
    }
}
