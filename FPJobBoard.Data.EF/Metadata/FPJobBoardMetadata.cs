using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FPJobBoard.Data.EF
{
    #region Applications
    [MetadataType(typeof(ApplicationMetadata))]
    public partial class Application { }

    public class ApplicationMetadata
    {
        //public int ApplicationID { get; set; }

        [Required(ErrorMessage ="** User ID is Required **")]
        [StringLength(128,ErrorMessage ="* Maximum 128 Characters *")]
        [Display(Name ="Employee ID")]
        public string UserID { get; set; }

        [Required(ErrorMessage ="** Open Position ID is Required **")]
        [Display(Name ="Open Position ID")]
        public int OpenPositionID { get; set; }

        [DisplayFormat(DataFormatString ="{0:d}")]
        public System.DateTime ApplicationDate { get; set; }

        [Display(Name ="Manager Notes")]
        [StringLength(100, ErrorMessage ="* Maximum 100 Characters *")]
        public string ManagerNotes { get; set; }

        [Required(ErrorMessage ="** Application Status is Required **")]
        [Display(Name ="Application Status ID")]
        public int ApplicationStatusID { get; set; }

        [Required(ErrorMessage ="** Resume is Required **")]
        [Display(Name ="Resume")]
        [StringLength(75,ErrorMessage ="* Maximum 75 Characters *")]
        public string ResumeFilename { get; set; }
    }
    #endregion

    #region Application Status
    [MetadataType(typeof(ApplicationStatuMetadata))]
    public partial class ApplicationStatu { }

    public class ApplicationStatuMetadata
    {
        //public int ApplicationStatusID { get; set; }

        [Required(ErrorMessage ="** Status Name is Required **")]
        [StringLength(50, ErrorMessage ="* Maximum 50 Characters *")]
        [Display(Name ="Status")]
        public string StatusName { get; set; }

        [Required(ErrorMessage ="** Status Description is Required **")]
        [StringLength(250, ErrorMessage ="* Maximum 250 Characters *")]
        [Display(Name ="Status Description")]
        public string StatusDescription { get; set; }
    }
    #endregion

    #region Location
    [MetadataType(typeof(LocationMetadata))]
    public partial class Location { }

    public class LocationMetadata
    {
        //public int LocationID { get; set; }

        [Required(ErrorMessage = "** Store Number is Required **")]
        [StringLength(10, ErrorMessage = "* Maximum 10 Characters *")]
        [Display(Name = "Store Number")]
        public string StoreNumber { get; set; }

        [Required(ErrorMessage = "** City is Required **")]
        [StringLength(50, ErrorMessage = "* Maximum 50 Characters *")]
        public string City { get; set; }

        [Required(ErrorMessage ="** State is Required **")]
        [StringLength(2, ErrorMessage ="* Maximum 2 Characters")]
        public string State { get; set; }

        [Required(ErrorMessage ="** Manager ID is Required **")]
        [StringLength(128, ErrorMessage ="* Maximum 128 Characters *")]
        [Display(Name ="Manager ID")]
        public string ManagerID { get; set; }
    }
    #endregion

    #region Open Position
    [MetadataType(typeof(OpenPositionMetedata))]
    public partial class OpenPosition { }

    public class OpenPositionMetedata
    {
        //public int OpenPositionID { get; set; }

        [Required(ErrorMessage ="** Location ID is Required **")]
        [Display(Name ="Location ID")]
        public int LocationID { get; set; }

        [Required(ErrorMessage ="** Position ID is Required **")]
        [Display(Name ="Position ID")]
        public int PositionID { get; set; }
    }
    #endregion

    #region Position
    [MetadataType(typeof(PositionMetadata))]
    public partial class Position { }

    public class PositionMetadata
    {
        //public int PositionID { get; set; }

        [Required(ErrorMessage ="** Title is Required **")]
        [StringLength(50, ErrorMessage ="* Maximum 50 Characters *")]
        public string Title { get; set; }

        [Display(Name ="Job Description")]
        public string JobDescription { get; set; }
    }
    #endregion

    #region User Details
    public class UserDetailMetadata
    {
        //public string UserID { get; set; }

        [Required(ErrorMessage ="** First Name is Required **")]
        [StringLength(50, ErrorMessage ="* Maximum 50 Characters *")]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="** Last Name is Required **")]
        [StringLength(50, ErrorMessage ="* Maximum 50 Characters *")]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }

        [StringLength(75, ErrorMessage ="* Maximum 75 Characters *")]
        [Display(Name ="Resume")]
        public string ResumeFilename { get; set; }
    }

    [MetadataType(typeof(UserDetailMetadata))]
    public partial class UserDetail
    {
        public string FullName
        {
            [Display(Name ="Full Name")]
            get { return FirstName + " " + LastName; }
        }
    }
    #endregion
}
