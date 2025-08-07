using EShop.Domain.DTOs.Paging;
using EShop.Domain.Entities.Account.Role;

namespace EShop.Domain.DTOs.Account.Role
{
    public class FilterRoleDto : BasePaging
    {
        public long Id { get; set; }
        public string RoleName { get; set; }
        public List<Entities.Account.Role.Role> Roles { get; set; }

        #region Methods

        public FilterRoleDto SetRoles(List<Entities.Account.Role.Role> roles)
        {
            Roles = roles;
            return this;
        }

        public FilterRoleDto SetPaging(BasePaging paging)
        {
            PageId = paging.PageId;
            AllEntitiesCount = paging.AllEntitiesCount;
            StartPage = paging.StartPage;
            EndPage = paging.EndPage;
            HowManyShowPageAfterAndBefore = paging.HowManyShowPageAfterAndBefore;
            TakeEntity = paging.TakeEntity;
            SkipEntity = paging.SkipEntity;
            PageCount = paging.PageCount;

            return this;
        }



        #endregion
    }
}
