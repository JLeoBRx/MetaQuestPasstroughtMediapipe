using UnityEngine;
using UnityEngine.Events;
public class TriggerNameContainsEvent : MonoBehaviour
{
    [Header("Filtro")]
    [Tooltip("Parte do nome que o objeto deve conter.")]
    public string nameContains = "Hand";
    [Header("Eventos")]
    public UnityEvent OnTriggerEnterEvent;
    public UnityEvent OnTriggerExitEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains(nameContains))
        {
            OnTriggerEnterEvent?.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains(nameContains))
        {
            OnTriggerExitEvent?.Invoke();
        }
    }
}