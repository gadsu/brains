using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDictionary : MonoBehaviour
{
    protected Dictionary<int, string> charAnimation;
    private string m_Anim;
    private string tempAnim;
    private KeyNotFoundException knfe;

    void Awake()
    {
        charAnimation = new Dictionary<int, string>();
        knfe = new KeyNotFoundException("<color=yellow>Animation Key Error</color>");
        m_Anim = "";
        tempAnim = "";

        DontDestroyOnLoad(gameObject);
    }

    public void AddToDictionary(int id, string p_Anim)
    {
        try
        {
            if (!charAnimation.ContainsKey(id))
            {
                charAnimation.Add(id, p_Anim);
            }
            else
                throw knfe;
        }
        catch (KeyNotFoundException k)
        {
            Debug.Log(k.Message + ": adding - " + p_Anim);
        }
    }

    public string ReadFromDictionary(int id)
    {
        //Debug.Log("<color=red>Attempting to read from dictionary</color>");
        try
        {
            if (charAnimation.TryGetValue(id, out m_Anim))
            {
                //Debug.Log("<color=blue>" + m_Anim.ToString() + "</color>");
                tempAnim = m_Anim;
            }
            else
                throw knfe;
        }
        catch (KeyNotFoundException k)
        {
            Debug.Log(k.Message + ": reading - " + id);
        }

        //Debug.Log("<color=orange>" + tempAnim + "</color>");
        return tempAnim;
    }
}
