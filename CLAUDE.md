# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build and Development Commands

- **Build**: `dotnet build`
- **Run API**: `dotnet run --project src/LightControl.Api`
- **Run tests**: `dotnet test`
- **Build for Raspberry Pi**: `dotnet publish --configuration Release --runtime linux-arm --self-contained true src/LightControl.Api/LightControl.Api.csproj`
- **PowerShell build script**: `./Make.ps1`

## Architecture Overview

This is a hierarchical LED control system designed for IoT hardware control:

1. **HTTP API Layer** (`Controllers/LedController.cs`) - REST endpoints for LED control
2. **Business Logic** (`AppModel/LedContext.cs`) - LED state management and validation
3. **Hardware Abstraction** (`Hardware/HardwareContext.cs` + `AppModel/IDevice.cs`) - Device-agnostic hardware interface
4. **Device Drivers** (`Hardware/Device/`) - Specific implementations for:
   - GPIO direct control (`GpioDevice.cs`)
   - MCP23017 I2C GPIO expanders (`Mcp23017Device.cs`)
   - PCA9685 PWM controllers (`Pca9685Device.cs`)
   - Dummy devices for testing (`DummyHardwareDevice.cs`)

## Key Patterns

- **LED Addressing**: Uses hexadecimal notation (0x10-0x1f for I2C device addresses)
- **Environment Configuration**: Production uses real hardware, Development/UnitTest environments use dummy devices
- **Singleton Services**: Hardware contexts are registered as singletons to maintain state
- **Factory Pattern**: `HardwareDeviceFactory` creates appropriate device instances based on configuration
- **Configuration-Driven**: Hardware mapping defined in JSON files rather than hardcoded

## Hardware Configuration

Hardware devices are configured via JSON files in `src/LightControl.Api/File/`:
- `hardware.json` - Production hardware mapping
- `dummy_hardware.json` - Development/test configuration
- `ModularBuildings.json` - Light grouping definitions

Device types supported: `GPIO`, `MCP23017`, `PCA9685`, `Dummy`

## Test Structure

- Uses xUnit with FluentAssertions
- Integration tests via `TestServerFactory` 
- Hardware tests use dependency injection with mock devices
- HTTP API tests available in `test-api.http`

## Error Handling

Controllers use consistent exception-to-HTTP status mapping via `ErrorController.cs` for unhandled exceptions.