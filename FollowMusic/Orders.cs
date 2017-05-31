//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FollowMusic
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Orders
    {
        [Key]
        [Required]
        public int Order_id { get; set; }
        [Required(ErrorMessage = "Заполните это поле!")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> OrderDate { get; set; }
        [Required]
        public int User_id { get; set; }
        [Required]
        public int Status_id { get; set; }
        [Required]
        public Nullable<int> Shop_id { get; set; }
        [Required]
        public Nullable<int> Album_id { get; set; }
    
        public virtual Album Album { get; set; }
        public virtual Status Status { get; set; }
        public virtual Users Users { get; set; }
        public virtual Shop Shop { get; set; }
    }
}
