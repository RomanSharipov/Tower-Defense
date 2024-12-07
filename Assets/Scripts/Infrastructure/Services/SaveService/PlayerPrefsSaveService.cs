using UnityEngine;
using VContainer;
using Newtonsoft.Json;

public class PlayerPrefsSaveService : ISaveService
{
    [Inject]
    public PlayerPrefsSaveService()
    {

    }
    public bool HasSaved(string key)
    {
        return PlayerPrefs.HasKey(key);
    }
    public T Load<T>(string key)
    {
        if (!HasSaved(key))
        {
            return default(T);
        }

        string savedValue = PlayerPrefs.GetString(key);
        return JsonConvert.DeserializeObject<T>(savedValue);
    }
    public void Save<T>(T value, string key)
    {
        string jsonString = JsonConvert.SerializeObject(value);

        PlayerPrefs.SetString(key, jsonString);
        PlayerPrefs.Save();
    }
}