using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerResourcesService : IPlayerResourcesService, IInitializable
{
    private readonly Dictionary<ResourcesType, ReactiveProperty<int>> _currencies = new();

    private readonly IReadOnlyDictionary<ResourcesType, int> _resourceInitialValues;
    private readonly IReadOnlyDictionary<ResourcesType, int> _resourceLimits;

    private Data _saveData;

    public string Filename;

    private readonly CompositeDisposable _disposables = new();
    private readonly ISaveService _saveService;

    [Inject]
    public PlayerResourcesService(
        string filename,
        IReadOnlyDictionary<ResourcesType, int> resourceInitialValues,
        IReadOnlyDictionary<ResourcesType, int> resourceLimits,
        ISaveService saveService)
    {
        Filename = filename;
        _resourceLimits = resourceLimits;
        _resourceInitialValues = resourceInitialValues;
        _saveService = saveService;
    }

    public void IncreaseValue(ResourcesType key, int value)
    {
        SetValue(key, GetValue(key).Value + value);
    }

    public void SetValue(ResourcesType key, int value)
    {
        if (TryGetLimit(key, out int limit))
        {
            _currencies[key].Value = Mathf.Clamp(value, 0, limit);
        }
        else
        {
            _currencies[key].Value = value;
        }
    }

    public bool TryDecreaseResource(ResourcesType resourceType, int value)
    {
        if (_currencies[resourceType].Value < value)
        {
            return false;
        }
        else
        {
            _currencies[resourceType].Value -= value;
            return true;
        }
    }

    public bool TryGetLimit(ResourcesType resourceType, out int limit)
    {
        limit = default;

        bool resourceLimitsNotNull = _resourceLimits != null;
        bool resourceLimitsTryGetValue = _resourceLimits.TryGetValue(resourceType, out limit);

        return resourceLimitsNotNull && resourceLimitsTryGetValue;
    }

    public bool ReachedLimit(ResourcesType resourceType)
        => TryGetLimit(resourceType, out int limit) && GetValue(resourceType).Value >= limit;

    public IReadOnlyReactiveProperty<int> GetValue(ResourcesType key)
    {
        return _currencies[key];
    }

    public void DecreaseValue(ResourcesType key, int value)
    {
        SetValue(key, GetValue(key).Value - value);
    }

    public int DecreaseSafe(ResourcesType key, int value = 1)
    {
        var newVal = Math.Max(0, GetValue(key).Value - value);
        SetValue(key, newVal);
        return newVal;
    }

    public bool EnoughResource(ResourcesType currencyType, int value)
    {
        return _currencies[currencyType].Value >= value;
    }

    public void Initialize()
    {
        foreach (ResourcesType item in Enum.GetValues(typeof(ResourcesType)))
        {
            _currencies[item] = new ReactiveProperty<int>();
        }

        if (_saveService.HasSaved(Filename))
        {
            _saveData = _saveService.Load<Data>(Filename);
            foreach (Entry c in _saveData.Currencies)
            {
                _currencies[c.CurrencyType].Value = c.Amount;
            }
        }
        else
        {
            _saveData = new Data();

            foreach (ResourcesType item in Enum.GetValues(typeof(ResourcesType)))
            {
                if (!_resourceInitialValues.TryGetValue(item, out var initVal))
                {
                    initVal = 0;
                }
                _saveData.Currencies.Add(new Entry() { CurrencyType = item, Amount = initVal });
            }
            _saveService.Save(_saveData, Filename);
        }

        for (int i = 0; i < _saveData.Currencies.Count; i++)
        {
            var entry = _saveData.Currencies[i];
            // synchronizing loaded data with map in memory
            _currencies[entry.CurrencyType].Value = entry.Amount;
            int index = i;
            _currencies[entry.CurrencyType].Subscribe(value =>
            {
                _saveData.Currencies[index].Amount = value;
                _saveService.Save(_saveData, Filename);
            }).AddTo(_disposables);
        }
    }
}

[Serializable]
public class Data
{
    public List<Entry> Currencies = new();
}

[Serializable]
public class Entry
{
    public ResourcesType CurrencyType;
    public int Amount;
}
