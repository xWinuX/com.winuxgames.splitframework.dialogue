using UnityEngine;
using WinuXGames.SplitFramework.Dialogue.Providers;

namespace WinuXGames.SplitFramework.Dialogue.Core
{
    public class DialogueUnityEventHandler : MonoBehaviour
    {
        [SerializeField] private SO_DialogueDependencyProvider _dialogueDependency;
        [SerializeField] private SO_DialoguePreset             _dialoguePreset;

        public void OpenDialogue(string node)
        {
            _dialogueDependency.DialogueManager.OpenDialogue(_dialoguePreset, node);
        }
    }
}