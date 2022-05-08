using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

namespace WinuXGames.SplitFramework.Dialogue.Markup.Processors
{
    public static class TMPTextExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ModifyVertexData(this TMP_Text tmpText, Action<TMP_MeshInfo, int, int> action)
        {
            ModifyVertexData(tmpText, 0, tmpText.textInfo.characterCount, action);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ModifyVertexData(this TMP_Text tmpText, int start, int end, Action<TMP_MeshInfo, int, int> action)
        {
            for (int i = start; i < end; i++)
            {
                TMP_CharacterInfo characterInfo = tmpText.textInfo.characterInfo[i];

                // Skip spaces
                if (characterInfo.character == ' ') { continue; }
                
                int materialIndex = characterInfo.materialReferenceIndex;
                int vertexIndex   = characterInfo.vertexIndex;
                
                action.Invoke(tmpText.textInfo.meshInfo[materialIndex], vertexIndex, i);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ModifyVertexColor(this TMP_Text tmpText, Color32 color )
        {
            ModifyVertexColor(tmpText, 0, tmpText.textInfo.characterCount, color);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ModifyVertexColor(this TMP_Text tmpText, int start, int end, Color32 color)
        {
            for (int i = start; i < end; i++)
            {
                TMP_CharacterInfo characterInfo = tmpText.textInfo.characterInfo[i];

                // Skip spaces
                if (characterInfo.character == ' ') { continue; }
                
                int materialIndex = characterInfo.materialReferenceIndex;
                int vertexIndex   = characterInfo.vertexIndex;
                
                Color32[] colors = tmpText.textInfo.meshInfo[materialIndex].colors32;

                colors[vertexIndex]     = color;
                colors[vertexIndex + 1] = color;
                colors[vertexIndex + 2] = color;
                colors[vertexIndex + 3] = color;
            }
        }
    }
}