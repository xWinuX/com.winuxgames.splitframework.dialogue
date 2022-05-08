using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WinuXGames.SplitFramework.UI.Elements;
using WinuXGames.SplitFramework.UI.Providers;
using WinuXGames.SplitFramework.UI.Selectables;
using Yarn.Unity;

namespace WinuXGames.SplitFramework.Dialogue
{
    public class SelectablesListView : DialogueViewBase, ISelectablesContainer
    {
        [Header("General")]
        [SerializeField] private SO_UIDependencyProvider _uiDependency;

        [Header("Scene Dependencies")]
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TMP_Text  _lastLineText;
        [SerializeField] private Transform _optionsParent;

        [Header("Prefabs")]
        [SerializeField] private UISelectableTextDialogueOption _uiSelectablePrefab;
        [SerializeField] private UISelector _selectorPrefab;

        public List<ISelectable> Selectables { get; } = new List<ISelectable>();

        private readonly List<UISelectableTextDialogueOption> _selectables = new List<UISelectableTextDialogueOption>();

        private Action<int>   _onOptionSelected;
        private LocalizedLine _lastSeenLine;

        private void Awake() { Hide(); }

        private void Hide()
        {
            _canvasGroup.alpha          = 0;
            _canvasGroup.interactable   = false;
            _canvasGroup.blocksRaycasts = false;
        }

        private void Show()
        {
            _canvasGroup.alpha          = 1;
            _canvasGroup.interactable   = true;
            _canvasGroup.blocksRaycasts = true;
        }
        
        public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
        {
            _lastSeenLine = dialogueLine;
            onDialogueLineFinished();
        }

        public override void RunOptions(DialogueOption[] dialogueOptions, Action<int> onOptionSelected)
        {
            Show();
            
            foreach (UISelectableTextDialogueOption selectable in _selectables) { selectable.gameObject.SetActive(false); }

            while (dialogueOptions.Length > _selectables.Count)
            {
                UISelectableTextDialogueOption selectable = CreateNewSelectable();
                selectable.gameObject.SetActive(false);
            }

            Debug.Log(dialogueOptions.Length);
            Selectables.Clear();
            for (int i = 0; i < dialogueOptions.Length; i++)
            {
                UISelectableTextDialogueOption optionView = _selectables[i];
                DialogueOption                 option     = dialogueOptions[i];

                if (option.IsAvailable == false) { continue; }

                optionView.gameObject.SetActive(true);
                optionView.AssignDialogueOption(option);

                Selectables.Add(optionView);
                Debug.Log("selectables added");
                Debug.Log(Selectables.Count);
            }

            if (_lastLineText != null)
            {
                if (_lastSeenLine != null)
                {
                    _lastLineText.gameObject.SetActive(true);
                    _lastLineText.text = _lastSeenLine.Text.Text;
                }
                else { _lastLineText.gameObject.SetActive(false); }
            }

            _uiDependency.SelectableManager.SetSelectableContainer(this, _selectorPrefab);

            _onOptionSelected = onOptionSelected;
        }

        private UISelectableTextDialogueOption CreateNewSelectable()
        {
            UISelectableTextDialogueOption selectable = Instantiate(_uiSelectablePrefab, _optionsParent, false);
            selectable.transform.SetAsLastSibling();

            selectable.AssignOnOptionSelected(OptionViewWasSelected);
            _selectables.Add(selectable);

            return selectable;
        }

        private void OptionViewWasSelected(DialogueOption option)
        {
            _onOptionSelected(option.DialogueOptionID);
            _uiDependency.SelectableManager.GoBack();
            Hide();
        }
    }
}