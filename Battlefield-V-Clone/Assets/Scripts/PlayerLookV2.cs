using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookV2 : MonoBehaviour
{
    [Header("Mouse Settings")]
    public float mouseSensitivity = 1f;
    Vector2 _currentRotation;

    void Update()
    {
        DetermineRotation();
    }

    void DetermineRotation()
    {
        Vector2 mouseAxis = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        mouseAxis *= mouseSensitivity;
        _currentRotation += mouseAxis;

        _currentRotation.y = Mathf.Clamp(_currentRotation.y, -90, 90);

        

        transform.root.localRotation = Quaternion.AngleAxis(_currentRotation.x, Vector3.up);
        transform.root.localRotation = Quaternion.AngleAxis(-_currentRotation.y, Vector3.right);
    }
}
