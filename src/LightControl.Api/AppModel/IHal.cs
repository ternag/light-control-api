namespace LightControl.Api.AppModel;

public interface IHal : IDisposable
{
    void Update(Led led);
}