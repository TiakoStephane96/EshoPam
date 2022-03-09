using EshoPam.Repository;
using EshoPam.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EshoPam.WebApi.Controllers
{
    public class UsersController : ApiController
    {
        private readonly UserRepository userRepository;

        public UsersController()
        {
            userRepository = new UserRepository();
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var users = userRepository.Get(id);
            if (users == null)
                return NotFound();

            return Ok(new UserModel(users));
        }

        [HttpGet]
        public IHttpActionResult Get(string Username)
        {
            var users = userRepository.Get(Username);
            if (users == null)
                return NotFound();

            return Ok(new UserModel(users));
        }

        [HttpGet]
        public IHttpActionResult Login(string Username, string password)
        {
            var users = userRepository.Get(Username,password);
            if (users == null)
                return NotFound();

            return Ok(new UserModel(users));
        }

        [HttpPost]
        public IHttpActionResult Post(UserModel userModel)
        {
            try
            {
                if (userModel == null)
                    return BadRequest();

                var user = new User
                    (
                        0,
                        userModel.Username,
                        userModel.Password,
                        userModel.Fullname,
                        userModel.Role
                    );
                user = userRepository.Add(user);
                return Ok(new UserModel(user));
            }
            catch (DuplicateWaitObjectException)
            {
                return Conflict();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IHttpActionResult Put(UserModel userModel)
        {
            try
            {
                if (userModel == null)
                    return BadRequest();

                var user = new User
                    (
                        userModel.Id,
                        userModel.Username,
                        userModel.Password,
                        userModel.Fullname,
                        userModel.Role
                    );
                user = userRepository.Set(user);
                return Ok(new UserModel(user));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (DuplicateWaitObjectException)
            {
                return Conflict();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
