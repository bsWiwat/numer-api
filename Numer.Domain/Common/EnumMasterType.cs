using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Domain.Common
{
    public class EnumMasterType
    {
        public enum MasterType {
            Continue = 100,
            SwitchingProtocols = 101,

            Success = 200,
            Created = 201,
            Accepted = 202,
            NoContent = 204,

            MultipleChoices = 300,
            MovedPermanently = 301,
            Found = 302,
            SeeOther = 303,
            NotModified = 304,

            BadRequest = 400,
            Unauthorized = 401,
            Forbidden = 403,
            NotFound = 404,
            MethodNotAllowed = 405,
            NotAcceptable = 406,
            RequestTimeout = 408,
            Conflict = 409,
            Gone = 410,
            LengthRequired = 411,
            PreconditionFailed = 412,
            PayloadTooLarge = 413,
            URITooLong = 414,
            UnsupportedMediaType = 415,
            TooManyRequests = 429,
            RequestHeaderFieldsTooLarge = 431,

            MissingParameter = 4401,
            InvalidParameter = 4402,
            EmptyStringNotSupport = 4403,
            UnrecognizedFieldName = 4404,
            DuplicatedData = 4409,

            InternalServerError = 500,
            NotImplemented = 501,
            BadGateway = 502,
            ServiceUnavailable = 503,
            GatewayTimeout = 504,
            HTTPVersionNotSupported = 505,

            DatabaseError = 5500,
            ExternalServiceError = 5501,

            UserAuthenRequired = 9100,
            TokenExpired = 9101,
        }
    }
}