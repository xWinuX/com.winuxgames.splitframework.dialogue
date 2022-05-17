using System;
using UnityEngine;
using Yarn.Unity;

namespace WinuXGames.SplitFramework.Dialogue.Core
{
    [Serializable]
    public class DialoguePresetEntry
    {
        [SerializeField] private SO_DialoguePreset _preset;
        [SerializeField] private bool              _keepAlive;
        [SerializeField] private DialogueRunner    _sceneDialogue;
        

        public SO_DialoguePreset Preset        => _preset;
        public bool              KeepAlive     => _keepAlive;
        public DialogueRunner    SceneDialogue => _sceneDialogue;
    }
}