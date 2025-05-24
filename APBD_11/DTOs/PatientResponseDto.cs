using System.ComponentModel.DataAnnotations;

namespace APBD_11.DTOs;

public class PatientResponseDto
{
    [Required]
    public int IdPatient { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [Required]
    public List<PrescriptionResponseDto> Prescriptions { get; set; }
}

public class PrescriptionResponseDto
{
    [Required]
    public int IdPrescription { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public DateTime DueDate { get; set; }

    [Required]
    public List<MedicamentResponseDto> Medicaments { get; set; }

    [Required]
    public DoctorShortDto Doctor { get; set; }
}

public class MedicamentResponseDto
{
    [Required]
    public int IdMedicament { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    public int Dose { get; set; }

    [MaxLength(100)]
    public string Description { get; set; }
}

public class DoctorShortDto
{
    [Required]
    public int IdDoctor { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
}