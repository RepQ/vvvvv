using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Checkpoint : MonoBehaviour
{
    public GameObject checkPointLight;
    public ParticleSystem checkpointParticles;
    public bool isActive = false;

    private Light2D lightProperties;
    private float targetLightRadius = 50f; // Define el radio deseado para cuando el checkpoint esté activo
    private float initialLightRadius;

    private void Awake()
    {
        checkpointParticles = GetComponentInChildren<ParticleSystem>();
        lightProperties = checkPointLight.GetComponent<Light2D>();
        initialLightRadius = lightProperties.pointLightOuterRadius;
    }

    private void Start()
    {
        UpdateCheckpointVisuals();
    }

    private void Update()
    {
        if (isActive && lightProperties.pointLightOuterRadius < targetLightRadius)
        {
            // Incrementa gradualmente el radio de luz usando Lerp
            lightProperties.pointLightOuterRadius = Mathf.Lerp(lightProperties.pointLightOuterRadius, targetLightRadius, Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ActivateCheckpoint();
    }

    private void ActivateCheckpoint()
    {
        isActive = true;
        UpdateCheckpointVisuals();
    }

    private void UpdateCheckpointVisuals()
    {
        // Configura los efectos visuales basados en el estado de isActive
        checkPointLight.SetActive(isActive);
        var emission = checkpointParticles.emission;
        var noise = checkpointParticles.noise;
        noise.enabled = true;
        emission.rateOverTime = 20;

        if (isActive)
        {

            // Inicia las partículas si no están activas
            if (!checkpointParticles.isPlaying)
            {
                checkpointParticles.Play();
            }
        }
        else
        {
            emission.rateOverTime = 5;
        }
    }
}