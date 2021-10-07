namespace Nut.Ioc
{
    public class NutIocConfig
    {
        private string[] ignorePaths;
        public string[] IgnorePaths
        {
            get { return ignorePaths ?? (ignorePaths = new string[0]); }
            set { ignorePaths = value; }
        }
    }
}