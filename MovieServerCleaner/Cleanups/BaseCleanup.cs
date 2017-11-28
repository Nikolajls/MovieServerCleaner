using System;

namespace MovieServerCleaner.Cleanups
{
    public abstract class BaseCleanup
    {
        private readonly CleanupType _cleanupType;

        protected BaseCleanup(CleanupType cleanupCleanupType)
        {
            _cleanupType = cleanupCleanupType;
        }

        protected BaseCleanup(CleanupType cleanupCleanupType, Action cIdleAction)
        {
            _cleanupType = cleanupCleanupType;
            IdleAction = cIdleAction;
        }

        public Action IdleAction { get; private set; } = () =>
        {
            Console.WriteLine("Idling");
            Console.ReadLine();
        };

        public void SetIdleAction(Action newIdle)
        {
            IdleAction = newIdle;
        }

        public bool Run()
        {
            var outputType = string.Empty;
            switch (_cleanupType)
            {
                case CleanupType.CheckForRars:
                    outputType = "Checking for any exisiting rar files";
                    break;
                case CleanupType.RenameFolders:
                    outputType = "Renaming moviefolders";
                    break;
                case CleanupType.DeleteSubtitles:
                    outputType = "Deleting all irrelevant subtitles";
                    break;
                case CleanupType.DeleteSamples:
                    outputType = "Deleting all samples";
                    break;
                case CleanupType.MoveSubtitles:
                    outputType = "Moving remaining subtitles";
                    break;
                case CleanupType.RenamingFiles:
                    outputType = "Renaming all files in directory";
                    break;
                case CleanupType.CopyfoFiles:
                    outputType = "Moving NFO files";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            ConsoleEx.WriteLine(outputType, ConsoleColor.DarkGreen);
            var result = Clean();
            if (!result)
                IdleAction?.Invoke();
            return result;
        }


        public abstract bool Clean();
    }
}