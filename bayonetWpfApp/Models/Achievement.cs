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
    
    public partial class Achievement
    {
        public Achievement()
        {
            this.UserAchievement = new HashSet<UserAchievement>();
        }
    
        public int AchievementId { get; set; }
        public string AchievementName { get; set; }
    
        public virtual ICollection<UserAchievement> UserAchievement { get; set; }
    }
}
