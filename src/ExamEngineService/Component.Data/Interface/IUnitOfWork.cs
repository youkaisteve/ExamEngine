// 源文件头信息：
// 文 件 名：IUnitOfWork.cs
// 接 口 名：IUnitOfWork
// 所属工程：Component.Data
// 最后修改：游凯
// 最后修改：2013-09-10 03:42:48

namespace Component.Data
{
    /// <summary>
    ///     业务单元操作接口
    /// </summary>
    public interface IUnitOfWork
    {
        bool IsCommited { get; }

        int Submit(bool validateOnSaveEnabled = true);

        void RollBack();
    }
}