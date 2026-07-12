using R3;
using UnityEngine;
using UnityEngine.UIElements;

public class SubstanceUIDocument : MonoBehaviour
{
  [SerializeField]
  private UIDocument uIDocument;

  private SliderInt stripeSlider;

  private ReactiveProperty<int> stripeChanged = new();
  public ReadOnlyReactiveProperty<int> StripeChanged => stripeChanged;

  void OnEnable()
  {
    var root = uIDocument.rootVisualElement;
    stripeSlider = root.Q<SliderInt>("slider-stripe");
    stripeSlider.RegisterValueChangedCallback(OnStripeChanged);
    stripeChanged.OnNext(stripeSlider.value);
  }

  void OnDisable()
  {
    stripeSlider.UnregisterValueChangedCallback(OnStripeChanged);
  }

  void OnDestroy()
  {
    stripeChanged.Dispose();
  }

  void OnStripeChanged(ChangeEvent<int> evt)
  {
    stripeChanged.OnNext(evt.newValue);
  }
}