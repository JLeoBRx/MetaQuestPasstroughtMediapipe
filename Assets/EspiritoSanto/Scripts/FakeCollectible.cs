using UnityEngine;

public class FakeCollectible : MonoBehaviour
{
    private bool collected;

    private void OnTriggerEnter(Collider other)
    {
        if (collected)
            return;

        if (!other.CompareTag("Hand"))
            return;

        collected = true;

        FruitGameManager.Instance.WrongFruit();

        gameObject.SetActive(false);
    }
}