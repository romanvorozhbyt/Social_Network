using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.ModelsDTO;
using BLL.Services;
using DAL.Interfaces;
using DAL.Models;
using Moq;
using NUnit;
using NUnit.Framework;

namespace BLL.Test
{
    [TestFixture]
    public class UserServiceTest
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IMapper> _mockMapper;
        private Mock<IUserRepository> _mockRepository;
        private UserService _userService;
        private List<UserDetails> _userList;
        private List<UserDetailsDTO> _userDTOList;
        [SetUp]
        public void Initialize()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockRepository = new Mock<IUserRepository>();
            _userService = new UserService(_mockUnitOfWork.Object, _mockMapper.Object);
            _userList = new List<UserDetails>()
            {
                //new UserDetails() {Address = "address1", Id = "sdff3445efgg", City = "city1", Country = "c1", FirstName = "Name", LastName = "Vor"},
                //new UserDetails() {Id = "sdfvbsd35666", FirstName = "Name2", LastName = "Wow", City = "city2", Country = "c2", Address = "address2"},
                //new UserDetails(){Id = "sdfsdggbbbbbbd", FirstName = "Name3", LastName = "vov", City = "city3", Country = "c3", Address = "address3"}

            };

        }

        [Test]
        public void Users_GetAll()
        {
            //Arrange
            _userDTOList = new List<UserDetailsDTO>()
            {
                //new UserDetailsDTO() {Address = "address1", Id = "sdff3445efgg", City = "city1", Country = "c1", FirstName = "Name", LastName = "Vor"},
                //new UserDetailsDTO() {Id = "sdfvbsd35666", FirstName = "Name2", LastName = "Wow", City = "city2", Country = "c2", Address = "address2"},
                //new UserDetailsDTO(){Id = "sdfsdggbbbbbbd", FirstName = "Name3", LastName = "vov", City = "city3", Country = "c3", Address = "address3"}

            };
            //_mockMapper.Setup(x => x.Map<IEnumerable<UserDetailsDTO>>(_userList)).Returns(_userDTOList);
            //_mockUnitOfWork.Setup(x => x.Users.GetAll()).Returns(_userList);
            ////Act
            //var result = _userService.GetAll().ToList();
            ////Assert
            //Assert.AreEqual(3, result.Count());
            //Assert.AreEqual("sdff3445efgg",result[0].Id);
            //Assert.AreEqual("sdfvbsd35666", result[1].Id);
            //Assert.AreEqual("sdfsdggbbbbbbd", result[2].Id);

        }
        [Test]
        public void Users_GetById()
        {
            //Arrange
            _userDTOList = new List<UserDetailsDTO>()
            {
                //new UserDetailsDTO() {Address = "address1", Id = "sdff3445efgg", City = "city1", Country = "c1", FirstName = "Name", LastName = "Vor"},
                //new UserDetailsDTO() {Id = "sdfvbsd35666", FirstName = "Name2", LastName = "Wow", City = "city2", Country = "c2", Address = "address2"},
                //new UserDetailsDTO(){Id = "sdfsdggbbbbbbd", FirstName = "Name3", LastName = "vov", City = "city3", Country = "c3", Address = "address3"}

            };
            //_mockMapper.Setup(x => x.Map<UserDetailsDTO>(_userList[1])).Returns(_userDTOList[1]);
            //_mockUnitOfWork.Setup(x => x.Users.GetById("sdfvbsd35666")).Returns(_userList[1]);
            ////Act
            //var result = _userService.GetById("sdfvbsd35666");
            ////Assert
            //Assert.AreEqual("sdfvbsd35666", result.Id);
            //Assert.AreEqual("Name2", result.FirstName);
           
        }
        [Test]
        public void Users_InsertUser_ValidUserModel()
        {
            //Arrange
            var id = "A";
            var newuser = new UserDetailsDTO()
            {
                Address = "address5",
                City = "city8",
                Country = "c5",
                FirstName = "aaaa",
                LastName = "Vor"
            };
            UserDetails u = new UserDetails()
            {
                //Id = id,
                City = newuser.City,
                FirstName = newuser.FirstName,
                LastName = newuser.LastName,
                Country = newuser.Country,
                Address = newuser.Address
            };
            _mockMapper.Setup(x => x.Map<UserDetails>(newuser)).Returns(u);
            _mockRepository.Setup(x => x.Insert(u)).Callback(()=>_userList.Add(u));
            //Act
            _userService.Insert(newuser);
            //Assert
            Assert.AreEqual(id, u.Id);
            _mockUnitOfWork.Verify(m => m.Save(), Times.Once);

        }


    }
}
