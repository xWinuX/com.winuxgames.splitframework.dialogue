using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using WinuXGames.SplitFramework.Dialogue.Markup.Reactors.InlineEvents;
using Yarn.Markup;

namespace WinuXGames.SplitFramework.Dialogue.Markup.Processors
{
    [RequireComponent(typeof(TMP_Text))]
    public class MarkupProcessorTMPTextTags : MarkupProcessor
    {
        private readonly SortedList<int, string> _richTextTags = new SortedList<int, string>();

        private TMP_Text _tmpText;

        private void Awake() { _tmpText = GetComponent<TMP_Text>(); }

        protected override InlineEvent ProcessMarkupAttribute(MarkupAttribute markupAttribute, int startPosition, int endPosition)
        {
            // Preprocessed attributes
            switch (markupAttribute.Name)
            {
                // Rich text tags
                case "color":
                case "b":
                    // Construct properties
                    StringBuilder sb = new StringBuilder("<" + markupAttribute.Name);
                    foreach ((string key, MarkupValue value) in markupAttribute.Properties)
                    {
                        // Handle shorthand tags ex. <color=blue>
                        if (key == markupAttribute.Name)
                        {
                            sb.Insert(key.Length + 1, "=" + value);
                            continue;
                        }

                        sb.Append(" " + key + "=" + value);
                    }

                    // Close tag and add to list
                    sb.Append(">");
                    _richTextTags.Add(startPosition, sb.ToString());

                    // Construct closing tag if it isn't a self closing one and add to list
                    if (startPosition != endPosition) { _richTextTags.Add(endPosition, "</" + markupAttribute.Name + ">"); }

                    break;
            }

            return null;
        }

        protected override void Prepare()
        {
            _richTextTags.Clear();

            base.Prepare();

            // Wait 1 frame for rich text tag insertion, because tmp needs to parse them first
            StartCoroutine(WaitForTMPToProcessNewStringCoroutine());
        }

        private void InsertRichTextTagsIntoText()
        {
            StringBuilder processedTextBuilder = new StringBuilder(_tmpText.GetParsedText());
            int           offset               = 0;
            foreach ((int key, string value) in _richTextTags)
            {
                int index = key + offset;

                processedTextBuilder.Insert(index, value);

                offset += value.Length;
            }

            _tmpText.text = processedTextBuilder.ToString();
        }

        private IEnumerator WaitForTMPToProcessNewStringCoroutine()
        {
            yield return null;
            InsertRichTextTagsIntoText();
        }
    }
}