using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyHandler : MonoBehaviour
{
    private int m_val;
    private bool m_updatedBody;

    protected struct Body
    {
        public  int body_arms, body_legs;
        public string body_hands, body_feet;

        public bool UpdateArms(int limb, string side)
        {
            switch (body_arms + a)
            {
                case 1:
                    if (side == "Right")
                        body_hands = "Left";
                    else
                    {
                        body_hands = "Right";
                    }
                    break;
                case 2:
                    body_hands = "both";
                    break;
                default:
                    m_updatedBody = false;
                    break;
            }

            if (m_updatedBody)
                arms = arms + a;

            return m_updatedBody;
        }

        public bool UpdateLegs(int limb, string side)
        {
            switch (body_legs + l)
            {
                case 0:
                    body_feet = "Neither";
                    break;
                case 1:
                    if (side == "Right")
                        body_feet = "Left";
                    else
                        body_feet = "Right";
                    break;
                case 2:
                    body_feet = "Both";
                    break;
                default:
                    m_updatedBody = false;
                    break;
            }

            if (m_updatedBody)
                body_legs = body_legs + l;

            return m_updatedBody;
        }
    };

    protected Body playerBody;

    private void Awake()
    {
        /* Initialize 'simple' data variables*/
        m_updatedBody = false;
        m_val = 0;
        /*************************************/

        /* Initialize 'complex' data variables. */
        playerBody = new Body
        { // Keeps track of the updated body state.
            body_arms = 2,
            body_legs = 2,
            body_hands = "Both",
            body_feet = "Both"
        };
        /****************************************/
    }

    public bool UpdateLimbs(int limb, string side, string part)
    {
        m_updatedBody = true;

        if (part == "Arms") playerBody.UpdateArms(limb, side);
        else if (part == "Legs") playerBody.UpdateLegs(limb, side);
        else
            return null;

        return m_updatedBody;
    }

    public int GetLegs()
    {
        m_val = 0;
        switch (playerBody.body_legs)
        {
            case 0:
                m_val = 0;
                break;
            case 1:
                m_val = 1;
                if (playerBody.body_feet == "Right")
                    m_val += 2;
                break;
            case 2:
                m_val = 2;
                break;
            default:
                break;
        }

        return m_val;
    }

    public int GetArms()
    {
        m_val = 0;
        switch (playerBody.body_arms)
        {
            case 1:
                m_val = 1;
                if (playerBody.body_hands == "Right")
                    m_val += 2;
                break;
            case 2:
                m_val = 2;
                break;   
            default:
                break;
        }

        return m_val;
    }
}
