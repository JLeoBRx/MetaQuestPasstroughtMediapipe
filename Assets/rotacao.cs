using UnityEngine;

public class rotacao : MonoBehaviour
{
    [Tooltip("Velocidade de rotação em graus por segundo")]
    public float rotationSpeed = 10f;

    void Update()
    {
        // Gira o objeto em torno do eixo Y (vertical)
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.Self);
    }
}
