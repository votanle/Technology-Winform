using System;

namespace SE214L22.Data.Entity.AppCustomer
{
    public class Customer : AppEntity
    {
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public int CustomerLevelId { get; set; }
        public string PhoneNumber { get; set; }
        public float AccumulatedPoint { get; set; }
        public DateTime CreationTime { get; set; }
        public CustomerLevel CustomerLevel { get; set; }
        public Customer()
        {
            IsDeleted = false;
        }
    }
}
