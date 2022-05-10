using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using WinuXGames.SplitFramework.Dialogue.Markup.Utility;
using Yarn.Markup;

namespace WinuXGames.SplitFramework.Dialogue.Markup.Reactors.TMPTextEffects
{
    public class TMPTextEffectShake : TMPTextEffect
    {
        protected override bool UpdateEachFrame { get; set; } = true;

        private readonly float _shakeIntensity = 20f; 
        
        public TMPTextEffectShake(IReadOnlyDictionary<string,MarkupValue> properties) : base(properties)
        {
            _shakeIntensity = MarkupUtility.GetPropertyNumberValue(properties, "shake", _shakeIntensity);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void Operation(TMP_MeshInfo meshInfo, TMP_MeshInfo cachedMeshInfo, int vertexIndex, int iteration)
        {
            float offset = Random.Range(-_shakeIntensity, _shakeIntensity);

            Vector3[] vertices = meshInfo.vertices;
            Vector3[] cachedVertices = cachedMeshInfo.vertices;
            
            Vector3 axis = new Vector3(1f, 1f, 0f);
            vertices[vertexIndex]     = cachedVertices[vertexIndex]     + axis * offset;
            vertices[vertexIndex + 1] = cachedVertices[vertexIndex + 1] + axis * offset;
            vertices[vertexIndex + 2] = cachedVertices[vertexIndex + 2] + axis * offset;
            vertices[vertexIndex + 3] = cachedVertices[vertexIndex + 3] + axis * offset;
        }
    }
}