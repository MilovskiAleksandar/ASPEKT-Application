using ASPEKT.Application.Core.Models;
using ASPEKT.Application.Core.Services;
using ASPEKT.Application.DTOS.Company;
using ASPEKT.Application.Services.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;

namespace ASPEKT.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private IService<CompanyDto> _companyService;

        public CompanyController(IService<CompanyDto> companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public ActionResult<List<CompanyDto>> Get()
        {
            try
            {
                return _companyService.GetAll();
            }
            catch(NotFoundException e)
            {
                Log.Error(e, "Company not found in Get: {Message}", e.Message);
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch
            {
                Log.Fatal("An fatal error occured!");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<CompanyDto> GetCompanyById(int id)
        {
            try
            {               
                return _companyService.GetById(id);
            }
            catch (NotFoundException e)
            {
                Log.Error(e, "Company not found in GetCompanyById: {Message}", e.Message);
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch
            {
                Log.Fatal("An fatal error occured!");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }

        [HttpPost]
        public IActionResult AddingCompany([FromBody] CompanyDto company)
        {
            try
            {
                _companyService.AddEntity(company);
                return StatusCode(StatusCodes.Status201Created, "Company created successfully");
            }
            catch (WrongDataException e)
            {
                Log.Error(e, "Wrong data entered in AddingCompany: {Message}", e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch
            {
                Log.Fatal("An fatal error occured!");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] CompanyDto company)
        {
            try
            {
                _companyService.UpdateEntity(company);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (WrongDataException e)
            {
                Log.Error(e, "Wrong data entered in UpdateCompany: {Message}", e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (NotFoundException e)
            {
                Log.Error(e, "Not found data in UpdateCompany: {Message}", e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch
            {
                Log.Fatal("An fatal error occured!");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }

        [HttpDelete]
        public IActionResult DeleteCompany(int id)
        {
            try
            {
                _companyService.DeleteEntity(id);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (NotFoundException e)
            {
                Log.Error(e, "Company not found in Delete: {Message}", e.Message);
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (WrongDataException e)
            {
                Log.Error(e, "Wrong data entered in DeleteCompany: {Message}", e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch
            {
                Log.Fatal("An fatal error occured!");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }
    }
}
