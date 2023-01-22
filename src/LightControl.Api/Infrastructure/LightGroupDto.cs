namespace LightControl.Api.Infrastructure;

public class LightGroupDto
{
    public string Name { get; set; }
    public IList<LightGroupDto> Groups { get; set; }
    public IList<LightDto> Lights { get; set; }
}