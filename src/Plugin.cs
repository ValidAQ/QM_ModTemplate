using HarmonyLib;
using MGSC;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;

using QM_ModTemplate.LocalizationSupport;

namespace QM_ModTemplate
{
    /// <summary>
    /// Entry point for the mod. Exposes shared state and registers Harmony patches.
    /// </summary>
    public static class Plugin
    {
        /// <summary>Unique identifier used to register this mod's Harmony instance.</summary>
        public static string HarmonyId { get; } = "valid.QM_ModTemplate";

        /// <summary>Shared logger for writing messages to the mod's log output.</summary>
        public static Logger Logger { get; } = new Logger();

        /// <summary>Resolves standard mod directory paths (config, persistence, etc.).</summary>
        public static ConfigDirectories ConfigDirectories { get; } = new ConfigDirectories();

        /// <summary>Mod configuration loaded from disk on startup.</summary>
        public static ModConfig Config { get; private set; }

        /// <summary>Game state provided by the mod context after configs are loaded.</summary>
        public static State State { get; private set; }

        /// <summary>
        /// Loads localization dictionary from embedded resources.
        /// </summary>
        private static void LoadLocalization()
        {
            LocalizationFileLoader.LoadFromEmbeddedJson(
                "QM_ModTemplate.localization.json",
                Assembly.GetExecutingAssembly(),
                Logger.LogError);
        }

        /// <summary>
        /// Called by the game after all configs have been loaded.
        /// Initializes mod state, loads config from disk, and applies Harmony patches.
        /// </summary>
        [Hook(ModHookType.AfterConfigsLoaded)]
        public static void AfterConfig(IModContext context)
        {
            State = context.State;

            // Ensure the persistence folder exists before reading/writing mod data.
            // Directory.CreateDirectory(ConfigDirectories.ModPersistenceFolder);
            // Config = ModConfig.LoadConfig(ConfigDirectories.ConfigPath);

#if MCM_PRESENT
            // Register with Mod Configuration Menu if it is installed.
            // Edit McmIntegration.cs to expose config fields in the MCM UI.
            McmIntegration.RegisterIfPresent();
#endif

            try
            {
                // Call this if you have localization entries to load.
                // LoadLocalization();
                new Harmony(HarmonyId).PatchAll(Assembly.GetExecutingAssembly());
                Logger.Log("Harmony patches applied.");
            }
            catch (Exception ex)
            {
                Logger.LogError("Failed to apply Harmony patches.");
                Logger.LogException(ex);
            }
        }

        // Other available hooks — uncomment and implement as needed:
        //
        // Called once before the game bootstraps any systems.
        // [Hook(ModHookType.BeforeBootstrap)]
        // public static void BeforeBootstrap(IModContext context) { }
        //
        // Called when the main menu scene has finished loading.
        // [Hook(ModHookType.MainMenuStarted)]
        // public static void MainMenuStarted(IModContext context) { }
        //
        // Called when the game loads a resource by path; return a replacement object or null to use the default.
        // [Hook(ModHookType.ResourcesLoad)]
        // public static object ResourcesLoad(string path) { return null; }
    }
}
