using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDictionary : MonoBehaviour
{
    protected Dictionary<int, string> charAnimation;
    private string m_Anim;
    private KeyNotFoundException knfe;

    void Awake()
    {
        charAnimation = new Dictionary<int, string>();
        knfe = new KeyNotFoundException("<color=yellow>Animation Key Error.</color>");
        m_Anim = "";

        DontDestroyOnLoad(gameObject);
    }

    public void AddToDictionary(int id, string p_Anim)
    {
        try
        {
            if (!charAnimation.TryGetValue(id, out m_Anim))
            {
                charAnimation.Add(id, m_Anim);
            }
            else
                throw knfe;
        }
        catch (KeyNotFoundException k)
        {
            Debug.Log(k.Message);
        }
    }

    public string ReadFromDictionary(int id)
    {
        try
        {
            if (!charAnimation.TryGetValue(id, out m_Anim))
                throw knfe;
        }
        catch (KeyNotFoundException k)
        {
            Debug.Log(k.Message);
        }
        return m_Anim;
    }
}
