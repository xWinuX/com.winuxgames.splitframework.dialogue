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

            float accumulator    = Time.deltaTime;
            int   lettersVisible = 0;
            while (lettersVisible < characterCount)
            {
                Debug.Log(lettersVisible);
                if (Paused) { yield return new WaitUntil(() => !Paused); }

                float secondsPerLetter = 1.0f / _lettersPerSecond;
                while (accumulator >= secondsPerLetter && lettersVisible < characterCount)
                {
                    Debug.Log(accumulator);
                    lettersVisible += 1;
                    onLetterChangeAction.Invoke(lettersVisible-1, text.text[lettersVisible-1]);
                    text.ModifyVertexData(lettersVisible-1, lettersVisible, MakeTextVisibleAction);
                    text.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
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