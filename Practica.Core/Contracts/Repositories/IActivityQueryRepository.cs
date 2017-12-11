﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Practica.Core
{
    public interface IActivityQueryRepository
    {
        List<Activity> Find(ActivityFilter filters);
    }
}
