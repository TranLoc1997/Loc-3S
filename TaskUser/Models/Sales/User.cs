using System;

namespace TaskUser.Models.Sales
{
    public class User 
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
        public string Phone { get; set; }
        public String Role { get; set; }
        public bool IsActiver { get; set;}
        
        public virtual Store Store { get; set; }
        
    }
}