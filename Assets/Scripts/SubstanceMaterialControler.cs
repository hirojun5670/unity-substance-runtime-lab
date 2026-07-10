using UnityEngine;
using UnityEngine.InputSystem;
using Adobe.Substance.Runtime;
using Adobe.Substance;

public class SubstanceMaterialControler : MonoBehaviour
{
    [SerializeField] private SubstanceRuntimeGraph substanceGraph;
    [SerializeField] private SubstanceGraphSO substanceGraphAsset;
    [SerializeField] private string inputName = "x_amount"; // Substance Designer側のIdentifier
    [SerializeField] private int step = 1;
    [SerializeField] private int minValue = 1;
    [SerializeField] private int maxValue = 10;

    void Start()
    {
        LogAllInputNames();
    }

    void LogAllInputNames()
    {
        if (substanceGraphAsset == null || substanceGraphAsset.Input == null)
        {
            Debug.LogWarning("SubstanceGraphSO未設定、またはInputが空です");
            return;
        }

        foreach (var input in substanceGraphAsset.Input)
        {
            Debug.Log($"Identifier: {input.Description.Identifier} / Type: {input.Description.Type} / Label: {input.Description.Label}");
        }
    }

    void Update()
    {
        if (substanceGraph == null) return;
        if (Keyboard.current == null) return; // キーボード未接続時のガード

        int delta = 0;
        if (Keyboard.current.upArrowKey.wasPressedThisFrame) delta = step;
        if (Keyboard.current.downArrowKey.wasPressedThisFrame) delta = -step;

        if (delta != 0f)
        {
            int current = substanceGraph.GetInputInt(inputName);
            int next = current + delta;

            substanceGraph.SetInputInt(inputName, next);
            substanceGraph.RenderAsync();

            Debug.Log($"{inputName}: {next:F2}");
        }
    }
}
