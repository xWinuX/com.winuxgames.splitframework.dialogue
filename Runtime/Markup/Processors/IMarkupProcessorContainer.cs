using System.Collections.Generic;

namespace WinuXGames.SplitFramework.Dialogue.Markup.Processors
{
    public interface IMarkupProcessorContainer
    {
        List<MarkupProcessor> MarkupProcessors { get; }
    }
}