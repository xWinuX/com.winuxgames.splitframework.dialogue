using System;
using System.Collections;
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

        public void StartEffect(TMP_Text text, Action<int, char> onLetterChangeAction, Action onComplete)
        {
            StartCoroutine(EffectCoroutine(text, onLetterChangeAction, onComplete));
        }
    }
}