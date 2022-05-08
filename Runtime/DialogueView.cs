using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WinuXGames.SplitFramework.Dialogue.LineAdvanceEffects;
using WinuXGames.SplitFramework.Dialogue.Markup.Processors;
using Yarn.Unity;

namespace WinuXGames.SplitFramework.Dialogue
{
    public class DialogueView : DialogueViewBase
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        [SerializeField] private TMP_Text              _tmpText;
        [SerializeField] private LineViewAdvanceEffect _lineViewAdvanceEffect;
        [SerializeField] private List<MarkupProcessor> _markupProcessors;

        private LocalizedLine     _currentLine;
        private Action<int, char> _onLetterChange;

        private Action _currentOnDialogueFinishedAction;
        private bool   _lineAdvanceEffectFinished;

        private void OnEnable() { _onLetterChange += OnLetterChange; }

        private void OnDisable() { _onLetterChange -= OnLetterChange; }

        private void OnLetterChange(int position, char character)
        {
            foreach (IMarkupProcessor markupProcessor in _markupProcessors) { markupProcessor.Handle(position); }
        }

        public override void DismissLine(Action onDismissalComplete) { onDismissalComplete.Invoke(); }

        public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
        {
            // Immediately appear 
            _tmpText.gameObject.SetActive(true);
            _canvasGroup.gameObject.SetActive(true);
            _canvasGroup.interactable   = true;
            _canvasGroup.alpha          = 1;
            _canvasGroup.blocksRaycasts = true;

            _currentLine = dialogueLine;

            // Get preprocessed text
            _tmpText.text    = _currentLine.Text.Text;
            _tmpText.enabled = true;

            // Prepare Markup processor
            foreach (IMarkupProcessor markupProcessor in _markupProcessors) { markupProcessor.AssignAttributes(_currentLine.Text.Attributes); }

            // Start line advance effect
            _lineViewAdvanceEffect.StartEffect(_tmpText, _onLetterChange, () => _lineAdvanceEffectFinished = true);

            _currentOnDialogueFinishedAction = onDialogueLineFinished;
        }

        public override void UserRequestedViewAdvancement()
        {
            if (!_lineAdvanceEffectFinished) { return; }

            _tmpText.enabled = false;

            _currentOnDialogueFinishedAction.Invoke();
            _lineAdvanceEffectFinished = false;
        }
    }
}