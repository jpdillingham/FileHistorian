﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHistorian.Domain.Entities
{
    public class Scan
    {
        public IEnumerable<File> Files { get; set; }
    }
}
