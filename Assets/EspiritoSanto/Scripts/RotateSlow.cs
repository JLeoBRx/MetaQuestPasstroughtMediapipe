using UnityEngine;

public class RotateSlow : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 15f;

    [Header("Float")]
    [SerializeField] private float floatSpeed = 1f;
    [SerializeField] private float floatHeight = 0.05f;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        // Rotação
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);

        // Flutuação
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = startPos + Vector3.up * yOffset;
    }
}