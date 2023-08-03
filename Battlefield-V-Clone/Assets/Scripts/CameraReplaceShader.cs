using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraReplaceShader : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;

    private void Start()
    {
        targetCamera.SetReplacementShader(Shader.Find("Unlit/Color"), "RenderType");
    }
}
