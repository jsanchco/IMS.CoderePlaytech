namespace IMS.CoderePlaytech.Services.ServiceBarcode.Helpers
{
    #region Using

    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    #endregion

    public static class Utils
    {
        private static Random random = new Random();

        public static string NewBarcode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 7)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static async Task<string> GetTemplateBarcode(string reference)
        {
            var path = "assets/templates/Barcode.html";
            if (!File.Exists(path))
                throw new Exception($"File not found ('{path}')");

            string result;
            using (var reader = File.OpenText(path))
            {
                result = await reader.ReadToEndAsync();
                result = result.Replace("<reference>", reference);
            }

            return result;
        }
    }
}
