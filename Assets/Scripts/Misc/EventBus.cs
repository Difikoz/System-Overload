using System;

namespace WinterUniverse
{
    public static class EventBus
    {
        public static Action OnGameStarted;

        public static void GameStarted() => OnGameStarted?.Invoke();
    }
}