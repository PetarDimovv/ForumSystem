using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StackOverflow.Exceptions;
using StackOverflow.Models;
using StackOverflow.Repositories.Interface;
using StackOverflow.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackOverflow.Test.Services.PostServiceTest
{
    public class GetPost_Should
    {
        [TestMethod]
        public void ReturnCorrectPost_When_IdIsValid()
        {
            Post expectedPost = TestHelper.TestPost;

            var repositoryMock = new Mock<IPostRepository>();

            repositoryMock
                .Setup(r=>r.GetById(1))
                .Returns(TestHelper.TestPost);

            var sut = new PostService(repositoryMock.Object);

            Post actualPost = sut.GetById(expectedPost.Id);

            //Assert
            Assert.AreEqual(expectedPost, actualPost);
        }

        [TestMethod]
        public void ThrowException_When_PostNotFound()
        {
            var repositoryMock = new Mock<IPostRepository>();
            repositoryMock
                .Setup(r => r.GetById(It.IsAny<int>()))
                .Throws(new EntityNotFoundException());

            var sut= new PostService(repositoryMock.Object);

           Assert.ThrowsException<EntityNotFoundException>(() => sut.GetById(It.IsAny<int>()));
        }

    }
}
