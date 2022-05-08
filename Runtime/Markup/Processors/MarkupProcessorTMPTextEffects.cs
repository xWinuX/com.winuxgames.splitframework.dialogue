using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WinuXGames.SplitFramework.Dialogue.Markup.Reactors.InlineEvents;
using WinuXGames.SplitFramework.Dialogue.Markup.Reactors.TMPTextEffects;
using WinuXGames.SplitFramework.Dialogue.Markup.Tags;
using Yarn.Markup;

namespace WinuXGames.SplitFramework.Dialogue.Markup.Processors
{
    [ExecuteAlways]
    [RequireComponent(typeof(TMP_Text))]
    public class MarkupProcessorTMPTextEffects : MarkupProcessor
    {
        private readonly List<TMPTextEffectTag> _textEffectList = new List<TMPTextEffectTag>();

        private TMP_Text       _tmpText;
        private TMP_MeshInfo[] _cachedMeshInfo;
        private string         _textBeforePreview;
        private bool           _readyToUpdate;

        private void Awake() { _tmpText = GetComponent<TMP_Text>(); }

        private void Update()
        {
            if (_readyToUpdate) { UpdateTextMesh(); }
        }

        private void OnEnable() { _tmpText.OnPreRenderText += info => { _cachedMeshInfo = info.CopyMeshInfoVertexData(); }; }

        public void EnablePreview()
        {
            _textBeforePreview = _tmpText.text;
            MarkupParseResult markupParseResult = DialogueUtility.ParseMarkup(_tmpText.text);
            AssignAttributes(markupParseResult.Attributes);
            _tmpText.text = markupParseResult.Text;
        }

        public void DisablePreview()
        {
            _tmpText.text  = _textBeforePreview;
            _readyToUpdate = false;
        }

        protected override InlineEvent ProcessMarkupAttribute(MarkupAttribute markupAttribute, int startPosition, int endPosition)
        {
            // Preprocessed attributes
            switch (markupAttribute.Name)
            {
                case "shake":
                    _textEffectList.Add(new TMPTextEffectTag(new TMPTextEffectShake(markupAttribute.Properties), startPosition, endPosition));
                    break;
                case "wave":
                    _textEffectList.Add(new TMPTextEffectTag(new TMPTextEffectWave(markupAttribute.Properties), startPosition, endPosition));
                    break;
            }

            return null;
        }

        protected override void Prepare()
        {
            _readyToUpdate = false;
            _textEffectList.Clear();

            base.Prepare();

            _readyToUpdate = true;
        }

        private void UpdateTextMesh()
        {
            foreach (TMPTextEffectTag textEffectElement in _textEffectList) { textEffectElement.ApplyEffect(_tmpText.textInfo, _cachedMeshInfo, _tmpText.textInfo.characterInfo); }

            _tmpText.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
        }
    }
}