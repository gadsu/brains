using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustVolume : MonoBehaviour
{
    public FloatVariable volume;

    public Slider mainSlider;
    public PersistentStateController psc;

    public void Start()
    {
        //Adds a listener to the main slider and invokes a method when the value changes.
        mainSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        psc = GameObject.Find("PersistentStateController").GetComponent<PersistentStateController>();
    }

    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()
    {
        volume.SetFloat(mainSlider.value);
        foreach (Sound s in psc.musicSounds.objectSounds)
        {
            psc.musicSounds.SetVolume(s.name, volume.GetFloat());
        }
    }
}
