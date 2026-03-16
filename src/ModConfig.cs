using System;
using System.IO;
using Newtonsoft.Json;

namespace QM_ModTemplate
{
    /// <summary>
    /// Holds this mod's user-editable settings.
    /// Add public properties here; they are automatically serialized to / deserialized from JSON.
    /// New fields added in later mod versions are automatically written back to the config file
    /// on next load with their default values.
    /// </summary>
    public class ModConfig
    {
        // ── Add config fields below. Example: ────────────────────────────────
        // public bool EnableFeature { get; set; } = true;
        // public int SomeValue { get; set; } = 42;
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Loads the config from <paramref name="configPath"/>, or creates a default one if it does not exist.
        /// Also re-saves the file when new fields have been added since the last run.
        /// </summary>
        public static ModConfig LoadConfig(string configPath)
        {
            var settings = new JsonSerializerSettings { Formatting = Formatting.Indented };

            if (File.Exists(configPath))
            {
                try
                {
                    string sourceJson = File.ReadAllText(configPath);
                    var config = JsonConvert.DeserializeObject<ModConfig>(sourceJson, settings);

                    // Re-save to pick up any new fields added in this mod version.
                    string upgraded = JsonConvert.SerializeObject(config, settings);
                    if (upgraded != sourceJson)
                    {
                        Plugin.Logger.Log("Config updated with new default fields.");
                        File.WriteAllText(configPath, upgraded);
                    }

                    return config;
                }
                catch (Exception ex)
                {
                    Plugin.Logger.LogError("Error reading config — using defaults.");
                    Plugin.Logger.LogException(ex);
                    return new ModConfig();
                }
            }
            else
            {
                var config = new ModConfig();
                File.WriteAllText(configPath, JsonConvert.SerializeObject(config, settings));
                return config;
            }
        }
    }
}
