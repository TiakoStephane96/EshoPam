using EshoPam.Models;
using EshoPam.Repository;
using System;
using System.Collections.Generic;
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

            return Ok(MapUser(users));
        }

        [HttpGet]
        public IHttpActionResult Get(string Username)
        {
            var users = userRepository.Get(Username);
            if (users == null)
                return NotFound();

            return Ok(MapUser(users));
        }

        [HttpGet]
        public IHttpActionResult Login(string Username, string password)
        {
            var users = userRepository.Get(Username,password);
            if (users == null)
                return NotFound();

            return Ok(MapUser(users));
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] UserModel userModel)
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
                return Ok(MapUser(user));

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

            return Ok(MapUser(user));
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

        private UserModel MapUser(User users)
        {
            return new UserModel
            (
                users.Id,
                users.Username,
                users.Fullname,
                users.Role
            );
        }
    }
}
