using UnityEngine;
using Oculus.Interaction.Input;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
    public float walkSpeed = 2.0f;
    public float sprintSpeed = 5.0f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;
    public float fallThresholdY = 0.5f;

    private CharacterController controller;
    private Animator animator;
    private Vector3 velocity;
    private bool isGrounded;

    [SerializeField] private Transform[] pontosDeRespawn;
    [Header("Referência aos Controladores VR")]
    public Controller leftController;
    public Controller rightController;

private bool jumpButtonWasPressedLastFrame = false;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        GameObject[] pontos = GameObject.FindGameObjectsWithTag("RespawnPoint");
        pontosDeRespawn = new Transform[pontos.Length];
        for (int i = 0; i < pontos.Length; i++)
        {
            pontosDeRespawn[i] = pontos[i].transform;
        }
    }

    void Update()
    {
        bool isSafe;
        Vector3 groundPoint;
        isGrounded = CustomIsGrounded(out isSafe, out groundPoint);
        animator.SetBool("Grounded", isGrounded);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            animator.SetBool("FreeFall", false);
        }

        if (transform.position.y < fallThresholdY)
        {
            Respawn();
            return;
        }

        Vector2 inputAxis = GetMovementInput();
        Vector3 input = new Vector3(inputAxis.x, 0f, inputAxis.y);
        input = input.normalized;

        float speed = IsSprinting() ? sprintSpeed : walkSpeed;
        float moveAmount = input.magnitude;
        float targetSpeed = moveAmount > 0 ? (IsSprinting() ? 6f : 2f) : 0f;

        animator.SetFloat("Speed", targetSpeed);
        animator.SetFloat("MotionSpeed", moveAmount);

        if (input != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(input);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        controller.Move(input * speed * Time.deltaTime);

bool jumpPressedNow = IsJumpPressed();

if (isGrounded)
{
    if (jumpPressedNow && !jumpButtonWasPressedLastFrame)
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        animator.SetBool("Jump", true);
    }
    else
    {
        animator.SetBool("Jump", false);
    }
}

// Atualiza o estado para o próximo frame
jumpButtonWasPressedLastFrame = jumpPressedNow;


        if (!isGrounded && velocity.y < 0)
        {
            animator.SetBool("FreeFall", true);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    Vector2 GetMovementInput()
    {
        Vector2 leftThumbstick = leftController != null ? leftController.ControllerInput.Primary2DAxis : Vector2.zero;
        return leftThumbstick;
    }

    bool IsJumpPressed()
    {
        return rightController != null && rightController.IsButtonUsageAnyActive(ControllerButtonUsage.PrimaryButton);
    }

   bool IsSprinting()
{
    return rightController != null && rightController.IsButtonUsageAnyActive(ControllerButtonUsage.SecondaryButton);
}

    void Respawn()
    {
        Debug.Log("\u26d4 Caiu! Procurando ponto de respawn mais próximo...");

        Transform pontoMaisProximo = null;
        float menorDistancia = Mathf.Infinity;

        foreach (Transform ponto in pontosDeRespawn)
        {
            float distancia = Vector3.Distance(transform.position, ponto.position);
            if (distancia < menorDistancia)
            {
                menorDistancia = distancia;
                pontoMaisProximo = ponto;
            }
        }

        if (pontoMaisProximo != null)
        {
            velocity = Vector3.zero;
            controller.enabled = false;
            transform.position = pontoMaisProximo.position + Vector3.up * 1f;
            controller.enabled = true;
            Debug.Log("\u2705 Respawnado em: " + pontoMaisProximo.name);
        }
        else
        {
            Debug.LogWarning("\u26a0\ufe0f Nenhum ponto de respawn encontrado!");
        }
    }

    bool CustomIsGrounded(out bool isSafeGround, out Vector3 groundHitPoint)
    {
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        float rayDistance = 0.2f;
        RaycastHit hit;

        Debug.DrawRay(origin, Vector3.down * rayDistance, Color.yellow);

        if (Physics.Raycast(origin, Vector3.down, out hit, rayDistance))
        {
            float groundAngle = Vector3.Angle(hit.normal, Vector3.up);
            isSafeGround = groundAngle < 30f;
            groundHitPoint = hit.point;
            return true;
        }

        isSafeGround = false;
        groundHitPoint = Vector3.zero;
        return false;
    }

    void OnFootstep(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            // Som de passo opcional
        }
    }

    void OnLand(AnimationEvent animationEvent)
    {
        Debug.Log("Aterrissou");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        if (pontosDeRespawn != null)
        {
            foreach (Transform ponto in pontosDeRespawn)
            {
                if (ponto != null)
                    Gizmos.DrawSphere(ponto.position, 0.2f);
            }
        }
    }
}