using System.Collections.Generic;
using WinuXGames.SplitFramework.Dialogue.Markup.Processors.Core;

namespace WinuXGames.SplitFramework.Dialogue.Markup.Processors
{
    public interface IMarkupProcessorContainer
    {
        List<MarkupProcessor> MarkupProcessors { get; }
    }
}