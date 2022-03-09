﻿using Microsoft.AspNetCore.Mvc;
using shared.web.infrastructure;

namespace mfe_versions.api.V1.Mfes
{
    [Tags("Mfes")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/mfes")]
    public class MfeGetController : ApiBaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
