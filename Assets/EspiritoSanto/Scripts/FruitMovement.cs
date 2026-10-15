using UnityEngine;

public class FruitMovement : MonoBehaviour
{
    [Header("Área de Movimento")]
    public float moveRadius = 0.20f;

    [Header("Velocidade")]
    public float moveSpeed = 1.5f;

    [Header("Tempo")]
    public float minMoveInterval = 1f;
    public float maxMoveInterval = 3f;

    [Header("Rotação")]
    public float rotationSpeed = 20f;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    private float nextMoveTime;

    private void Start()
    {
        startPosition = transform.position;
        PickNewTarget();
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime);

        if (Time.time >= nextMoveTime)
        {
            PickNewTarget();
        }
    }

    private void PickNewTarget()
    {
        Vector2 randomCircle = Random.insideUnitCircle * moveRadius;

        targetPosition =
            startPosition +
            new Vector3(
                randomCircle.x,
                Random.Range(-0.05f, 0.05f),
                randomCircle.y);

        nextMoveTime =
            Time.time +
            Random.Range(minMoveInterval, maxMoveInterval);
    }
}