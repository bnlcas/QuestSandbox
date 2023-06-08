using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlaneGesture
{
    public class PlaneGestureFeedback : MonoBehaviour
    {
        [SerializeField]
        private GameObject _feedbackPlane;

        private Quaternion _initialRotation;

        public void SetFeedbackCenter(Vector3 position)
        {
            _feedbackPlane.transform.position = position;
        }

        public void SetFeedbackOrientation(Quaternion rotation)
        {
            _feedbackPlane.transform.rotation = rotation * _initialRotation;
        }

        public void SetFeedbackSize(float planeSize)
        {
            _feedbackPlane.transform.localScale = new Vector3(planeSize, planeSize, planeSize);
        }

        public void ShowFeedback(bool enableFeedback)
        {
            _feedbackPlane.SetActive(enableFeedback);
        }

        private void Awake()
        {
            _initialRotation = _feedbackPlane.transform.rotation;
        }
    }
}
