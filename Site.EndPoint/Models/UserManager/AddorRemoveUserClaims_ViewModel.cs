namespace Site.EndPoint.Models.UserManager
{
    public class AddorRemoveUserClaims_ViewModel
    {
        #region Constructor



        public AddorRemoveUserClaims_ViewModel()
        {
            UserClaims = new List<ClaimsViewModel>();
        }

        public AddorRemoveUserClaims_ViewModel(string userId, IList<ClaimsViewModel> userClaims)
        {
            UserId = userId;
            UserClaims = userClaims;
        }

        #endregion


        public string UserId { get; set; }
        public IList<ClaimsViewModel> UserClaims { get; set; }
    }

    public class ClaimsViewModel
    {

        #region Constructor

        public ClaimsViewModel()
        {
        }

        public ClaimsViewModel(string claimType)
        {
            ClaimType = claimType;
        }

        #endregion

        public string ClaimType { get; set; }
        public bool IsSelected { get; set; }
    }
}
