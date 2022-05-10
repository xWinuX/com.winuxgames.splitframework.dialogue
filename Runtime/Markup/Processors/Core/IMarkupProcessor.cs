using System.Collections.Generic;
using Yarn.Markup;

namespace WinuXGames.SplitFramework.Dialogue.Markup.Processors
{
    public interface IMarkupProcessor
    {
        void AssignAttributes(IEnumerable<MarkupAttribute> markupAttributes);
        void Handle(int position);
    }
}