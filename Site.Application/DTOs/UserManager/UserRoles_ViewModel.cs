namespace Site.Application.DTOs.UserManager 
{ 
    public class UserRoles_ViewModel
    {

        #region Constructor

        public UserRoles_ViewModel()
        {
        }

        public UserRoles_ViewModel(string roleName)
        {
            RoleName = roleName;
        }


        #endregion

        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}
