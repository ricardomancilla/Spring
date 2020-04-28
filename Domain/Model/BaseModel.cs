using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class BaseModel
    {
        [Column(TypeName = "datetime2")]
        public DateTime CreateDtm { get; set; }

        public string CreateUsr { get; set; }
        
        [Column(TypeName = "datetime2")]
        public DateTime? UpdateDtm { get; set; }
        
        public string UpdateUsr { get; set; }

        
        public BaseModel()
        {
            CreateDtm = DateTime.Now;
        }
    }
}
