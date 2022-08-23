using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Api
{
    public class ProducesDefaultContentTypeAttribute : ProducesAttribute
    {
        public ProducesDefaultContentTypeAttribute(params string[] additionalContentTypes) 
            : base("application/json", additionalContentTypes)
        {
        }
    }
}
