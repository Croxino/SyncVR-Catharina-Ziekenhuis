using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SyncVR.Presence
{
    public class PicoPlayerRig : MonoBehaviour
    {
        public GameObject controller;

        public void SetSittingPosition()
        {
            SetPosition(new Vector3(0f, 0f, 0f));
        }

        public void SetRecliningPosition()
        {
            SetPosition(new Vector3(45f, 0f, 0f));
        }

        public void SetLyingBackPosition()
        {
            SetPosition(new Vector3(75f, 0f, 0f));
        }

        public void SetLyingLeftSidePosition()
        {
            SetPosition(new Vector3(0f, 0f, -90f));
        }

        public void SetLyingRightSidePosition()
        {
            SetPosition(new Vector3(0f, 0f, 90f));
        }

        public void SetChinOnChestPosition()
        {
            SetPosition(new Vector3(-60f, 0f, 0f));
        }

        public void SetFaceDownPosition()
        {
            SetPosition(new Vector3(-90f, 0f, 0f));
        }

        private void SetPosition(Vector3 eulerAngles)
        {
            // only required for Oculus Go
            Vector3 controllerWorldPos = controller.transform.position;

            transform.localRotation = Quaternion.Euler(eulerAngles);

            // only required for Oculus Go
            controller.transform.position = controllerWorldPos;
        }
    }
}