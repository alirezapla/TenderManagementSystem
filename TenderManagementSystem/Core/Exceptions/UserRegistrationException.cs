namespace TenderManagementSystem.Core.Exceptions;

public sealed class UserRegistrationException(string msg) : Exception(msg);