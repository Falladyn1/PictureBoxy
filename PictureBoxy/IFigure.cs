using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureBoxy
{
    internal interface IFigure
    {
        void Update(int width, int height);
        void Draw(Graphics g);

    }
}
