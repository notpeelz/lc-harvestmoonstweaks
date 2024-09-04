using System.Linq;
using BepInEx;
using LobbyCompatibility.Attributes;
using LobbyCompatibility.Enums;
using UnityEngine.SceneManagement;

namespace HarvestMoonsTweaks;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency("BMX.LobbyCompatibility", BepInDependency.DependencyFlags.HardDependency)]
[LobbyCompatibility(CompatibilityLevel.ClientOnly, VersionStrictness.None)]
public class HarvestMoonsTweaks : BaseUnityPlugin {
  public static new Config Config { get; private set; } = null!;

  private void Awake() {
    Config = new Config(base.Config);
    SceneManager.sceneLoaded += this.OnSceneLoaded;
  }

  private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
    switch (scene.name) {
      case "SierraScene": {
        if (Config.SierraDisableInsideFog.Value) {
          DisableInsideFog();
        }
        break;
      }
      case "FrayScene": {
        if (Config.FrayDisableInsideFog.Value) {
          DisableInsideFog();
        }
        break;
      }
      default: {
        break;
      }
    }

    void DisableInsideFog() {
      var environment = scene.GetRootGameObjects().FirstOrDefault(x => x.name == "Environment");
      if (environment == null) {
        this.Logger.LogWarning($"GameObject 'Environment' doesn't exist in scene '{scene.name}'");
        return;
      }

      var lighting = environment.Children().FirstOrDefault(x => x.name == "Lighting");
      if (lighting == null) {
        this.Logger.LogWarning($"GameObject 'Environment/Lighting' doesn't exist in scene '{scene.name}'");
        return;
      }

      var n = 0;
      var insideFog = lighting.Children().Where(x => x.name.StartsWith("Inside Fog"));
      foreach (var o in insideFog) {
        this.Logger.LogInfo($"Disabling GameObject 'Environment/Lighting/{o.name}' in scene '{scene.name}'");
        o.SetActive(false);
        n++;
      }

      if (n == 0) {
        this.Logger.LogWarning($"No 'Inside Fog' GameObjects found in scene '{scene.name}'");
      }
    }
  }
}
