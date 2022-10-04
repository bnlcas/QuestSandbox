using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManipulation : MonoBehaviour
{
    private Transform _targetTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        _targetTransform = other.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) > 0.8f)
        {
            _targetTransform.parent = this.transform;
        }
        else
        {
            _targetTransform.parent = null;
        }
    }
}
