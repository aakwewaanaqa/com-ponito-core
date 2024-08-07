﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ponito.Core.Paths
{
    public readonly partial struct PathNameInfo
    {
        public PathNameInfo(string path)
        {
            names = path.Split(
                    new[]
                    {
                        Path.PathSeparator,
                        Path.DirectorySeparatorChar,
                        Path.AltDirectorySeparatorChar
                    }, StringSplitOptions.RemoveEmptyEntries)
               .Where(s => s != ".")
               .ToList();
        }

        private PathNameInfo(IEnumerable<string> path)
        {
            names = path
               .Where(s => s != ".")
               .ToList();
        }
    }
}