namespace LightControl.Api.Infrastructure;

public record LightConfigDto(string Name, string Description, IList<LightGroupDto> Groups);
