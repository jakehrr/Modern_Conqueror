using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public Transform playerCamera;
    public float mouseSensitivity = 100f;
    public float clampAngle = 80f;

    private Vector2 turn;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Hides and locks cursor
        Cursor.visible = false;
    }

    void Update()
    {
        turn.x += Input.GetAxis("Mouse X");
        turn.y += Input.GetAxis("Mouse Y");

        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
    }
}
