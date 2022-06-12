using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace WinuXGames.SplitFramework.Dialogue.LineAdvanceEffects
{
    public abstract class DialogueLetterRevealHandler : MonoBehaviour
    {
        protected bool Paused;

        private Coroutine _effectCoroutine;
        private Coroutine _pauseCoroutine;

        private Action _onComplete;

        private void Pause() { Paused = true; }

        private void Unpause() { Paused = false; }

        public void Stop(bool executeOnComplete = false)
        {
            if (_effectCoroutine != null) { StopCoroutine(_effectCoroutine); }
            if (_pauseCoroutine != null) { StopCoroutine(_pauseCoroutine); }

            Unpause();

            if (executeOnComplete) { _onComplete?.Invoke(); }
        }

        public void PauseFor(float seconds)
        {
            Pause();
            _pauseCoroutine = StartCoroutine(PauseForCoroutine(seconds));
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
            _onComplete      = onComplete;
            _effectCoroutine = StartCoroutine(EffectCoroutine(text, onLetterChangeAction, onComplete));
        }
    }
}