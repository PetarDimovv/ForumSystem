using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StackOverflow.Exceptions;
using StackOverflow.Models;
using StackOverflow.Repositories.Interface;
using StackOverflow.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackOverflow.Test.Services.CommentServiceTest
{
    [TestClass]
    public class GetComment_Should
    {
        [TestMethod]
        public void ReturnCorrectComment_When_IdIsValid()
        {
            Comment expectedComment = TestHelper.TestComment;

            var repositoryMock = new Mock<ICommentRepository>();

            repositoryMock
                .Setup(r => r.GetById(1))
                .Returns(TestHelper.TestComment);

            var sut = new CommentService(repositoryMock.Object);

            Comment actualComment = sut.GetById(expectedComment.Id);

            Assert.AreEqual(expectedComment, actualComment);
        }

        [TestMethod]
        public void ThrowException_When_CommentNotFound()
        {
            var repositoryMock = new Mock<ICommentRepository>();

            repositoryMock
                .Setup(r => r.GetById(It.IsAny<int>()))
                .Throws(new EntityNotFoundException());

            var sut=new CommentService(repositoryMock.Object);

            Assert.ThrowsException<EntityNotFoundException>(() => sut.GetById(It.IsAny<int>()));

        }
    }
}