using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactions
{
    public enum InteractionState {
        Idle,
        Hover,
        Active,
    }

    public class BasicGrabTranslate : MonoBehaviour
    {
        private Transform _targetTransform;

        private InteractionState _currentState;

        private void Awake(){
            _targetTransform = this.transform;
            _currentState = InteractionState.Idle;
        }

        public void SetInteractionState(InteractionState state)
        {
            _currentState = state;
        }

        public void StartInteraction()
        {

        }

        public void Manipulate()
        {
            if(_currentState == InteractionState.Active)
            {

            }
        }

        public void EndInteraction()
        {
            
        }
    }
}