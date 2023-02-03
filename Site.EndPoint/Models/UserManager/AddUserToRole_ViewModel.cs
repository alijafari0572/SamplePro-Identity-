namespace Site.EndPoint.Models.UserManager
{
    public class AddUserToRole_ViewModel
    {
        #region Constructor

        public AddUserToRole_ViewModel()
        {
            UserRoles = new List<UserRoles_ViewModel>();
        }

        public AddUserToRole_ViewModel(string userId, IList<UserRoles_ViewModel> userRoles)
        {
            UserId = userId;
            UserRoles = userRoles;
        }


        #endregion



        public string UserId { get; set; }

        public IList<UserRoles_ViewModel> UserRoles { get; set; }
    }
}
