using CanvasDrawing.UtalEngine2D_2023_1;
using System.Collections.Generic;

namespace CanvasDrawing.Game
{
    public class FrameManager: EmptyUpdatable
    {
        public static List<Frame> AllFrames = new List<Frame>();
        public static int selectedIndex;
        public static FrameManager Instance;
        public override void Update()
        {
            // Centrar la cámara en el personaje seleccionado
            if (AllFrames.Count > selectedIndex)
            {
                Vector2 selectedCharacterPosition = AllFrames[selectedIndex].transform.position;
                float cameraX = selectedCharacterPosition.x - (GameEngine.MainCamera.xSize / 2);
                float cameraY = selectedCharacterPosition.y - (GameEngine.MainCamera.ySize / 2);
                GameEngine.MainCamera.Position = new Vector2(cameraX, cameraY);
            }
        }

    }
}
