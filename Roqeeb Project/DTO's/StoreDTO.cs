using System.Collections.Generic;

namespace Roqeeb_Project.DTO_s
{
    public class StoreDTO
    {
        public string Id { get; set; }
        public string StoreName { get; set; }
        public IList<SectionDTO> Sections { get; set; }
        public string StoreDescription { get; set; }
    }
}
