using ClearWork.Domain.Entities;

namespace ClearWork.Domain.Exceptions;

public class NotFoundException : Exception
{
    public string ResourceType { get; }
    public object ResourceId { get; }
    public string? ParentType { get; }
    public object? ParentId { get; }

    public NotFoundException(string resourceType, object resourceId)
        : base($"{resourceType} with id [{resourceId}] was not found.")
    {
        ResourceType = resourceType;
        ResourceId = resourceId;
    }

    private NotFoundException(string resourceType, object resourceId, string parentType, object parentId)
        : base($"{resourceType} with id [{resourceId}] was not found in {parentType} with id [{parentId}].")
    {
        ResourceType = resourceType;
        ResourceId = resourceId;
        ParentType = parentType;
        ParentId = parentId;
    }
    
    public static NotFoundException ForUserResource<TResource>(object resourceId, string userId)
        => new(typeof(TResource).Name, resourceId, nameof(User), userId);

    public static NotFoundException ForChildResource<TResource, TParent>(object resourceId, object parentId)
        => new(typeof(TResource).Name, resourceId, typeof(TParent).Name, parentId);
}