﻿using Confab.Modules.Speakers.Core.DTO;
using Confab.Modules.Speakers.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Speakers.Core.Mappings
{
    internal static class Extensions
    {
        public static SpeakerDto AsDto(this Speaker entity)
            => new SpeakerDto()
            {
                Id = entity.Id,
                Email = entity.Email,
                FullName = entity.FullName,
                Bio = entity.Bio,
                AvatarUrl = entity.AvatarUrl
            };

        public static Speaker AsEntity(this SpeakerDto dto)
            => new Speaker()
            {
                Id = dto.Id,
                Email = dto.Email,
                FullName = dto.FullName,
                Bio = dto.Bio,
                AvatarUrl = dto.AvatarUrl
            };
    }
}
