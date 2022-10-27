using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FingerPainting
{
    public class BrushSettings : MonoBehaviour
    {
        [SerializeField]
        GameObject _brushIndicator;

        [SerializeField]
        Material _brushIndicatorMaterial;

        [SerializeField]
        Material _svSliderMaterial;

        [SerializeField]
        Material _hueSliderMaterial;

        [SerializeField]
        Material _sizeSliderMaterial;

        [SerializeField]
        Material _fingerPaintingMaterial;

        [SerializeField]
        OVRSkeleton _rightHand;

        Vector3 _indexFingerTip;

        Vector3 _indexKnuckle;

        private const float DRAWING_DISTANCE = 0.06f;

        public Color BrushColor { get; private set; }

        public float BrushSize { get; private set; }

        [SerializeField]
        private float _minSize = 0.1f;

        [SerializeField]
        private float _maxSize = 0.6f;

        private const float Size_Indicator_Scaling = 0.5f;

        private Vector2 _svSetting = new Vector2(0.9f, 0.7f);

        private float _hueSetting = 0.06f;

        private void Start()
        {
            InitializeBrushValues();
        }

        private void Update()
        {
            _indexFingerTip = _rightHand.Bones[8].Transform.position;
            _indexKnuckle = _rightHand.Bones[7].Transform.position;

            Vector3 rayOrigin = _indexKnuckle;
            Vector3 rayDirection = _indexFingerTip - _indexKnuckle;
            Ray fingerRay = new Ray(rayOrigin, rayDirection);
            RaycastHit hit;

            if(Physics.Raycast(rayOrigin, rayDirection, out hit, DRAWING_DISTANCE))
            {
                if(hit.collider.gameObject.name == "SL_Slider")
                {
                    UpdateSV(hit.textureCoord);
                }
                if(hit.collider.gameObject.name == "Hue_Slider")
                {
                    UpdateHue(hit.textureCoord);
                }
                if(hit.collider.gameObject.name == "SizeSlider")
                {
                    UpdateSize(hit.textureCoord);
                }
            }
        }

        private void UpdateSV(Vector2 touchPosition)
        {
            _svSetting = touchPosition;
            Vector4 normPos = new Vector4(touchPosition.x, touchPosition.y,0,0);
            _svSliderMaterial.SetVector("_indicatorUV", normPos);
            UpdateBrushColor();
        }

        private void UpdateHue(Vector2 touchPosition)
        {
            _hueSetting = touchPosition.y;
            Vector4 normPos = new Vector4(0, touchPosition.y,0,0);
            _hueSliderMaterial.SetVector("_indicatorUV", normPos);
            _svSliderMaterial.SetFloat("_hue", _hueSetting);

            UpdateBrushColor();
        }

        private void UpdateSize(Vector2 touchPosition)
        {
            Vector4 normPos = new Vector4(touchPosition.x, 0,0,0);
            _sizeSliderMaterial.SetVector("_indicatorUV", normPos);

            BrushSize = Mathf.Lerp(_minSize, _maxSize, touchPosition.x);
            
            _fingerPaintingMaterial.SetFloat("_brushSize", BrushSize);
            _brushIndicator.transform.localScale = Size_Indicator_Scaling * BrushSize * Vector3.one;
        }

        private void UpdateBrushColor()
        {
            BrushColor = Color.HSVToRGB(_hueSetting,_svSetting.x, _svSetting.y);
            _brushIndicatorMaterial.color = BrushColor;
            _fingerPaintingMaterial.SetVector("_brushColor", BrushColor);
        }

        private void InitializeBrushValues()
        {
            UpdateHue(new Vector2(0.0f, _hueSetting));
            UpdateSV(_svSetting);
            UpdateSize(new Vector2(0.5f, 0.0f));
        }
    }
}

