using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ContactService.Controllers;
using ContactService.Data.Abstract;
using ContactService.Dtos;
using ContactService.Entities;
using ContactService.Profiles;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using Xunit;

namespace ContactService.Tests
{
    public class ContactControllerTest
    {
        private readonly Mock<IContactRepository> _contactRepository;
        private readonly Mock<IContactInformationRepository> _contactInformationRepository;

        private readonly ContactController _contactContoller;
        private readonly List<ContactReadDto> _contactReadList;

        public ContactControllerTest()
        {
            _contactRepository = new Mock<IContactRepository>();
            _contactInformationRepository = new Mock<IContactInformationRepository>();

            _contactReadList = new List<ContactReadDto> {
            new ContactReadDto(){Firm = "Setur Antalya", Name = "Tayfun",Surname = "Uyar", Uuid = Guid.Parse("8c85766c-027c-459f-ae8f-a42c5f47872c")},
            new ContactReadDto(){Firm = "Setur Ankara",Name = "Tufan",Surname = "Uyar", Uuid= Guid.Parse("95f3c1c4-ace2-4e86-a09a-4e019a73f87c")},
        };
        }
        [Fact]
        public void GetAllContact_ReturnSuccess_Test()
        {
            try
            {
                //Arrange
                //Repository 

                var mockMapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new ContactsProfile());
                });
                var mapper = mockMapper.CreateMapper();
                var returnData = mapper.Map<List<Contact>>(_contactReadList).AsQueryable();
                _contactRepository.Setup(m => m.GetAll()).Returns(returnData);

                ContactController controller = new ContactController(_contactRepository.Object, mapper, _contactInformationRepository.Object);

                //Act
                var result = controller.GetContacts();
                var responseContact = result.Result as OkObjectResult;
                var list = Assert.IsAssignableFrom<IEnumerable<ContactReadDto>>(responseContact.Value);


                Assert.Equal<int>(_contactReadList.Count, list.Count());
            }
            catch (Exception ex)
            {
                //Assert
                Assert.False(false, ex.Message);
            }
        }
        [Fact]
        public void GetAllContact_ReturnEmpty_Test()
        {

            //Arrange
            //Repository 
          
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ContactsProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var returnData = mapper.Map<List<Contact>>(new List<ContactReadDto>()).AsQueryable();
            _contactRepository.Setup(m => m.GetAll()).Returns(returnData);

            ContactController controller = new ContactController(_contactRepository.Object, mapper, _contactInformationRepository.Object);

            //Act
            var result = controller.GetContacts();
            var responseContact = result.Result as OkObjectResult;
            var list = Assert.IsAssignableFrom<IEnumerable<ContactReadDto>>(responseContact.Value);
            Assert.Empty(list);
        }
        
    }
}

