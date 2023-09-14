namespace AuroraMonitor.Utilities
{
    public class AssetBundleUtilities
    {
        public static AssetBundle? LoadBundle(string path)
        {
            AssetBundle? temp;
            MemoryStream? memory;
            Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            memory = new((int)stream?.Length);
            stream.CopyTo(memory);

            temp = AssetBundle.LoadFromMemory(memory.ToArray());

            memory.Dispose();
            stream.Dispose();

            return temp;
        }
    }
}
