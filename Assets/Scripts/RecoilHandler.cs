using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilHandler : MonoBehaviour
{
    [Header("Recoil Settings")]
    public float recoilKickAmount = 3f;       // How much to kick upward per shot
    public float recoilRecoverySpeed = 10f;   // How fast it recovers
    public float maxRecoilAngle = 30f;        // Clamp max recoil tilt

    private float currentRecoilAngle = 0f;
    private float currentRecoilVelocity = 0f;

    void Update()
    {
        // Smooth recoil recovery over time
        currentRecoilAngle = Mathf.SmoothDamp(
            currentRecoilAngle,
            0f,
            ref currentRecoilVelocity,
            1f / recoilRecoverySpeed
        );

        // Apply the tilt to the camera holder
        transform.localRotation = Quaternion.Euler(currentRecoilAngle, 0f, 0f);
    }

    /// <summary>
    /// Call this from your shooting script every time you fire.
    /// </summary>
    public void ApplyRecoil()
    {
        currentRecoilAngle -= recoilKickAmount;
        currentRecoilAngle = Mathf.Clamp(currentRecoilAngle, -maxRecoilAngle, maxRecoilAngle);
    }
}
