using Yarn;
using Yarn.Markup;

namespace WinuXGames.SplitFramework.Dialogue.Utility
{
    public static class DialogueUtility
    {
        private static readonly Yarn.Dialogue Dialogue = new Yarn.Dialogue(new MemoryVariableStore());
        public static MarkupParseResult ParseMarkup(string text) => Dialogue.ParseMarkup(text);
    }
}