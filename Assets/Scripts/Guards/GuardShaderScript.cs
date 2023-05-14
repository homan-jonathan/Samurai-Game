using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardShaderScript : MonoBehaviour
{
    Shader _thisMat;

    Shader basicShader;
    Shader xRayShader;
    SkinnedMeshRenderer _skinnedMeshRenderer;

    PlayerRaycastScript _raycastScript;

    public bool _isTagged = false;
    // Start is called before the first frame update
    void Start()
    {
        _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        basicShader = Shader.Find("Standard");
        xRayShader = Shader.Find("XRay Shaders/Diffuse-XRay-Replaceable");

        _skinnedMeshRenderer.material.shader = basicShader;

        _raycastScript = FindObjectOfType<PlayerRaycastScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_isTagged)
        {
            _skinnedMeshRenderer.material.shader = xRayShader;
        }
        else
        {
            _skinnedMeshRenderer.material.shader = basicShader;
        }
    }
}
