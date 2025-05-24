using APBD_11.DTOs;

namespace APBD_11.Services;

public interface IPrescriptionService
{
    Task AddPrescriptionAsync(AddPrescriptionRequest request);
    Task<PatientResponseDto> GetPatientAsync(int id);
}