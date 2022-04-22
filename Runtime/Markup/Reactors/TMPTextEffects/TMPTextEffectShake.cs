using System.Collections.Generic;
using UnityEngine;
using Yarn.Markup;

namespace WinuXGames.SplitFramework.Dialogue.Markup.Reactors.TMPTextEffects
{
    public class TMPTextEffectShake : TMPTextEffect
    {
        private readonly float _shakeIntensity = 20f; 
        
        public TMPTextEffectShake(IReadOnlyDictionary<string,MarkupValue> properties)
        {
            _shakeIntensity = GetPropertyNumberValue(properties, "shake", _shakeIntensity);

        }
        
        protected override void Operation(Vector3[] vertices, Color32[] colors, int vertexIndex, int iteration)
        {
            float offset = Random.Range(-_shakeIntensity, _shakeIntensity);

            Vector3 axis = new Vector3(1f, 1f, 0f);
            vertices[vertexIndex]     += axis * offset;
            vertices[vertexIndex + 1] += axis * offset;
            vertices[vertexIndex + 2] += axis * offset;
            vertices[vertexIndex + 3] += axis * offset;
        }
    }
}