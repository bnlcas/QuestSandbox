using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FingerPainting
{
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
            DrawOffscreen();
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if(Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity) && hit.collider.gameObject == this.gameObject)
                {
                    Vector4 maskPos = new Vector4(hit.textureCoord.x, hit.textureCoord.y,0,0);
                    _paintMaterial.SetVector("_maskSampleCoord", maskPos);
                }
            }

            if (Input.GetMouseButton(0))
            {
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if(Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity) && hit.collider.gameObject == this.gameObject)
                {
                    Vector4 normPos = new Vector4(hit.textureCoord.x, hit.textureCoord.y,0,0);
                    _paintMaterial.SetVector("_brushPosition", normPos);
                    Graphics.Blit(_paintedTexture , _paintedTexture, _paintMaterial);
                }
            }

            if(Input.GetMouseButtonUp(0))
            {
                DrawOffscreen();
            }
        }

        private void DrawOffscreen()
        {
            Vector4 offScreenPosition = new Vector4(-1.0f, -1.0f, -1.0f, 1.0f);
            _paintMaterial.SetVector("_brushPosition", offScreenPosition);
        }
    }
}