using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactions
{
    public class HandSelection : MonoBehaviour
    {
        [SerializeField]
        private OVRHand _rightHand;

        [SerializeField]
        private GameObject _debug;

        private BasicGrabTranslate _currentInteraction;

        private bool _isPinching;

        private const float CAST_RADIUS = 0.05f;
        private const float CAST_DISTANCE = 1.2f;


        private Vector3 GetHandAnchor()
        {
            return _rightHand.PointerPose.position;
        }

        private Vector3 GetHandDirection()
        {
            return _rightHand.PointerPose.forward;
        }

        private void CheckPinchChange()
        {
            bool isPinch = _rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
            if(_isPinching != isPinch)
            {
                if(isPinch)
                {
                    if(_currentInteraction != null)
                    {
                        _currentInteraction.StartInteraction();
                    }
                }
                else
                {
                    if(_currentInteraction != null)
                    {
                        _currentInteraction.EndInteraction();
                    }
                }
            }
        }
        private void  UpdateCurrentInteraction()
        {
            
            BasicGrabTranslate closestTarget;
            float nearestDist = Mathf.Infinity;
            RaycastHit[] hits = Physics.SphereCastAll(GetHandAnchor(), CAST_RADIUS, GetHandDirection(), CAST_DISTANCE);
            if(hits.Length > 0)
            {
                for(int i = 0; i < hits.Length; i++)
                {
                    if(hits[i].transform.gameObject.GetComponent<BasicGrabTranslate>())
                    {
                        float targetDist = Vector3.Distance(hits[i].point, GetHandAnchor());
                        if(targetDist < nearestDist)
                        {
                            nearestDist = targetDist;
                            closestTarget = hits[i].transform.gameObject.GetComponent<BasicGrabTranslate>();
                        }
                    }
                }

                /*if(_currentInteraction != closestTarget)
                {
                    _currentInteraction.SetInteractionState(InteractionState.Hover);
                    _currentInteraction = closestTarget;
                    closestTarget.SetInteractionState(InteractionState.Idle);
                }*/
            }
        }

        private void Update()
        {
            _debug.transform.position = _rightHand.PointerPose.position;
            _debug.transform.rotation = _rightHand.PointerPose.rotation;

            CheckPinchChange();

             
            if(_isPinching && _currentInteraction != null)
            {
                _currentInteraction.Manipulate();
            }
            
            if(!_isPinching)
            {
                UpdateCurrentInteraction();
            }
            _isPinching = _rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        }
    }
}
