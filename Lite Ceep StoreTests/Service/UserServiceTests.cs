using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lite_Ceep_Store.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lite_Ceep_Store.Service.Tests
{
    [TestClass()]
    public class UserServiceTests
    {
        //[TestMethod()]
        //public void AuthorizeUserTest()
        //{
        //    //Assert.IsTrue(await new UserService().AuthorizeUser("qwerty123", "qwerty123"));
        //    //Assert.IsTrue(UserService.AuthorizeUser("qwerty123", "qwerty123"));
            
        //}
        [TestMethod]
        public async Task AuthorizeUser_Log_qwerty123_Pass_qwerty123_Return_True()
        {
            Assert.IsTrue(await UserService.AuthorizeUser("qwerty123", "qwerty123"));
        }
        [TestMethod]
        public async Task AuthorizeUser_Log_qwerty123_Pass_qwertyyyy123_Return_False()
        {
            Assert.IsFalse(await UserService.AuthorizeUser("qwerty123", "qwertyyyy123"));
        }
    }
}