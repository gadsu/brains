using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyHandler : MonoBehaviour
{
    private int val;
    private bool updatedBody;
    protected struct Body
    {
        public  int arms, legs;
        public string hands, feet;
    };

    protected Body playerBody;

    private void Awake()
    {
        playerBody = new Body
        {
            arms = 2,
            legs = 2,
            hands = "Both",
            feet = "Both"
        };
        updatedBody = false;
        val = 0;
    }

    public bool UpdateArms(int a, string side)
    {
        updatedBody = true;

        switch (playerBody.arms + a)
        {
            case 1:
                if (side == "Right")
                    playerBody.hands = "Left";
                else
                {
                    playerBody.hands = "Right";
                }
                break;
            case 2:
                playerBody.hands = "both";
                break;
            default:
                updatedBody = false;
                break;
        }

        if (updatedBody)
            playerBody.arms = playerBody.arms + a;

        return updatedBody;
    }

    public bool UpdateLegs(int l, string side)
    {
        updatedBody = true;
        switch (playerBody.legs + l)
        {
            case 0:
                playerBody.feet = "Neither";
                break;
            case 1:
                if (side == "Right")
                    playerBody.feet = "Left";
                else
                    playerBody.feet = "Right";
                break;
            case 2:
                playerBody.feet = "Both";
                break;
            default:
                updatedBody = false;
                break;
        }

        if (updatedBody)
            playerBody.legs = playerBody.legs + l;

        return updatedBody;
    }

    public int GetLegs()
    {
        val = 0;
        switch (playerBody.feet)
        {
            case "Neither":
                val = 0;
                break;
            case "Left":
                val = 1;
                break;
            case "Right":
                val = 3;
                break;
            case "Both":
                val = 2;
                break;
            default:
                break;
        }

        return val;
    }

    public int GetArms()
    {
        val = 0;
        switch (playerBody.hands)
        {
            case "Left":
                val = 1;
                break;
            case "Both":
                val = 2;
                break;
            case "Right":
                val = 3;
                break;
            default:
                break;
        }

        return val;
    }
}
