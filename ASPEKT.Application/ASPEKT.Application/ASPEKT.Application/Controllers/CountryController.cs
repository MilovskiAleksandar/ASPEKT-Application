using ASPEKT.Application.Core.Services;
using ASPEKT.Application.DTOS.Country;
using ASPEKT.Application.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using FluentValidation;

namespace ASPEKT.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private IService<CountryDto> _service;
        public CountryController(IService<CountryDto> service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<CountryDto>> GetAllCountry()
        {
            try
            {
                return _service.GetAll();
            }
            catch (NotFoundException e)
            {
                Log.Error(e, "Country not found in GetAllCountry: {Message}", e.Message);
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch
            {
                Log.Fatal("An fatal error occured!");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<CountryDto> GetCountryById(int id)
        {
            try
            {
                return _service.GetById(id);
            }
            catch (NotFoundException e)
            {
                Log.Error(e, "Country not found in GetCountryById: {Message}", e.Message);
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch
            {
                Log.Fatal("An fatal error occured!");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }

        [HttpPost]
        public IActionResult AddingCountry([FromBody] CountryDto country)
        {
            try
            {
                _service.AddEntity(country);
                return StatusCode(StatusCodes.Status201Created, "Country created successfully");
            }
            catch (ValidationException e)
            {
                Log.Error(e, "Wrong data entered in AddingCOuntry: {Message}", e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (WrongDataException e)
            {
                Log.Error(e, "Wrong data entered in AddingCountry: {Message}", e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch
            {
                Log.Fatal("An fatal error occured!");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }

        [HttpPut]
        public IActionResult UpdateCountry([FromBody] CountryDto country)
        {
            try
            {
                _service.UpdateEntity(country);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (ValidationException e)
            {
                Log.Error(e, "Wrong data entered in UpdateCountry: {Message}", e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (WrongDataException e)
            {
                Log.Error(e, "Wrong data entered in UpdateCountry: {Message}", e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (NotFoundException e)
            {
                Log.Error(e, "Not found data in UpdateCountry: {Message}", e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch
            {
                Log.Fatal("An fatal error occured!");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCountryById(int id)
        {
            try
            {
                _service.DeleteEntity(id);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (NotFoundException e)
            {
                Log.Error(e, "Country not found in Delete: {Message}", e.Message);
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (WrongDataException e)
            {
                Log.Error(e, "Wrong data entered in DeleteCountryById: {Message}", e.Message);
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
