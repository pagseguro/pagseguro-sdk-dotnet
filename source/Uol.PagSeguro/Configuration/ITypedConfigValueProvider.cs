namespace Uol.PagSeguro.Configuration
{
    /// <summary>
    /// </summary>
    public interface ITypedConfigValueProvider
    {
        /// <summary>
        /// </summary>
        /// <param name="elementKey"></param>
        /// <param name="sandbox"></param>
        /// <returns></returns>
        T GetValue<T>(string elementKey = null, bool sandbox = false);
    }
}