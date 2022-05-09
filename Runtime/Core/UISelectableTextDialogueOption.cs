using System;
using WinuXGames.SplitFramework.UI.Selectables;
using Yarn.Unity;

namespace WinuXGames.SplitFramework.Dialogue.Core
{
    public class UISelectableTextDialogueOption : UISelectableText, IDialogueOption
    {
        private DialogueOption         _dialogueOption;
        private Action<DialogueOption> _onOptionSelected;

        protected override void OnEnable()
        {
            base.OnEnable();
            OnSubmitUnityEvent.AddListener(OnOptionSelected);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            OnSubmitUnityEvent.RemoveListener(OnOptionSelected);
        }

        public void AssignDialogueOption(DialogueOption option)
        {
            _dialogueOption = option;
            UIElement.SetText(_dialogueOption.Line.TextWithoutCharacterName.Text);
        }

        public void AssignOnOptionSelected(Action<DialogueOption> action) { _onOptionSelected = action; }

        public void OnOptionSelected() { _onOptionSelected.Invoke(_dialogueOption); }
    }
}