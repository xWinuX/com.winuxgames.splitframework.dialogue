using UnityEngine;
using Yarn.Unity;

namespace WinuXGames.SplitFramework.Dialogue
{
    [RequireComponent(typeof(DialogueRunner))]
    public class Dialogue : MonoBehaviour
    {
        [SerializeField] private bool _startDialogue;
        
        
        private DialogueRunner _dialogueRunner;

        private void Awake()
        {
            _dialogueRunner = GetComponent<DialogueRunner>();

            //StartCoroutine(CoroutineUtility.WaitForOneFrame(() => { _dialogueRunner.StartDialogue("Test"); }));
        }

        private void Update()
        {
            if (_startDialogue)
            {
                _dialogueRunner.StartDialogue("Test");
                _startDialogue = false;
            }
        }
    }
}