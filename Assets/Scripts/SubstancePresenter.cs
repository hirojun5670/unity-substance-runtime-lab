
using System;
using System.Diagnostics;
using R3;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using VContainer.Unity;

public class SubstancePresenter : IStartable, IDisposable
{
  private readonly SubstanceMaterialControler substanceMaterialControler;
  private readonly SubstanceUIDocument substanceUIDocument;
  private readonly CompositeDisposable disposables = new();

  public SubstancePresenter(
    SubstanceMaterialControler substanceMaterialControler,
    SubstanceUIDocument substanceUIDocument
  )
  {
    this.substanceMaterialControler = substanceMaterialControler;
    this.substanceUIDocument = substanceUIDocument;
  }
  public void Start()
  {
    substanceUIDocument.RandomButtonClicked
      .ThrottleLast(TimeSpan.FromMilliseconds(100))
      .Subscribe(value =>
      {
        substanceMaterialControler.SetRandomSeed();
      })
      .AddTo(disposables);

    substanceUIDocument.DisplacementChanged
      .Subscribe(value =>
      {
        substanceMaterialControler.SetDisplacement(value);
      })
      .AddTo(disposables);

    substanceUIDocument.RoughnessChanged
      .ThrottleLast(TimeSpan.FromMilliseconds(100))
      .Subscribe(value =>
      {
        substanceMaterialControler.SetRoughness(value);
      })
      .AddTo(disposables);

    substanceUIDocument.NormalChanged
      .ThrottleLast(TimeSpan.FromMilliseconds(100))
      .Subscribe(value =>
      {
        substanceMaterialControler.SetNormal(value);
      })
      .AddTo(disposables);

    substanceUIDocument.MaskChanged
      .ThrottleLast(TimeSpan.FromMilliseconds(100))
      .Subscribe(value =>
      {
        substanceMaterialControler.SetMask(value);
      })
      .AddTo(disposables);
  }

  public void Dispose()
  {
    disposables.Dispose();
  }
}