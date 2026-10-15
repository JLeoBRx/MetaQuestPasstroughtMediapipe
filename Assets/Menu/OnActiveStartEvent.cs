using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class OnActiveStartEvent : MonoBehaviour
{
    [Header("Event Settings")]
    [SerializeField] private float delay = 0f;

    [SerializeField] private UnityEvent onActiveStart;

    private Coroutine eventRoutine;

    private void OnEnable()
    {
        if (eventRoutine != null)
            StopCoroutine(eventRoutine);

        eventRoutine = StartCoroutine(InvokeEvent());
    }

    private IEnumerator InvokeEvent()
    {
        if (delay > 0f)
            yield return new WaitForSeconds(delay);

        onActiveStart?.Invoke();

        eventRoutine = null;
    }
}