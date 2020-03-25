namespace FManager.Core.Dtos.Users
{
    using FManager.Core.Dtos.Shared;
    using System;

    public class UserDto : BaseEntityDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid SessionToken { get; set; }
        public string PaymentTransactionCode { get; set; }
        public Guid PaymentStatusId { get; set; }
    }
}
