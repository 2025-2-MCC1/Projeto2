using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CarForward : MonoBehaviour
{
    [Header("Movimento Frontal")]
    public float baseSpeed = 10f;
    public float maxSpeed = 25f;
    public float acceleration = 5f;
    public float brakingForce = 10f;

    [Header("Controle do Jogador")]
    public bool canControl = true;

    private float currentSpeed;
    private bool slowingDown = false;
    private float speedMultiplier = 1f;

    [Header("Som do Motor")]
    public AudioClip engineSound;
    public float minPitch = 0.8f;
    public float maxPitch = 2.0f;
    public float engineVolume = 0.6f;
    private AudioSource engineAudio;

    [Header("Som de Freio")]
    public AudioClip brakeSound;
    public float brakeVolume = 0.8f;
    private AudioSource brakeAudio;
    private bool isBrakingSoundPlaying = false;

    [Header("Som de Nitro")]
    public AudioClip nitroSound;
    public float nitroVolume = 1f;
    private AudioSource nitroAudio;
    private bool isNitroActive = false;

    void Start()
    {
        currentSpeed = baseSpeed;

        // 🎧 Som do motor
        engineAudio = gameObject.AddComponent<AudioSource>();
        engineAudio.clip = engineSound;
        engineAudio.loop = true;
        engineAudio.volume = engineVolume;
        engineAudio.Play();

        // 🎧 Som de freio
        brakeAudio = gameObject.AddComponent<AudioSource>();
        brakeAudio.clip = brakeSound;
        brakeAudio.loop = false;
        brakeAudio.volume = brakeVolume;

        // 🎧 Som de nitro
        nitroAudio = gameObject.AddComponent<AudioSource>();
        nitroAudio.clip = nitroSound;
        nitroAudio.loop = true; // o som toca enquanto o nitro estiver ativo
        nitroAudio.volume = nitroVolume;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * currentSpeed * speedMultiplier * Time.deltaTime);

        if (!canControl)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, baseSpeed, brakingForce * Time.deltaTime);
            UpdateEngineSound();
            return;
        }

        bool isBraking = false;

        if (!slowingDown)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                currentSpeed += acceleration * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                currentSpeed -= brakingForce * Time.deltaTime;
                isBraking = true;
            }
            else
            {
                if (currentSpeed > baseSpeed)
                    currentSpeed -= acceleration * Time.deltaTime * 0.5f;
                else if (currentSpeed < baseSpeed)
                    currentSpeed += acceleration * Time.deltaTime * 0.5f;
            }
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, baseSpeed, brakingForce * Time.deltaTime);
        }

        currentSpeed = Mathf.Clamp(currentSpeed, baseSpeed, maxSpeed * speedMultiplier);

        UpdateEngineSound();
        HandleBrakeSound(isBraking);
        HandleNitroSound();
    }

    private void UpdateEngineSound()
    {
        if (engineAudio && engineAudio.isPlaying)
        {
            float t = (currentSpeed - baseSpeed) / (maxSpeed - baseSpeed);
            engineAudio.pitch = Mathf.Lerp(minPitch, maxPitch, t);
        }
    }

    private void HandleBrakeSound(bool isBraking)
    {
        if (isBraking)
        {
            if (!isBrakingSoundPlaying)
            {
                brakeAudio.Play();
                isBrakingSoundPlaying = true;
            }
        }
        else
        {
            if (isBrakingSoundPlaying && !brakeAudio.isPlaying)
            {
                isBrakingSoundPlaying = false;
            }
        }
    }

    private void HandleNitroSound()
    {
        bool shouldPlayNitro = speedMultiplier > 1.05f;

        if (shouldPlayNitro && !isNitroActive)
        {
            nitroAudio.Play();
            isNitroActive = true;
        }
        else if (!shouldPlayNitro && isNitroActive)
        {
            nitroAudio.Stop();
            isNitroActive = false;
        }
    }

    public float GetCurrentSpeed() => currentSpeed;

    public void ReduceSpeedOverTime(float rate)
    {
        currentSpeed = Mathf.MoveTowards(currentSpeed, baseSpeed, rate * Time.deltaTime);
    }

    public void SlowDownToBaseSpeed()
    {
        slowingDown = true;
        Invoke(nameof(StopSlowing), 2f);
    }

    private void StopSlowing() => slowingDown = false;

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = Mathf.Clamp(multiplier, 1f, 2f);

        // Atualiza o som do nitro imediatamente
        HandleNitroSound();
    }
}

