namespace OS.Secruity.Data
{
    /// <summary>
    /// Represents one User Account
    /// Jonas Ahlf 30.07.2015 14:00:52
    /// </summary>
    internal class User
    {
        #region Personal Data (Optional)
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        #endregion
        public string Login { get; set; }
        public string Password { get; set; }

        public SecruityLevel SecruityLevel { get; set; }
    }
}
