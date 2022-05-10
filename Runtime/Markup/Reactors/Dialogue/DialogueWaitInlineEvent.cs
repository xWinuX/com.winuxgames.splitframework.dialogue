using System.Collections.Generic;
using WinuXGames.SplitFramework.Dialogue.LineAdvanceEffects;
using WinuXGames.SplitFramework.Dialogue.Markup.Utility;
using Yarn.Markup;

namespace WinuXGames.SplitFramework.Dialogue.Markup.Reactors.Dialogue
{
    public class DialogueWaitInlineEvent : InlineEvent
    {
        private readonly DialogueLetterRevealHandler _dialogueLetterRevealHandler;
        private readonly float                 _seconds;

        public DialogueWaitInlineEvent(IReadOnlyDictionary<string, MarkupValue> properties, DialogueLetterRevealHandler dialogueLetterRevealHandler)
        {
            _dialogueLetterRevealHandler = dialogueLetterRevealHandler;
            _seconds               = MarkupUtility.GetPropertyNumberValue(properties, "wait", 1f);
        }
        
        public override void Set() { _dialogueLetterRevealHandler.PauseFor(_seconds); }
    }
}