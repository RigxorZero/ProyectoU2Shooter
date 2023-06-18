
using System;

namespace CanvasDrawing.UtalEngine2D_2023_1
{
    public static class Time
    {
        private static DateTime previousTime = DateTime.Now;
        public static double deltaTimeDouble { get; private set; }
        public static float deltaTime { get; private set; }

        public static void UpdateDeltaTime()
        {
            DateTime currentTime = DateTime.Now;
            TimeSpan elapsedTime = currentTime - previousTime;
            deltaTimeDouble = elapsedTime.TotalSeconds;
            deltaTime = (float)deltaTimeDouble;
            previousTime = currentTime;
        }
    }
}



