
using System.Collections.Generic;
using System.Windows.Forms;

namespace CanvasDrawing.UtalEngine2D_2023_1
{
    public static class InputManager
    {
        private static List<Keys> keysDown = new List<Keys>();
        public static Dictionary<Keys, bool> keysPressed = new Dictionary<Keys, bool>();
        private static List<Keys> keysUp = new List<Keys>();
        private static List<KeyPressEventArgs> lastFrameKeyEvents = new List<KeyPressEventArgs>();

        public static void KeyPressHandler(object sender, KeyPressEventArgs e)
        {
            lastFrameKeyEvents.Add(e);
            // Agregar la lógica adicional aquí si es necesario
        }

        public static void KeyDownHandler(object sender, KeyEventArgs e)
        {
            if (!keysDown.Contains(e.KeyCode))
            {
                keysDown.Add(e.KeyCode);
                keysPressed.Add(e.KeyCode, true);
            }
        }

        public static void KeyUpHandler(object sender, KeyEventArgs e)
        {
            keysDown.Remove(e.KeyCode);
            keysPressed.Remove(e.KeyCode);
            keysUp.Add(e.KeyCode);
        }

        public static void Update()
        {
            keysPressed.Clear();
            keysUp.Clear();
            lastFrameKeyEvents.Clear();
        }

        public static bool GetKey(Keys keyCode)
        {
            return keysDown.Contains(keyCode) || keysPressed.ContainsKey(keyCode);
        }

        public static bool GetKeyDown(Keys keyCode)
        {
            return keysDown.Contains(keyCode);
        }

        public static bool GetKeyPressed(Keys keyCode)
        {
            return keysPressed.ContainsKey(keyCode);
        }

        public static bool GetKeyUp(Keys keyCode)
        {
            return keysUp.Contains(keyCode);
        }
    }
}


