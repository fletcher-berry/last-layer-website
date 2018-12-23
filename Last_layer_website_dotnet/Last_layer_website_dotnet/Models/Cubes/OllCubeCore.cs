using Cubing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace Last_layer_website_dotnet.Models.Cubes
{
    public class OllCubeCore : OllCube, ICubeCore
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
