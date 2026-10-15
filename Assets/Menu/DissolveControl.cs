
using UnityEngine;
using System.Collections;

public class DissolveControl : MonoBehaviour
{
    [Header("Material")]
    [SerializeField] private Renderer targetRenderer;

    [Header("Skybox")]
    [SerializeField] private GameObject skyboxObject;

    [Header("Dissolve")]
    [SerializeField] private float duration = 1f;

    [Header("Initial State")]
    [SerializeField] private bool startDissolved = false;

    private Material material;
    private Coroutine currentRoutine;

    private bool isDissolved;

    private static readonly int DissolveID =
        Shader.PropertyToID("_Dissolve");

    private void Awake()
    {
        if (targetRenderer == null)
            targetRenderer = GetComponent<Renderer>();

        if (targetRenderer == null)
        {
            Debug.LogError(
                "DissolveControl: Nenhum Renderer encontrado.",
                this
            );
            return;
        }

        material = targetRenderer.material;

        isDissolved = startDissolved;

        material.SetFloat(
            DissolveID,
            isDissolved ? 1f : 0f
        );

        if (skyboxObject != null)
            skyboxObject.SetActive(!isDissolved);
    }

    public void ToggleDissolve()
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        if (isDissolved)
        {
            currentRoutine = StartCoroutine(ReconstituteSequence());
        }
        else
        {
            currentRoutine = StartCoroutine(DissolveSequence());
        }
    }

    private IEnumerator DissolveSequence()
    {
        // Dissolve
        yield return StartCoroutine(
            DissolveRoutine(1f)
        );

        // Garante que terminou antes de esconder o Skybox
        isDissolved = true;

        // Esconde o Skybox para revelar o passthrough
        if (skyboxObject != null)
            skyboxObject.SetActive(false);

        currentRoutine = null;
    }


private IEnumerator ReconstituteSequence()
    {
        // Mostra o Skybox primeiro
        if (skyboxObject != null)
            skyboxObject.SetActive(true);

        // Depois começa a reconstituição
        yield return StartCoroutine(
            DissolveRoutine(0f)
        );

        // Garante que terminou
        isDissolved = false;

        currentRoutine = null;
    }



    private IEnumerator DissolveRoutine(float target)
    {
        float start = material.GetFloat(DissolveID);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(
                elapsed / duration
            );

            t = Mathf.SmoothStep(
                0f,
                1f,
                t
            );

            float value = Mathf.Lerp(
                start,
                target,
                t
            );

            material.SetFloat(
                DissolveID,
                value
            );

            yield return null;
        }

        material.SetFloat(
            DissolveID,
            target
        );
    }
}

