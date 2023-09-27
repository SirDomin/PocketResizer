using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketResizer.Models
{
    public class PocketModel
    {
        public uint Width;
        public uint Height;

        public PocketModel() { Width = 5; Height = 3; }
        public PocketModel(uint width, uint height)
        {
            Width = width;
            Height = height;
        }
    }
}
