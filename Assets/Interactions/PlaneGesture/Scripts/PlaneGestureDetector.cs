using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace PlaneGesture
{
    public class PlaneGestureDetector : MonoBehaviour
    {
        public UnityEvent PlaneGestureDetected {get ; private set; }

        [SerializeField]
        OVRSkeleton _rightHand;
        
        [SerializeField]
        OVRSkeleton _leftHand;

        [SerializeField]
        TMP_Text _debug;

        [SerializeField]
        PlaneGestureFeedback _feedbackPlane;

        private const float APPEAR_THRESHOLD = 1.4f;
        private const int APPEAR_FRAMES = 20;

        private const float DEPLOY_THRESHOLD = 1.8f;
        private const float DEPLOY_DISTANCE = 0.1f;

        private float _startDistance = 0.0f;
        private int _appearFrameNumber = 0;
        private bool _hasAppeared = false;

        private float CalculateHeightVariance()
        {
            float tally = 0.0f;
            float squareTally = 0.0f;
            float count = (float) _rightHand.Bones.Count +  (float) _leftHand.Bones.Count;

            for(int i = 0; i < _rightHand.Bones.Count; i++)
            {
                tally += _rightHand.Bones[i].Transform.position.y;
                squareTally += Mathf.Pow(_rightHand.Bones[i].Transform.position.y, 2.0f);
            }
            for(int i = 0; i < _leftHand.Bones.Count; i++)
            {
                tally += _leftHand.Bones[i].Transform.position.y;
                squareTally += Mathf.Pow(_leftHand.Bones[i].Transform.position.y, 2.0f);
            }
            float variance = squareTally/count - Mathf.Pow(tally/count, 2.0f);
            return 10000.0f * variance;
        }

        private Vector3 GetRightHandCenter()
        {
            Vector3 center = Vector3.zero;
            for(int i = 0; i < _rightHand.Bones.Count; i++)
            {
                center += _rightHand.Bones[i].Transform.position;
            }
            center /= (float) _rightHand.Bones.Count;
            return center;
        }

        private Vector3 GetLeftHandCenter()
        {
            Vector3 center = Vector3.zero;
            for(int i = 0; i < _leftHand.Bones.Count; i++)
            {
                center += _leftHand.Bones[i].Transform.position;
            }
            center /= (float) _leftHand.Bones.Count;
            return center;
        }
        
        private bool CheckAppearance(float yVariance)
        {
            if(yVariance < APPEAR_THRESHOLD)
            {
                _appearFrameNumber += 1;
            }
            else
            {
                _appearFrameNumber = 0;
            }
            return _appearFrameNumber > APPEAR_FRAMES;
        }

        private void DeployFeedback()
        {
            _startDistance = Vector3.Distance(GetRightHandCenter(), GetLeftHandCenter());
            _feedbackPlane.ShowFeedback(true);
            _feedbackPlane.SetFeedbackCenter(Vector3.Lerp(GetRightHandCenter(), GetLeftHandCenter(), 0.5f));
            _feedbackPlane.SetFeedbackSize(_startDistance);
        }

        private void HideFeedback()
        {
            _appearFrameNumber = 0;
            _hasAppeared = false;
            _feedbackPlane.ShowFeedback(false);
        }

        private void UpdateFeedback()
        {
            _feedbackPlane.SetFeedbackCenter(Vector3.Lerp(GetRightHandCenter(), GetLeftHandCenter(), 0.5f));
            _feedbackPlane.SetFeedbackSize(Vector3.Distance(GetRightHandCenter(), GetLeftHandCenter()));
        }

        private void Start()
        {
            PlaneGestureDetected= new UnityEvent();
        }

        private void Update()
        {
            float handYVariance = CalculateHeightVariance();
            _debug.text = handYVariance.ToString();
            if(!_hasAppeared)
            {
                _hasAppeared = CheckAppearance(handYVariance);
                if(_hasAppeared)
                {
                    DeployFeedback();
                }
            }
            else
            {
                if(handYVariance < DEPLOY_THRESHOLD)
                {
                    UpdateFeedback();
                }
                else
                {
                    float travelDist = Vector3.Distance(GetRightHandCenter(), GetLeftHandCenter());
                    if(travelDist > DEPLOY_DISTANCE)
                    {
                        PlaneGestureDetected.Invoke();
                    }
                    HideFeedback();

                }                
            }
        }
    }
}