using UnityEditor;
using EditorUtility = WinuXGames.SplitFramework.Utility.Editor.EditorUtility;

namespace WinuXGames.SplitFramework.Dialogue.Editor
{
    public static class CustomGameObjectMenuItems
    {
        [MenuItem("GameObject/Split Framework/Dialogue/Manager", priority = 0)]
        private static void CreateDialogueManager() => EditorUtility.InstantiatePrefabFromResources("GameObjectMenuItemPresets/Dialogue/MAN_Dialogue");

        [MenuItem("GameObject/Split Framework/Dialogue/Presets/Default Overworld", priority = 1)]
        private static void CreateDialoguePresetDefaultOverworld() =>
            EditorUtility.InstantiatePrefabFromResources("GameObjectMenuItemPresets/Dialogue/Presets/DefaultOverworldDialogue");
        
        [MenuItem("GameObject/Split Framework/Dialogue/Presets/Barebones Text Only", priority = 2)]
        private static void CreateDialoguePresetBarebonesTextOnly() =>
            EditorUtility.InstantiatePrefabFromResources("GameObjectMenuItemPresets/Dialogue/Presets/BarebonesTextOnlyDialogue");
    }
}