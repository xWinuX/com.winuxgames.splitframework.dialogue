using UnityEditor;
using UnityEngine;
using WinuXGames.SplitFramework.Dialogue.Core;

namespace WinuXGames.SplitFramework.Dialogue.Editor
{
    [CustomPropertyDrawer(typeof(DialoguePresetEntry))]
    public class DialoguePresetEntryEditor : PropertyDrawer
    {
        private const float KeepAliveOffset = 20f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            float presetWidth    = (position.width / 1.5f);
            float keepAliveWidth = (position.width - presetWidth);

            Rect presetRect    = new Rect(position.x, position.y, presetWidth, position.height);
            Rect keepAliveRect = new Rect(position.x + presetWidth + KeepAliveOffset, position.y, keepAliveWidth - KeepAliveOffset, position.height);
            EditorGUIUtility.labelWidth = 50;
            EditorGUI.PropertyField(presetRect, property.FindPropertyRelative("_preset"));
            EditorGUIUtility.labelWidth = 70;
            EditorGUI.PropertyField(keepAliveRect, property.FindPropertyRelative("_keepAlive"));
            EditorGUIUtility.labelWidth = 0;
            EditorGUI.EndProperty();
        }
    }
}