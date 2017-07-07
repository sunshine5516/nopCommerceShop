namespace Nop.Core.Domain.Media
{
    /// <summary>
    /// 图片实体
    /// </summary>
    public partial class Picture : BaseEntity
    {
        /// <summary>
        /// 图片二进制
        /// </summary>
        public byte[] PictureBinary { get; set; }

        /// <summary>
        /// 图片MIME类型
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// 图片的SEO友好的文件名
        /// </summary>
        public string SeoFilename { get; set; }

        /// <summary>
        /// “img”HTML元素的“alt”属性。 如果为空，则将使用默认规则（例如产品名称）
        /// </summary>
        public string AltAttribute { get; set; }

        /// <summary>
        /// “img”HTML元素的“title”属性。 如果为空，则将使用默认规则（例如产品名称）
        /// </summary>
        public string TitleAttribute { get; set; }

        /// <summary>
        /// 图片是否是新的
        /// </summary>
        public bool IsNew { get; set; }
    }
}
