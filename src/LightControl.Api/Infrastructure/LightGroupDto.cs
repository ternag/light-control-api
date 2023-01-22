namespace LightControl.Api.Infrastructure;

public record LightGroupDto(string Name, IList<LightGroupDto> Groups, IList<LightDto> Lights);
