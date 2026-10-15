using UnityEngine;
using UnityEngine.Events;

public class ActiveObjectsCounter : MonoBehaviour
{
    [Header("Quantidade esperada")]
    public int totalObjects = 6;

    [Header("Debug")]
    [SerializeField] private int activeCount;

    [Header("Eventos")]
    public UnityEvent OnAllObjectsActive;
    public UnityEvent OnNotAllObjectsActive;

    private bool allActive;

    public void ObjectEnabled()
    {
        activeCount++;

        Debug.Log($"Objetos ativos: {activeCount}");

        if (!allActive && activeCount >= totalObjects)
        {
            allActive = true;
            OnAllObjectsActive?.Invoke();
        }
    }

    public void ObjectDisabled()
    {
        activeCount--;

        if (activeCount < 0)
            activeCount = 0;

        Debug.Log($"Objetos ativos: {activeCount}");

        if (allActive && activeCount < totalObjects)
        {
            allActive = false;
            OnNotAllObjectsActive?.Invoke();
        }
    }

    public int GetActiveCount()
    {
        return activeCount;
    }

    public bool AllActive()
    {
        return activeCount >= totalObjects;
    }
}