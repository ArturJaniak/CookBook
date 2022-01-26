﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Subs
    {
        public Guid Id { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }


        [ForeignKey(nameof(Sub))]
        public string SubId { get; set; }
        public User Sub { get; set; }
    }
}
