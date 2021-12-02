using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ContactService.Data.Abstract;
using ContactService.Dtos;
using ContactService.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ContactService.Controllers
{
    [ApiController]
    [Route("api/contact/{contactUuid}/[controller]")]
    public class ContactInformationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IContactRepository _contactRepository;
        private readonly IContactInformationRepository _contactInformationRepository;

        public ContactInformationController(IMapper mapper, IContactRepository contactRepository, IContactInformationRepository contactInformationRepository)
        {
            _mapper = mapper;
            _contactRepository = contactRepository;
            _contactInformationRepository = contactInformationRepository;
        }
        [HttpGet]
        public ActionResult<IEnumerable<ContactInformationReadDto>> GetContactInformationForContact(Guid contactUuid)
        {

            if (!_contactRepository.IsExist(contactUuid))
            {
                return NotFound();
            }
            var commands = _contactInformationRepository.GetContactInformationByContactId(contactUuid);

            return Ok(_mapper.Map<IEnumerable<ContactInformationReadDto>>(commands));
        }
        [HttpPut]
        public async Task<ActionResult<ContactReadDto>> UpdateContactInformation(Guid contactUuid, ContactInformationDto updateDto)
        {
            if (!_contactRepository.IsExist(contactUuid))
            {
                return NotFound();
            }
            var contactInfo = _mapper.Map<ContactInformation>(updateDto);
            contactInfo.ContactUuid = contactUuid;
            await _contactInformationRepository.Update(contactInfo);

            var contactInfoReadDto = _mapper.Map<ContactInformationReadDto>(contactInfo);
            return CreatedAtRoute(nameof(GetContactInformationById), new { Id = contactInfoReadDto.Uuid, ContactUuid = contactUuid }, updateDto);
        }

        [HttpPost]
        public async Task<ActionResult<ContactInformationReadDto>> CreateContactInformationForContact(Guid contactUuid, ContactInformationCreateDto createDto)
        {

            if (!_contactRepository.IsExist(contactUuid))
            {
                return NotFound();
            }
            var contactInfo = _mapper.Map<ContactInformation>(createDto);
            contactInfo.ContactUuid = contactUuid;
            await _contactInformationRepository.Insert(contactInfo);

            var contactInfoReadDto = _mapper.Map<ContactInformationReadDto>(contactInfo);
            return CreatedAtRoute(nameof(GetContactInformationById), new { Id = contactInfoReadDto.Uuid, ContactUuid = contactUuid }, createDto);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ContactReadDto>> DeleteContactInformation(Guid id)
        {
            await _contactInformationRepository.Delete(id);

            return NotFound();
        }

        [HttpGet("{id}", Name = "GetContactInformationById")]
        public async Task<ActionResult<ContactInformationReadDto>> GetContactInformationById(Guid id)
        {
            var contactItem = await _contactInformationRepository.GetById(id);
            if (contactItem != null)
            {
                return Ok(_mapper.Map<ContactInformationReadDto>(contactItem));
            }


            return NotFound();
        }
    }
}