namespace HTLegal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class E_CMS_Home
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string Logo_1920x940 { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(200)]
        public string Title_En { get; set; }

        [StringLength(200)]
        public string SubTitle { get; set; }

        [StringLength(200)]
        public string SubTitle_En { get; set; }

        [StringLength(200)]
        public string Logo_1920x940_Slide { get; set; }

        [StringLength(200)]
        public string Title_Slide { get; set; }

        [StringLength(200)]
        public string Title_Slide_En { get; set; }

        [StringLength(200)]
        public string SubTitle_Slide { get; set; }

        [StringLength(200)]
        public string SubTitle_Slide_En { get; set; }

        [StringLength(200)]
        public string LegalConsultantPanelTitle { get; set; }

        [StringLength(200)]
        public string LegalConsultantPanelTitle_En { get; set; }

        [StringLength(200)]
        public string LegalConsultantPanelWorkTime { get; set; }

        [StringLength(200)]
        public string LegalConsultantPanelWorkTime_En { get; set; }

        [StringLength(200)]
        public string FreeConsultantBottonPanel { get; set; }

        [StringLength(200)]
        public string FreeConsultantBottonPanel_En { get; set; }

        [StringLength(200)]
        public string Name_Slide { get; set; }
    }
}
