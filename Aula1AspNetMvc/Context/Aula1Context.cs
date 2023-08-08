using Aula1AspNetMvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Aula1AspNetMvc.Context
{
    public class Aula1Context : DbContext
    {
        public DbSet<Cliente> Cliente { get; set; }

        public Aula1Context():base("Aula1Context")
        {

        }
    }
}