using Cubing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;

namespace Last_layer_website_dotnet.Models.Cubes
{
    public class OneLookLLCubeCore : OneLookLLCube, ICubeCore
    {
        public Bitmap DrawCore()
        {
            var bitmap = new Bitmap(100, 100, PixelFormat.Format32bppArgb);
            var graphics = Graphics.FromImage(bitmap);
            Paint2D(graphics, 0.25, -25, -10, 18);
            return bitmap;
        }
    }
}
