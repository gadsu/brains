using UnityEngine;
using System.Collections;

// https://unity3d.com/learn/tutorials/topics/scripting/extension-methods
//It is common to create a class to contain all of your
//extension methods. This class must be static.
public static class ExtensionMethods
{
    //Even though they are used like normal methods, extension
    //methods must be declared static. Notice that the first
    //parameter has the 'this' keyword followed by a Transform
    //variable. This variable denotes which class the extension
    //method becomes a part of.

    // https://forum.unity.com/threads/howto-add-method-to-the-gameobject-class-ie-add-custom-variables-to-every-instance.89791/
    internal static Transform FindChildByRecursion(this Transform aParent, string aName)
    {
        if (aParent == null) return null;
        var result = aParent.Find(aName);
        if (result != null)
            return result;
        foreach (Transform child in aParent)
        {
            result = child.FindChildByRecursion(aName);
            if (result != null)
                return result;
        }
        return null;
    }
}