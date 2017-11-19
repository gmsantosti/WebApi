using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Diff
    {
        public string Id { get; set; }

        public string Left { get; set; }

        public string Right { get; set; }
    }
}