namespace TaskUser.ViewsModels.User
{
    public class UserViewsModels
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
        public string Phone { get; set; }
        public bool IsActiver { get; set;}
        public RoleName Role { get; set; }
        public enum RoleName
        {
            Admin = 1,
            User = 2
        }
        
        public virtual Models.Sales.Store Store { get; set; }
    }
}