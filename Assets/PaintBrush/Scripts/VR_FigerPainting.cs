using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FingerPainting
{
    public class VR_FigerPainting : MonoBehaviour
    {
        [SerializeField]
        Material _paintMaterial;

        [SerializeField]
        RenderTexture _paintedTexture;

        [SerializeField]
        Texture _baseTex;

        [SerializeField]
        OVRSkeleton _rightHand;

        Vector3 _indexFingerTip;

        Vector3 _indexKnuckle;

        //[SerializeField]
        //GameObject _debugSphere;

        private const float DRAWING_DISTANCE = 0.05f;

        private bool _inContact = false;

        private void Start()
        {
            Graphics.Blit(_baseTex , _paintedTexture, _paintMaterial);
            DrawOffscreen();
        }

        private void Update()
        {
            _indexFingerTip = _rightHand.Bones[8].Transform.position;
            _indexKnuckle = _rightHand.Bones[7].Transform.position;

            //_debugSphere.transform.position = _indexKnuckle;
            Vector3 rayOrigin = _indexKnuckle;
            Vector3 rayDirection = _indexFingerTip - _indexKnuckle;
            Ray fingerRay = new Ray(rayOrigin, rayDirection);
            RaycastHit hit;

            if(Physics.Raycast(rayOrigin, rayDirection, out hit, DRAWING_DISTANCE) && hit.collider.gameObject == this.gameObject)
            {
                if(_inContact)
                {
                    UpdateTouch(hit.textureCoord);
                }
                else
                {
                    InitializeTouch(hit.textureCoord);
                }
                _inContact = true;
            }
            else
            {
                DrawOffscreen();
                _inContact = false;
            }
        }

        private void InitializeTouch(Vector2 touchPosition)
        {
            Vector4 maskPos = new Vector4(touchPosition.x, touchPosition.y,0,0);
            _paintMaterial.SetVector("_maskSampleCoord", maskPos);
        }

        private void UpdateTouch(Vector2 touchPosition)
        {
            Vector4 normPos = new Vector4(touchPosition.x, touchPosition.y,0,0);
            _paintMaterial.SetVector("_brushPosition", normPos);
            Graphics.Blit(_paintedTexture , _paintedTexture, _paintMaterial);
        }

        private void DrawOffscreen()
        {
            Vector4 offScreenPosition = new Vector4(-1.0f, -1.0f, -1.0f, 1.0f);
            _paintMaterial.SetVector("_brushPosition", offScreenPosition);
        }
    }
}
