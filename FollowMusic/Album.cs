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
    
    public partial class Album
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Album()
        {
            this.Orders = new HashSet<Orders>();
            this.Song = new HashSet<Song>();
        }
        [Key]
        [Required]
        public int Album_id { get; set; }

        [Required(ErrorMessage = "Заполните это поле!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string AlbumName { get; set; }

        [Required(ErrorMessage = "Заполните это поле!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ReleaseDate { get; set; }
        public Nullable<int> Artist_id { get; set; }
        public Nullable<int> Genre_id { get; set; }
        public Nullable<int> Label_id { get; set; }
    
        public virtual Artist Artist { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual Label Label { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Song> Song { get; set; }
    }
}
