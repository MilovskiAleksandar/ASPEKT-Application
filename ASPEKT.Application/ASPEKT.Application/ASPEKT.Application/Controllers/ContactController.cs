using ASPEKT.Application.Core.Services;
using ASPEKT.Application.DTOS.Contact;
using ASPEKT.Application.Services.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ASPEKT.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private IContactService<ContactDto, FilterContactDto> _contactService;

        public ContactController(IContactService<ContactDto, FilterContactDto> contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public ActionResult<List<ContactDto>> GetAll()
        {
            try
            {
                return _contactService.GetAll();
            }
            catch(NotFoundException e)
            {
                Log.Error(e, "Contacts not found in GetAll: {Message}", e.Message);
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch
            {
                Log.Fatal("An fatal error occured!");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ContactDto> GetById(int id)
        {
            try
            {
                return _contactService.GetById(id);
            }
            catch(NotFoundException e)
            {
                Log.Error(e, "Contact not found in GetById: {Message}", e.Message);
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch
            {
                Log.Fatal("An fatal error occured!");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }
        [HttpPost]
        public IActionResult AddContact([FromBody] ContactDto contact)
        {
            try
            {
                _contactService.AddEntity(contact);
                return StatusCode(StatusCodes.Status201Created, "Contact created successfully");
            }
            catch(WrongDataException e)
            {
                Log.Error(e, "Wrong data entered in AddingContact: {Message}", e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch
            {
                Log.Fatal("An fatal error occured!");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }

        [HttpPut]
        public IActionResult UpdateContact([FromBody] ContactDto contactDto)
        {
            try
            {
                _contactService.UpdateEntity(contactDto);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (WrongDataException e)
            {
                Log.Error(e, "Wrong data entered in UpdateContact: {Message}", e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch(NotFoundException e)
            {
                Log.Error(e, "Not found data in UpdateContact: {Message}", e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch
            {
                Log.Fatal("An fatal error occured!");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _contactService.DeleteEntity(id);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch(NotFoundException e)
            {
                Log.Error(e, "Contact not found in Delete: {Message}", e.Message);
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (WrongDataException e)
            {
                Log.Error(e, "Wrong data entered in DeleteContact: {Message}", e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch
            {
                Log.Fatal("An fatal error occured!");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }

        [HttpGet("GetContacts")]
        public ActionResult<List<FilterContactDto>> GetContactWithCompanyAndCountry()
        {
            try
            {
                return _contactService.GetContactWithCompanyAndCountry();
            }
            catch (NotFoundException e)
            {
                Log.Error(e, "Cannot get contacts for company and country in GetContactWithCompanyAndCountry: {Message}", e.Message);
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch
            {
                Log.Fatal("An fatal error occured!");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }

        [HttpGet("FilterContacts")]
        public ActionResult<List<FilterContactDto>> GetFilteredByCompanyAndCountryId(int? companyId, int? countryId)
        {
            try
            {
                return _contactService.FilterByCompanyAndCountry(companyId, countryId);
            }
            catch(NotFoundException e)
            {
                Log.Error(e, "Can not found by ID in GetFilteredByCompanyAndCOuntryId: {Message}", e.Message);
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch
            {
                Log.Fatal("An fatal error occured!");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }
    }
}
