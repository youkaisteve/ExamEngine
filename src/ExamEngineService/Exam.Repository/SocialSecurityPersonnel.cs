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
    
    public partial class SocialSecurityPersonnel : EntityBase<int>
    {
        public int SysNo { get; set; }
        public string Name { get; set; }
        public string IDCardNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Sex { get; set; }
        public Nullable<System.DateTime> InDate { get; set; }
        public string InUser { get; set; }
        public string ExtendInfo { get; set; }
    }
}