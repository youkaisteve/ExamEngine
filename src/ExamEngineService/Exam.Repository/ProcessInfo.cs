//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Exam.Repository
{
    using Component.Data;
    using System;
    using System.Collections.Generic;
    
    public partial class ProcessInfo : EntityBase<int>
    {
        public int SysNo { get; set; }
        public string ProcessName { get; set; }
        public string Category { get; set; }
        public string DifficultyLevel { get; set; }
        public string Description { get; set; }
        public System.DateTime InDate { get; set; }
        public string InUser { get; set; }
        public Nullable<System.DateTime> LastEditDate { get; set; }
        public string LastEditUser { get; set; }
    }
}
