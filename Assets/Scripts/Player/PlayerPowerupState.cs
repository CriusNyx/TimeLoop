using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPowerupState
{

    public static bool isWin = false;
    public static bool hasGrappleUnlocked = false;
    public static bool hasSuperJump = false;

    public static void ResetAllPowerups()
    {
        isWin = false;
        hasGrappleUnlocked = false;
        hasSuperJump = false;
    }
}
