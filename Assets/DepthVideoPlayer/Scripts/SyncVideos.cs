using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace DepthVideos
{
    public class SyncVideos : MonoBehaviour
    {
        [SerializeField]
        private VideoPlayer _video1;

        [SerializeField]
        private VideoPlayer _video2;

        private int _index = 0;

        [SerializeField]
        private List<VideoClip> _clips;

        [SerializeField]
        private List<VideoClip> _depthClips;

        private bool _resetting = false;

        private List<Vector3> _scales = new List<Vector3>();

        private void Start()
        {

            _scales.Add(new Vector3(0.4f, 0.40f, 1.0f));
            _scales.Add(new Vector3(0.7f, 0.5625f * 0.7f, 1.0f));

            StartCoroutine(PlayVideos());
        }

        private void Update()
        {
            if(!_resetting)
            {
                if(OVRInput.Get(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.A))
                {
                    _index += 1;
                if(_index >= _clips.Count)
                {
                    _index = 0;
                }
                ResetClip();
            }
            if(OVRInput.Get(OVRInput.Button.Two) || Input.GetKeyDown(KeyCode.S))
            {
                _index -= 1;
                if(_index < 0)
                {
                    _index = _clips.Count - 1;
                }   
                    ResetClip();
                }
            }
        }

        private void ResetClip()
        {
            _video2.clip = _clips[_index];
            _video1.clip = _depthClips[_index];
            this.transform.localScale = _scales[_index];
            StartCoroutine(PlayVideos());
        }

        private IEnumerator PlayVideos()
        {
            _resetting = true;
            yield return new WaitForSeconds(0.25f);
            _video1.Play();
            _video2.Play();
            _resetting = false;
            yield return null;
        }
    }
}