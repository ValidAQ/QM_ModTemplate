# Quasimorph Mod Template

This is a template for creating mods for Quasimorph.
It includes a basic structure for the mod, as well as some example code to get you started.

## Build Scripts

The `scripts/` folder contains helper batch scripts for building the mod with `dotnet build`:

| Script | Purpose |
|---|---|
| `scripts/build_local.bat` | Build locally; output stays in `src/bin/`. |
| `scripts/build_steam.bat` | Build and deploy to the Steam Workshop folder (reads the item ID from `SteamWorkshopId.txt`). |

Run either script from the repository root or from inside `scripts/`.

## Mod Configuration Menu (MCM) Integration

The template includes optional support for [Mod Configuration Menu (MCM)](https://github.com/Crynano/Mod-Configuration-Menu), which lets players configure your mod through the game's main menu UI.

MCM support is enabled automatically at build time if `MCM.dll` is found in the workshop folder. No changes to the project are needed - just make MCM available before building.

### Getting MCM.dll

**Option 1 - Steam Workshop (recommended):**
Subscribe to MCM on Steam. The DLL will be placed automatically at the right location and detected at build time.

**Option 2 - Manual download:**
Download a release from https://github.com/Crynano/Mod-Configuration-Menu and place `MCM.dll` in `src/lib/MCM.dll` inside the project folder. The build will find it there automatically.

### Adding MCM config entries

Edit `src/McmIntegration.cs`. The `Register()` method shows where to add `IConfigValue` entries and how to apply saved values back to `Plugin.Config`.

## Changelog

### 1.0.0
* Initial release.