using R3;
using UnityEngine;
using UnityEngine.UIElements;

public class SubstanceUIDocument : MonoBehaviour
{
  [SerializeField]
  private UIDocument uIDocument;

  private readonly string buttunRandomName = "buttun-random";
  private readonly string sliderDisplacementName = "slider-displacement";
  private readonly string sliderRoughnessName = "slider-roughness";
  private readonly string sliderNormalName = "slider-normal";
  private readonly string sliderMaskName = "slider-mask";


  private Button randomButton;
  private Slider displacementSlider;
  private Slider roughnessSlider;
  private Slider normalSlider;
  private Slider maskSlider;

  private readonly ReactiveProperty<float> displacementChanged = new();
  private readonly ReactiveProperty<float> roughnessChanged = new();
  private readonly ReactiveProperty<float> normalChanged = new();
  private readonly ReactiveProperty<float> maskChanged = new();

  private readonly Subject<Unit> randomButtonClicked = new();
  public Observable<Unit> RandomButtonClicked => randomButtonClicked;
  public ReadOnlyReactiveProperty<float> DisplacementChanged => displacementChanged;
  public ReadOnlyReactiveProperty<float> RoughnessChanged => roughnessChanged;
  public ReadOnlyReactiveProperty<float> NormalChanged => normalChanged;
  public ReadOnlyReactiveProperty<float> MaskChanged => maskChanged;



  private ReactiveProperty<int> stripeChanged = new();
  public ReadOnlyReactiveProperty<int> StripeChanged => stripeChanged;

  void OnEnable()
  {
    var root = uIDocument.rootVisualElement;
    randomButton = root.Q<Button>(buttunRandomName);
    randomButton.clicked += OnRandomButtonClicked;

    displacementSlider = root.Q<Slider>(sliderDisplacementName);
    displacementSlider.RegisterValueChangedCallback(OnDisplacementChanged);
    displacementChanged.OnNext(displacementSlider.value);

    roughnessSlider = root.Q<Slider>(sliderRoughnessName);
    roughnessSlider.RegisterValueChangedCallback(OnRoughnessChanged);
    roughnessChanged.OnNext(roughnessSlider.value);

    normalSlider = root.Q<Slider>(sliderNormalName);
    normalSlider.RegisterValueChangedCallback(OnNormalChanged);
    normalChanged.OnNext(normalSlider.value);

    maskSlider = root.Q<Slider>(sliderMaskName);
    maskSlider.RegisterValueChangedCallback(OnMaskChanged);
    maskChanged.OnNext(maskSlider.value);
  }

  void OnDisable()
  {
    randomButton.clicked -= OnRandomButtonClicked;
    displacementSlider.UnregisterValueChangedCallback(OnDisplacementChanged);
    roughnessSlider.UnregisterValueChangedCallback(OnRoughnessChanged);
    normalSlider.UnregisterValueChangedCallback(OnNormalChanged);
    maskSlider.UnregisterValueChangedCallback(OnMaskChanged);
  }

  void OnDestroy()
  {
    displacementChanged.Dispose();
    roughnessChanged.Dispose();
    normalChanged.Dispose();
    maskChanged.Dispose();
  }

  void OnRandomButtonClicked()
  {
    randomButtonClicked.OnNext(Unit.Default);
  }

  void OnDisplacementChanged(ChangeEvent<float> evt)
  {
    displacementChanged.OnNext(evt.newValue);
  }

  void OnRoughnessChanged(ChangeEvent<float> evt)
  {
    roughnessChanged.OnNext(evt.newValue);
  }

  void OnNormalChanged(ChangeEvent<float> evt)
  {
    normalChanged.OnNext(evt.newValue);
  }

  void OnMaskChanged(ChangeEvent<float> evt)
  {
    maskChanged.OnNext(evt.newValue);
  }
}