using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

namespace DataLogging
{

    public class HandDataLog : MonoBehaviour
    {
        [SerializeField]
        private OVRSkeleton _leftHand;

        [SerializeField]
        private OVRSkeleton _rightHand;

        private Transform _headsetTransform;

        private StreamWriter _dataWriter;

        string[] _fields = {"Time", 
                            "HeadX", "HeadY", "HeadZ",
                            "HeadForwardX","HeadForwardY", "HeadForwardZ",
                            "HeadUpX","HeadUpY", "HeadUpZ",
                            "LeftWristPosX", "LeftWristPosY", "LeftWristPosZ",
                            "LeftIndexKnucklePosX", "LeftIndexKnucklePosY", "LeftIndexKnucklePosZ",
                            "LeftIndexTipPosX", "LeftIndexTipPosY", "LeftIndexTipPosZ",
                            "LeftMiddleKnucklePosX", "LeftMiddleKnucklePosY", "LeftMiddleKnucklePosZ",
                            "LeftMiddleTipPosX", "LeftMiddleTipPosY", "LeftMiddleTipPosZ",
                            "LeftThumbKnucklePosX", "LeftThumbKnucklePosY", "LeftThumbKnucklePosZ",
                            "LeftThumbTipPosX", "LeftThumbTipPosY", "LeftThumbTipPosZ",
                            "RightWristPosX", "RightWristPosY", "RightWristPosZ",
                            "RightIndexKnucklePosX", "RightIndexKnucklePosY", "RightIndexKnucklePosZ",
                            "RightIndexTipPosX", "RightIndexTipPosY", "RightIndexTipPosZ",
                            "RightMiddleKnucklePosX", "RightMiddleKnucklePosY", "RightMiddleKnucklePosZ",
                            "RightMiddleTipPosX", "RightMiddleTipPosY", "RightMiddleTipPosZ",
                            "RightThumbKnucklePosX", "RightThumbKnucklePosY", "RightThumbKnucklePosZ",
                            "RightThumbTipPosX", "RightThumbTipPosY", "RightThumbTipPosZ"};

        private void Start()
        {
            _headsetTransform = Camera.main.transform;
            InitializeDataLog(_fields);
        }

        private void OnApplicationQuit()
        {
            _dataWriter.Close();
        }

        private void InitializeDataLog(string[] fields)
        {
            string dataDir = Application.dataPath + "/Datalogs/";
            if (!System.IO.Directory.Exists(dataDir))
            {
                System.IO.Directory.CreateDirectory(dataDir);
            }
            string filename = "HandDataLog" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm") + ".csv";
            string filepath = dataDir + filename;
            _dataWriter = new StreamWriter(filepath);
            _dataWriter.WriteLine(string.Join(",", fields));
        }

        private void LogRow()
        {
            string[] data = {Time.time.ToString("0.0000"), 
                            _headsetTransform.position.x.ToString("0.0000"), _headsetTransform.position.y.ToString("0.0000"), _headsetTransform.position.z.ToString("0.0000"),
                            _headsetTransform.forward.x.ToString("0.0000"), _headsetTransform.forward.y.ToString("0.0000"), _headsetTransform.forward.z.ToString("0.0000"),
                            _headsetTransform.up.x.ToString("0.0000"), _headsetTransform.up.y.ToString("0.0000"), _headsetTransform.up.z.ToString("0.0000"),
            };
            _dataWriter.WriteLine(string.Join(",", data));
        }
    }    
}
