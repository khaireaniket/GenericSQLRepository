using AutoMapper;
using GenericRepository.SQL;
using HowToConsume.GenericRepository.SQL.Data;
using HowToConsume.GenericRepository.SQL.Models.Entities;
using HowToConsume.GenericRepository.SQL.Models.Request;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HowToConsume.GenericRepository.SQL.Controllers
{
    /// <summary>
    /// CONTROLLER CONSUMING GENERIC SQL REPOSITORY
    /// </summary>
    [Route("person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<GenericRepositoryDBContext> _unitOfWork;
        private readonly IGenericRepository<Person> _entityRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="unitOfWork"></param>
        public PersonController(IMapper mapper, IUnitOfWork<GenericRepositoryDBContext> unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _entityRepository = _unitOfWork.GetRepository<Person>();
        }

        /// <summary>
        /// Get all the people
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            return new JsonResult(_entityRepository.GetAll().Take(10).ToList());
        }

        /// <summary>
        /// Get person by PersonId
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("id/{personId}")]
        public IActionResult GetPersonById([FromRoute] string personId)
        {
            return new JsonResult(_entityRepository.GetById<string>(personId));
        }

        /// <summary>
        /// Get person by first name
        /// </summary>
        /// <param name="firstname"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("firstname/{firstname}")]
        public IActionResult GetEntityByLegalName([FromRoute] string firstname)
        {
            return new JsonResult(_entityRepository.FindBy(a => a.FirstName == firstname).ToList());
        }

        /// <summary>
        /// Create a person
        /// </summary>
        /// <param name="personCreateRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public IActionResult CreatePerson([FromBody] PersonCreateRequest personCreateRequest)
        {
            Person person = _mapper.Map<Person>(personCreateRequest);
            _entityRepository.Add(person);
            _unitOfWork.Commit();
            return Ok();
        }

        /// <summary>
        /// Update person
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="personUpdateRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{personId}")]
        public IActionResult UpdatePerson([FromRoute] string personId, [FromBody] PersonUpdateRequest personUpdateRequest)
        {
            Person person = _mapper.Map<Person>(personUpdateRequest);
            person.PersonId = personId;
            _entityRepository.Update(person);
            _unitOfWork.Commit();
            return Ok();
        }

        /// <summary>
        /// Delete person by PersonId
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{personId}")]
        public IActionResult DeletePerson([FromRoute] string personId)
        {
            var person = _entityRepository.GetById<string>(personId);
            _entityRepository.Remove(person);
            _unitOfWork.Commit();
            return Ok();
        }
    }
}
