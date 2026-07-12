using VContainer;
using VContainer.Unity;
public class SubstanceLifetimeScope : LifetimeScope
{
  protected override void Configure(IContainerBuilder builder)
  {
    builder.RegisterEntryPoint<SubstancePresenter>();
    builder.RegisterComponentInHierarchy<SubstanceMaterialControler>();
    builder.RegisterComponentInHierarchy<SubstanceUIDocument>();
  }
}
