using System;
using UnityEngine;
using UnityEngine.UI;

public class TextSetter : MonoBehaviour
{
    [SerializeField]
    public string nameOfGameObject = "";
    private GameObject text;
    private Text wordTarget;
    private Color defaultColor;
    private int defaultSize;

    private void Awake()
    {
        text = GameObject.Find(nameOfGameObject);
        if(!text) { throw new Exception("No text with name \""+nameOfGameObject+"\" found. Check spelling, and make sure the object is visible."); }
        wordTarget = text.GetComponent<Text>();
        defaultSize = wordTarget.fontSize;
        defaultColor = wordTarget.color;//TODO This is assigned to 0,0,0,0 for some reason.
    }

    public void Show(bool show)
    {
        text.SetActive(show);
    }

    // @Paul: i wanted optional args, and... uh... this worked, i guess?
    public void Set(string words, [System.Runtime.InteropServices.Optional] Color color, [System.Runtime.InteropServices.Optional] int size)
    {
        if(color.a != 0f) {wordTarget.color = color;} else { wordTarget.color = defaultColor; }
        //if(size != null && size > 1) { wordTarget.fontSize = size; } else { wordTarget.fontSize = defaultSize; }
        wordTarget.text = words;
    }

}
