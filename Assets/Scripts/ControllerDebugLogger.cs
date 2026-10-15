using UnityEngine;
using Oculus.Interaction.Input;

public class ControllerDebugLogger : MonoBehaviour
{
    [Header("Referências aos Controllers da Oculus Interaction")]
    [SerializeField] private Controller leftController;
    [SerializeField] private Controller rightController;

    void Update()
    {
        CheckController(leftController);
        CheckController(rightController);
    }

    private void CheckController(Controller controller)
    {
        if (controller == null || !controller.IsConnected) return;

        var input = controller.ControllerInput;

        // ✋ GRAB (Grip)
        if (controller.IsButtonUsageAnyActive(ControllerButtonUsage.GripButton))
        {
            Debug.Log($"{controller.Handedness} ➤ Grab pressionado");
        }

        // ☝️ TRIGGER
        if (controller.IsButtonUsageAnyActive(ControllerButtonUsage.TriggerButton))
        {
            Debug.Log($"{controller.Handedness} ➤ Trigger pressionado");
        }

        // 🔘 Botão A ou X
        if (controller.IsButtonUsageAnyActive(ControllerButtonUsage.PrimaryButton))
        {
            Debug.Log($"{controller.Handedness} ➤ Botão A/X pressionado");
        }

        // 🔘 Botão B ou Y
        if (controller.IsButtonUsageAnyActive(ControllerButtonUsage.SecondaryButton))
        {
            Debug.Log($"{controller.Handedness} ➤ Botão B/Y pressionado");
        }

        // 🕹️ Thumbstick (movimento)
        if (input.Primary2DAxis.magnitude > 0.1f)
        {
            Debug.Log($"{controller.Handedness} ➤ Thumbstick movido: {input.Primary2DAxis}");
        }
    }
}
