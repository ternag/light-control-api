using LightControl.Api.AppModel;

namespace LightControl.Api.Infrastructure;

public record LedDto(int Id, string Display, LedState State);
