using LL.MDE.Components.Common.EnArLoader;

namespace LL.MDE.Components.Qvt.TestUtil
{
    public class EnArLoaderTestSingleton
    {
        private EnArLoader loader;
        private string currentFile = "";

        private static EnArLoaderTestSingleton instance;

        public static EnArLoaderTestSingleton GetInstance()
        {
            return instance ?? (instance = new EnArLoaderTestSingleton());
        }

        public EnArLoader GetLoader(string file)
        {
            if (!currentFile.Equals(file))
            {
                currentFile = file;
                loader?.Close();
                loader = new EnArLoader(file);
            }
            return loader;
        }

        public void Close()
        {
            loader.Close();
        }
    }
}