using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using WinuXGames.SplitFramework.Dialogue.Markup.Utility;
using Yarn.Markup;

namespace WinuXGames.SplitFramework.Dialogue.Markup.Reactors.TMPTextEffects
{
    public class TMPTextEffectColor : TMPTextEffect
    {
        private readonly Color32 _color;

        public TMPTextEffectColor(IReadOnlyDictionary<string, MarkupValue> properties) : base(properties)
        {
            string colorName = MarkupUtility.GetPropertyStringValue(properties, "color", "white");

            _color = colorName switch
            {
                "white" => Color.white,
                "red"   => Color.red,
                "blue"  => Color.blue,
                "green" => Color.green,
                _       => Color.white,
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void Operation(TMP_MeshInfo meshInfo, TMP_MeshInfo cachedMeshInfo, int vertexIndex, int iteration)
        {
            Color32[] colors       = meshInfo.colors32;

            colors[vertexIndex]     = new Color32(_color.r, _color.g, _color.b, colors[vertexIndex].a);
            colors[vertexIndex + 1] = new Color32(_color.r, _color.g, _color.b, colors[vertexIndex + 1].a);
            colors[vertexIndex + 2] = new Color32(_color.r, _color.g, _color.b, colors[vertexIndex + 2].a);
            colors[vertexIndex + 3] = new Color32(_color.r, _color.g, _color.b, colors[vertexIndex + 3].a);
        }
    }
}