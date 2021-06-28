using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace migrationProject.Models
{
    public class Cadre
    { /*PA,PH,pes,ingénieur,administrateur,technicien,administrateur adjoint,technicien adjoint */
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCadre { get; set; }
        [Required]
        public string Nom { get; set; }

        public string NomComplet{ get; set; }

        public int idCorps { get; set; }

        public virtual IList<Personnel> Personnels { get; set; }
        public virtual IList<Grades> Grades{ get; set; }
    }
}
