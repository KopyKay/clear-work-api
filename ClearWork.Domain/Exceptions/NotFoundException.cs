namespace ClearWork.Domain.Exceptions;

public class NotFoundException(string sourceType, string sourceIdentifier) 
    : Exception($"{sourceType} with identifier [{sourceIdentifier}] was not found.");