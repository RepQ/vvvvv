using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraLimit : MonoBehaviour
{
    public CameraMove cameraMove;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cameraMove.CameraON = !cameraMove.CameraON;
        bool iscameraMoveON = cameraMove.CameraON;
        if (iscameraMoveON == false)
            cameraMove.targetPosition = transform.position;
    }
}
