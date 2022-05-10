using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using WinuXGames.SplitFramework.Dialogue.Markup.Utility;
using Yarn.Markup;

namespace WinuXGames.SplitFramework.Dialogue.Markup.Reactors.TMPTextEffects
{
    public class TMPTextEffectWave : TMPTextEffect
    {
        protected override bool UpdateEachFrame { get; set; } = true;

        private readonly float _frequency = 50f;
        private readonly float _amplitude = 4f;
        private readonly float _speed     = 2f;

        public TMPTextEffectWave(IReadOnlyDictionary<string, MarkupValue> properties) : base(properties)
        {
            _frequency = MarkupUtility.GetPropertyNumberValue(properties, "freq", _frequency) * Mathf.Deg2Rad;
            _amplitude = MarkupUtility.GetPropertyNumberValue(properties, "amp", _amplitude);
            _speed     = MarkupUtility.GetPropertyNumberValue(properties, "spd", _speed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void Operation(TMP_MeshInfo meshInfo, TMP_MeshInfo cachedMeshInfo, int vertexIndex, int iteration)
        {
            float offset = Mathf.Sin((Time.time * _speed) + (vertexIndex * _frequency)) * _amplitude;

            Vector3[] vertices       = meshInfo.vertices;
            Vector3[] cachedVertices = cachedMeshInfo.vertices;

            vertices[vertexIndex]     = cachedVertices[vertexIndex] + Vector3.up * offset;
            vertices[vertexIndex + 1] = cachedVertices[vertexIndex + 1] + Vector3.up * offset;
            vertices[vertexIndex + 2] = cachedVertices[vertexIndex + 2] + Vector3.up * offset;
            vertices[vertexIndex + 3] = cachedVertices[vertexIndex + 3] + Vector3.up * offset;
        }
    }
}