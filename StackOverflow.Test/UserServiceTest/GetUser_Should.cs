using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StackOverflow.Exceptions;
using StackOverflow.Models;
using StackOverflow.Repositories.Interface;
using StackOverflow.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace StackOverflow.Test.Services.UserServiceTest
{
    [TestClass]
    public class GetUser_Should
    {
        [TestMethod]
        public void ReturnCorrectUser_When_IdIsValid()
        {
            //Arange
            User expectedUser=TestHelper.TestUser;
            var repositoryMock = new Mock<IUserRepository>();

            repositoryMock
                .Setup(r => r.GetById(1))
                .Returns(TestHelper.TestUser);

            var sut= new UserService(repositoryMock.Object);

            //Act
            User actualUser = sut.GetById(expectedUser.Id);

            //Assert
            Assert.AreEqual(expectedUser, actualUser);


        }

        [TestMethod]
        public void ThrowException_When_UserNotFound()
        {
            var repositoryMock = new Mock<IUserRepository>();

            repositoryMock
                .Setup(r => r.GetById(It.IsAny<int>()))
                .Throws(new EntityNotFoundException());

            var sut= new UserService(repositoryMock.Object);

            Assert.ThrowsException<EntityNotFoundException>(() => sut.GetById(It.IsAny<int>()));
        }

        [TestMethod]
        public void ReturnCorrectUser_When_UsernameIsValid()
        {
            //Arange
            User expectedUser = TestHelper.TestUser;
            var repositoryMock = new Mock<IUserRepository>();

            repositoryMock
                .Setup(r => r.GetByUsername("TestUser1"))
                .Returns(TestHelper.TestUser);

            var sut = new UserService(repositoryMock.Object);

            //Act
            User actualUser = sut.GetByUsername(expectedUser.Username);

            //Assert
            Assert.AreEqual(expectedUser, actualUser);

        }

        [TestMethod]
        public void ThrowException_When_UsernameNotFound()
        {
            var repositoryMock = new Mock<IUserRepository>();

            repositoryMock
                .Setup(r => r.GetByUsername(It.IsAny<string>()))
                .Throws(new EntityNotFoundException());

            var sut = new UserService(repositoryMock.Object);

            Assert.ThrowsException<EntityNotFoundException>(() => sut.GetByUsername(It.IsAny<string>()));
        }
    }
}
