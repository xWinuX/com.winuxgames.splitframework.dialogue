using System;
using System.Collections;
using TMPro;
using UnityEngine;
using WinuXGames.SplitFramework.Dialogue.Extensions;

namespace WinuXGames.SplitFramework.Dialogue.LineAdvanceEffects
{
    public class DialogueTypewriter : DialogueLetterRevealHandler
    {
        [SerializeField] private float _lettersPerSecond;

        protected override IEnumerator EffectCoroutine(TMP_Text text, Action<int, char> onLetterChangeAction, Action onComplete)
        {
            text.ModifyVertexData(MakeTextInvisibleAction);
            text.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
            
            int characterCount = text.textInfo.characterCount;

            if (_lettersPerSecond <= 0 || characterCount == 0)
            {
                onComplete?.Invoke();
                yield break;
            }
            
            // Execute first letter change
            onLetterChangeAction.Invoke(0, text.text[0]);
            
            float accumulator    = Time.deltaTime;
            int   lettersVisible = 0;
            while (lettersVisible < characterCount)
            {
                if (Paused) { yield return new WaitUntil(() => !Paused); }

                float secondsPerLetter = 1.0f / _lettersPerSecond;
                while (accumulator >= secondsPerLetter)
                {
                    text.ModifyVertexData(lettersVisible, lettersVisible+1, MakeTextVisibleAction);
                    text.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
                    lettersVisible += 1;
                    Debug.Log(lettersVisible);
                    Debug.Log(text.text);
                    onLetterChangeAction.Invoke(lettersVisible, text.text[lettersVisible - 1]);
                    accumulator -= secondsPerLetter;
                }

                accumulator += Time.deltaTime;

                yield return null;
            }
            
            onComplete?.Invoke();
        }

        private static void SetTextVisibility(byte alpha, TMP_MeshInfo meshInfo, int vertexIndex)
        {
            Color32[] colors = meshInfo.colors32;

            colors[vertexIndex].a     = alpha;
            colors[vertexIndex + 1].a = alpha;
            colors[vertexIndex + 2].a = alpha;
            colors[vertexIndex + 3].a = alpha;
        }
        
        private static void MakeTextVisibleAction(TMP_MeshInfo meshInfo, int vertexIndex, int iteration) { SetTextVisibility(255, meshInfo, vertexIndex); }
        
        private static void MakeTextInvisibleAction(TMP_MeshInfo meshInfo, int vertexIndex, int iteration) { SetTextVisibility(0, meshInfo, vertexIndex); }
    }
}