using TMPro;
using UnityEngine;
using WinuXGames.SplitFramework.Dialogue.Markup.Reactors.InlineEvents;
using Yarn.Markup;

namespace WinuXGames.SplitFramework.Dialogue.Markup.Processors
{
    public class MarkupProcessorPortrait : MarkupProcessor
    {
        [SerializeField] private PortraitController _portraitController;
        [SerializeField] private TMP_Text           _text;

        protected override InlineEvent ProcessMarkupAttribute(MarkupAttribute markupAttribute, int startPosition, int endPosition)
        {
            InlineEvent inlineEvent = markupAttribute.Name switch
            {
                "mood" => new InlineEventMood(markupAttribute.Properties, _portraitController),
                _      => null
            };

            return inlineEvent;
        }
    }
}