using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Conferences.Core.DTO
{
    internal class ConferenceDetailsDto : ConferenceDto
    {
        [Required]
        [StringLength(1000, MinimumLength = 3)]
        public string Description { get; set; }
    }
}
