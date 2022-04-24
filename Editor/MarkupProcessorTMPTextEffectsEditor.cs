using UnityEditor;
using UnityEngine;
using WinuXGames.SplitFramework.Dialogue.Markup.Processors;

namespace WinuXGames.SplitFramework.Dialogue.Editor
{
    [CustomEditor(typeof(MarkupProcessorTMPTextEffects))]
    public class MarkupProcessorTMPTextEffectsEditor : UnityEditor.Editor
    {
        private bool                          _isPreviewing;
        private MarkupProcessorTMPTextEffects _script;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!_isPreviewing && GUILayout.Button("Preview Effects"))
            {
                _script.EnablePreview();
                _isPreviewing = true;
            }

            if (_isPreviewing && GUILayout.Button("Stop Preview"))
            {
                _script.DisablePreview();
                _isPreviewing = false;
            }
        }

        private void Awake() { _script = (MarkupProcessorTMPTextEffects)target; }

        private void OnDisable()
        {
            if (_isPreviewing) { _script.DisablePreview(); }
        }
    }
}