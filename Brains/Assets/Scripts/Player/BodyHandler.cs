using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyHandler : MonoBehaviour
{
    private int _val;

    protected struct Body
    {
        public int bodyArms, bodyLegs;
        public string bodyHands, bodyFeet;
        public bool updatedBody;

        public bool UpdateArms(int pLimb, string pSide)
        {
            switch (bodyArms + pLimb)
            {
                case 1:
                    if (pSide == "Right")
                        bodyHands = "Left";
                    else
                    {
                        bodyHands = "Right";
                    }
                    break;
                case 2:
                    bodyHands = "both";
                    break;
                default:
                    updatedBody = false;
                    break;
            }

            if (updatedBody)
                bodyArms = bodyArms + pLimb;

            return updatedBody;
        }

        public bool UpdateLegs(int pLimb, string pSide)
        {
            switch (bodyLegs + pLimb)
            {
                case 0:
                    bodyFeet = "Neither";
                    break;
                case 1:
                    if (pSide == "Right")
                        bodyFeet = "Left";
                    else
                        bodyFeet = "Right";
                    break;
                case 2:
                    bodyFeet = "Both";
                    break;
                default:
                    updatedBody = false;
                    break;
            }

            if (updatedBody)
                bodyLegs = bodyLegs + pLimb;

            return updatedBody;
        }
    };

    protected Body mPlayerBody;

    private void Awake()
    {
        /* Initialize 'simple' data variables*/
        _val = 0;
        /*************************************/

        /* Initialize 'complex' data variables. */
        mPlayerBody = new Body
        { // Keeps track of the updated body state.
            updatedBody = false,
            bodyArms = 2,
            bodyLegs = 2,
            bodyHands = "Both",
            bodyFeet = "Both"
        };
        /****************************************/
    }

    public bool UpdateLimbs(int pLimb, string pSide, string pPart)
    {
       mPlayerBody.updatedBody = true;

        if (pPart == "Arms") mPlayerBody.UpdateArms(pLimb, pSide);
        else if (pPart == "Legs") mPlayerBody.UpdateLegs(pLimb, pSide);

        return mPlayerBody.updatedBody;
    }

    public int GetLegs()
    {
        _val = 0;
        switch (mPlayerBody.bodyLegs)
        {
            case 0:
                _val = 0;
                break;
            case 1:
                _val = 1;
                if (mPlayerBody.bodyFeet == "Right")
                    _val += 2;
                break;
            case 2:
                _val = 2;
                break;
            default:
                break;
        }

        return _val;
    }

    public int GetArms()
    {
        _val = 0;
        switch (mPlayerBody.bodyArms)
        {
            case 1:
                _val = 1;
                if (mPlayerBody.bodyHands == "Right")
                    _val += 2;
                break;
            case 2:
                _val = 2;
                break;   
            default:
                break;
        }

        return _val;
    }
}
