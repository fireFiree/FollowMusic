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
    
    public partial class Label
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Label()
        {
            this.Album = new HashSet<Album>();
        }
        [Key]
        [Required]
        public int Label_id { get; set; }
        [Required(ErrorMessage = "Заполните это поле!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string Label_name { get; set; }
        [StringLength(200, ErrorMessage = "Длина строки не должна превышать 200 символов")]
        public string Description { get; set; }
        [StringLength(200, ErrorMessage = "Длина строки не должна превышать 200 символов")]
        public string Countries { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Album> Album { get; set; }
    }
}