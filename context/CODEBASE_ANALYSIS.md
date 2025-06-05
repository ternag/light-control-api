# Light Control API - Codebase Analysis & Development Guide

## Overview

The Light Control API is a .NET 7 Web API application designed to control GPIO and I²C devices on Raspberry Pi hardware. It provides REST endpoints for managing LED lighting systems with support for direct GPIO pins and I²C expansion chips.

## Current Architecture

### Technology Stack
- **.NET 9** with C# 13 and nullable reference types
- **ASP.NET Core Web API** with Swagger/OpenAPI
- **System.Device.Gpio** + **Iot.Device.Bindings** for hardware control  
- **Serilog** for structured logging
- **Dependency Injection** container for service management

### Project Structure
```
src/
├── LightControl.Api/           # Main API project
│   ├── Controllers/            # API endpoints
│   ├── AppModel/              # Core domain models
│   ├── Hardware/              # Hardware abstraction layer
│   ├── Infrastructure/        # Cross-cutting concerns
│   ├── File/                  # Configuration files
│   └── Utils/                 # Utility classes
└── LightControl.Api.Unittest/  # Unit tests
```

### Supported Hardware Devices
- **GPIO**: Direct Raspberry Pi pins (8 pins configured)
- **MCP23017**: I²C GPIO expander (2 devices, 32 pins total)  
- **PCA9685**: PWM driver (implemented but unused)
- **Dummy Device**: For testing without hardware

## Current API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/led` | Get all LEDs |
| GET | `/api/led/{id}` | Get specific LED |
| PUT | `/api/led/{id}` | Update LED display name |
| GET | `/api/led/{id}/_flick` | Toggle LED state |
| GET | `/api/reset` | System reset |
| GET | `/` | Health check |

## Technical Debt & Improvement Areas

### High Priority Issues

#### 1. **Hardcoded LED Configuration** ⚠️
**Location**: `LedContext.cs` constructor  
**Issue**: All 40 LEDs are hardcoded in the constructor  
**Impact**: Cannot add/remove LEDs without code changes

**Recommendation**:
```csharp
// Move to configuration file
public class LedConfiguration 
{
    public List<LedDefinition> Leds { get; set; } = new();
}

public class LedDefinition 
{
    public ushort Id { get; set; }
    public string DisplayName { get; set; }
    public LedState InitialState { get; set; } = LedState.Off;
    public string? Group { get; set; }
}
```

#### 2. **Missing Input Validation**
**Issue**: No validation on LED IDs, pin numbers, or request bodies  
**Risk**: Runtime exceptions, security vulnerabilities

**Recommendation**: Add FluentValidation or data annotations
```csharp
[ApiController]
public class LedController : ControllerBase
{
    [HttpPut("/api/led/{id:int:min(0):max(65535)}")]
    public ActionResult<LedDto> Put(ushort id, [FromBody] LedUpdateDisplay request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        // ...
    }
}
```

#### 3. **Exception Handling Strategy**
**Issue**: Generic exception catching loses important error context  
**Location**: `LedController.CatchExceptions<T>` method

**Recommendation**: Create specific exception types
```csharp
public class LedNotFoundException : Exception { }
public class HardwareException : Exception { }
public class InvalidPinException : Exception { }
```

### Medium Priority Issues

#### 4. **Logging Configuration**
**Issue**: Hardcoded log path `/var/log/light-ctl/` may not exist  
**Recommendation**: Make log path configurable with fallback options

#### 5. **Hardware Device Disposal**
**Issue**: No explicit cleanup of hardware resources  
**Recommendation**: Implement proper disposal patterns and lifetime management

#### 6. **Configuration Management**
**Issue**: Hardware configuration mixing JSON files and appsettings  
**Recommendation**: Consolidate configuration approach

## Unimplemented Features

### 1. **Light Groups** 🚧
**Status**: Interface exists but not implemented  
**Code**: `LedContext.FlickGroup()` throws `NotImplementedException`

**Implementation Plan**:
```csharp
public class LightGroup 
{
    public LightGroupId Id { get; set; }
    public string Name { get; set; }
    public List<LedId> LedIds { get; set; } = new();
}

// Add to configuration
public class LightConfiguration 
{
    public List<LedDefinition> Leds { get; set; } = new();
    public List<LightGroup> Groups { get; set; } = new();
}
```

### 2. **PWM/Dimming Support** 💡
**Current**: Only on/off control  
**Opportunity**: PCA9685 device supports PWM but unused

**New Endpoints**:
```
PUT /api/led/{id}/brightness  # Set brightness 0-100%
GET /api/led/{id}/brightness  # Get current brightness
```

### 3. **Scheduling/Automation** ⏰
**Opportunity**: Add time-based LED control
```
POST /api/schedule           # Create scheduled actions
GET /api/schedule           # List schedules  
DELETE /api/schedule/{id}   # Remove schedule
```

### 4. **Device Discovery** 🔍
**Current**: Static hardware configuration  
**Enhancement**: Auto-discover I²C devices
```
GET /api/hardware/scan      # Scan for available devices
GET /api/hardware/status    # Device health check
```

## Development Recommendations

### Code Quality Improvements

#### 1. **Add Comprehensive Validation**
```csharp
public class LedUpdateDisplayValidator : AbstractValidator<LedUpdateDisplay>
{
    public LedUpdateDisplayValidator()
    {
        RuleFor(x => x.Display)
            .NotEmpty()
            .MaximumLength(50)
            .Matches(@"^[a-zA-Z0-9\s\-_]+$");
    }
}
```

