using TMPro;
using WinuXGames.SplitFramework.Dialogue.Markup.Reactors.TMPTextEffects;

namespace WinuXGames.SplitFramework.Dialogue.Markup.Tags
{
    public class TMPTextEffectTag : Tag<TMPTextEffect>
    {
        public TMPTextEffectTag(TMPTextEffect representation, int startPosition, int endPosition) : base(representation, startPosition, endPosition) { }
        
        public void ApplyEffect(TMP_TextInfo textInfo, TMP_MeshInfo[] cachedMeshInfo, TMP_CharacterInfo[] characterInfos)
        {
            Representation.ApplyEffect(textInfo, cachedMeshInfo, characterInfos, StartPosition, EndPosition);
        }
    }
}