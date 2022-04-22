using System.Collections.Generic;
using UnityEngine;
using Yarn.Markup;

namespace WinuXGames.SplitFramework.Dialogue.Markup.Reactors.TMPTextEffects
{
    public class TMPTextEffectWave : TMPTextEffect
    {
        private readonly float _frequency = 50f;
        private readonly float _amplitude = 4f;
        private readonly float _speed     = 2f;

        public TMPTextEffectWave(IReadOnlyDictionary<string, MarkupValue> properties)
        {
            _frequency = GetPropertyNumberValue(properties, "freq", _frequency) * Mathf.Deg2Rad;
            _amplitude = GetPropertyNumberValue(properties, "amp", _amplitude);
            _speed     = GetPropertyNumberValue(properties, "spd", _speed);
        }

        protected override void Operation(Vector3[] vertices, Color32[] colors, int vertexIndex, int iteration)
        {
            float offset = Mathf.Sin((Time.time * _speed) + (vertexIndex * _frequency)) * _amplitude;

            vertices[vertexIndex]     += Vector3.up * offset;
            vertices[vertexIndex + 1] += Vector3.up * offset;
            vertices[vertexIndex + 2] += Vector3.up * offset;
            vertices[vertexIndex + 3] += Vector3.up * offset;
        }
    }
}