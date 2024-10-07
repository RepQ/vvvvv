using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    public Player playerToFollow;
    public float durationSmooth;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(SmoothLightPlayerTransition(playerToFollow.transform.position));
    }

    private IEnumerator SmoothLightPlayerTransition(Vector3 targetPosition)
    {
        float elapsed = 0f;

        Vector3 startPosition = transform.position;
        while (elapsed < durationSmooth)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / durationSmooth;
            float easeValue = EaseInOutBounce(t);
            transform.position = Vector3.Lerp(startPosition, targetPosition, easeValue);

            yield return null;
        }

        transform.position = targetPosition;
    }

    public static float EaseOutBounce(float x)
    {
        float n1 = 7.5625f;
        float d1 = 2.75f;

        if (x < 1 / d1)
            return n1 * x * x;
        else if (x < 2 / d1)
            return n1 * (x -= 1.5f / d1) * x + 0.75f;
        else if (x < 2.5 / d1)
            return n1 * (x -= 2.25f / d1) * x + 0.9375f;
        else
            return n1 * (x -= 2.625f / d1) * x + 0.984375f;
    }

    public static float EaseInOutBounce(float x)
    {
        return x < 0.5
            ? (1 - EaseOutBounce(1 - 2 * x)) / 2
            : (1 + EaseOutBounce(2 * x - 1)) / 2;
    }
}
