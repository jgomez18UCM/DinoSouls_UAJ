﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telemetria
{
    public interface ISerializer
    {
        object Serialize(Event e);
    }
}
