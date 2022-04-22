using TMPro;
using WinuXGames.SplitFramework.Dialogue.Markup.Reactors.TMPTextEffects;

namespace WinuXGames.SplitFramework.Dialogue.Markup.Tags
{
    public class TMPTextEffectTag : Tag<TMPTextEffect>
    {
        public TMPTextEffectTag(TMPTextEffect representation, int startPosition, int endPosition) : base(representation, startPosition, endPosition) { }
        
        public void ApplyVertices(TMP_TextInfo textInfo, TMP_CharacterInfo[] characterInfos)
        {
            Representation.ApplyEffect(textInfo, characterInfos, StartPosition, EndPosition);
        }

    }
}