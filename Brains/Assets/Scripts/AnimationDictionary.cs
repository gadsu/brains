using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDictionary : MonoBehaviour
{
    protected Dictionary<int, string> mCharAnimation;
    string _anim;
    string _tempAnim;
    KeyNotFoundException _knfe;

    void Awake()
    {
        mCharAnimation = new Dictionary<int, string>();
        _knfe = new KeyNotFoundException("<color=yellow>Animation Key Error</color>");
        _anim = "";
        _tempAnim = "";

        AddSpudAnimations();
        DontDestroyOnLoad(gameObject);
    }

    private void AddToDictionary(int id, string p_Anim)
    {
        try
        {
            if (!mCharAnimation.ContainsKey(id))
            {
                mCharAnimation.Add(id, p_Anim);
            }
            else
                throw _knfe;
        }
        catch (KeyNotFoundException k)
        {
            Debug.Log(k.Message + ": adding - " + p_Anim);
        }
    }

    public string ReadFromDictionary(int id)
    {
        try
        {
            if (mCharAnimation.TryGetValue(id, out _anim))
            {
                _tempAnim = _anim;
            }
            else
                throw _knfe;
        }
        catch (KeyNotFoundException k)
        {
            Debug.Log(k.Message + ": reading - " + id);
        }

        //Debug.Log("<color=orange>" + tempAnim + "</color>");
        return _tempAnim;
    }

    private void AddSpudAnimations()
    {
        /* sends in a reference for the formated key and string pair. */

        /* PlayDead */
        AddToDictionary(001, "A_SpudPlayDead"); // not moving, MvState = 0, full body, playing dea
        /************/

        /* Idle */
        AddToDictionary(000, "A_SpudIdle"); // not moving, MvState = 0, full body, not playing dead
        /********/

        /* Creep */
        AddToDictionary(200, "A_SpudCreep"); // moving, MvState = 10, full body, not playing dead
        /*********/

        /* Crawl Idle */
        AddToDictionary(050, "A_SpudCrawlIdle"); // not moving, MvState = 5, full body, not playing dead
        /**************/

        /* Crawl */
        AddToDictionary(150, "A_SpudCrawl"); // moving, MvState = 5, full body, not playing dead
        /*********/
		
        /**************************************************************/
    }
}
