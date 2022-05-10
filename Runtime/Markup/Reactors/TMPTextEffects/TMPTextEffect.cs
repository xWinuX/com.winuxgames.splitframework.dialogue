using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Yarn.Markup;

namespace WinuXGames.SplitFramework.Dialogue.Markup.Reactors.TMPTextEffects
{
    public abstract class TMPTextEffect
    {
        protected virtual bool UpdateEachFrame { get; set; } = false;

        private bool _effectWasAppliedAtLeastOnce;

        protected TMPTextEffect(IReadOnlyDictionary<string, MarkupValue> properties) { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ApplyEffect(TMP_TextInfo textInfo, TMP_MeshInfo[] cachedMeshInfo, TMP_CharacterInfo[] characterInfos, int startPosition, int endPosition)
        {
            if (!UpdateEachFrame && _effectWasAppliedAtLeastOnce) { return; }

            for (int i = startPosition; i < endPosition; i++)
            {
                TMP_CharacterInfo characterInfo = characterInfos[i];

                // Skip spaces
                if (characterInfo.character == ' ') { continue; }

                int materialIndex = characterInfo.materialReferenceIndex;
                int vertexIndex   = characterInfo.vertexIndex;

                Operation(textInfo.meshInfo[materialIndex], cachedMeshInfo[materialIndex], vertexIndex, i);
            }

            _effectWasAppliedAtLeastOnce = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void Operation(TMP_MeshInfo meshInfo, TMP_MeshInfo cachedMeshInfo, int vertexIndex, int iteration);
    }
}