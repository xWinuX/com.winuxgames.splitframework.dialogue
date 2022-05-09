using System;
using UnityEngine;

namespace WinuXGames.SplitFramework.Dialogue.Core
{
    [Serializable]
    public class DialoguePresetEntry
    {
        [SerializeField] private SO_DialoguePreset _preset;
        [SerializeField] private bool              _keepAlive;

        public SO_DialoguePreset Preset    => _preset;
        public bool              KeepAlive => _keepAlive;
    }
}