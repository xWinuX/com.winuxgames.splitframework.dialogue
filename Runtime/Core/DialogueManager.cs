using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Yarn.Unity;

namespace WinuXGames.SplitFramework.Dialogue.Core
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private List<DialoguePresetEntry> _dialoguePresets;

        private readonly Dictionary<SO_DialoguePreset, DialogueRunner> _presets = new Dictionary<SO_DialoguePreset, DialogueRunner>();

        private void Awake()
        {
            foreach (DialoguePresetEntry dialoguePresetEntry in _dialoguePresets.Where(preset => preset.KeepAlive))
            {
                DialogueRunner dialogueRunner = Instantiate(dialoguePresetEntry.Preset.DialoguePrefab, transform);
                _presets.Add(dialoguePresetEntry.Preset, dialogueRunner);
                //dialogueRunner.gameObject.SetActive(false);
            }
        }

        public void OpenDialogue(SO_DialoguePreset preset, string node)
        {
            if (_presets.TryGetValue(preset, out DialogueRunner runner))
            {
                runner.gameObject.SetActive(true);
            }
            else
            {
                runner = Instantiate(preset.DialoguePrefab, transform);
                runner.onDialogueComplete.AddListener(() =>
                {
                    Destroy(runner);
                });
            }

            runner.StartDialogue(node);
        }

        public void OpenDefaultDialogue(string node)
        {
            OpenDialogue(_dialoguePresets.First().Preset, node);
        }
    }
}