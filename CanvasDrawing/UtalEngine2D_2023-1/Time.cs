
using System;

namespace CanvasDrawing.UtalEngine2D_2023_1
{
    public static class Time
    {
        private static DateTime previousTime = DateTime.Now;
        public static double deltaTimeDouble { get; private set; }
        public static float deltaTime { get; private set; }
        private static float timeScale = 1f; // Variable para el factor de escala del tiempo

        public static void UpdateDeltaTime()
        {
            DateTime currentTime = DateTime.Now;
            TimeSpan elapsedTime = currentTime - previousTime;
            deltaTimeDouble = elapsedTime.TotalSeconds * timeScale; // Ralentizar el tiempo
            deltaTime = (float)deltaTimeDouble;
            previousTime = currentTime;
        }

        public static void SetTimeScale(float scale)
        {
            timeScale = scale;
        }
    }
}



