//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MDCafe
{
    using System;
    using System.Collections.Generic;
    
    public partial class item
    {
        public int code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasure { get; set; }
        public Nullable<float> CurrentPrice { get; set; }
        public Nullable<int> CategoryId { get; set; }
    
        public virtual category category { get; set; }
    }
}
