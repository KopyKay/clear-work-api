namespace ClearWork.Domain.Exceptions;

public class DuplicateResourceException(string resourceName, string keyName, object keyValue) 
    : Exception($"{resourceName} with {keyName}: {keyValue}, already exists.");