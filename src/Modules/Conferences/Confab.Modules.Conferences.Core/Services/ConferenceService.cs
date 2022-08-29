﻿using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Conferences.Core.Entities;
using Confab.Modules.Conferences.Core.Exceptions;
using Confab.Modules.Conferences.Core.Policies;
using Confab.Modules.Conferences.Core.Repositories;
using Confab.Modules.Conferences.Messages.Events;
using Confab.Shared.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Conferences.Core.Services
{
    internal class ConferenceService : IConferenceService
    {
        private readonly IConferenceRepository _conferenceRepository;
        private readonly IHostRepository _hostRepository;
        private readonly IConferenceDeletionPolicy _conferenceDeletionPolicy;
        private readonly IEventDispatcher _eventDispatcher;

        public ConferenceService(IConferenceRepository conferenceRepository, IHostRepository hostRepository,
            IConferenceDeletionPolicy conferenceDeletionPolicy, IEventDispatcher eventDispatcher)
        {
            _conferenceRepository = conferenceRepository;
            _hostRepository = hostRepository;
            _conferenceDeletionPolicy = conferenceDeletionPolicy;
            _eventDispatcher = eventDispatcher;
        }


        public async Task AddAsync(ConferenceDetailsDto dto)
        {
            if (await _hostRepository.GetAsync(dto.HostId) is null)
                throw new HostNotFoundException(dto.HostId);

            dto.Id = Guid.NewGuid();

            var conference = new Conference()
            {
                Id = dto.Id,
                HostId = dto.HostId,
                Name = dto.Name,
                Description = dto.Description,
                From = dto.From,
                To = dto.To,
                Location = dto.Location,
                LogoUrl = dto.LogoUrl,
                ParticipantsLimit = dto.ParticipantsLimit
            };

            await _conferenceRepository.AddAsync(conference);

            await _eventDispatcher.PublishAsync(new ConferenceCreated(conference.Id, conference.Name, conference.ParticipantsLimit,
                conference.From, conference.To));
        }

        public async Task<IReadOnlyList<ConferenceDto>> BrowseAync()
        {
            var conferences = await _conferenceRepository.BrowseAync();

            return conferences.Select(Map<ConferenceDto>).ToList();
        }

        public async Task DeleteAsync(Guid id)
        {
            var conference = await _conferenceRepository.GetAsync(id);
            if (conference is null)
            {
                throw new ConferenceNotFoundException(id);
            }

            if (await _conferenceDeletionPolicy.CanDeleteAsync(conference) is false)
            {
                throw new CannotDeleteConferenceException(id);
            }

            await _conferenceRepository.DeleteAsync(conference);
        }

        public async Task<ConferenceDetailsDto> GetAsync(Guid id)
        {
            var conference = await _conferenceRepository.GetAsync(id);
            if (conference is null)
                return null;

            var dto = Map<ConferenceDetailsDto>(conference);
            dto.Description = conference.Description;

            return dto;
        }

        public async Task UpdateAsync(ConferenceDetailsDto dto)
        {
            var conference = await _conferenceRepository.GetAsync(dto.Id);
            if (conference is null)
            {
                throw new ConferenceNotFoundException(dto.Id);
            }

            conference.Name = dto.Name;
            conference.Description = dto.Description;
            conference.Location = dto.Location;
            conference.LogoUrl = dto.LogoUrl;
            conference.From = dto.From;
            conference.To = dto.To;
            conference.ParticipantsLimit = dto.ParticipantsLimit;

            await _conferenceRepository.UpdateAsync(conference);
        }

        private static T Map<T>(Conference conference) where T : ConferenceDto, new()
            => new T()
            {
                Id = conference.Id,
                HostId = conference.HostId,
                HostName = conference.Host?.Name,
                Name = conference.Name,
                Location = conference.Location,
                LogoUrl = conference.LogoUrl,
                From = conference.From,
                To = conference.To,
                ParticipantsLimit = conference.ParticipantsLimit
            };
    }
}
