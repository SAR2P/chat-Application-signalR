namespace ChatApp_SingleR.Client.AppStates
{
    public class AvailableUserState
    {
        public string ReciverId { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public void setStates(string fullname,string reciverid)
        {
            FullName = fullname;
            ReciverId = reciverid;
        }
    }
}
