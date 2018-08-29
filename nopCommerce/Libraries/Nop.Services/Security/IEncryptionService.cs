
namespace Nop.Services.Security 
{
    public interface IEncryptionService 
    {
        /// <summary>
        /// 创建盐键
        /// </summary>
        /// <param name="size">Key size</param>
        /// <returns>Salt key</returns>
        string CreateSaltKey(int size);

        /// <summary>
        /// 创建哈希密码
        /// </summary>
        /// <param name="password">{assword</param>
        /// <param name="saltkey">Salk key</param>
        /// <param name="passwordFormat">Password format (hash algorithm)</param>
        /// <returns>Password hash</returns>
        string CreatePasswordHash(string password, string saltkey, string passwordFormat = "SHA1");

        /// <summary>
        /// 创建哈希数据
        /// </summary>
        /// <param name="data">用于计算哈希的数据</param>
        /// <param name="hashAlgorithm">散列算法</param>
        /// <returns>Data hash</returns>
        string CreateHash(byte [] data, string hashAlgorithm = "SHA1");

        /// <summary>
        /// 加密文字
        /// </summary>
        /// <param name="plainText">要加密的文本</param>
        /// <param name="encryptionPrivateKey">加密私钥</param>
        /// <returns>加密的文本</returns>
        string EncryptText(string plainText, string encryptionPrivateKey = "");

        /// <summary>
        /// 解密文本
        /// </summary>
        /// <param name="cipherText">要解密的文本</param>
        /// <param name="encryptionPrivateKey">加密私钥</param>
        /// <returns>Decrypted text</returns>
        string DecryptText(string cipherText, string encryptionPrivateKey = "");
    }
}
