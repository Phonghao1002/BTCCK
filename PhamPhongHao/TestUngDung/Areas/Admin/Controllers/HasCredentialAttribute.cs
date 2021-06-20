using System;

namespace TestUngDung.Areas.Admin.Controllers
{
    internal class HasCredentialAttribute : Attribute
    {
        public string RoleID { get; set; }
    }
}