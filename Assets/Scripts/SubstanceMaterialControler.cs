using UnityEngine;
using UnityEngine.InputSystem;
using Adobe.Substance.Runtime;
using Adobe.Substance;
using UnityEngine.UIElements;
using R3;
using System;
using VContainer;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Experimental.Rendering;

public class SubstanceMaterialControler : MonoBehaviour
{
    [SerializeField] private SubstanceRuntimeGraph substanceGraph;
    [SerializeField] private SubstanceGraphSO substanceGraphAsset;
    [SerializeField] private Material substanceMaterial;

    // Identifier
    private readonly string seedIdentifier = "seed";
    private readonly string roughnessIdentifier = "roughness_intensity";
    private readonly string normalIdentifier = "normal_intensity";
    private readonly string maskIdentifier = "stone_mask";

    private readonly string displacementIdentifier = "_DisplacementStrength";

    public void SetSeed(int value)
    {
        Debug.Log("SetSeed : " + value);
        substanceGraph.SetInputInt(seedIdentifier, value);
        substanceGraph.RenderAsync();
    }

    public void SetRandomSeed()
    {
        SetSeed(UnityEngine.Random.Range(0, 5000));
    }

    public void SetDisplacement(float value)
    {
        substanceMaterial.SetFloat(displacementIdentifier, value);
    }

    public void SetRoughness(float value)
    {
        substanceGraph.SetInputFloat(roughnessIdentifier, value);
        substanceGraph.RenderAsync();
    }

    public void SetNormal(float value)
    {
        substanceGraph.SetInputFloat(normalIdentifier, value);
        substanceGraph.RenderAsync();
    }

    public void SetMask(float value)
    {
        substanceGraph.SetInputFloat(maskIdentifier, value);
        substanceGraph.RenderAsync();
    }

    void OnEnable()
    {
        //LogAllOutputNames();
        //LogAllInputNames();
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

    void LogAllOutputNames()
    {
        if (substanceGraphAsset == null || substanceGraphAsset.Output == null)
        {
            Debug.LogWarning("SubstanceGraphSO未設定、またはOutputが空です");
            return;
        }

        foreach (var output in substanceGraphAsset.Output)
        {
            Debug.Log($"Identifier: {output.Description.Identifier} / Label: {output.Description.Label}");
        }
    }
}
