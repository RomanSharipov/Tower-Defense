using Cysharp.Threading.Tasks;
using UniRx;

public interface IPlayerResourcesService
{
    public IReadOnlyReactiveProperty<int> GetValue(ResourcesType currencyType);
    public void IncreaseValue(ResourcesType currencyType, int value);
    public void DecreaseValue(ResourcesType currencyType, int value);
    public bool TryDecreaseResource(ResourcesType currencyType, int value);
    public bool EnoughResource(ResourcesType currencyType, int value);
    public void SetValue(ResourcesType key, int value);
}
