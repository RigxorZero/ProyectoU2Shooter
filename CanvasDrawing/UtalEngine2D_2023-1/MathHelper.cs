using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDrawing.UtalEngine2D_2023_1
{
    public static class MathHelper
    {
        public static float ToRadians(float degrees)
        {
            return degrees * (float)Math.PI / 180f;
        }
    }
}

