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