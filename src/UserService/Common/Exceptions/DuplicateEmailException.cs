﻿namespace UserService.Common.Exceptions;

public class DuplicateEmailException : Exception
{
    public DuplicateEmailException(string message)
        : base(message) { }
}

