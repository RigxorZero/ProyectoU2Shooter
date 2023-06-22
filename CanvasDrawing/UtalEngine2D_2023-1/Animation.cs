using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDrawing.UtalEngine2D_2023_1
{
    public class Animation
    {
        private List<Image> frames;
        private int currentFrameIndex;
        private float frameDuration;
        private float frameTimer;

        public Animation(List<Image> frames, float frameDuration)
        {
            this.frames = frames;
            this.frameDuration = frameDuration;
            currentFrameIndex = 0;
            frameTimer = 0;
        }

        public Image CurrentFrame
        {
            get { return frames[currentFrameIndex]; }
        }

        public void Update()
        {
            frameTimer += Time.deltaTime;

            if (frameTimer >= frameDuration)
            {
                // Avanzar al siguiente fotograma
                currentFrameIndex = (currentFrameIndex + 1) % frames.Count;
                frameTimer = 0;
            }
        }

        public void Reset()
        {
            // Reiniciar la animación al primer fotograma
            currentFrameIndex = 0;
            frameTimer = 0;
        }
    }
}

