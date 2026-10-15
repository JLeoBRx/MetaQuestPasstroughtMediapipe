using UnityEngine;

public class ActiveNotifier : MonoBehaviour
{
    [Header("Referências")]
    public ActiveObjectsCounter counter;
    public ManagerTime managerTime;

    private void OnEnable()
    {
        counter?.ObjectEnabled();
        managerTime?.ObjectEnabled();
    }

    private void OnDisable()
    {
        Debug.Log($"{name} -> OnDisable");

        counter?.ObjectDisabled();
        managerTime?.ObjectDisabled();
    }
}