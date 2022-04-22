using System.Collections.Generic;
using UnityEngine;
using WinuXGames.SplitFramework.Dialogue.Markup.Reactors.InlineEvents;
using WinuXGames.SplitFramework.Dialogue.Markup.Tags;
using Yarn.Markup;

namespace WinuXGames.SplitFramework.Dialogue.Markup.Processors
{
    public abstract class MarkupProcessor : MonoBehaviour, IMarkupProcessor
    {
        private readonly List<MarkupAttribute> _markupAttributes = new List<MarkupAttribute>();
        private readonly List<InlineEventTag>  _markupList       = new List<InlineEventTag>();

        public void AssignAttributes(IEnumerable<MarkupAttribute> markupAttributes)
        {
            _markupAttributes.Clear();
            _markupAttributes.AddRange(markupAttributes);
            Prepare();
        }

        public void Handle(int position)
        {
            // Early out if markup list is empty
            if (_markupList.Count == 0) { return; }

            List<int> removeList = new List<int>();
            for (int i = 0; i < _markupList.Count; i++)
            {
                InlineEventTag inlineEventTag = _markupList[i];

                // Early out if position doesn't match
                if (inlineEventTag.TriggerPosition != position) { continue; }

                // Run behaviour and add index to remove list if markup was closed or is self closing
                if (inlineEventTag.Run()) { removeList.Add(i); }
            }

            // Remove stuff to remove
            foreach (int i in removeList) { _markupList.RemoveAt(i); }
        }

        protected virtual InlineEvent ProcessMarkupAttribute(MarkupAttribute markupAttribute, int startPosition, int endPosition)
        {
            InlineEvent inlineEvent = markupAttribute.Name switch
            {
                _ => null
            };

            return inlineEvent;
        }

        protected virtual void Prepare()
        {
            _markupList.Clear();

            foreach (MarkupAttribute markupAttribute in _markupAttributes)
            {
                int startPosition = markupAttribute.Position;
                int endPosition   = startPosition + markupAttribute.Length;

                InlineEvent inlineEvent = ProcessMarkupAttribute(markupAttribute, startPosition, endPosition);

                if (inlineEvent == null) { continue; }

                _markupList.Add(new InlineEventTag(inlineEvent, startPosition, endPosition));
            }
        }
    }
}