using WinuXGames.SplitFramework.Dialogue.Markup.Reactors.InlineEvents;

namespace WinuXGames.SplitFramework.Dialogue.Markup.Tags
{
    public class InlineEventTag : Tag<InlineEvent>
    {
        public int TriggerPosition => _closeNext ? EndPosition : StartPosition;

        private bool _closeNext;

        public InlineEventTag(InlineEvent inlineEvent, int startPosition, int endPosition) : base(inlineEvent, startPosition, endPosition) { }

        public bool Run()
        {
            if (!_closeNext)
            {
                Representation.Set();
                _closeNext = true;
                return StartPosition == EndPosition;
            }

            Representation.Reset();
            return true;
        }
    }
}