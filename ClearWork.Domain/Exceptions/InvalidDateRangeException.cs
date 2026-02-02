namespace ClearWork.Domain.Exceptions;

public class InvalidDateRangeException() : Exception("End date must be after start date.");