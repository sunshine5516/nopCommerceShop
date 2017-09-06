using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.Media;

namespace Nop.Services.Media
{
    /// <summary>
    /// 图片服务接口
    /// </summary>
    public partial interface IPictureService
    {
        /// <summary>
        /// 根据图片存储设置获取加载的图片二进制
        /// </summary>
        /// <param name="picture">Picture</param>
        /// <returns>Picture binary</returns>
        byte[] LoadPictureBinary(Picture picture);

        /// <summary>
        /// 获取图片SEO友好的名字
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Result</returns>
        string GetPictureSeName(string name);

        /// <summary>
        /// 获取默认图片URL
        /// </summary>
        /// <param name="targetSize">默认大小</param>
        /// <param name="defaultPictureType">图片类型</param>
        /// <param name="storeLocation">Store location URL; null to use determine the current store location automatically</param>
        /// <returns>Picture URL</returns>
        string GetDefaultPictureUrl(int targetSize = 0, 
            PictureType defaultPictureType = PictureType.Entity,
            string storeLocation = null);

        /// <summary>
        /// 获取图片路径
        /// </summary>
        /// <param name="pictureId">Picture identifier</param>
        /// <param name="targetSize">默认大小</param>
        /// <param name="showDefaultPicture">A value indicating whether the default picture is shown</param>
        /// <param name="storeLocation">Store location URL; null to use determine the current store location automatically</param>
        /// <param name="defaultPictureType">图片类型</param>
        /// <returns>Picture URL</returns>
        string GetPictureUrl(int pictureId, 
            int targetSize = 0,
            bool showDefaultPicture = true, 
            string storeLocation = null, 
            PictureType defaultPictureType = PictureType.Entity);

        /// <summary>
        /// 获取图片URL路径
        /// </summary>
        /// <param name="picture">Picture实例</param>
        /// <param name="targetSize">图片尺寸</param>
        /// <param name="showDefaultPicture">是否显示默认的图片</param>
        /// <param name="storeLocation">Store location URL; null to use determine the current store location automatically</param>
        /// <param name="defaultPictureType">默认图片类型</param>
        /// <returns>Picture URL</returns>
        string GetPictureUrl(Picture picture, 
            int targetSize = 0,
            bool showDefaultPicture = true, 
            string storeLocation = null, 
            PictureType defaultPictureType = PictureType.Entity);

        /// <summary>
        /// 获取图片本地路径
        /// </summary>
        /// <param name="picture">Picture instance</param>
        /// <param name="targetSize">The target picture size (longest side)</param>
        /// <param name="showDefaultPicture">A value indicating whether the default picture is shown</param>
        /// <returns></returns>
        string GetThumbLocalPath(Picture picture, int targetSize = 0, bool showDefaultPicture = true);

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="pictureId">Picture identifier</param>
        /// <returns>Picture</returns>
        Picture GetPictureById(int pictureId);

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="picture">Picture</param>
        void DeletePicture(Picture picture);

        /// <summary>
        /// 获取图片集合
        /// </summary>
        /// <param name="pageIndex">Current page</param>
        /// <param name="pageSize">Items on each page</param>
        /// <returns>Paged list of pictures</returns>
        IPagedList<Picture> GetPictures(int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 按ID符获取图片
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <param name="recordsToReturn">要返回的记录数。 0返回所有</param>
        /// <returns>Pictures</returns>
        IList<Picture> GetPicturesByProductId(int productId, int recordsToReturn = 0);

        /// <summary>
        /// 插入图片
        /// </summary>
        /// <param name="pictureBinary">图片二进制</param>
        /// <param name="mimeType">图片MIME类型</param>
        /// <param name="seoFilename">SEO文件名</param>
        /// <param name="altAttribute">“img”HTML元素的“alt”属性</param>
        /// <param name="titleAttribute">“img”HTML元素的“title”属性</param>
        /// <param name="isNew">图片是否是新的值</param>
        /// <param name="validateBinary">指示是否验证提供的图片二进制的值</param>
        /// <returns>Picture</returns>
        Picture InsertPicture(byte[] pictureBinary, string mimeType, string seoFilename, 
            string altAttribute = null, string titleAttribute = null,
            bool isNew = true, bool validateBinary = true);

        /// <summary>
        /// Updates the picture
        /// </summary>
        /// <param name="pictureId">The picture identifier</param>
        /// <param name="pictureBinary">The picture binary</param>
        /// <param name="mimeType">The picture MIME type</param>
        /// <param name="seoFilename">The SEO filename</param>
        /// <param name="altAttribute">"alt" attribute for "img" HTML element</param>
        /// <param name="titleAttribute">"title" attribute for "img" HTML element</param>
        /// <param name="isNew">A value indicating whether the picture is new</param>
        /// <param name="validateBinary">A value indicating whether to validated provided picture binary</param>
        /// <returns>Picture</returns>
        Picture UpdatePicture(int pictureId, byte[] pictureBinary, string mimeType,
            string seoFilename, string altAttribute = null, string titleAttribute = null,
            bool isNew = true, bool validateBinary = true);

        /// <summary>
        /// 更新图片的SEO文件名
        /// </summary>
        /// <param name="pictureId">The picture identifier</param>
        /// <param name="seoFilename">The SEO filename</param>
        /// <returns>Picture</returns>
        Picture SetSeoFilename(int pictureId, string seoFilename);

        /// <summary>
        /// 验证输入图片尺寸
        /// </summary>
        /// <param name="pictureBinary">Picture binary</param>
        /// <param name="mimeType">MIME type</param>
        /// <returns>Picture binary or throws an exception</returns>
        byte[] ValidatePicture(byte[] pictureBinary, string mimeType);

        /// <summary>
        /// 获取或设置一个值，指示图像是否应存储在数据库中。
        /// </summary>
        bool StoreInDb { get; set; }

        /// <summary>
        /// 获取图片散列
        /// </summary>
        /// <param name="picturesIds">Pictures Ids</param>
        /// <returns></returns>
        IDictionary<int, string> GetPicturesHash(int [] picturesIds);
    }
}
