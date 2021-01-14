using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SyncVRCursor : MonoBehaviour
{
    public abstract void SetCursorRay(Transform ray);
    public abstract void SetCursorStartDest(Vector3 start, Vector3 dest, Vector3 normal);
}
