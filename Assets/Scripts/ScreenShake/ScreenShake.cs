using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : TimeBehaviour
{
    public enum ShakeAmount
    {
        small,
        medium,
        large
    }

    private float thetaAt1 = 30f;

    private float myShake = 0f;

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

    public static void Shake(ShakeAmount amount)
    {
        float f = ShakeAmountToFloat(amount);

        ScreenShake shake = FindObjectOfType<ScreenShake>();

        shake.myShake = Mathf.Max(shake.myShake, f);
    }

    protected override void ProtectedFixedUpdate()
    {
        myShake -= Time.fixedDeltaTime;

    }
}