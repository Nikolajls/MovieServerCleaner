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
            ConsoleEx.WriteLine(OutputType, ConsoleColor.DarkGreen);
            var result = Clean();
            if (!result)
                IdleAction?.Invoke();
            return result;
        }

        public abstract string OutputType { get; }
        public abstract bool Clean();
    }
}