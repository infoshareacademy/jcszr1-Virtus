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
    public class ProductController : ControllerBase
    {
        private readonly IProductActionsRepository _repository;
        private readonly IMapper _mapper;
        public ProductController(IProductActionsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("product")]
        public ActionResult<List<ProductActionDto>> GetAllProductActions()
        {
            var actionList = _repository.GetAllProductActions().Select(_mapper.Map<ProductActionDto>);
            return Accepted(actionList);
        }

        [HttpGet("product/{id}")]
        public ActionResult<ProductActionDto> GetProductActionById(int id)
        {
            var action = _repository.GetProductActionById(id);
            if (action == null)
            {
                return NoContent();
            }
            return Accepted(_mapper.Map<ProductActionDto>(action));
        }

        [HttpPost("product")]
        public ActionResult<ProductActionDto> CreateProductAction(CreateProductAction newAction)
        {
            var action = _mapper.Map<ProductAction>(newAction);
            _repository.AddProductAction(action);
            return CreatedAtAction(nameof(GetProductActionById), new {id = action.Id},
                _mapper.Map<ProductActionDto>(action));
        }
        [HttpGet("search/value")]
        public ActionResult<List<SearchValueActionDto>> GetAllSearchValueActions()
        {
            var actionList = _repository.GetAllSearchValueActions().Select(_mapper.Map<SearchValueActionDto>);
            return Accepted(actionList);
        }
        [HttpGet("search/value/{id}")]
        public ActionResult<SearchValueActionDto> GetSearchValueActionById(int id)
        {
            var action = _repository.GetSearchValueActionById(id);
            if (action == null)
            {
                return NoContent();
            }
            return Accepted(_mapper.Map<SearchValueActionDto>(action));
        }
        [HttpPost("search/value")]
        public ActionResult<SearchValueActionDto> CreateSearchValueAction(CreateSearchValueAction newAction)
        {
            var action = _mapper.Map<SearchValueAction>(newAction);
            _repository.AddSearchValueAction(action);
            return CreatedAtAction(nameof(GetSearchValueActionById), new { id = action.Id },
                _mapper.Map<SearchValueActionDto>(action));
        }
        [HttpGet("search/string")]
        public ActionResult<List<SearchStringActionDto>> GetAllSearchStringActions()
        {
            var actionList = _repository.GetAllSearchStringActions().Select(_mapper.Map<SearchStringActionDto>);
            return Accepted(actionList);
        }
        [HttpGet("search/string/{id}")]
        public ActionResult<SearchStringActionDto> GetSearchStringActionById(int id)
        {
            var action = _repository.GetSearchStringActionById(id);
            if (action == null)
            {
                return NoContent();
            }
            return Accepted(_mapper.Map<SearchStringActionDto>(action));
        }
        [HttpPost("search/string")]
        public ActionResult<SearchStringActionDto> CreateSearchStringAction(CreateSearchStringAction newAction)
        {
            var action = _mapper.Map<SearchStringAction>(newAction);
            _repository.AddSearchStringAction(action);
            return CreatedAtAction(nameof(GetSearchStringActionById), new { id = action.Id },
                _mapper.Map<SearchStringActionDto>(action));
        }

    }
}
