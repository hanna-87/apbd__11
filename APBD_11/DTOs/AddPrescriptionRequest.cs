namespace APBD_11.DTOs;

using System.ComponentModel.DataAnnotations;

public class AddPrescriptionRequest
{
    [Required]
    public PatientDto Patient { get; set; }

    [Required]
    public DoctorDto Doctor { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(10)]
    public List<MedicamentDto> Medicaments { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public DateTime DueDate { get; set; }
}

public class PatientDto
{
    [Required]
    public int IdPatient { get; set; }
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }

    [Required]
    public DateTime Birthdate { get; set; }
}

public class DoctorDto
{
    [Required]
    public int IdDoctor { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}


public class MedicamentDto
{
    [Required]
    public int IdMedicament { get; set; }

    [Required]
    public int Dose { get; set; }

    [MaxLength(100)]
    public string Description { get; set; }
}

