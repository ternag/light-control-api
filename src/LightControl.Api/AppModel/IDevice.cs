namespace LightControl.Api.AppModel;

public interface IDevice : IDisposable
{
    string DisplayName { get; } // TODO: Add a type name prop and add display name to hw config file 
    void Write(PinNumber pin, LedState value);
    void InitPin(PinNumber pin);
}