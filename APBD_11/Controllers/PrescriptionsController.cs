using APBD_11.DTOs;
using APBD_11.Services;
using Microsoft.AspNetCore.Mvc;
using TestFinal_APBD.Exceptions;

namespace APBD_11.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController : ControllerBase    
{
    private readonly IPrescriptionService _service;

    public PrescriptionsController(IPrescriptionService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] AddPrescriptionRequest request)
    {
        try
        {
            await _service.AddPrescriptionAsync(request);
            return Ok("Prescription added.");
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ConflictException e)
        {
            return Conflict(e.Message);
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var result = await _service.GetPatientAsync(id);
            return Ok(result);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ConflictException e)
        {
            return Conflict(e.Message);
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}