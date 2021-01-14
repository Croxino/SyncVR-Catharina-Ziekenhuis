﻿/************************************************************************************
Copyright : Copyright (c) Facebook Technologies, LLC and its affiliates. All rights reserved.

Licensed under the Oculus Utilities SDK License Version 1.31 (the "License"); you may not use
the Utilities SDK except in compliance with the License, which is provided at the time of installation
or download, or which otherwise accompanies this software in either electronic or hard copy form.

You may obtain a copy of the License at
https://developer.oculus.com/licenses/utilities-1.31

Unless required by applicable law or agreed to in writing, the Utilities SDK distributed
under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF
ANY KIND, either express or implied. See the License for the specific language governing
permissions and limitations under the License.
************************************************************************************/

using System;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;


namespace UnityEngine.EventSystems
{
    /// <summary>
    /// Extension of Unity's PointerEventData to support ray based pointing and also touchpad swiping
    /// </summary>
    public class SyncVRPointerEventData : PointerEventData
    {
        public SyncVRPointerEventData(EventSystem eventSystem)
            : base(eventSystem)
        {

        }

        public Ray worldSpaceRay;
        public Vector2 swipeStart;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("<b>Position</b>: " + position);
            sb.AppendLine("<b>delta</b>: " + delta);
            sb.AppendLine("<b>eligibleForClick</b>: " + eligibleForClick);
            sb.AppendLine("<b>pointerEnter</b>: " + pointerEnter);
            sb.AppendLine("<b>pointerPress</b>: " + pointerPress);
            sb.AppendLine("<b>lastPointerPress</b>: " + lastPress);
            sb.AppendLine("<b>pointerDrag</b>: " + pointerDrag);
            sb.AppendLine("<b>worldSpaceRay</b>: " + worldSpaceRay);
            sb.AppendLine("<b>swipeStart</b>: " + swipeStart);
            sb.AppendLine("<b>Use Drag Threshold</b>: " + useDragThreshold);
            return sb.ToString();
        }

    }


    /// <summary>
    /// Static helpers for OVRPointerEventData.
    /// </summary>
    public static class PointerEventDataExtension
    {

        public static bool IsSyncVRPointer(this PointerEventData pointerEventData)
        {
            return (pointerEventData is SyncVRPointerEventData);
        }
        public static Ray SyncVRGetRay(this PointerEventData pointerEventData)
        {
            SyncVRPointerEventData vrPointerEventData = pointerEventData as SyncVRPointerEventData;
            Assert.IsNotNull(vrPointerEventData);

            return vrPointerEventData.worldSpaceRay;
        }
        public static Vector2 SyncVRGetSwipeStart(this PointerEventData pointerEventData)
        {
            SyncVRPointerEventData vrPointerEventData = pointerEventData as SyncVRPointerEventData;
            Assert.IsNotNull(vrPointerEventData);

            return vrPointerEventData.swipeStart;
        }
        public static void SyncVRSetSwipeStart(this PointerEventData pointerEventData, Vector2 start)
        {
            SyncVRPointerEventData vrPointerEventData = pointerEventData as SyncVRPointerEventData;
            Assert.IsNotNull(vrPointerEventData);

            vrPointerEventData.swipeStart = start;
        }




    }
}