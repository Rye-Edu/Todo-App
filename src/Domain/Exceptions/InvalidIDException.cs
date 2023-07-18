﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo_App.Domain.Exceptions;
public class InvalidIDException:Exception
{
    public InvalidIDException(int id) : base($"Id value{id} invalid")
    {
        
    }
}
