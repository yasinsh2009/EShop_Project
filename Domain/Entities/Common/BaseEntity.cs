using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Domain.Entities.Common;

public abstract class BaseEntity
{
    [Key]
    public long Id { get; set; }
    public string? Modifiedby { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset LastModifiedAt { get; set; }
    public bool IsPublished { get; set; }
}