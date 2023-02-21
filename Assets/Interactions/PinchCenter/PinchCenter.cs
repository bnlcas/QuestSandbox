using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinchCenter
{
    public class PinchCenter : MonoBehaviour
    {
        [SerializeField]
        private OVRHand _rightHand;

        [SerializeField]
        private OVRHand _leftHand;

        [SerializeField]
        private Transform _targetTransform;

        private void Update()
        {
            bool isRightPinch = _rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
            bool isLeftPich = _leftHand.GetFingerIsPinching(OVRHand.HandFinger.Index);

            if(isRightPinch && isLeftPich)
            {
                CenterPinchTarget();
            }
        }
        
        private void CenterPinchTarget()
        {
            Vector3 rightCenter = _rightHand.PointerPose.position;
            Vector3 leftCenter = _leftHand.PointerPose.position;

            Vector3 pinchCenter = Vector3.Lerp(rightCenter, leftCenter, 0.5f) - 0.03f * Vector3.up;
            Vector3 pinchRight = (rightCenter - leftCenter).normalized;
            Vector3 pinchForward = Vector3.Cross(Vector3.up, pinchRight);

            _targetTransform.position = pinchCenter;
            //_targetTransform.rotation = Quaternion.LookRotation(pinchForward);

        }
    }
}
