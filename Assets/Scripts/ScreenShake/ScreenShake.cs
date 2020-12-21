using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public enum ShakeAmount
    {
        small,
        medium,
        large
    }

    public static float ShakeAmountToFloat(ShakeAmount amount)
    {
        return amount switch
        {
            ShakeAmount.small => 0.5f,
            ShakeAmount.medium => 1f,
            ShakeAmount.large => 2f,
            _ => throw new System.InvalidOperationException()
        };
    }

    public static void Shake(float ammount)
    {

    }
}