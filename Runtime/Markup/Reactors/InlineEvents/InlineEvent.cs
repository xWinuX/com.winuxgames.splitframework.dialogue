using WinuXGames.SplitFramework.Dialogue.Markup.Utility;

namespace WinuXGames.SplitFramework.Dialogue.Markup.Reactors.InlineEvents
{
    public abstract class InlineEvent : MarkupUtility
    {
        public virtual void Set() { }

        public virtual void Reset() { }
    }
}