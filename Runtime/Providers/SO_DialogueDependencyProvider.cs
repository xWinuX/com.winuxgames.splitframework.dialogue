using UnityEngine;
using WinuXGames.SplitFramework.Core.Providers;
using WinuXGames.SplitFramework.Dialogue.Core;

namespace WinuXGames.SplitFramework.Dialogue.Providers
{
    [CreateAssetMenu(menuName = "Split Framework/Providers/Dialogue", fileName = "DialogueDependencyProvider", order = 0)]
    public class SO_DialogueDependencyProvider : SO_ScriptableProvider
    {
        public DialogueManager DialogueManager { get; internal set; }

        protected override void ResetValues()
        {
            DialogueManager = null;
        }
    }
}