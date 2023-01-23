using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Panoramas
{
    public class VideoSequence : MonoBehaviour
    {
        [SerializeField]
        private VideoPlayer _videoPlayer;

        [SerializeField]
        private List<VideoClip> _clips;

        private bool _prepared = false;

        private int _clipIndex = 0;

        private void Start()
        {
            _videoPlayer.prepareCompleted += VideoReady;
        }

        private void Update()
        {
            if(_prepared)
            {
                if(OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.A))
                {
                    _clipIndex += 1;
                    if(_clipIndex >= _clips.Count)
                    {
                        _clipIndex = 0;
                    }
                    PlayCurrentClip();
                }
                if(OVRInput.GetDown(OVRInput.Button.Two) || Input.GetKeyDown(KeyCode.S))
                {
                    _clipIndex -= 1;
                    if(_clipIndex < 0)
                    {
                        _clipIndex = _clips.Count - 1;
                    }   
                    PlayCurrentClip();
                }
            }
        }

        private void PlayCurrentClip()
        {
            _videoPlayer.clip = _clips[_clipIndex];
            _videoPlayer.Play();
        }

        void VideoReady(UnityEngine.Video.VideoPlayer vp)
        {
            _prepared = true;
        }
    }
}