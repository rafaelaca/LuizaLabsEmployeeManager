using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;

namespace EmployeeManager.Models
{
    [DisplayColumn("Employee")]
    [DisplayName("name")]
    [Table("tb_employee")]
    public class Employee
    {

        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("name")]
        [Key]
        public int id { get; set; }

        [Column("name")]
        [DisplayName("Name")]
        [Required]
        [MaxLength(300)]
        public string name { get; set; }

        [Column("email")]
        [DisplayName("E-mail")]
        [Required]
        [MaxLength(300)]
        public string email { get; set; }

        [Column("department")]
        [DisplayName("Department")]
        [MaxLength(300)]
        public string department { get; set; }
    }
}