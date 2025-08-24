using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

public class AddressablesPathChecker
{
    [MenuItem("Tools/Check Addressables Paths")]
    public static void CheckPaths()
    {
        var settings = AddressableAssetSettingsDefaultObject.Settings;
        if (settings == null)
        {
            Debug.LogError("❌ Addressables settings not found!");
            return;
        }

        foreach (var group in settings.groups)
        {
            if (group == null) continue;

            foreach (var schema in group.Schemas)
            {
                if (schema is UnityEditor.AddressableAssets.Settings.GroupSchemas.BundledAssetGroupSchema bag)
                {
                    Debug.Log($"📦 Group: {group.Name}\n" +
                              $"   BuildPath: {settings.profileSettings.EvaluateString(settings.activeProfileId, bag.BuildPath.GetValue(settings))}\n" +
                              $"   LoadPath : {settings.profileSettings.EvaluateString(settings.activeProfileId, bag.LoadPath.GetValue(settings))}\n");
                }
            }
        }

        Debug.Log("✅ Addressables path check complete.");
    }
}

