using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPowerupState
{
    public static bool hasGrappleUnlocked = false;
    public static bool hasSuperJump = false;

    public static void ResetAllPowerups()
    {
        hasGrappleUnlocked = false;
    }
}
