using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintDebug : MonoBehaviour
{
    [SerializeField]
    Material _paintMaterial;

    [SerializeField]
    RenderTexture _paintedTexture;

    [SerializeField]
    Texture _baseTex;

    Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
        Graphics.Blit(_baseTex , _paintedTexture, _paintMaterial);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if(Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                Vector4 maskPos = new Vector4(hit.textureCoord.x, hit.textureCoord.y,0,0);
                _paintMaterial.SetVector("_maskSampleCoord", maskPos);
            }
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if(Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                Vector4 normPos = new Vector4(hit.textureCoord.x, hit.textureCoord.y,0,0);
                _paintMaterial.SetVector("_brushPosition", normPos);
                Graphics.Blit(_paintedTexture , _paintedTexture, _paintMaterial);
            }
        }
    }
}
