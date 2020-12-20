using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtusFitApi.DAL;
using VirtusFitApi.DTO;
using VirtusFitApi.Models;

namespace VirtusFitApi.Controllers
{
    [Route("VirtusFit")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserAccountActionsRepository _repository;
        private readonly IMapper _mapper;

        public UserController(IUserAccountActionsRepository userAccountActionsRepository, IMapper mapper)
        {
            _repository = userAccountActionsRepository;
            _mapper = mapper;
        }
        [HttpPost("user")]
        public ActionResult<UserAccountActionDto> CreateUserAccountAction (CreateUserAccountAction newAction)
        {
            var action = _mapper.Map<UserAccountAction>(newAction);
            _repository.AddUserAccountAction(action);
            return Ok();
        }
    }
}
