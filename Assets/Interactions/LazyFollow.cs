using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _targetTransform;

    [SerializeField]
    private Transform _followerTransform;

    [SerializeField]
    private bool _followRotation = true;

    [SerializeField]
    private float _rotationSmoothing = 0.0f;//0 is no filtering

    [SerializeField]
    private bool _useLocalUpDirect  = false;

    [SerializeField]
    private bool _followPosition = true;

    [SerializeField]
    private float _followDistance_2D = 0.0f;

    void Start()
    {
        if(_followerTransform == null)
        {
            _followerTransform = this.transform;
        }
        if(_targetTransform == null)
        {
            _targetTransform = Camera.main.transform;
        }
        if(_followDistance_2D == 0)
        {
            _followDistance_2D = Get2DDirectionVector().magnitude;
        }
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
        
    }

    private Vector3 Get2DDirectionVector()
    {
        Vector3 targetDirection = _targetTransform.position - _followerTransform.position;
        Vector3 targetDirection2d = Vector3.ProjectOnPlane(targetDirection, Vector3.up);
        return targetDirection2d;
    }

    private void FollowTarget()
    {
        if(_followRotation)
        {
            LookAtTarget();
        }
        if(_followPosition)
        {
            TrackPosition();
        }
    }

    private void TrackPosition()
    {
        Vector3 direction2d = Get2DDirectionVector();
        float deltaPosition = (direction2d.magnitude - _followDistance_2D);//.magnitude;

        _followerTransform.position += deltaPosition * direction2d;
    }

    private Quaternion SmoothRotation(Quaternion q0, Quaternion q1, float rotationSmoothing)
    {
        return Quaternion.Slerp(q0, q1, rotationSmoothing);
    }

    private void LookAtTarget()
    {
        Vector3 upDir = Vector3.up;
        if(_useLocalUpDirect)
        {
            upDir = _followerTransform.up;
        }
        Vector3 targetDirection = _targetTransform.position - _followerTransform.position;
        Quaternion targetRoation = Quaternion.LookRotation(targetDirection, upDir);
        Quaternion currentRotation = _followerTransform.rotation;
        _followerTransform.rotation = SmoothRotation(currentRotation, targetRoation, 1.0f - _rotationSmoothing);
    }
}
