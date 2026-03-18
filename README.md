# Quasimorph Mod Template

This is a template for creating mods for Quasimorph.
It includes a basic structure for the mod, as well as some example code to get you started.

Information partially lifted from the official [Quasimorph modding guide](https://steamcommunity.com/sharedfiles/filedetails/?id=3281671312).

Inspired by [NBKRedSpy's QM_Template](https://github.com/NBKRedSpy/QM_Template).

## Build Scripts

The `scripts/` folder contains helper scripts for creating a new project from the template and building the mod with `dotnet build`.

## Workshop Mod Creation

* First, prepare your mod content and place it in a folder. The folder should contain a `modmanifest.json` file with the required fields, and preferably a thumbnail image named `thumbnail.png`.

* In game, run the console command `mod_createworkshopitem` where contentPath is the path to the folder. During command execution, a Steam Workshop item with initial content will be created. Save the shown item ID in `SteamWorkshopId.txt`.

* To update mod content, run the console command `mod_updateworkshopitem` with the saved item ID and content path. If you want to upload a thumbnail add `TRUE` as the third parameter. Thumbnail should be placed in `contentpath/thumbnail.png`. This is the only way to upload a mod thumbnail.

* Don’t forget to subscribe to your mod and make it visible to other users after it is ready.

### In-game modding commands

* `mod_createworkshopitem` - Create Steam Workshop item. **Warning!** This command will create a new entity in Steam.
  * Syntax: `mod_createworkshopitem <contentPath>`

* `mod_updateworkshopitem` - Update Steam Workshop item.
  * Syntax: `mod_updateworkshopitem <item_id> <contentPath> <update_thumbnail:TRUE|FALSE>`

* `mod_listworkshopitems` - List all Steam Workshop items created by the current user.

* `listmod` - List all mods currently active.

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

# Source Code
Source code is available on GitHub at https://github.com/<USER>/<PROJECT>

## Changelog

### 1.0.0
* Initial release.