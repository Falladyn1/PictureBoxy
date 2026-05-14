using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PictureBoxy
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(Square), "squares")]
    [JsonDerivedType(typeof(Circle), "circles")]

    internal interface IFigure
    {
        void Update(int width, int height);
        void Draw(Graphics g);
        bool IsFinished { get; }

    }
}
