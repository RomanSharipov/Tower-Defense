using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Installers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[CreateAssetMenu(fileName = "PlayerResourcesServiceInstaller",
    menuName = "Scriptable Installers/PlayerResourcesServiceInstaller")]
public class PlayerResourcesServiceInstaller : AScriptableInstaller
{
    [SerializeField]
    private string _userResourcesTrackingFileName = "UserResources";

    [SerializeField, Tooltip("If some resource initial value should not be 0, define it here")]
    private UserResourceValue[] _resourceInitialValues;

    [SerializeField, Tooltip("If some resource must have a limit, define it here")]
    private UserResourceValue[] _resourceLimits;

    public override void Install(IContainerBuilder builder)
    {
        builder.Register<PlayerResourcesService>(Lifetime.Singleton)
            .WithParameter(_userResourcesTrackingFileName)
            .WithParameter("resourceInitialValues", getUserResourceLimitMap(_resourceInitialValues))
            .WithParameter("resourceLimits", getUserResourceLimitMap(_resourceLimits))
            .As<IPlayerResourcesService>()
            .As<IInitializable>();
    }

    private IReadOnlyDictionary<ResourcesType, int> getUserResourceLimitMap(UserResourceValue[] resourceValues)
    {
        Dictionary<ResourcesType, int> valMap = new();
        if (resourceValues != null)
        {
            foreach (var val in resourceValues)
            {
                valMap[val.PlayerResourceType] = val.Value;
            }
        }
        return valMap;
    }

    [Serializable]
    public class UserResourceValue
    {
        public ResourcesType PlayerResourceType;
        public int Value;
    }
}