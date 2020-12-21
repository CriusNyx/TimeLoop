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

    private float thetaAt1 = 15f;
    private float perlinSpeed = 5f;
    private const int xOffset = 10000;
    private const int yOffset = 1000000;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Shake(ShakeAmount.small);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Shake(ShakeAmount.medium);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Shake(ShakeAmount.large);
        }
    }

    protected override void ProtectedFixedUpdate()
    {
        float shakeValue = myShake * myShake;

        float x = Mathf.PerlinNoise(xOffset + Time.time * perlinSpeed, 0f);
        float y = Mathf.PerlinNoise(yOffset + Time.time * perlinSpeed, 0f);

        x = x * 2 - 1;
        y = y * 2 - 1;

        Quaternion localRot = Quaternion.Euler(
            x * shakeValue * thetaAt1, 
            y * shakeValue * thetaAt1, 
            0f);

        foreach(Transform child in transform)
        {
            child.localRotation = localRot;
        }

        myShake -= Time.fixedDeltaTime;
        myShake = Mathf.Clamp(myShake, 0f, myShake);
    }
}