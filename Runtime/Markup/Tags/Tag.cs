namespace WinuXGames.SplitFramework.Dialogue.Markup.Tags
{
    public class Tag<T>
    {
        protected int StartPosition { get; }

        protected int EndPosition { get; }

        protected readonly T Representation;

        protected Tag(T representation, int startPosition, int endPosition)
        {
            Representation = representation;
            StartPosition  = startPosition;
            EndPosition    = endPosition;
        }
    }
}