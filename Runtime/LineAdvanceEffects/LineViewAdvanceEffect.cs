using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace WinuXGames.SplitFramework.Dialogue.LineAdvanceEffects
{
    public abstract class LineViewAdvanceEffect : MonoBehaviour
    {
        protected virtual IEnumerator EffectCoroutine(TMP_Text text, Action<int, char> onLetterChangeAction, Action onComplete)
        {
            onComplete.Invoke();
            yield return null;
        }

        protected virtual void OnBeforeStart(TMP_Text text) { }
        
        public void StartEffect(TMP_Text text, Action<int, char> onLetterChangeAction, Action onComplete)
        {
            OnBeforeStart(text);
            StartCoroutine(EffectCoroutine(text, onLetterChangeAction, onComplete));
        }
    }
}