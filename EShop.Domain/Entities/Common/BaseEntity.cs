using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.Entities.Common;

public class BaseEntity
{
    [Key]
    public long Id { get; set; }
    public string? editorName { get; set; }
    public bool IsDelete { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime LastUpdateDate { get; set; }
}