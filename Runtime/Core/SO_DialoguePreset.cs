using UnityEngine;
using Yarn.Unity;

namespace WinuXGames.SplitFramework.Dialogue
{
    [CreateAssetMenu(menuName = "Split Framework/Dialogue/Preset", fileName = "DialoguePreset", order = 0)]
    public class SO_DialoguePreset : ScriptableObject
    {
        [SerializeField] private DialogueRunner _dialoguePrefab;

        public DialogueRunner DialoguePrefab => _dialoguePrefab;
    }
}