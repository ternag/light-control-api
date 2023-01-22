using LightControl.Api.Infrastructure;

namespace LightControl.Api.AppModel;

public interface ILedContext
{
    IEnumerable<Led> All { get; } // TODO: Is this needed by the client? Could maybe be private to the context!

    LightConfigDto Config { get; }

    Led Get(LedId ledId);
    Led Flick(LedId id);

    LightGroupDto FlickGroup(LightGroupId id);
}