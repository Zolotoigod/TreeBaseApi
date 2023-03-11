﻿using System.Xml.Linq;

namespace TreeBase.Models
{
    public class Node
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string? Name { get; set; }

        public string? TreeName { get; set; }
        public string? Description { get; set; }
    }
}
