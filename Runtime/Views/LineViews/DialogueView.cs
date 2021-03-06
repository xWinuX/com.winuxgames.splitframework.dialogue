using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WinuXGames.SplitFramework.Dialogue.LineAdvanceEffects;
using WinuXGames.SplitFramework.Dialogue.Markup.Processors;
using WinuXGames.SplitFramework.Dialogue.Markup.Processors.Core;
using Yarn.Unity;

namespace WinuXGames.SplitFramework.Dialogue.Views.LineViews
{
    public class DialogueView : DialogueViewBase, IMarkupProcessorContainer
    {
        [SerializeField] private CanvasGroup                 _canvasGroup;
        [SerializeField] private TMP_Text                    _tmpText;
        [SerializeField] private DialogueLetterRevealHandler _dialogueLetterRevealHandler;
        [SerializeField] private List<MarkupProcessor>       _markupProcessors;

        public virtual bool ReadyForNextLine => LineAdvanceEffectFinished;

        public List<MarkupProcessor> MarkupProcessors => _markupProcessors;

        private LocalizedLine     _currentLine;
        private Action<int, char> _onLetterChange;

        protected Action                      OnComplete;
        protected Action                      CurrentOnDialogueFinishedAction;
        protected bool                        LineAdvanceEffectFinished;

        private void OnEnable() { _onLetterChange += OnLetterChange; }

        private void OnDisable() { _onLetterChange -= OnLetterChange; }

        protected virtual void OnLetterChange(int position, char character)
        {
            foreach (IMarkupProcessor markupProcessor in _markupProcessors) { markupProcessor.Handle(position); }
        }

        public override void DismissLine(Action onDismissalComplete)
        {
            _dialogueLetterRevealHandler.Stop();
            onDismissalComplete.Invoke();
        }

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
            _tmpText.text                 = _currentLine.Text.Text;
            _tmpText.maxVisibleCharacters = 0;

            Canvas.ForceUpdateCanvases();

            // Prepare Markup processor
            foreach (IMarkupProcessor markupProcessor in _markupProcessors) { markupProcessor.AssignAttributes(_currentLine.Text.Attributes); }

            // Start line advance effect
            OnComplete ??= () => LineAdvanceEffectFinished = true;
            _dialogueLetterRevealHandler.StartEffect(_tmpText, _onLetterChange, OnComplete);

            CurrentOnDialogueFinishedAction = onDialogueLineFinished;
        }

        public override void UserRequestedViewAdvancement()
        {
            if (!LineAdvanceEffectFinished) { return; }

            CurrentOnDialogueFinishedAction.Invoke();
            LineAdvanceEffectFinished = false;
        }
    }
}