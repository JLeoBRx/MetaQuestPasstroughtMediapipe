using UnityEngine;

public class HandTrackingAlarm : MonoBehaviour
{
    [Header("Hands")]
    [SerializeField] private OVRHand leftHand;
    [SerializeField] private OVRHand rightHand;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    private bool alarmPlaying = false;

    private void Awake()
    {
        if (audioSource != null)
        {
            audioSource.loop = true;
            audioSource.playOnAwake = false;
        }
    }

    private void Update()
    {
        if (leftHand == null || rightHand == null || audioSource == null)
            return;

        // Alarme ativo se qualquer uma das mãos perder o tracking
        bool trackingLost = !leftHand.IsTracked || !rightHand.IsTracked;

        if (trackingLost)
        {
            if (!alarmPlaying)
            {
                audioSource.Play();
                alarmPlaying = true;
            }
        }
        else
        {
            if (alarmPlaying)
            {
                audioSource.Stop();
                alarmPlaying = false;
            }
        }
    }
}