using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.EventBus.Entities;
using API.EventBus.Pusher;
using API.Microservice.Account.Models;
using API.Microservice.Account.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Core.Common;

namespace API.AccountMicroservice.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : BaseApiController
    {
        IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IEnumerable<UserModel> Get()
        {
            return _userRepository.Get(this.CurrentUser);
        }

        [HttpGet("{id}")]
        public UserModel Get(int id)
        {
            return _userRepository.Get(id, this.CurrentUser);
        }

        [HttpPost("{id}")]
        public void Push(string id)
        {
            RabbitMQPush rabbitMq = new RabbitMQPush(new Stoc { Consumer = id, Pusher = "Account-service", Value="test event" });
            rabbitMq.Push();
        }
    }
}