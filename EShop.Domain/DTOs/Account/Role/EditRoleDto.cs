namespace EShop.Domain.DTOs.Account.Role
{
    public class EditRoleDto : CreateRoleDto
    {
        public long Id { get; set; }
    }

    public enum EditRoleResult
    {
        Success,
        NotFound,
        Error
    }
}
