using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Quanlyvadatphonghomestay.Models
{
    public class Locations
    {
       

        public Locations(int id, string imageUrl)
        {
            Id = id;
            ImageUrl = imageUrl;
        }

        public int Id { get; set; }

        [Column("imageUrl")]
        public string ImageUrl { get; set; }
    

    public string FullImageUrl
        {
            get
            {
                if (string.IsNullOrEmpty(ImageUrl))
                    return string.Empty;

                // Nếu đã là link http/https thì giữ nguyên, nếu không thì nối domain API
                if (ImageUrl.StartsWith("http://") || ImageUrl.StartsWith("https://"))
                    return ImageUrl;

                return $"http://10.0.2.2:5166/images/{ImageUrl}";
            }
        }
        }
    }
