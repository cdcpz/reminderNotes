using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensolvers.Note.Domain.ValueObjects
{
    public class TagColor
    {
        public int Alpha { get; set; }
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }

        public TagColor(Color color)
        {
            Alpha = color.A;
            Red = color.R;
            Green = color.G;
            Blue = color.B;
        }

        public TagColor(int alpha, int red, int green, int blue)
        {
            Alpha = alpha;
            Red = red;
            Green = green;
            Blue = blue;
        }
    }
}
