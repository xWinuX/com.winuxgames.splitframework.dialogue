using System;
using Yarn.Unity;

namespace WinuXGames.SplitFramework.Dialogue.Core
{
    public interface IDialogueOption
    {
        void AssignDialogueOption(DialogueOption           option);
        void AssignOnOptionSelected(Action<DialogueOption> action);
    }
}