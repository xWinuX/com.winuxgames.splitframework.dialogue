using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace WinuXGames.SplitFramework.Dialogue.LineAdvanceEffects
{
    public class DialogueTypewriter : LineViewAdvanceEffect
    {
        [SerializeField] private float _lettersPerSecond;
        
        protected override IEnumerator EffectCoroutine(TMP_Text text, Action<int, char> onLetterChangeAction, Action onComplete)
        {
            text.maxVisibleCharacters = 0;
            
            yield return null;

            int characterCount = text.textInfo.characterCount;
            
            if (_lettersPerSecond <= 0 || characterCount == 0)
            {
                text.maxVisibleCharacters = characterCount;
                onComplete?.Invoke();
                yield break;
            }
            
            float accumulator      = Time.deltaTime;

            while (text.maxVisibleCharacters < characterCount)
            {
                float secondsPerLetter = 1.0f / _lettersPerSecond;
                while (accumulator >= secondsPerLetter)
                {
                    text.maxVisibleCharacters += 1;
                    onLetterChangeAction.Invoke(text.maxVisibleCharacters, text.text[text.maxVisibleCharacters-1]);
                    accumulator -= secondsPerLetter;
                }
                accumulator += Time.deltaTime;

                yield return null;
            }
            
            text.maxVisibleCharacters = characterCount;
            
            onComplete?.Invoke();
        }
    }
}