using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardShaderScript : MonoBehaviour
{
    Shader _thisMat;

    Shader basicShader;
    Shader xRayShader;
    SkinnedMeshRenderer _skinnedMeshRenderer;

    public bool _isTagged = false;
    // Start is called before the first frame update
    void Start()
    {
        _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        basicShader = Shader.Find("Standard");
        xRayShader = Shader.Find("XRay Shaders/Diffuse-XRay-Replaceable");

        _skinnedMeshRenderer.material.shader = basicShader;
    }

    // Update is called once per frame
    void Update()
    {
        //_thisMat = Shader.Find("Standard");
        
    }
}
