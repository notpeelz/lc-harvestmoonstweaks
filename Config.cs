using System.Collections.Generic;
using HarmonyLib;
using BepInEx.Configuration;

public class Config {
  public ConfigEntry<bool> FrayDisableInsideFog { get; }

  public ConfigEntry<bool> SierraDisableInsideFog { get; }

  public Config(ConfigFile cfg) {
    const string SECTION_FRAY = "Fray";
    this.FrayDisableInsideFog = cfg.Bind<bool>(
      section: SECTION_FRAY,
      key: "DisableInsideFog",
      defaultValue: default,
      description: "Hides the fog that appears inside the facility."
    );

    const string SECTION_SIERRA = "Sierra";
    this.SierraDisableInsideFog = cfg.Bind<bool>(
      section: SECTION_SIERRA,
      key: "DisableInsideFog",
      defaultValue: default,
      description: "Hides the fog that appears inside the facility."
    );

    cfg.OrphanedEntries.Clear();
    cfg.Save();
  }
}
