namespace Uol.PagSeguro.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUrlCollectionElement
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="urlKey"></param>
        /// <param name="sandbox"></param>
        /// <returns></returns>
        string Get(string urlKey, bool sandbox);
    }
}