﻿namespace RealTimeChat.Domain.Shared.Exceptions;
public static class HttpStatusCodes
{
    public const int DataNotFound = 404;
    public const int BusinessException = 405;
    public const int DataNotValid = 422;
    public const int DataDuplicated = 409;
}
