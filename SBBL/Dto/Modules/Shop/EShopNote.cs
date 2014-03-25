using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SBBL.Dto.Modules.Master;

namespace SBBL.Dto.Modules.Shop
{
    public class EShopNote
    {
        public string active { get; set; }
        public string createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public string updatedBy { get; set; }
        public DateTime updatedDate { get; set; }

        public IList<ENote> NoteList { get; set; }

        public EShopNote()
        {
            NoteList = new List<ENote>();
            ENote obj = new ENote();
            obj.noteID = 1;
            obj.note = "KCS";
            NoteList.Add(obj);

            obj = new ENote();
            obj.noteID = 2;
            obj.note = "KTB";
            NoteList.Add(obj);

        }

    }
}
