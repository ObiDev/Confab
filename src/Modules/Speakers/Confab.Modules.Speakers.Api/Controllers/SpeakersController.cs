using Confab.Modules.Speakers.Core.DTO;
using Confab.Modules.Speakers.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Speakers.Api.Controllers
{
    internal class SpeakersController : BaseController
    {
        private readonly ISpeakerService _speakerService;

        public SpeakersController(ISpeakerService speakerService)
        {
            _speakerService = speakerService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SpeakerDto>> GetAsync(Guid id)
        {
            return OkOrNotFound(await _speakerService.GetAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync(SpeakerDto dto)
        {
            await _speakerService.AddAsync(dto);
            return CreatedAtAction(nameof(GetAsync), new { id = dto.Id }, null);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpeakerDto>>> BrowseAsync()
        {
            return Ok(await _speakerService.BrowseAync());
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _speakerService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateAsync(Guid id, SpeakerDto dto)
        {
            dto.Id = id;
            await _speakerService.UpdateAsync(dto);
            return NoContent();
        }
    }
}
