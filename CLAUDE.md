# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Standard Workflow

1. First, think through the problem, read the codebase for relevant files, and write a plan to task/todo.md.
2. The plan should have a list of todo items that you can check off as you complete them.
3. Before you begin working, check in with me and I will verify the plan.
4. Then, begin working on the todo items, marking them complete as you go.
5. Finally, write a review section to the task/todo.md file, with a summery of the changes you made and any other relevant information.

## Build and Development Commands

- **Build**: `dotnet build`
- **Build Release**: `dotnet build --configuration Release`
- **Run API**: `dotnet run --project src/LightControl.Api`
- **Run tests**: `dotnet test`
- **Run single test**: `dotnet test --filter "MethodName"`
- **Build for Raspberry Pi**: `dotnet publish --configuration Release --runtime linux-arm --self-contained true src/LightControl.Api/LightControl.Api.csproj`
- **PowerShell build script**: `./Make.ps1`

## Technology Stack

- **.NET 9.0** with C# 13
- **ASP.NET Core** with OpenAPI/Swagger
- **System.Device.Gpio** and **Iot.Device.Bindings** for hardware control
- **xUnit** with **FluentAssertions** for testing
- **Nullable reference types** enabled

## Git Commit Guidelines

- **Commit titles**: Maximum 72 characters
- **Commit body**: Use second `-m` parameter for detailed descriptions
- **Format**: `git commit -m "Short title" -m "Detailed body with bullet points"`
- **Branch creation**: When on master branch and asked to commit, create a new branch first
- **Branch naming**: Use lowercase with hyphens (e.g., `feature/rename-mapinfo`, `fix/nullable-warnings`)

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