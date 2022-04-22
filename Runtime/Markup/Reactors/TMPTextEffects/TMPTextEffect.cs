using TMPro;
using UnityEngine;

namespace WinuXGames.SplitFramework.Dialogue.Markup.Reactors.TMPTextEffects
{
    public abstract class TMPTextEffect : MarkupReactor
    {
        public void ApplyEffect(TMP_TextInfo textInfo, TMP_CharacterInfo[] characterInfos, int startPosition, int endPosition)
        {
            for (int i = startPosition; i < endPosition; i++)
            {
                TMP_CharacterInfo characterInfo = characterInfos[i];

                // Skip spaces
                if (characterInfo.character == ' ') { continue; }
                
                int materialIndex = characterInfo.materialReferenceIndex;
                int vertexIndex   = characterInfo.vertexIndex;

                Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;
                Color32[] colors   = textInfo.meshInfo[materialIndex].colors32;

                Operation(vertices, colors, vertexIndex, i);
            }
        }

        protected abstract void Operation(Vector3[] vertices, Color32[] colors, int vertexIndex, int iteration);

    }
}