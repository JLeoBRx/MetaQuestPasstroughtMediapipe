using UnityEngine;
using UnityEngine.Events;

public class ColherDetector : MonoBehaviour
{
    [Header("Evento a ser chamado ao tocar na letra 'jota'")]
    public UnityEvent aoTocarLetraJota;

    [Tooltip("Nome exato do objeto alvo (case-insensitive)")]
    public string nomeLetraAlvo = "jota";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Letras") && other.name.ToLower() == nomeLetraAlvo.ToLower())
        {
            Debug.Log("✅ Letra JOTA detectada pela colher!");
            aoTocarLetraJota?.Invoke();
        }
    }
}
