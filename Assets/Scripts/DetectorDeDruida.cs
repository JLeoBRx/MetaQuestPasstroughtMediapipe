using UnityEngine;
using UnityEngine.Events;

public class DetectorDeDruida : MonoBehaviour
{
    [Header("Evento disparado quando um Druida entra")]
    public UnityEvent aoEntrarDruida;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Druida"))
        {
            Debug.Log("🧙 Druida entrou na área!");
            aoEntrarDruida?.Invoke();
        }
    }
}
