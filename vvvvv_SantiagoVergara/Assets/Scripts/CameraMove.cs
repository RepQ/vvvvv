using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Camera_SO cameraData;
    private Camera m_Camera;

    public Vector3 cameraPosition;
    private void Awake()
    {
        m_Camera = GetComponent<Camera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        InitPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitPosition()
    {
        cameraPosition = cameraData.initPosition;
        StartCoroutine(SmoothCameraTransition(cameraPosition));
    }
    public void ChangeScenarioHorizontal(Collider2D collider, float direction)
    {
        cameraPosition = new Vector3
            (cameraPosition.x + Mathf.Abs(collider.transform.position.x - cameraPosition.x) * 2 * direction,
            cameraPosition.y, 
            cameraPosition.z);

        StartCoroutine(SmoothCameraTransition(cameraPosition));
    }

    private IEnumerator SmoothCameraTransition(Vector3 targetPosition)
    {
        float duration = 0.4f;
        float elapsed = 0f;

        Vector3 startPosition = m_Camera.transform.position;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            m_Camera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);

            yield return null;
        }

        m_Camera.transform.position = targetPosition;
    }
}
