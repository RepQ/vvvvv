using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraLimit : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CameraMove.instance.CameraON = !CameraMove.instance.CameraON;
        bool isCameraON = CameraMove.instance.CameraON;
        if (isCameraON == false)
            CameraMove.instance.targetPosition = transform.position;
    }
}
