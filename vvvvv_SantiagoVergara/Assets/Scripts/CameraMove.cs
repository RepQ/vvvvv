using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Camera m_Camera;

    public Vector3 cameraPosition;
    private void Awake()
    {
        m_Camera = GetComponent<Camera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        cameraPosition = m_Camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScenarioRight(Collider2D collider)
    {
        m_Camera.transform.position = new Vector3((collider.transform.position.x - cameraPosition.x) * 2, cameraPosition.y, cameraPosition.z);
    }
}
