//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AcademyApplication.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class QuestionType
    {
        public QuestionType()
        {
            this.Questions = new HashSet<Question>();
        }
    
        public int QuestionTypeID { get; set; }
        public string QuestionTypeName { get; set; }
    
        public virtual ICollection<Question> Questions { get; set; }
    }
}
