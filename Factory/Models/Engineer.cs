using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;

namespace Factory.Models
{
    public class Engineer
    {
        public Engineer()
        {
          this.Machines = new HashSet<EngineerMachine>();
        }
        public int EngineerId {get; set;}
        public string EngineerName {get;set;}
        public bool Status { get; set; } = false;
        public virtual ICollection<EngineerMachine> Machines  {get; set;}
    } 
}