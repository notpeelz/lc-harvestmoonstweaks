
using System.Collections.Generic;
using UnityEngine;

namespace HarvestMoonsTweaks;

internal static class GameObjectExtensions {
  public static IEnumerable<GameObject> Children(this GameObject o) {
    for (var i = 0; i < o.transform.childCount; i++) {
      var child = o.transform.GetChild(i);
      yield return child.gameObject;
    }
  }
}
