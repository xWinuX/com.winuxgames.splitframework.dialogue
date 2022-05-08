using System.Runtime.CompilerServices;
using TMPro;

namespace WinuXGames.SplitFramework.Dialogue.Markup.Reactors.TMPTextEffects
{
    public abstract class TMPTextEffect
    {
        public void ApplyEffect(TMP_TextInfo textInfo, TMP_MeshInfo[] cachedMeshInfo, TMP_CharacterInfo[] characterInfos, int startPosition, int endPosition)
        {
            for (int i = startPosition; i < endPosition; i++)
            {
                TMP_CharacterInfo characterInfo = characterInfos[i];

                // Skip spaces
                if (characterInfo.character == ' ') { continue; }
                
                int materialIndex = characterInfo.materialReferenceIndex;
                int vertexIndex   = characterInfo.vertexIndex;

                Operation(textInfo.meshInfo[materialIndex], cachedMeshInfo[materialIndex], vertexIndex, i);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void Operation(TMP_MeshInfo meshInfo, TMP_MeshInfo cachedMeshInfo, int vertexIndex, int iteration);
    }
}