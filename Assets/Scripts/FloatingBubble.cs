using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FloatingBubble : MonoBehaviour
{
    public float floatSpeed = 0.5f;
    public float floatAmplitude = 0.5f;
    public float floatFrequency = 1f;

    private Vector3 startPosition;
    private float timeOffset;

    private void Start()
    {
        startPosition = transform.position;
        timeOffset = Random.Range(0f, 100f); // Para variar movimento entre bolhas
    }

    private void Update()
    {
        FloatMotion();
    }

    private void FloatMotion()
    {
        Vector3 offset = Vector3.up * Mathf.Sin((Time.time + timeOffset) * floatFrequency) * floatAmplitude;
        transform.position = startPosition + offset;
    }

    private void OnTriggerExit(Collider other)
    {
        // Só resetar se saiu do volume designado (ex: "BubbleArea")
        if (other.CompareTag("BubbleArea"))
        {
            transform.position = startPosition;
            timeOffset = Random.Range(0f, 100f); // Reinicia flutuação aleatória
        }
    }
}
