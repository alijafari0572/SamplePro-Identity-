namespace Site.EndPoint.Models.UserManager
{
    public class AddorRemoveUserClaims_ViewModel
    {
        #region Constructor
        public AddorRemoveUserClaims_ViewModel()
        {
            UserClaims = new List<Claims_ViewModel>();
        }

        public AddorRemoveUserClaims_ViewModel(string userId, IList<Claims_ViewModel> userClaims)
        {
            UserId = userId;
            UserClaims = userClaims;
        }

        #endregion


        public string UserId { get; set; }
        public IList<Claims_ViewModel> UserClaims { get; set; }
    }

   
}
