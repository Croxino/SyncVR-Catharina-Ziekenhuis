/************************************************************************************

Copyright (c) Facebook Technologies, LLC and its affiliates. All rights reserved.  

See SampleFramework license.txt for license terms.  Unless required by applicable law 
or agreed to in writing, the sample code is provided “AS IS” WITHOUT WARRANTIES OR 
CONDITIONS OF ANY KIND, either express or implied.  See the license for specific 
language governing permissions and limitations under the license.

************************************************************************************/
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class SyncVRHandedInputSelector : MonoBehaviour
{
#if OCULUS
    OVRCameraRig m_CameraRig;
#else
    Pvr_Controller m_PvrController;
#endif

    SyncVRInputModule m_InputModule;

    void Start()
    {
#if OCULUS
        m_CameraRig = FindObjectOfType<OVRCameraRig>();
#else
        m_PvrController = FindObjectOfType<Pvr_Controller>();
        Pvr_ControllerManager.ChangeMainControllerCallBackEvent += OnChangeMainControllerCallback;
#endif
        m_InputModule = FindObjectOfType<SyncVRInputModule>();
    }

    void Update()
    {
#if OCULUS
        if(OVRInput.GetActiveController() == OVRInput.Controller.LTouch)
        {
            SetActiveController(OVRInput.Controller.LTouch);
        }
        else
        {
            SetActiveController(OVRInput.Controller.RTouch);
        }
#else
#endif
    }

#if OCULUS
    void SetActiveController(OVRInput.Controller c)
    {
        Transform t;
        if(c == OVRInput.Controller.LTouch)
        {
            t = m_CameraRig.leftHandAnchor;
        }
        else
        {
            t = m_CameraRig.rightHandAnchor;
        }
        m_InputModule.rayTransform = t;
    }
#else
    public void OnChangeMainControllerCallback (string index)
    {
        Debug.Log("Main Controller changed to: " + index);

        int i = Convert.ToInt16(index);
        m_InputModule.activeControllerIndex = i;
        m_InputModule.rayTransform = i == 0 ? m_PvrController.controller0.transform : m_PvrController.controller1.transform;
    }

    public void OnDestroy()
    {
        Pvr_ControllerManager.ChangeMainControllerCallBackEvent -= OnChangeMainControllerCallback;
    }
#endif

}
