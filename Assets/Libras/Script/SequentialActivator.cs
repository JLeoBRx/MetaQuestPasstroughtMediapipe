using UnityEngine;

public class SequentialActivator : MonoBehaviour
{
    [Header("Objetos na ordem")]
    public GameObject[] objects;

    private int currentIndex = 0;

    public void Next()
    {
        if (currentIndex >= objects.Length)
            return;

        if (objects[currentIndex] != null)
        {
            objects[currentIndex].SetActive(true);
        }

        currentIndex++;
    }

    public void ResetSequence()
    {
        currentIndex = 0;
    }
}