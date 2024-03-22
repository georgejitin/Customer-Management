using CustomerAPI.Data;
using CustomerAPI.Models;
using CustomerAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public CustomerController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CustomerDTO>> GetCustomers()
        {
            return Ok(_db.customers.ToList());
        }

        [HttpGet("{id:int}",Name ="GetCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<CustomerDTO> GetCustomers(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var customer = _db.customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<CustomerDTO> CreateCustomer([FromBody] CustomerDTO customerDTO)
        {
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (_db.customers.FirstOrDefault(u => u.FirstName.ToLower() == customerDTO.FirstName.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Customer First name already exits");
                    return BadRequest(ModelState);
            }

            if (customerDTO == null)
            {
                return BadRequest(customerDTO);
            }
            if (customerDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Customer model = new()
            {
                FirstName = customerDTO.FirstName,
                LastName = customerDTO.LastName,
                Password = customerDTO.Password,
                LoginUser = customerDTO.LoginUser,
                Email = customerDTO.Email,
                Phonenumber = customerDTO.Phonenumber
            };

            _db.customers.Add(model);
            _db.SaveChanges();

            return CreatedAtRoute("GetCustomer" ,new {id=customerDTO.Id}, customerDTO);

        }


        [HttpDelete("{id:int}",Name ="DeleteCustomer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteCustomer(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var cust = _db.customers.FirstOrDefault(u => u.Id == id);

            if (cust == null)
            {
                return NotFound();
            }
            _db.customers.Remove(cust);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateCustomer(int id, [FromBody] CustomerDTO customerDTO)
        {
            if(customerDTO==null || id != customerDTO.Id)
            {
                return BadRequest();    
            }

            Customer model = new()
            {
                FirstName = customerDTO.FirstName,
                LastName = customerDTO.LastName,
                Password = customerDTO.Password,
                LoginUser = customerDTO.LoginUser,
                Email = customerDTO.Email,
                Phonenumber = customerDTO.Phonenumber,
                Id=customerDTO.Id
            };

            _db.customers.Update(model);
            _db.SaveChanges();

            return NoContent();
        }
    }
}



