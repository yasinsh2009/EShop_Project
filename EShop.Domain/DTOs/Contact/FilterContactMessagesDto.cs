using EShop.Domain.DTOs.Paging;
using EShop.Domain.Entities.Contact;

namespace EShop.Domain.DTOs.Contact
{
    public class FilterContactMessagesDto : BasePaging
    {
        #region Properties

        public long? UserId { get; set; }
        public string UserIp { get; set; }
        //public string UserName { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string Mobile { get; set; }
        public string MessageSubject { get; set; }
        public string MessageText { get; set; }
        public List<ContactUs> ContactUs { get; set; }


        #endregion

        #region Methods

        public FilterContactMessagesDto SetContactUs(List<ContactUs> contact)
        {
            ContactUs = contact;
            return this;
        }

        public FilterContactMessagesDto SetPaging(BasePaging paging)
        {
            PageId = paging.PageId;
            AllEntitiesCount = paging.AllEntitiesCount;
            StartPage = paging.StartPage;
            EndPage = paging.EndPage;
            HowManyShowPageAfterAndBefore = paging.HowManyShowPageAfterAndBefore;
            TakeEntity = 9;
            SkipEntity = paging.SkipEntity;
            PageCount = paging.PageCount;

            return this;
        }

        #endregion
    }
}
