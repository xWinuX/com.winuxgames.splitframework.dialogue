using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using WinuXGames.SplitFramework.Dialogue.LineAdvanceEffects;
using WinuXGames.SplitFramework.Dialogue.Markup.Processors;
using Yarn.Unity;

namespace WinuXGames.SplitFramework.Dialogue
{
    public class CustomLineView : DialogueViewBase
    {
        [SerializeField] private CanvasGroup           _canvasGroup;
        [SerializeField] private TMP_Text              _lineText;
        //[SerializeField] private EventReference        _talkingSoundEvent;
        [SerializeField] private InputActionReference  _continueActionReference;
        [SerializeField] private LineViewAdvanceEffect _lineViewAdvanceEffect;

        [SerializeField] private List<MarkupProcessor> _markupProcessors;

        private LocalizedLine     _currentLine;
        private Action<int, char> _onLetterChange;
        
        private void Awake() { _canvasGroup.alpha = 0; }

        private void OnEnable()
        {
            if (_continueActionReference == null) { return; }

            _continueActionReference.action.Enable();
            _continueActionReference.action.performed += UserPerformedSkipAction;
            _onLetterChange                           += OnLetterChange;
        }

        private void OnDisable()
        {
            if (_continueActionReference == null) { return; }

            _continueActionReference.action.Disable();
            _continueActionReference.action.performed -= UserPerformedSkipAction;
            _onLetterChange                           -= OnLetterChange;
        }

        private void OnLetterChange(int position, char character)
        {
            //if (character != ' ') { RuntimeManager.PlayOneShot(_talkingSoundEvent, transform.position); }

            foreach (IMarkupProcessor markupProcessor in _markupProcessors) { markupProcessor.Handle(position); }
        }

        public override void DismissLine(Action onDismissalComplete)
        {
            _currentLine = null;

            onDismissalComplete.Invoke();
        }

        private void UserPerformedSkipAction(InputAction.CallbackContext context)
        {
            if (_currentLine == null || _currentLine.Status == LineStatus.Presenting) { return; }

            ReadyForNextLine();
        }

        public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
        {
            // Immediately appear 
            _lineText.gameObject.SetActive(true);
            _canvasGroup.gameObject.SetActive(true);
            _canvasGroup.interactable   = true;
            _canvasGroup.alpha          = 1;
            _canvasGroup.blocksRaycasts = true;

            _currentLine = dialogueLine;

            // Get preprocessed text
            _lineText.text = _currentLine.Text.Text;

            // Prepare Markup processor
            foreach (IMarkupProcessor markupProcessor in _markupProcessors) { markupProcessor.AssignAttributes(_currentLine.Text.Attributes); }

            // Start line advance effect
            _lineViewAdvanceEffect.StartEffect(_lineText, _onLetterChange, onDialogueLineFinished);
        }
    }
}