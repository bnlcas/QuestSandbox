using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FingerPainting
{
    public class ControlsHandAnchor : MonoBehaviour
    {
        [SerializeField]
        private OVRSkeleton _leftHand;

        private Transform _controlsTransform;

        private void Start()
        {
            _controlsTransform = this.transform;
        }

        private void Update()
        {
            Vector3 handUp = _leftHand.Bones[9].Transform.position - _leftHand.Bones[0].Transform.position;
            Vector3 handAccross = _leftHand.Bones[6].Transform.position - _leftHand.Bones[12].Transform.position;
            Vector3 handOut = Vector3.Cross(handUp, handAccross);

            Vector3 surfaceOffset = -0.03f * handOut.normalized + handUp * 2.4f + handAccross.normalized * 0.01f;
            _controlsTransform.position = _leftHand.Bones[0].Transform.position + surfaceOffset;
            _controlsTransform.rotation = Quaternion.LookRotation(handOut, handAccross);            
        }
    }
}
