using UnityEngine;
using WinuXGames.SplitFramework.Core.Providers;
using WinuXGames.SplitFramework.Dialogue.Core;

namespace WinuXGames.SplitFramework.Dialogue.Providers
{
    [DefaultExecutionOrder(-1)]
    public class DialogueDependencyProviderInitializer : ScriptableProviderInitializer<SO_DialogueDependencyProvider>
    {
        [SerializeField] private DialogueManager _dialogueManager;
        
        private void Awake() { ProviderToInitialize.DialogueManager = _dialogueManager; }
    }
}