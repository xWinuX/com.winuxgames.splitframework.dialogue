using System.Collections.Generic;
using Yarn.Markup;

namespace WinuXGames.SplitFramework.Dialogue.Markup.Reactors.InlineEvents
{
    public class InlineEventMood : InlineEvent
    {
        private          CharacterEmotion   _previousEmotion;
        private readonly PortraitController _portraitController;

        private readonly string _mood = "neutral";

        public InlineEventMood(IReadOnlyDictionary<string, MarkupValue> properties, PortraitController portraitController)
        {
            _portraitController = portraitController;
            _mood               = GetPropertyStringValue(properties, "mood", _mood);
        }

        public override void Set()
        {
            _previousEmotion = _portraitController.CurrentEmotion;
            _portraitController.ChangeEmotion(_mood);
        }

        public override void Reset() { _portraitController.ChangeEmotion(_previousEmotion); }
    }
}