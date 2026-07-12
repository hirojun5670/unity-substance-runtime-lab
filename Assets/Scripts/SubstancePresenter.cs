
using System;
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
    substanceUIDocument.StripeChanged
      .ThrottleLast(TimeSpan.FromMilliseconds(100))
      .Subscribe(value => substanceMaterialControler.SetMaterialState(value))
      .AddTo(disposables);
  }

  public void Dispose()
  {
    disposables.Dispose();
  }
}