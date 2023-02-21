using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlaneGesture
{
    public class PlaneGestureFeedback : MonoBehaviour
    {
        [SerializeField]
        private GameObject _feedbackPlane;
        
        public void SetFeedbackCenter(Vector3 position)
        {
            _feedbackPlane.transform.position = position;
        }

        public void SetFeedbackSize(float planeSize)
        {
            _feedbackPlane.transform.localScale = new Vector3(planeSize, planeSize, planeSize);
        }

        public void ShowFeedback(bool enableFeedback)
        {
            _feedbackPlane.SetActive(enableFeedback);
        }
    }
}
