using UnityEngine;
using UnityEngine.Events;

public class TriggerMultipleNameContain : MonoBehaviour
{
    [Header("Filtro")]
    [Tooltip("Se qualquer um destes textos estiver no nome do objeto, o evento será acionado.")]
    public string[] nameContainsM;

    [Header("Eventos")]
    public UnityEvent OnTriggerEnterEvent;
    public UnityEvent OnTriggerExitEvent;

    private bool IsValidName(string objectName)
    {
        if (nameContainsM == null || nameContainsM.Length == 0)
            return false;

        foreach (string filter in nameContainsM)
        {
            if (!string.IsNullOrWhiteSpace(filter) && objectName.Contains(filter))
                return true;
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsValidName(other.gameObject.name))
        {
            OnTriggerEnterEvent?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsValidName(other.gameObject.name))
        {
            OnTriggerExitEvent?.Invoke();
        }
    }
}