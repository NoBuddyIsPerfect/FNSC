﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FNSC.Data
{
    [DebuggerDisplay("{Description, Id}")]
    public class Song
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Description{ get; set; }
        public string Code{ get; set; }
        public string Channel{ get; set; }
        public TimeSpan Length{ get; set; }
        public bool IsBlocked{ get; set; }
        public Uri Url{ get; set; }
        public Viewer Viewer{ get; set; }
        [NotMapped]
        public bool isOut { get; set; } = false;
        [NotMapped]
        public bool won { get; set; } = false;
        public int InitialStarttime { get; set; }
        public int Starttime { get; set; }

        public override string ToString()
        {
            return Description;
        }
    }
}