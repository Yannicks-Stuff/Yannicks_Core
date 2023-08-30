namespace Yannick.Network.Protocol.HTTP
{
    public enum Status : uint
    {
        InformationalResponse = Continue | SwitchingProtocols | Processing | EarlyHints,
        Continue = 100,
        SwitchingProtocols = 101,
        Processing = 102,
        EarlyHints = 103,

        Success = OK | Created | Accepted | NonAuthoritativeInformation | NoContent | ResetContent |
                  PartialContent | MultiStatus | AlreadyReported | IMUsed,
        OK = 200,
        Created = 201,
        Accepted = 202,
        NonAuthoritativeInformation = 203,
        NoContent = 204,
        ResetContent = 205,
        PartialContent = 206,
        MultiStatus = 207,
        AlreadyReported = 208,
        IMUsed = 226,

        Redirection = MultipleChoices | MovedPermanently | Found | SeeOther | NotModified | UseProxy | SwitchProxy |
                      TemporaryRedirect | PermanentRedirect,
        MultipleChoices = 300,
        MovedPermanently = 301,
        Found = 302,
        SeeOther = 303,
        NotModified = 304,
        UseProxy = 305,
        SwitchProxy = 306,
        TemporaryRedirect = 307,
        PermanentRedirect = 308,

        ClientError = BadRequest | Unauthorized | PaymentRequired | Forbidden | NotFound | MethodNotAllowed |
                      NotAcceptable | ProxyAuthenticationRequired | RequestTimeout | Conflict | Gone |
                      LengthRequired |
                      PreconditionFailed | PayloadTooLarge | URITooLong | UnsupportedMediaType |
                      RangeNotSatisfiable |
                      ExpectationFailed | ImATeapot | MisdirectedRequest | UnprocessableEntity | Locked |
                      FailedDependency |
                      TooEarly | UpgradeRequired | PreconditionRequired | TooManyRequests |
                      RequestHeaderFieldsTooLarge | UnavailableForLegalReasons,
        BadRequest = 400,
        Unauthorized = 401,
        PaymentRequired = 402,
        Forbidden = 403,
        NotFound = 404,
        MethodNotAllowed = 405,
        NotAcceptable = 406,
        ProxyAuthenticationRequired = 407,
        RequestTimeout = 408,
        Conflict = 409,
        Gone = 410,
        LengthRequired = 411,
        PreconditionFailed = 412,
        PayloadTooLarge = 413,
        URITooLong = 414,
        UnsupportedMediaType = 415,
        RangeNotSatisfiable = 416,
        ExpectationFailed = 417,
        ImATeapot = 418,
        MisdirectedRequest = 421,
        UnprocessableEntity = 422,
        Locked = 423,
        FailedDependency = 424,
        TooEarly = 425,
        UpgradeRequired = 426,
        PreconditionRequired = 428,
        TooManyRequests = 429,
        RequestHeaderFieldsTooLarge = 431,
        UnavailableForLegalReasons = 451,

        ServerError = InternalServerError | NotImplemented | BadGateway | ServiceUnavailable | GatewayTimeout |
                      HTTPVersiotNotSupported | VariantAlsoNegotiates | InsufficientStorage
                      | LoopDetected | NotExtended | NetworkAuthenticationRequired,
        InternalServerError = 500,
        NotImplemented = 501,
        BadGateway = 502,
        ServiceUnavailable = 503,
        GatewayTimeout = 504,
        HTTPVersiotNotSupported = 505,
        VariantAlsoNegotiates = 506,
        InsufficientStorage = 507,
        LoopDetected = 508,
        NotExtended = 510,
        NetworkAuthenticationRequired = 511
    }
}