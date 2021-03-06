﻿using Microsoft.Xna.Framework;
using RainBase.Entities.GraphComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainWindows.Controller
{
    interface ICameraController
    {
        void UpdateCamera(Vector3 position, Matrix rotation);
        GraphComponent CastPickRay(int screenX, int screenY);
    }
}
