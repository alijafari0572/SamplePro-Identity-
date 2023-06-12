namespace Site.Application.DTOs.UserManager
{
    public class Claims_ViewModel
    {

        #region Constructor

        public Claims_ViewModel()
        {
        }

        public Claims_ViewModel(string claimType)
        {
            ClaimType = claimType;
        }

        #endregion

        public string ClaimType { get; set; }
        public bool IsSelected { get; set; }
    }
}
