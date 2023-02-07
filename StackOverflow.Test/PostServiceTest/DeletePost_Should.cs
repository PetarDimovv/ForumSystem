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
    [TestClass]
    public class DeletePost_Should
    {
        [TestMethod]
        public void DeleteUser_When_ValidParams()
        {
            var user = new User
            {
                Id = 1
            };

            var post = new Post
            {
                Id = 1,
               //CreatedById = 1
            };

            var posts=new List<Post>() { post};

            var repositoryMock = new Mock<IPostRepository>();

            repositoryMock
                .Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(post);

            repositoryMock
                .Setup(r => r.DeletePost(It.IsAny<int>()))
                .Callback(() => posts.Remove(post));

            var sut = new PostService(repositoryMock.Object);
            sut.DeletePost(1, user);

            CollectionAssert.DoesNotContain(posts, post);
        }

        [TestMethod]
        public void ThrowException_When_UserNotCreator()
        {
            var user = new User
            {
                Id = 1
            };

            var post = new Post
            {
                Id = 1,
                //CreatedById = 2
            };

            var repositoryMock= new Mock<IPostRepository>();

            repositoryMock
                .Setup(r => r.GetById(1))
                .Returns(post);

            var sut = new PostService(repositoryMock.Object);

            Assert.ThrowsException<UnauthorizedOperationException>(() => sut.DeletePost(post.Id, user));
        }

    }
}
