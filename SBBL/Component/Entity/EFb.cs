using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBBL.Component.Entity
{
    public class EFb
    {
        public string FbID { get; set; }
        public string FbName { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
        public EFanPage FanPage { get; set; } 
    }

    public class EFanPage
    {
        public string FanPageID { get; set; }
        public string FanPageName { get; set; }
        public string FanPageImageUrl { get; set; }
        public string FanPageLink { get; set; }
        //public IList<EAlbum> AlbumList { get; set; }
        //public IList<EImage> ImageList { get; set; }
    }

    public class EAlbum
    {
        public string AlbumID { get; set; }
        public string AlbumCode { get; set; }
        public string AlbumName { get; set; }
        public string AlbumImageUrl { get; set; }
    }

    public class EImage
    {
        public string ImageID { get; set; }
        public string AlbumID { get; set; }
        public string ImageCode { get;set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
    }
}
