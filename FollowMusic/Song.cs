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
    
    public partial class Song
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Song()
        {
            this.Album = new HashSet<Album>();
            this.Playlist = new HashSet<Playlist>();
        }
        [Key]
        [Required]
        public int Song_id { get; set; }
        [Required(ErrorMessage = "Заполните это поле!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть 3 символа.")]
        public string SongName { get; set; }
        [Required(ErrorMessage = "Заполните это поле!")]
        [Range(1, 1200, ErrorMessage = "Длина песни не может превышать 1200 секунд!")]
        public Nullable<int> Duration { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Album> Album { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Playlist> Playlist { get; set; }
    }
}
