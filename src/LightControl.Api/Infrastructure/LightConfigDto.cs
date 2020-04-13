using System.Collections.Generic;

namespace LightControl.Api.Infrastructure
{
  public class LightConfigDto
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public IList<LightGroupDto> Groups { get; set; }
  }
}