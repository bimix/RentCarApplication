//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarDataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Booking
    {
        public int PersonID { get; set; }
        public string Username { get; set; }
        public string model { get; set; }
        public string make { get; set; }
        public string location { get; set; }
        public Nullable<System.DateTime> avaStart { get; set; }
        public Nullable<System.DateTime> avaEnd { get; set; }
        public string price { get; set; }
    }
}
