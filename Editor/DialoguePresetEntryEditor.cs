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

            float defaultWidth    = (position.width / 3f);
            
            Rect presetRect    = new Rect(position.x, position.y, defaultWidth, position.height);
            Rect sceneRect = new Rect(position.x + defaultWidth, position.y, defaultWidth*1.5f, position.height);
            Rect keepAliveRect = new Rect(position.x + defaultWidth*2.5f, position.y, defaultWidth/2f, position.height);
            
            
            SerializedProperty sceneDialogue    = property.FindPropertyRelative("_sceneDialogue");
            SerializedProperty keepAlive = property.FindPropertyRelative("_keepAlive");
            
            
            EditorGUIUtility.labelWidth = 50;
            EditorGUI.PropertyField(presetRect, property.FindPropertyRelative("_preset"));
            
            if (!keepAlive.boolValue)
            {
                EditorGUIUtility.labelWidth = 100;
                EditorGUI.PropertyField(sceneRect, sceneDialogue);
            }

            if (sceneDialogue.objectReferenceValue == null)
            {
                EditorGUIUtility.labelWidth = 70;
                EditorGUI.PropertyField(keepAliveRect, keepAlive);
                EditorGUIUtility.labelWidth = 0;
            }

            EditorGUI.EndProperty();
        }
    }
}