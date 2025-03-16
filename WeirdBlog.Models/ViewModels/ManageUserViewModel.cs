namespace WeirdBlog.Models.ViewModels
{
    public class ManageUserViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public List<string> AllRoles { get; set; } = new List<string>();
        public string SelectedRole { get; set; }
        public List<string> SelectedRoles { get; set; } = new List<string>();
    }
}