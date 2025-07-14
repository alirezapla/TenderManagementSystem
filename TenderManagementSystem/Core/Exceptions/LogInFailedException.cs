namespace TenderManagementSystem.Core.Exceptions;

public sealed class LogInFailedException(string msg) : Exception(msg);