using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDictionary : MonoBehaviour
{
    protected Dictionary<int, Animation> charAnimation;
    private Animation m_Anim;
    private KeyNotFoundException knfe;

    void Awake()
    {
        knfe = new KeyNotFoundException("<color=yellow>Animation Key Error.</color>");

        DontDestroyOnLoad(gameObject);
    }

    public void AddToDictionary(int id, Animation p_Anim)
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

    public Animation ReadFromDictionary(int id)
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
