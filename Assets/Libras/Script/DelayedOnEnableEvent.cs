using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DelayedOnEnableEvent : MonoBehaviour
{
    [Header("Configuração")]
    [Min(0)]
    public float delay = 1f;

    [Header("Evento")]
    public UnityEvent OnDelayedEnable;

    private Coroutine routine;

    private void OnEnable()
    {
        routine = StartCoroutine(InvokeEvent());
    }

    private void OnDisable()
    {
        if (routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }
    }

    private IEnumerator InvokeEvent()
    {
        yield return new WaitForSeconds(delay);
        OnDelayedEnable?.Invoke();
    }
}