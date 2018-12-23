using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using Cubing;

namespace Last_layer_website_dotnet.Models.Cubes
{
    public interface ICubeCore : ICube
    {
        Bitmap DrawCore();
    }
}
