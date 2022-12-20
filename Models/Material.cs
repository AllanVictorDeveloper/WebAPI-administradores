using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{

    [Table("tb_alunos")]
    public partial class Material
    {
        // Propriedades
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("nome", TypeName = "varchar")]
        [Required]
        [MaxLength(150)]
        public string Nome { get; set; }

        [Column("aluno_id")]
        [Required]
        [MaxLength(8)]
        public int AlunoId { get; set; }

        
    }
}