using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosCopy : MonoBehaviour {
    protected void CopyPosition(float x, float y) {
        const float z = 5f;
        transform.position = new Vector3(x, y, z);
    }
}
