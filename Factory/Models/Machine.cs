using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
    public class Machine
    {
        public Machine()
        {
          this.Engineers = new HashSet<EngineerMachine>();
        }
        public int MachineId  {get;set;}
        public string Name {get; set;}
        [DisplayName("InspectionDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime InspectionDate { get; set; }
        public virtual ICollection<EngineerMachine> Engineers {get; set;}
    }
}