#### 2. **Implement Health Checks**
```csharp
builder.Services.AddHealthChecks()
    .AddCheck<HardwareHealthCheck>("hardware")
    .AddCheck<ConfigurationHealthCheck>("configuration");
```

#### 3. **Add Rate Limiting**
```csharp
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("LedControl", opt =>
    {
        opt.PermitLimit = 100;
        opt.Window = TimeSpan.FromMinutes(1);
    });
});
```

#### 4. **Enhance Error Responses**
```csharp
public class ApiErrorResponse
{
    public string Error { get; set; }
    public string Detail { get; set; }
    public int StatusCode { get; set; }
    public DateTime Timestamp { get; set; }
}
```

### New Feature Suggestions

#### 1. **Configuration API**
```
GET /api/config/hardware     # Current hardware config
PUT /api/config/hardware     # Update hardware config
POST /api/config/reload      # Reload configuration
```

#### 2. **Bulk Operations**
```
PUT /api/led/bulk           # Update multiple LEDs
POST /api/led/group/{id}/action  # Group operations
```

#### 3. **Event System**
```csharp
public interface ILedEventService
{
    Task PublishLedStateChanged(LedId id, LedState oldState, LedState newState);
    Task PublishGroupStateChanged(LightGroupId groupId, GroupState state);
}
```

#### 4. **Backup/Restore**
```
GET /api/backup             # Export current state
POST /api/restore           # Import state
```

## Testing Strategy

### Current State
- Basic unit tests exist
- HTTP test file for manual API testing

### Recommendations

#### 1. **Expand Unit Test Coverage**
```csharp
[Theory]
[InlineData(0, true)]
[InlineData(0x2F, true)]
[InlineData(0xFF, false)]
public void Get_ValidatesLedIdRange(ushort ledId, bool shouldSucceed)
{
    // Test LED ID validation
}
```

#### 2. **Add Integration Tests**
```csharp
public class LedControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task GetAllLeds_ReturnsExpectedCount()
    {
        // Test full API pipeline
    }
}
```

#### 3. **Hardware Simulation Tests**
```csharp
public class HardwareSimulationTests
{
    [Fact]
    public void Mcp23017Device_SetPin_UpdatesCorrectRegister()
    {
        // Test hardware logic without physical devices
    }
}
```

## Configuration Improvements

### Current Issues
- Hardware config in separate JSON file
- Application config in appsettings.json
- No validation of configuration files

### Recommended Structure
```json
{
  "LightControl": {
    "Hardware": {
      "ConfigurationFile": "./config/hardware.json",
      "ValidateOnStartup": true,
      "AutoDiscovery": {
        "Enabled": false,
        "I2cBuses": [1]
      }
    },
    "Leds": {
      "ConfigurationFile": "./config/leds.json",
      "DefaultState": "Off"
    },
    "Groups": {
      "ConfigurationFile": "./config/groups.json"
    },
    "Features": {
      "EnableScheduling": false,
      "EnableGroupOperations": true,
      "EnablePwmControl": false
    }
  }
}
```

## Performance Considerations

### Current Bottlenecks
1. **I²C Communication**: Sequential pin updates to MCP23017
2. **Logging**: Synchronous file I/O
3. **No Caching**: Hardware state not cached

### Optimization Opportunities
```csharp
// Batch I²C updates
public async Task WriteBulkAsync(Dictionary<PinNumber, LedState> updates)
{
    // Update multiple pins in single I²C transaction
}

// Cache hardware state
public class CachedHardwareContext : IHardwareContext
{
    private readonly IMemoryCache _cache;
    // Cache device states to reduce I²C calls
}
```

## Deployment & Operations

### Current Deployment
- Manual deployment to Raspberry Pi
- Systemd service configuration implied
- Log rotation configured

### Recommended Enhancements

#### 1. **Docker Support**
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0-arm64v8
# Add GPIO access configuration
```

#### 2. **Configuration Validation**
```csharp
public class StartupConfigurationValidator : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        // Validate all configuration files
        // Test hardware connectivity
        // Verify LED mappings
    }
}
```

#### 3. **Monitoring Integration**
- Add Prometheus metrics
- Health check endpoints
- Performance counters

## Security Considerations

### Current State
- No authentication/authorization
- No HTTPS enforcement in production
- No rate limiting

### Recommendations
1. **Add API Key Authentication** for production use
2. **Implement CORS** policy for web clients  
3. **Add Request Validation** middleware
4. **Enable HTTPS** redirection in production

## Migration Path for Major Changes

### Phase 1: Configuration Externalization
1. Move LED definitions to configuration file
2. Add configuration validation
3. Maintain backward compatibility

### Phase 2: Feature Enhancement  
1. Implement light groups
2. Add PWM/dimming support
3. Create bulk operation endpoints

### Phase 3: Advanced Features
1. Add scheduling system
2. Implement event notifications
3. Create web UI for management

---

## Quick Start for New Developers

### Development Setup
1. Install .NET 9 SDK
2. Clone repository
3. Run `dotnet restore` in solution directory
4. Use `dummy_hardware.json` for development without Pi hardware
5. Test with provided `.http` file

### Key Files to Understand
- `Program.cs` - Application startup and DI configuration
- `LedController.cs` - Main API endpoints
- `LedContext.cs` - Core LED management logic  
- `hardware.json` - Hardware device configuration
- `HardwareContext.cs` - Hardware abstraction layer

### Common Development Tasks
- **Add new LED**: Currently requires code change in `LedContext.cs`
- **Add new device type**: Implement `IDevice` interface
- **Modify API**: Update controller and corresponding DTOs
- **Change hardware config**: Edit `hardware.json` file

---

*Last Updated: December 2024*  
*Version: 1.0* 