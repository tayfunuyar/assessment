using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ContactService.Data.Abstract;
using ContactService.Dtos;
using ContactService.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ContactService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {

        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public ContactController(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ContactReadDto>> GetContacts()
        {
            var contacts = _contactRepository.GetAll().ToList();
            
            return Ok(_mapper.Map<IEnumerable<ContactReadDto>>(contacts));
        }
        [HttpPost]
        public async Task<ActionResult<ContactReadDto>> CreateContact(ContactCreateDto createDto)
        {
            var contact = _mapper.Map<Contact>(createDto);
            await _contactRepository.Insert(contact);

            var contactReadDto = _mapper.Map<ContactReadDto>(contact);
            return CreatedAtRoute(nameof(GetContactById), new { Id = contactReadDto.Uuid }, createDto);
        }

        [HttpPut]
        public async Task<ActionResult<ContactReadDto>> UpdateContact(ContactDto updateDto)
        {
            var contact = _mapper.Map<Contact>(updateDto);
            await _contactRepository.Update(contact);

            var contactReadDto = _mapper.Map<ContactReadDto>(contact);
            return CreatedAtRoute(nameof(GetContactById), new { Id = contactReadDto.Uuid }, updateDto);
        }

        [HttpGet("{id}", Name = "GetContactById")]
        public async Task<ActionResult<ContactReadDto>> GetContactById(Guid id)
        {
            var contactItem =await _contactRepository.GetById(id);
            if (contactItem != null)
            {
                return Ok(_mapper.Map<ContactReadDto>(contactItem));
            }


            return NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ContactReadDto>> DeleteContact(Guid id)
        {
            await _contactRepository.Delete(id);

            return NotFound();
        }



    }
}