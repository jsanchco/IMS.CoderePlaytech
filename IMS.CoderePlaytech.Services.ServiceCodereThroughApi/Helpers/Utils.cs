namespace IMS.CoderePlaytech.Services.ServiceCodereThroughApi.Helpers
{
    #region Using

    using System;
    using System.IO;
    using System.Threading.Tasks;

    #endregion

    public static class Utils
    {
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

        public static string GetCookie()
        {
            var cookie = ".CodereApuestas=";
            return string.Concat(
                cookie,
                "E530DDF8457D3F12615FEC3420F2A36181B08ED1B20C669DC3095FF9AA286A8E96E0BFCB2116E942AD91B6DFDB576826D6519C91C51E7997AF13C3299C14DBE23DFC414E5C71F792CE430EA6D9CA28A238E539FBA076CAB24CF3B5A79D0B3E0799CAF33691A14ABB80E6BB888268FAE603D0EEB7010756AF6F28AE42CD67846B1C977623A37C189F3A3F1CDF5FB7810B071CAD6B4B5F725BD41D51D1F489571FE098D6EC3B0FC9945F81D2F2F1A95B5B28454ADDC2BB98A1488F661D264A21504277B940C79A8E68DC49F3153EFE132CBDD549E439DF2F14FC8D903AA45009753CD2D62854C619996D145306BEC331FBF98A5B82241E180E054BD7DF5581BBC1761181131BECCA0A5835D07666E343031A58AE635F03AFB8AC9777C1C1D3B25E0F791BA235DB2C1E5D45AB70DF5EB030328A657AB5B46FB47270933363E563A1AE8BD31B1873FECEBF571DFE0EFA8DA92809619A6BA6D82527097DF187F124B618B04C39B53BAA8FC5BCCA9CFD3BC1AF2AEB54CC3F682A68D01CB9C9AA2F7DDB4422130AE7F20AFE1C3DA0D84235DC06CFCAF1CC8D7AF70A19EB16CD8FDD09B942402D09");
        }
    }
}
