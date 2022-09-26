using Confab.Modules.Speakers.Core.DAL.Repositories;
using Confab.Modules.Speakers.Core.DTO;
using Confab.Modules.Speakers.Core.Entities;
using Confab.Modules.Speakers.Core.Events;
using Confab.Modules.Speakers.Core.Exceptions;
using Confab.Modules.Speakers.Core.Mappings;
using Confab.Shared.Abstractions.Messaging;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Speakers.Core.Services
{
    internal class SpeakerService : ISpeakerService
    {
        private readonly ISpeakerRepository _speakerRepository;
        private readonly IMessageBroker _messageBroker;

        public SpeakerService(ISpeakerRepository speakerRepository, IMessageBroker messageBroker)
        {
            _speakerRepository = speakerRepository;
            _messageBroker = messageBroker;
        }

        public async Task AddAsync(SpeakerDto dto)
        {
            dto.Id = Guid.NewGuid();

            var alreadyExists = await _speakerRepository.ExistsAsync(dto.Id);
            if (alreadyExists)
                throw new SpeakerAlreadyExistsException(dto.Id);

            await _speakerRepository.AddAsync(dto.AsEntity());
            await _messageBroker.PublishAsync(new SpeakerCreated(dto.Id, dto.FullName));
        }

        public async Task<IEnumerable<SpeakerDto>> BrowseAync()
        {
            var speakers = await _speakerRepository.BrowseAync();
            return speakers?.Select(s => s.AsDto());
        }

        public async Task DeleteAsync(Guid id)
        {
            var alreadyExists = await _speakerRepository.ExistsAsync(id);
            if (!alreadyExists)
                throw new SpeakerNotFoundException(id);

            var speaker = await _speakerRepository.GetAsync(id);
            await _speakerRepository.DeleteAsync(speaker);
        }

        public async Task<SpeakerDto> GetAsync(Guid id)
        {
            var speaker = await _speakerRepository.GetAsync(id);
            return speaker?.AsDto();
        }

        public async Task UpdateAsync(SpeakerDto dto)
        {
            var alreadyExists = await _speakerRepository.ExistsAsync(dto.Id);
            if (!alreadyExists)
                throw new SpeakerNotFoundException(dto.Id);

            await _speakerRepository.UpdateAsync(dto.AsEntity());
        }
    }
}
