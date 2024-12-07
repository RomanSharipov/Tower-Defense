public interface ISaveService
{
    public bool HasSaved(string key);
    public T Load<T>(string key);
    public void Save<T>(T value, string key);
}