using System;
using System.IO;
using PortalAdmin.Common.Extensions;

namespace PortalAdmin.Common.Configs
{
    public class SystemConfig
    {
        /// <summary>
        /// 应用数据库Keys
        /// </summary>
        public string[] DbKeys { get; set; }

        /// <summary>
        /// 监听Curd操作
        /// </summary>
        public bool WatchCurd { get; set; }

        /// <summary>
        /// 用于雪花算法ID生成 - 数据中心取值范围为0-31
        /// </summary>
        public uint DataCenterId { get; set; }

        /// <summary>
        /// 用于雪花算法ID生成 - 机器码取值范围为0-31
        /// </summary>
        public uint WorkId { get; set; }

        /// <summary>
        /// 启用Api访问控制
        /// </summary>
        public bool EnableApiAccessControl { get; set; }

        /// <summary>
        /// 头像上传配置参数
        /// </summary>
        public FileUpLoadConfig UpLoadConfig { get; set; }
    }

    /// <summary>
    /// 文件上传配置
    /// </summary>
    public class FileUpLoadConfig
    {
        private string _uploadPath;

        /// <summary>
        /// 上传路径
        /// </summary>
        public string UploadPath
        {
            get {
                if (_uploadPath.IsNull())
                {
                    _uploadPath = Path.Combine(AppContext.BaseDirectory, "upload").ToPath();
                }

                if (!Path.IsPathRooted(_uploadPath))
                {
                    
                }

                return _uploadPath;
            }
            set => _uploadPath = value;
        }
    }
}