using Microsoft.EntityFrameworkCore;

namespace APBD_11.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[PrimaryKey (nameof(IdMedicament), nameof(IdPrescription))]
[Table("Prescription_Medicament")]

public class PrescriptionMedicament
{
    [ForeignKey("Medicament")]
    public int IdMedicament { get; set; }

    [ForeignKey("Prescription")]
    public int IdPrescription { get; set; }

    public Medicament Medicament { get; set; }
    public Prescription Prescription { get; set; }

    [Required]
    public int Dose { get; set; }

    [MaxLength(100)]
    public string Description { get; set; }
    
}
