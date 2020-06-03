using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProLab.Model.Entidades
{
    public class USER_LAB
    {
        public int ID { get; set; }
        public string USR { get; set; }
        public string PASSWORD { get; set; }
        public string FIRST_NAME { get; set; }
        public string SECOND_NAME { get; set; }
        public string FIRST_LAST_NAME { get; set; }
        public string SECOND_LAST_NAME { get; set; }
        public string EMAIL { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public bool IS_ACTIVE { get; set; } = true;
        public string USER_CREATE { get; set; }
        public string USER_UPDATE { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DATE_CREATE { get; set; } =  DateTime.Now;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DATE_UPDATE { get; set; } = DateTime.Now;
        public string ID_USER_ROL { get; set; }

    }
}
