using System;
using System.Collections;
using UnityEngine;

public class PlayerPowerups : MonoBehaviour
{
    [Header("Shield")]
    [SerializeField, Min(0f)] private float shieldDurationSeconds = 8f;
    [SerializeField] private GameObject shieldVisualPrefab;
    [SerializeField, Min(0f)] private float shieldBlinkWarningSeconds = 2f;
    [SerializeField, Min(0.01f)] private float shieldBlinkIntervalSeconds = 0.15f;

    private const string ShieldPowerupPrefabName = "ShieldPowerup";

    private Coroutine shieldTimerRoutine;
    private GameObject shieldVisualInstance;
    private SpriteRenderer[] shieldRenderers;
    private PlayerAudio playerAudio;

    public bool IsShieldActive { get; private set; }

    private void Awake()
    {
        playerAudio = GetComponent<PlayerAudio>();
        InitializeShieldVisual();
        SetShieldActive(false);
    }

    private void LateUpdate()
    {
        if (shieldVisualInstance == null || !shieldVisualInstance.activeSelf)
        {
            return;
        }

        // Keep the visual glued to player transform.
        shieldVisualInstance.transform.localPosition = Vector3.zero;
        shieldVisualInstance.transform.localRotation = Quaternion.identity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsShieldPowerup(other))
        {
            return;
        }

        ActivateShield(shieldDurationSeconds);
        if (playerAudio != null)
        {
            playerAudio.PlayPowerupPickupSound();
        }
        Destroy(other.gameObject);
    }

    public bool HasActiveShield()
    {
        return IsShieldActive;
    }

    private void ActivateShield(float duration)
    {
        SetShieldActive(true);

        if (shieldTimerRoutine != null)
        {
            StopCoroutine(shieldTimerRoutine);
        }

        shieldTimerRoutine = StartCoroutine(ShieldTimer(duration));
    }

    private IEnumerator ShieldTimer(float duration)
    {
        float warningDuration = Mathf.Clamp(shieldBlinkWarningSeconds, 0f, duration);
        float stableDuration = duration - warningDuration;

        if (stableDuration > 0f)
        {
            yield return new WaitForSeconds(stableDuration);
        }

        if (warningDuration > 0f)
        {
            float remaining = warningDuration;
            bool isVisible = true;

            while (remaining > 0f)
            {
                isVisible = !isVisible;
                SetShieldVisualVisible(isVisible);

                float waitTime = Mathf.Min(shieldBlinkIntervalSeconds, remaining);
                if (waitTime <= 0f)
                {
                    break;
                }

                yield return new WaitForSeconds(waitTime);
                remaining -= waitTime;
            }

            SetShieldVisualVisible(true);
        }

        SetShieldActive(false);
        shieldTimerRoutine = null;
    }

    private void SetShieldActive(bool isActive)
    {
        IsShieldActive = isActive;
        if (shieldVisualInstance != null)
        {
            shieldVisualInstance.SetActive(isActive);
            if (isActive)
            {
                SetShieldVisualVisible(true);
            }
        }
    }

    private void InitializeShieldVisual()
    {
        if (shieldVisualPrefab == null)
        {
            return;
        }

        shieldVisualInstance = Instantiate(shieldVisualPrefab, transform);
        shieldVisualInstance.transform.localPosition = Vector3.zero;
        shieldVisualInstance.transform.localRotation = Quaternion.identity;
        shieldRenderers = shieldVisualInstance.GetComponentsInChildren<SpriteRenderer>(true);

        Shield shieldComponent = shieldVisualInstance.GetComponent<Shield>();
        if (shieldComponent == null)
        {
            shieldComponent = shieldVisualInstance.AddComponent<Shield>();
        }
        shieldComponent.Initialize(this);
    }

    private void SetShieldVisualVisible(bool isVisible)
    {
        if (shieldVisualInstance == null)
        {
            return;
        }

        if (shieldRenderers != null && shieldRenderers.Length > 0)
        {
            for (int i = 0; i < shieldRenderers.Length; i++)
            {
                shieldRenderers[i].enabled = isVisible;
            }
            return;
        }

        shieldVisualInstance.SetActive(isVisible);
    }

    private static bool IsShieldPowerup(Collider2D other)
    {
        return other.gameObject.name.StartsWith(ShieldPowerupPrefabName, StringComparison.Ordinal);
    }
}
