using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Encurta_Url.API.Entities;
using Encurta_Url.API.Models;
using Encurta_Url.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Encurta_Url.API.Controllers
{
    [ApiController]
    [Route("api/shortenedLinks")]
    public class ShortenedLinksController : ControllerBase
    {
        private readonly EncurtaUrlDbContext _context;

        public ShortenedLinksController(EncurtaUrlDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Links);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var link = _context.Links.SingleOrDefault(l => l.Id == id);

            if (link == null) return NotFound();

            return Ok(link);
        }

        /// <summary>
        /// Cadastrar um link encurtado
        /// </summary>
        /// <remark>
        /// {"title": "ultimo-artigo Blog", "destinationLink": " "}
        /// </remark>
        /// <param name="model">Dados de link</param>
        /// <returns>Objeto recém-criado</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Post(AddOrUpdateShortenedLinkModel model)
        {
            var link = new ShortenedCustomLink(model.Title, model.DestinationLink);

            _context.Links.Add(link);

            _context.SaveChanges();

            return CreatedAtAction("GetById", new { id = link.Id }, link);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AddOrUpdateShortenedLinkModel model)
        {
            var link = _context.Links.SingleOrDefault(l => l.Id == id);

            if (link == null) return NotFound();

            link.Update(model.Title, model.DestinationLink);

            //_context.Links.Update(link);

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var link = _context.Links.SingleOrDefault(l => l.Id == id);

            if (link == null) return NotFound();

            _context.Links.Remove(link);
            
            _context.SaveChanges();

            return NoContent();
        } 
        [HttpGet("/{code}")]
        public IActionResult RedirectLink(string code)
        {
            var link = _context.Links.SingleOrDefault(l => l.Code == code);

            if (link == null) return NotFound();

            return Redirect(link.DestinationLink);
        }
    }
}