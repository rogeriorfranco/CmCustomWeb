using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmCustomDto
{
    [Table("HISTORICO_ACESSO", Schema = "API")]
    public class AcessHistoric
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public decimal Id { get; set; }
        [Column("USUARIO")]
        public string usuario { get; set; }
        [Column("PERFIL")]
        public string perfil { get; set; }
        [Column("TIPO_USUARIO")]
        public string tipo_usuario { get; set; }
    }
}
