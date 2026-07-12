using UnityEngine;
using UnityEngine.InputSystem;
using Adobe.Substance.Runtime;
using Adobe.Substance;
using UnityEngine.UIElements;
using R3;
using System;
using VContainer;
using UnityEngine.InputSystem.LowLevel;

public class SubstanceMaterialControler : MonoBehaviour
{
    [SerializeField] private SubstanceRuntimeGraph substanceGraph;
    [SerializeField] private SubstanceGraphSO substanceGraphAsset;
    [SerializeField] private string inputName = "x_amount"; // Substance Designer側のIdentifier

    public void SetMaterialState(int value)
    {
        substanceGraph.SetInputInt(inputName, value);
        substanceGraph.RenderAsync();
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
}
