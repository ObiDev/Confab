using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Conferences.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Conferences.Api.Controllers
{
    [Authorize(Policy = Policy)]
    internal class ConferencesController : BaseController
    {
        private const string Policy = "conferences";
        private readonly IConferenceService _conferenceService;

        public ConferencesController(IConferenceService conferenceService)
        {
            _conferenceService = conferenceService;
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ConferenceDetailsDto>> GetAsync(Guid id)
        {
            return OkOrNotFound(await _conferenceService.GetAsync(id));
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IReadOnlyList<ConferenceDto>>> BrowseAsync()
        {
            return Ok(await _conferenceService.BrowseAync());
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult> AddAsync(ConferenceDetailsDto dto)
        {
            await _conferenceService.AddAsync(dto);
            return CreatedAtAction(nameof(GetAsync), new { id = dto.Id }, null);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult> UpdateAsync(Guid id, ConferenceDetailsDto dto)
        {
            dto.Id = id;
            await _conferenceService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _conferenceService.DeleteAsync(id);
            return NoContent();
        }
    }
}
