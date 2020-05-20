namespace IMS.CoderePlaytech.Services.ServiceCodereWithoutApi.Helpers
{
    #region Using

    using System;
    using System.Linq;

    #endregion

    public static class Utils
    {
        private static Random random = new Random();

        public static string RandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 7)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
