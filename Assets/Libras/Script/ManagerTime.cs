using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ManagerTime : MonoBehaviour
{
    [Header("Tempo de validação (segundos)")]
    public float validationTime = 15f;

    [Header("Quantidade necessária")]
    public int requiredObjects = 6;

    [Header("Eventos")]
    public UnityEvent OnValidationStarted;
    public UnityEvent OnValidationPaused;
    public UnityEvent OnValidationResumed;
    public UnityEvent OnValidationCancelled;
    public UnityEvent OnValidationSuccess;

    private int activeCount;
    private Coroutine validationRoutine;

    private bool isValidating;
    private bool isPaused;

    private float currentTime;

    public void ObjectEnabled()
    {
        activeCount++;

        if (!isValidating && activeCount >= requiredObjects)
        {
            StartValidation();
        }
        else if (isValidating && isPaused && activeCount >= requiredObjects)
        {
            ResumeValidation();
        }
    }

    public void ObjectDisabled()
    {
        activeCount--;

        if (activeCount < 0)
            activeCount = 0;

        if (isValidating && activeCount < requiredObjects)
        {
            PauseValidation();
        }
    }

    private void StartValidation()
    {
        isValidating = true;
        isPaused = false;
        currentTime = 0f;

        OnValidationStarted?.Invoke();

        validationRoutine = StartCoroutine(ValidationCoroutine());
    }

    public void PauseValidation()
    {
        if (!isValidating || isPaused)
            return;

        isPaused = true;
        OnValidationPaused?.Invoke();
    }

    public void ResumeValidation()
    {
        if (!isValidating || !isPaused)
            return;

        if (activeCount >= requiredObjects)
        {
            isPaused = false;
            OnValidationResumed?.Invoke();
        }
    }

    public void CancelValidation()
    {
        if (!isValidating)
            return;

        isValidating = false;
        isPaused = false;

        if (validationRoutine != null)
        {
            StopCoroutine(validationRoutine);
            validationRoutine = null;
        }

        currentTime = 0f;

        OnValidationCancelled?.Invoke();
    }

    private IEnumerator ValidationCoroutine()
    {
        while (currentTime < validationTime)
        {
            if (!isPaused)
            {
                currentTime += Time.deltaTime;
            }

            yield return null;
        }

        isValidating = false;
        validationRoutine = null;

        OnValidationSuccess?.Invoke();
    }

    public void ResetValidation()
    {
        CancelValidation();
        activeCount = 0;
    }
}