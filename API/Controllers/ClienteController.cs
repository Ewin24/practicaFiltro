using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Persistence.Entities;

namespace API.Controllers
{
    public class EntidadController : BaseController
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper _mapper;

        public EntidadController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<Cliente>>> Get()
        {
            var items = await _UnitOfWork.Cliente.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<IEnumerable<Cliente>>> Get(int id)
        {
            var item = await _UnitOfWork.Cliente.GetByIdAsync(id);
            return Ok(item);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Cliente>> Post(EntidadDto itemDto)
        {
            var item = _mapper.Map<Cliente>(itemDto);
            _UnitOfWork.Cliente.Add(item);
            await _UnitOfWork.SaveAsync();
            if (item == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Cliente>> Put(int id, [FromBody] Cliente item)
        {
            if (item.Id == 0)
            {
                item.Id = id;
            }
            if (item.Id != id)
            {
                return BadRequest();
            }
            if (item == null)
            {
                return NotFound();
            }
            _UnitOfWork.Cliente.Update(item);
            await _UnitOfWork.SaveAsync();
            return item;
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var item = await _UnitOfWork.Cliente.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            _UnitOfWork.Cliente.Delete(item);
            await _UnitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
