﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDictionary : MonoBehaviour
{
    protected Dictionary<int, string> charAnimation;
    string m_Anim;
    string tempAnim;
    KeyNotFoundException knfe;

    void Awake()
    {
        charAnimation = new Dictionary<int, string>();
        knfe = new KeyNotFoundException("<color=yellow>Animation Key Error</color>");
        m_Anim = "";
        tempAnim = "";

        AddSpudAnimations();
        DontDestroyOnLoad(gameObject);
    }

    private void AddToDictionary(int id, string p_Anim)
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

    private void AddSpudAnimations()
    {
        /* sends in a reference for the formated key and string pair. */

        /* PlayDead */
        AddToDictionary(00221, "A_SpudPlayDead"); // not moving, MvState = 0, full body, playing dead
        AddToDictionary(00211, "A_SpudPlayDead"); // -1 leg
        AddToDictionary(00201, "A_SpudPlayDead"); // 0 legs
        AddToDictionary(00121, "A_SpudPlayDead"); // -1 arm
        AddToDictionary(00111, "A_SpudPlayDead"); // -1 arm, -1 leg
        AddToDictionary(00101, "A_SpudPlayDead"); // -1 arm, 0 legs
        /************/

        /* Idle */
        AddToDictionary(00220, "A_SpudIdle"); // not moving, MvState = 0, full body, not playing dead
        AddToDictionary(00210, "A_SpudIdle"); // -1 leg
        AddToDictionary(00120, "A_SpudIdle"); // -1 arm
        AddToDictionary(00110, "A_SpudIdle"); // -1 arm, -1 leg
        /********/

        /* Creep */
        AddToDictionary(20220, "A_SpudCreep"); // moving, MvState = 10, full body, not playing dead
        AddToDictionary(20210, "A_SpudCreep"); // -1 leg
        AddToDictionary(20120, "A_SpudCreep"); // -1 arm
        AddToDictionary(20110, "A_SpudCreep"); // -1 arm, -1 leg
        /*********/

        /* Crawl Idle */
        AddToDictionary(05220, "A_SpudCrawlIdle"); // not moving, MvState = 5, full body, not playing dead
        AddToDictionary(05210, "A_SpudCrawlIdle"); // -1 leg
        AddToDictionary(05200, "A_SpudCrawlIdle"); // 0 legs
        AddToDictionary(05120, "A_SpudCrawlIdle"); // -1 arm
        AddToDictionary(05110, "A_SpudCrawlIdle"); // -1 arm, -1 leg
        AddToDictionary(05100, "A_SpudCrawlIdle"); // -1 arm, 0 legs
        /**************/

        /* Crawl */
        AddToDictionary(15220, "A_SpudCrawl"); // moving, MvState = 5, full body, not playing dead
        AddToDictionary(15210, "A_SpudCrawl"); // -1 leg
        AddToDictionary(15200, "A_SpudCrawl"); // 0 legs
        AddToDictionary(15120, "A_SpudCrawl"); // -1 arm
        AddToDictionary(15110, "A_SpudCrawl"); // -1 arm, -1 leg
        AddToDictionary(15100, "A_SpudCrawl"); // -1 arm, 0 legs
        /*********/
		
        /**************************************************************/
    }
}
