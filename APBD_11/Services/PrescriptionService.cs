using APBD_11.DAL;
using APBD_11.DTOs;
using APBD_11.Models;
using Microsoft.EntityFrameworkCore;
using TestFinal_APBD.Exceptions;

namespace APBD_11.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly DatabaseContext _context;

    public PrescriptionService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task AddPrescriptionAsync( AddPrescriptionRequest request )
    {
        if (request.Medicaments.Count > 10)
            throw new BadRequestException("Number of medicaments must be less than 10");

        if (request.DueDate < request.Date)
            throw new BadRequestException("DueDate must be higher or equal to Date.");
        
        var doctor = await _context.Doctors.FindAsync(request.Doctor.IdDoctor);
        if (doctor == null)
        {
            throw new NotFoundException("Doctor does not exist.");
        }
        
        var patient = await _context.Patients.FindAsync( request.Patient.IdPatient);

        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = request.Patient.FirstName,
                LastName = request.Patient.LastName,
                Birthdate = request.Patient.Birthdate
            };
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
        }
        
        
        
        foreach (var med in request.Medicaments)
        {
            if (!_context.Medicaments.Any(m => m.IdMedicament == med.IdMedicament))
                throw new NotFoundException($"Medicament ID {med.IdMedicament} not found.");
        }

        var prescription = new Prescription
        {
            Date = request.Date,
            DueDate = request.DueDate,
            Doctor = doctor,
            Patient = patient

        };
        await _context.Prescriptions.AddAsync(prescription);
        await _context.SaveChangesAsync();
        
        prescription.PrescriptionMedicaments = request.Medicaments.Select(m => new PrescriptionMedicament
        {
            IdMedicament = m.IdMedicament,
            IdPrescription = prescription.IdPrescription, 
            Dose = m.Dose,
            Description = " "
        }).ToList();
        
        await _context.SaveChangesAsync();
        

    }

    public async Task<PatientResponseDto> GetPatientAsync(int id)
    {
        var patient = await _context.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.Doctor)
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .FirstOrDefaultAsync(p => p.IdPatient == id);

        if (patient == null)
            throw new NotFoundException($"Patient with ID {id} not found.");

        return new PatientResponseDto
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            Prescriptions = patient.Prescriptions
                .OrderBy(p => p.DueDate)
                .Select(pr => new PrescriptionResponseDto
                {
                    IdPrescription = pr.IdPrescription,
                    Date = pr.Date,
                    DueDate = pr.DueDate,
                    Doctor = new DoctorShortDto
                    {
                        IdDoctor = pr.Doctor.IdDoctor,
                        FirstName = pr.Doctor.FirstName
                    },
                    Medicaments = pr.PrescriptionMedicaments.Select(pm => new MedicamentResponseDto
                    {
                        IdMedicament = pm.Medicament.IdMedicament,
                        Name = pm.Medicament.Name,
                        Dose = pm.Dose,
                        Description = pm.Medicament.Description
                    }).ToList()
                }).ToList()
        };
    }
    
    
    
    
}