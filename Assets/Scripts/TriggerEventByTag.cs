using UnityEngine;
using UnityEngine.Events;

public class TriggerEventByTag : MonoBehaviour
{
    public string triggerTag = "Ave";
    public UnityEvent onTriggerEnter;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(triggerTag))
        {
            onTriggerEnter.Invoke();
        }
    }
}
