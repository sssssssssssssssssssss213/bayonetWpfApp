//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace bayonetWpfApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class News
    {
        public News()
        {
            this.Schedule = new HashSet<Schedule>();
        }
    
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    
        public virtual ICollection<Schedule> Schedule { get; set; }
    }
}