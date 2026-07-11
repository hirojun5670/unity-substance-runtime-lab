using UnityEngine;
using UnityEngine.InputSystem;
using Adobe.Substance.Runtime;
using Adobe.Substance;
using UnityEngine.UIElements;
using R3;
using System;

public class SubstanceMaterialControler : MonoBehaviour
{
    [SerializeField] private SubstanceRuntimeGraph substanceGraph;
    [SerializeField] private SubstanceGraphSO substanceGraphAsset;
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private string inputName = "x_amount"; // Substance Designer側のIdentifier
    [SerializeField] private int step = 1;
    [SerializeField] private int minValue = 1;
    [SerializeField] private int maxValue = 10;

    private readonly Subject<int> stripeChanged = new();
    private readonly CompositeDisposable disposables = new();


    private SliderInt stripeSlider;


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

    void Start()
    {
        LogAllInputNames();
    }

    void OnEnable()
    {
        var root = uiDocument.rootVisualElement;
        stripeSlider = root.Q<SliderInt>("slider-stripe");
        stripeSlider.RegisterValueChangedCallback(OnStripeChanged);

        stripeChanged
            .ThrottleLast(TimeSpan.FromMilliseconds(100)) // 100ms間隔に間引く
            .Subscribe(value =>
            {
                substanceGraph.SetInputInt("x_amount", value);
                substanceGraph.RenderAsync();
                Debug.Log($"x_amount updated: {value}");
            })
            .AddTo(disposables);
    }

    void OnDisable()
    {
        stripeSlider.UnregisterValueChangedCallback(OnStripeChanged);
        disposables.Clear();
    }

    void OnDestroy()
    {
        disposables.Dispose();
        stripeChanged.Dispose();
    }

    void OnStripeChanged(ChangeEvent<int> evt)
    {
        stripeChanged.OnNext(evt.newValue);
    }
}
