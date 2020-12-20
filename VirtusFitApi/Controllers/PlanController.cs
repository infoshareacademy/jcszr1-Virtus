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
    public class PlanController : ControllerBase
    {
        private readonly IPlanActionsRepository _repository;
        private readonly IMapper _mapper;

        public PlanController(IPlanActionsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost("plan/dietplan")]
        public ActionResult<DietPlanActionDto> CreateDietPlanAction(CreateDietPlanAction newAction)
        {
            var action = _mapper.Map<DietPlanAction>(newAction);
            _repository.AddDietPlanAction(action);
            return Ok();
        }
        [HttpPost("plan/productinplan")]
        public ActionResult<ProductInPlanActionDto> CreateProductInPlanAction(CreateProductInPlanAction newAction)
        {
            var action = _mapper.Map<ProductInPlanAction>(newAction);
            _repository.AddProductInPlanAction(action);
            return Ok();
        }

        [HttpPost("plan/bmi")]
        public ActionResult<BmiActionDto> CreateBmiAction(CreateBmiAction newAction)
        {
            var action = _mapper.Map<BmiAction>(newAction);
            _repository.AddBmiAction(action);
            
            return Ok();
        }
    }
}
