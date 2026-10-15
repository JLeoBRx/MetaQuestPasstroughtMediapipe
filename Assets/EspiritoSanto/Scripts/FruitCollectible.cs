using UnityEngine;

public class FruitCollectible : MonoBehaviour
{
    private bool collected;

    private void OnTriggerEnter(Collider other)
    {
        if (collected)
            return;

        if (!other.CompareTag("Hand"))
            return;

        collected = true;
        Debug.Log("Coletou um");

        FruitGameManager.Instance.CollectFruit();

        gameObject.SetActive(false);
    }
}