using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSQBot_API.Business.Interfaces.Meters;

namespace MSQBot_API.Controllers
{
    [Route("api/meters")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MeterController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMeterServices _rateServices;

        #region GET


        #endregion

        #region POST

        #endregion

        #region UPDATE
        #endregion

        #region DELETE
        #endregion
    }
}
