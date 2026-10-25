using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FencePainting
{
    public class HandAnchorObject : MonoBehaviour
    {
        [SerializeField]
        private OVRSkeleton _hand;

        private Transform _objectTransform;

        [SerializeField]
        private Vector3 _offset;

        [SerializeField]
        private bool _lockGravity = false;

        private void Start()
        {
            _objectTransform = this.transform;
        }

        private void Update()
        {
            Vector3 handUp = _hand.Bones[9].Transform.position - _hand.Bones[0].Transform.position;
            Vector3 handAccross = _hand.Bones[6].Transform.position - _hand.Bones[12].Transform.position;
            Vector3 handOut = Vector3.Cross(handUp, handAccross);

            Vector3 surfaceOffset = _offset.x * handAccross.normalized + _offset.y * handUp + handOut.normalized * _offset.z;

            _objectTransform.position = _hand.Bones[0].Transform.position + surfaceOffset;
            if(_lockGravity){
                //_objectTransform.rotation = Quaternion.LookRotation(handUp, Vector3.up);            
            } else {
                _objectTransform.rotation = Quaternion.LookRotation(handAccross, handUp);            
            }
        }
    }
}
