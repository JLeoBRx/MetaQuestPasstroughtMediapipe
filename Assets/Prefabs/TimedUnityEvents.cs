using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TimedUnityEvents : MonoBehaviour
{
    [System.Serializable]
    public class TimedEvent
    {
        [Tooltip("Tempo (em segundos) até o evento ser executado após Start()")]
        public float delay;

        [Tooltip("Evento a ser executado após o tempo definido")]
        public UnityEvent onTrigger;
    }

    [Header("Lista de eventos temporizados")]
    public TimedEvent[] timedEvents = new TimedEvent[10]; // até 10, mas pode ser qualquer número

    private void Start()
    {
        // Inicia uma coroutine para cada evento configurado
        foreach (var timedEvent in timedEvents)
        {
            if (timedEvent != null && timedEvent.onTrigger != null)
                StartCoroutine(ExecuteAfterDelay(timedEvent.delay, timedEvent.onTrigger));
        }
    }

    private IEnumerator ExecuteAfterDelay(float delay, UnityEvent evt)
    {
        yield return new WaitForSeconds(delay);
        evt.Invoke();
    }

    // ✅ Chamada manual opcional (caso queira disparar em outro momento)
    public void TriggerAllNow()
    {
        foreach (var e in timedEvents)
            e.onTrigger?.Invoke();
    }
}
