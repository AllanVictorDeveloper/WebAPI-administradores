using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{

    [Table("adminitradores")]
    public partial class Administrador
    {
        // Propriedades
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("nome", TypeName = "varchar")]
        [Required]
        [MaxLength(150)]
        public string Nome { get; set; }

        [Column("email", TypeName = "varchar")]
        [Required]
        [MaxLength(150)]
        public string Email { get; set; }

        [Column("senha", TypeName = "varchar")]
        [Required]
        [MaxLength(10)]
        public string Senha { get; set; }

        

        
    }
}