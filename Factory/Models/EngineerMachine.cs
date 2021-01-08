using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
  public class EngineerMachine
  {
    public int EngineerMachineId  {get; set;}
    public int EngineerId  {get; set;}
    public int MachineId  {get; set;}
    public Engineer Engineer  {get; set;}
    public Machine Machine {get; set;}
  }
} 