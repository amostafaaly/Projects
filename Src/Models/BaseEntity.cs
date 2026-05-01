using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Src.Models;

    public class BaseEntity
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }