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
        private readonly List<InlineEventTag>  _inlineEvents     = new List<InlineEventTag>();
        private readonly List<int>             _removeList       = new List<int>();

        public void AssignAttributes(IEnumerable<MarkupAttribute> markupAttributes)
        {
            _markupAttributes.Clear();
            _markupAttributes.AddRange(markupAttributes);
            Prepare();
        }

        public void Handle(int position)
        {
            // Early out if markup list is empty
            if (_inlineEvents.Count == 0) { return; }

            _removeList.Clear();
            for (int i = 0; i < _inlineEvents.Count; i++)
            {
                InlineEventTag inlineEventTag = _inlineEvents[i];

                // Early out if position doesn't match
                if (inlineEventTag.TriggerPosition != position) { continue; }

                // Run behaviour and add index to remove list if markup was closed or is self closing
                if (inlineEventTag.Run()) { _removeList.Add(i); }
            }

            // Remove stuff to remove
            foreach (int i in _removeList) { _inlineEvents.RemoveAt(i); }
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
            _inlineEvents.Clear();

            foreach (MarkupAttribute markupAttribute in _markupAttributes)
            {
                int startPosition = markupAttribute.Position;
                int endPosition   = startPosition + markupAttribute.Length;

                InlineEvent inlineEvent = ProcessMarkupAttribute(markupAttribute, startPosition, endPosition);

                if (inlineEvent == null) { continue; }

                _inlineEvents.Add(new InlineEventTag(inlineEvent, startPosition, endPosition));
            }
        }
    }
}