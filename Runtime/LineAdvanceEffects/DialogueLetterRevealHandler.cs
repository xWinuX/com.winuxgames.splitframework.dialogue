using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace WinuXGames.SplitFramework.Dialogue.LineAdvanceEffects
{
    public abstract class DialogueLetterRevealHandler : MonoBehaviour
    {
        protected bool Paused;

        private void Pause() { Paused = true; }

        private void Unpause() { Paused = false; }

        public void PauseFor(float seconds)
        {
            Pause();
            StartCoroutine(PauseForCoroutine(seconds));
        }

        private IEnumerator PauseForCoroutine(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            Unpause();
        }
        
        protected virtual IEnumerator EffectCoroutine(TMP_Text text, Action<int, char> onLetterChangeAction, Action onComplete)
        {
            onComplete.Invoke();
            yield return null;
        }

        protected virtual void OnBeforeStart(TMP_Text text)
        {
            text.maxVisibleCharacters = int.MaxValue;
            text.ForceMeshUpdate();
        }

        public void StartEffect(TMP_Text text, Action<int, char> onLetterChangeAction, Action onComplete)
        {
            OnBeforeStart(text);
            StartCoroutine(EffectCoroutine(text, onLetterChangeAction, onComplete));
        }
    }
}