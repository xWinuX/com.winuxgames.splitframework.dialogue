using System;
using Yarn.Unity;

namespace WinuXGames.SplitFramework.Dialogue
{
    public interface IDialogueOption
    {
        void AssignDialogueOption(DialogueOption           option);
        void AssignOnOptionSelected(Action<DialogueOption> action);
    }
}