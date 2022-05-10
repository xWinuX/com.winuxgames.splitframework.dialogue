using UnityEngine;
using UnityEngine.Serialization;
using WinuXGames.SplitFramework.Dialogue.LineAdvanceEffects;
using WinuXGames.SplitFramework.Dialogue.Markup.Processors.Core;
using WinuXGames.SplitFramework.Dialogue.Markup.Reactors;
using WinuXGames.SplitFramework.Dialogue.Markup.Reactors.Dialogue;
using Yarn.Markup;
using Yarn.Unity;

namespace WinuXGames.SplitFramework.Dialogue.Markup.Processors
{
    public class MarkupProcessorDialogue : MarkupProcessor
    {
        [FormerlySerializedAs("_lineViewAdvanceEffect")] [SerializeField] private DialogueLetterRevealHandler _dialogueLetterRevealHandler;
        
        protected override InlineEvent ProcessMarkupAttribute(MarkupAttribute markupAttribute, int startPosition, int endPosition)
        {
            switch (markupAttribute.Name)
            {
                case "wait": return new DialogueWaitInlineEvent(markupAttribute.Properties, _dialogueLetterRevealHandler);
            }

            return null;
        }
    }
}