using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cqrswritewebapi.Models
{
    public class TelevisionViewModel
    {
        public Guid Id { get; set; }

        public bool IsOn { get; set; }

        public int Channel { get; set; }

        public int Version { get; set; }
    }
}