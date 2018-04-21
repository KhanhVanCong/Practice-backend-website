namespace HTLegal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class E_WebsiteConfiguration
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string NotificationEmail { get; set; }

        [StringLength(200)]
        public string SupportEmail { get; set; }

        [StringLength(200)]
        public string SupportEmailPassword { get; set; }

        public bool? ShowFreeConsultantPanel { get; set; }

        public bool? ShowHotlinePanel { get; set; }

        [StringLength(500)]
        public string AddressCompany { get; set; }

        [StringLength(200)]
        public string Website { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string HotLine { get; set; }

        [StringLength(800)]
        public string AboutUs { get; set; }

        [StringLength(500)]
        public string FaceBook { get; set; }

        [StringLength(500)]
        public string Twitter { get; set; }

        [StringLength(500)]
        public string GooglePlus { get; set; }

        [StringLength(500)]
        public string LinkedIn { get; set; }

        [StringLength(500)]
        public string CopyRight { get; set; }

        [StringLength(200)]
        public string EmailCompany { get; set; }

        [StringLength(500)]
        public string LegalProblemContext { get; set; }

        [StringLength(200)]
        public string EmailAdmin { get; set; }

        public string MetaDescription { get; set; }
    }
}
