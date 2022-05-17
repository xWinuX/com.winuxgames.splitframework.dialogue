using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WinuXGames.SplitFramework.Utility;
using Yarn.Unity;

namespace WinuXGames.SplitFramework.Dialogue.Core
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private YarnProject               _yarnProject;
        [SerializeField] private List<DialoguePresetEntry> _dialoguePresets;

        private readonly Dictionary<SO_DialoguePreset, DialogueRunner> _presets = new Dictionary<SO_DialoguePreset, DialogueRunner>();

        private void Start()
        {
            foreach (DialoguePresetEntry dialoguePresetEntry in _dialoguePresets.Where(preset => preset.KeepAlive))
            {
                DialogueRunner dialogueRunner = Instantiate(dialoguePresetEntry.Preset.DialoguePrefab, transform);
                _presets.Add(dialoguePresetEntry.Preset, dialogueRunner);
                dialogueRunner.SetProject(_yarnProject);
                StartCoroutine(CoroutineUtility.WaitForOneFrame(() => dialogueRunner.gameObject.SetActive(false)));
            }
        }

        public DialogueRunner OpenDialogue(SO_DialoguePreset preset, string node)
        {
            if (_presets.TryGetValue(preset, out DialogueRunner runner))
            {
                runner.gameObject.SetActive(true);
            }
            else
            {
                runner = Instantiate(preset.DialoguePrefab, transform);
                runner.SetProject(_yarnProject);
                runner.onDialogueComplete.AddListener(() =>
                {
                    Destroy(runner.gameObject);
                });
            }

           runner.StartDialogue(node);

           return runner;
        }
    }
}